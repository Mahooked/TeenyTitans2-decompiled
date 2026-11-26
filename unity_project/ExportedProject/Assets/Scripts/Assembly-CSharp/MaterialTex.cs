using System;
using UnityEngine;

[Serializable]
public class MaterialTex
{
	public Material mat;

	public Texture originalTexture;

	public MaterialTex(Material mat, Texture originalTexture)
	{
		this.mat = mat;
		this.originalTexture = originalTexture;
	}
}
