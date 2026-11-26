Shader "Shader Forge/arenaNBTS_ground" {
	Properties {
		_Grid_01_Color ("Grid_01_Color", Vector) = (0.07843138,0.3921569,0.7843137,1)
		_Grid_01 ("Grid_01", 2D) = "white" {}
		_Grid_02_Color ("Grid_02_Color", Vector) = (0.07843138,0.3921569,0.7843137,1)
		_Grid_02 ("Grid_02", 2D) = "white" {}
		_GridSpeedU ("Grid Speed U", Float) = 0
		_GridSpeedV ("Grid Speed V", Float) = 0
		_Cloud_Texture_01 ("Cloud_Texture_01", 2D) = "white" {}
		_node_4131 ("node_4131", Vector) = (1,0,0,1)
		_CloudSpeedU ("Cloud Speed U", Float) = 0
		_CloudSpeedV ("Cloud Speed V", Float) = 0
		_Cloud_Tint_01 ("Cloud_Tint_01", Vector) = (0,0.958621,1,1)
		_Cloud_Tint_02 ("Cloud_Tint_02", Vector) = (0.07586193,0,1,1)
		_Cloud_Texture_02 ("Cloud_Texture_02", 2D) = "white" {}
		_Cloud_02SpeedU ("Cloud_02 Speed U", Float) = 0
		_Cloud_02SpeedV ("Cloud_02 Speed V", Float) = 0
		_tintpower ("tint power", Float) = 0
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