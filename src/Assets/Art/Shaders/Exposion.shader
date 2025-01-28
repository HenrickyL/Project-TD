Shader "Custom/URP_ExplosionEffectRadial" {
    Properties {
        _Color1 ("Inner Color (Center)", Color) = (1, 0.8, 0, 1)
        _Color2 ("Outer Color (Edges)", Color) = (1, 0.4, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {} // Textura de ru�do
        _FadeSpeed ("Fade Speed", Range(0, 5)) = 1.0
        _ExpansionSpeed ("Expansion Speed", Range(0, 10)) = 2.0
        _WaveFrequency ("Wave Frequency", Range(0, 20)) = 10.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.1
        _EmissionColor ("Emission Color", Color) = (1, 0.5, 0, 1)
        _EmissionIntensity ("Emission Intensity", Range(0, 5)) = 1.0
        _NoiseScale ("Noise Scale", Range(0, 10)) = 1.0 // Escala do ru�do
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass {
            Name "ExplosionEffectRadial"
            Tags { "LightMode"="UniversalForward" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_NoiseTex); // Textura de ru�do
            SAMPLER(sampler_NoiseTex);

            float4 _Color1;
            float4 _Color2;
            float _FadeSpeed;
            float _ExpansionSpeed;
            float _WaveFrequency;
            float _WaveAmplitude;
            float4 _EmissionColor;
            float _EmissionIntensity;
            float _NoiseScale; // Escala do ru�do

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 localPos : TEXCOORD1;
            };

            Varyings vert (Attributes v) {
                Varyings o;

                // Posi��o no espa�o da tela
                o.position = TransformObjectToHClip(v.positionOS);

                // Posi��o local (para gradiente radial)
                o.localPos = v.uv - 0.5;

                // Adiciona movimento din�mico ao UV
                float time = _Time.y * _ExpansionSpeed;
                float2 waveOffset = float2(
                    sin(_WaveFrequency * v.uv.x + time),
                    cos(_WaveFrequency * v.uv.y + time)
                ) * _WaveAmplitude;

                o.uv = v.uv + waveOffset;

                return o;
            }

            float4 frag (Varyings i) : SV_Target {
                // Dist�ncia radial (gradiente)
                float radialDist = length(i.localPos) * 2.0;

                // Gradiente de cor
                float4 dynamicColor = lerp(_Color1, _Color2, saturate(radialDist));

                // Aplica fade ao tempo
                float fade = 1.0 - saturate(_FadeSpeed * _Time.y);

                // Combina cor da textura com o gradiente
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                texColor.rgb *= dynamicColor.rgb; // Multiplica pela cor din�mica
                texColor.a *= fade;

                // Amostra a textura de ru�do
                float2 noiseUV = i.uv * _NoiseScale; // Aplica escala ao UV do ru�do
                float noiseValue = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, noiseUV).r;

                // Emiss�o com base no ru�do
                float4 emission = _EmissionColor * _EmissionIntensity * fade * noiseValue;

                // Cor final
                return texColor + emission;
            }
            ENDHLSL
        }
    }
}