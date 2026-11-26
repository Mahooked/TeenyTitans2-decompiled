Shader "Shader Forge/handheld_screen" {
	Properties {
		_MaskColor ("Mask Color", Vector) = (0.4980392,0.4627451,0.4627451,1)
		_Maintex ("Maintex", 2D) = "white" {}
		_U_Speed ("U_Speed", Float) = 0
		_V_Speed ("V_Speed", Float) = 1
		_Mask ("Mask", 2D) = "white" {}
		_MaskIntensity ("Mask Intensity", Range(0, 1)) = 1
		_NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.3312748
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}