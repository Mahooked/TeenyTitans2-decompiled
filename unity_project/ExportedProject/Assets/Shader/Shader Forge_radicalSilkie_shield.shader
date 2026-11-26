Shader "Shader Forge/radicalSilkie_shield" {
	Properties {
		_tileanimmap01type1 ("tile anim map 01 type1", 2D) = "white" {}
		_tileanimmap02type1 ("tile anim map 02 type1", 2D) = "white" {}
		_tileanimtype1color ("tile anim type1 color", Vector) = (0.3970588,1,0.7006085,1)
		_tileanimmap01type2 ("tile anim map 01 type2", 2D) = "white" {}
		_tileanimmap02type2 ("tile anim map 02 type2", 2D) = "white" {}
		_tileanimmap01type2amount ("tile anim map 01 type2 amount", Range(0, 4)) = 3
		_tileanimmap02type2amount ("tile anim map 02 type2 amount", Range(0, 4)) = 3
		_vertexRedgecolor ("vertex(R) edge color", Vector) = (0.2452422,0.7941176,0.5215726,1)
		_vertexRedgecoloramount ("vertex(R) edge color amount", Range(0, 1)) = 0
		_scanlinemap ("scanline map", 2D) = "white" {}
		_scanlinepoweramount ("scanline power amount", Float) = 3
		_scanlinetilemap ("scanline tile map", 2D) = "white" {}
		_scanlinemaptileblend ("scanline map tile blend", Range(0, 1)) = 1
		_scanlinecombindblendamount ("scanline combind blend amount", Float) = 0.3
		_cutoutmap ("cutout map", 2D) = "white" {}
		_cutoutmapdissolveamount ("cutout map dissolve amount", Range(0, 12)) = 12
		_cutoutmapblendamount ("cutout map blend amount", Float) = 0
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