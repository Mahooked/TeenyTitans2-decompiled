using UnityEngine;

[ExecuteInEditMode]
public class IPhoneXOverlayEffect : MonoBehaviour
{
	public Color color = new Color(1f, 1f, 1f, 0.25f);

	private int colorID = -1;

	private bool doRender;

	[SerializeField]
	[DisableField]
	private Shader _overlayShader;

	[SerializeField]
	[DisableField]
	private Material _overlayMat;

	private Shader overlayShader
	{
		get
		{
			if (_overlayShader == null)
			{
				_overlayShader = Shader.Find("Shader Forge/IPhoneXOverlay");
			}
			return _overlayShader;
		}
	}

	private Material overlayMat
	{
		get
		{
			if (_overlayMat == null)
			{
				_overlayMat = new Material(overlayShader);
			}
			return _overlayMat;
		}
	}

	private void Update()
	{
		if ((Screen.width == 281 && Screen.height == 609) || (Screen.width == 1125 && Screen.height == 2436) || (Screen.width == 828 && Screen.height == 1792))
		{
			doRender = true;
		}
		else
		{
			doRender = false;
		}
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (!doRender)
		{
			Graphics.Blit(src, dest);
			return;
		}
		if (colorID == -1)
		{
			colorID = Shader.PropertyToID("_Color");
		}
		overlayMat.SetColor(colorID, color);
		Graphics.Blit(src, dest, overlayMat);
	}
}
