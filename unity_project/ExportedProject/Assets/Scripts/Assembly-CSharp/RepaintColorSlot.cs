using System;
using UnityEngine;

[Serializable]
public class RepaintColorSlot
{
	public int position;

	public int id = -1;

	public int cost;

	public bool rare;

	public string texturePath;

	[SerializeField]
	private string _colorSprite = string.Empty;

	[SerializeField]
	private int _colorSpriteId = -1;

	public string texturePathScaled
	{
		get
		{
			string text = "1x";
			text = tk2dSystem.CurrentPlatform;
			return texturePath.Replace(texturePath.Substring(texturePath.Length - 2), text);
		}
	}

	public string colorSprite
	{
		get
		{
			return _colorSprite;
		}
	}

	public int colorSpriteId
	{
		get
		{
			return _colorSpriteId;
		}
	}

	public RepaintColorSlot()
	{
	}

	public RepaintColorSlot(int position, int id = -1)
	{
		this.position = position;
		this.id = id;
	}
}
