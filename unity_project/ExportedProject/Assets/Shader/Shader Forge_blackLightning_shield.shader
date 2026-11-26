Shader "Shader Forge/blackLightning_shield" {
	Properties {
		_node_3348 ("node_3348", 2D) = "white" {}
		_outlinewidth ("outline width", Range(0, 1)) = 1
		_node_577 ("node_577", Float) = 3.14
		_node_6470 ("node_6470", Float) = 5
		_sinWave ("sinWave", Float) = 0
		_sinWave_copy ("sinWave_copy", Float) = 0.3
		_node_2643 ("node_2643", Range(0, 1)) = 0.4957343
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
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