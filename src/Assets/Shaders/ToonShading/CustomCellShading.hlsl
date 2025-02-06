
void AdditionalCellShading_float(float3 WorldPos, float3 Normal, out float3 Shading)
{
    Shading = float3(0, 0, 0);

    // Obt�m o n�mero de luzes adicionais
    #ifdef _ADDITIONAL_LIGHTS
            uint lightCount = GetAdditionalLightsCount();

            for (uint i = 0; i < lightCount; i++)
            {
                // Obt�m os dados da luz adicional
                Light additionalLight = GetAdditionalLight(i, WorldPos);

                // C�lculo da ilumina��o estilo Cel Shading
                float lightIntensity = max(0, dot(Normal, additionalLight.direction));
                float toonStep = step(0.5, lightIntensity); // Bordas n�tidas (Cel Shading)

                // Multiplica pela cor da luz e atenua��o
                Shading += toonStep * additionalLight.color.rgb * additionalLight.distanceAttenuation;
            }
    #endif
}
