// Lightsaber shader
// -----------------------------------
//
// Create lightsaber like objects with inner solid color and outer rim color.
// Use with Unity postprocessing Bloom effect.
// 
Shader "Custom/Lightsaber"
{
	Properties
	{
		_GlowColor("Glow Color", Color) = (1,0,0)
		_InnerGlow("Inner Glow", Range(0 , 1)) = 0.5
		_OuterGlow("Outer Glow", Range(0 , 10)) = 2
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" }
		Cull Off // turn off culling, otherwise we'd see only 1 side when we swing the saber; default is Back

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 

		struct Input
		{
			float4 color : COLOR;
		};

		uniform float4 _GlowColor;
		uniform float _OuterGlow;
		uniform float _InnerGlow;

		void surf(Input input , inout SurfaceOutputStandard output)
		{
			output.Emission = lerp((_GlowColor * _OuterGlow) , _OuterGlow, _InnerGlow).rgb;
		}

		ENDCG
	}
	
	Fallback "Diffuse"
}