using UnityEngine;

public class ResolutionAdjust : MonoBehaviour
{
	[SerializeField]
	private Renderer renderer;

	private Texture loadedTexture;

	private void Awake()
	{
		if (renderer == null)
		{
			renderer = GetComponent<Renderer>();
		}
		if (renderer != null)
		{
			CheckTexture(tk2dSystem.CurrentPlatform);
		}
		if (loadedTexture == null)
		{
			Object.Destroy(this);
		}
	}

	private void Reset()
	{
		renderer = GetComponent<Renderer>();
	}

	private void CheckTexture(string scale)
	{
		Material sharedMaterial = renderer.sharedMaterial;
		if (sharedMaterial != null && sharedMaterial.mainTexture != null && !sharedMaterial.mainTexture.name.Contains(scale))
		{
			string text = sharedMaterial.mainTexture.name.Replace(sharedMaterial.mainTexture.name.Substring(sharedMaterial.mainTexture.name.Length - 2), scale);
			Texture texture = null;
			text = text.Replace("_etc", string.Empty);
			if (SystemInfo.SupportsTextureFormat(TextureFormat.ETC2_RGBA8) && !scale.Equals("1x"))
			{
				string text2 = text;
				text = text.Replace("-" + scale, "_etc-" + scale);
				texture = Resources.Load<Texture>("Textures/" + text);
				text = text2;
			}
			if (texture == null)
			{
				texture = Resources.Load<Texture>("Textures/" + text);
			}
			if (texture != null)
			{
				Texture mainTexture = sharedMaterial.mainTexture;
				sharedMaterial.mainTexture = texture;
				loadedTexture = texture;
				Resources.UnloadAsset(mainTexture);
			}
		}
	}
}
