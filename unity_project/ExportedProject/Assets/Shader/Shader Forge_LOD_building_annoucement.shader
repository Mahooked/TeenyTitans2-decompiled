Shader "Shader Forge/LOD_building_annoucement" {
	Properties {
		_Color ("Color", Vector) = (1,0,0,1)
		_LineMap ("LineMap", 2D) = "white" {}
		_RingSpeed ("Ring Speed", Float) = 0
		_OpacityAll ("OpacityAll", Range(0, 1)) = 1
		_NoiseMap ("NoiseMap", 2D) = "white" {}
		_vertex ("vertex", Range(0, 1)) = 1
		_VertexSpeed ("Vertex Speed", Float) = 0
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
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

			float4 _Color;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return _Color; // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}