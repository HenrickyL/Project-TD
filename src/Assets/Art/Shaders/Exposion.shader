Shader "Custom/URP_TextureMovement" {
    Properties {
        _Color1 ("Inner Color (Center)", Color) = (1, 0.8, 0, 1)
        _Color2 ("Outer Color (Edges)", Color) = (1, 0.4, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _FadeSpeed ("Fade Speed", Range(0, 5)) = 1.0
        _ExpansionSpeed ("Expansion Speed", Range(0, 10)) = 2.0
        _WaveFrequency ("Wave Frequency", Range(0, 20)) = 10.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.1
        _EmissionColor ("Emission Color", Color) = (1, 0.5, 0, 1)
        _EmissionIntensity ("Emission Intensity", Range(0, 5)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass {
            Name "TextureMovement"
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

            float4 _Color1;
            float4 _Color2;
            float _FadeSpeed;
            float _ExpansionSpeed;
            float _WaveFrequency;
            float _WaveAmplitude;
            float4 _EmissionColor;
            float _EmissionIntensity;

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

                // Posição no espaço da tela
                o.position = TransformObjectToHClip(v.positionOS);

                // Posição local (para gradiente radial)
                o.localPos = v.uv - 0.5;

                // Adiciona movimento dinâmico ao UV
                float time = _Time.y * _ExpansionSpeed;
                float2 waveOffset = float2(
                    sin(_WaveFrequency * v.uv.x + time),
                    cos(_WaveFrequency * v.uv.y + time)
                ) * _WaveAmplitude;

                o.uv = v.uv + waveOffset;

                return o;
            }

            float4 frag (Varyings i) : SV_Target {
                // Distância radial (gradiente)
                float radialDist = length(i.localPos) * 2.0;

                // Gradiente de cor
                float4 dynamicColor = lerp(_Color1, _Color2, saturate(radialDist));

                // Aplica fade ao tempo
                float fade = 1.0 - saturate(_FadeSpeed * _Time.y);

                // Combina cor da textura com o gradiente
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                texColor.rgb *= dynamicColor.rgb; // Multiplica pela cor dinâmica
                texColor.a *= fade;

                // Emissão
                float4 emission = _EmissionColor * _EmissionIntensity * fade;

                // Cor final
                return texColor + emission;
            }
            ENDHLSL
        }
    }
}