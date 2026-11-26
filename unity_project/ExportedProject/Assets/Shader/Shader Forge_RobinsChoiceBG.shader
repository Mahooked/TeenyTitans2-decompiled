Shader "Shader Forge/RobinsChoiceBG" {
	Properties {
		_LeftGradientColorTop ("LeftGradientColorTop", Vector) = (0.4784314,0.1372549,1,1)
		_LeftGradientColorBottom ("LeftGradientColorBottom", Vector) = (0.6627451,0.5411765,0.9843137,1)
		_RightGradientColorTop ("RightGradientColorTop", Vector) = (0.4313726,0.8392157,1,1)
		_RightGradientColorBottom ("RightGradientColorBottom", Vector) = (0.7921569,0.9686275,1,1)
		_LightningBoltColor ("LightningBoltColor", Vector) = (1,1,1,1)
		_DissolveAmount ("DissolveAmount", Range(-1, 1)) = 1
		_WhiteHotAmount ("WhiteHotAmount", Range(0, 1)) = 0
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