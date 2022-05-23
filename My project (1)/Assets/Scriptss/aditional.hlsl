void AdditionalLights_float(float3 WorldPos, float Index, out float3 Direction, 
	out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
    Direction = normalize(float3(0.5f, 0.5f, 0.25f));
    Color = float3(0.0f, 0.0f, 0.0f);
    DistanceAtten = 0.0f;
    ShadowAtten = 0.0f;

#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    if(Index < pixelLightCount)
    {
        Light light = GetAdditionalLight(Index, WorldPos);
    
        Direction = light.direction;
        Color = light.color;
        DistanceAtten = light.distanceAttenuation;
        ShadowAtten = light.shadowAttenuation;
    }
#endif
}

void AdditionalLight_half(half3 WorldPos, float Index, out half3 Direction,
	out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
	Direction = normalize(half3(0.5f, 0.5f, 0.25f));
	Color = half3(0.0f, 0.0f, 0.0f);
	DistanceAtten = 0.0f;
	ShadowAtten = 0.0f;

#ifndef SHADERGRAPH_PREVIEW
	int pixelLightCount = GetAdditionalLightsCount();
	if (Index < pixelLightCount)
	{
		Light light = GetAdditionalLight(Index, WorldPos);

		Direction = light.direction;
		Color = light.color;
		DistanceAtten = light.distanceAttenuation;
		ShadowAtten = light.shadowAttenuation;
	}
#endif
}