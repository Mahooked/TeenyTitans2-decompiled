using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSkinData
{
	[SerializeField]
	private int _uid = -1;

	public int spriteID;

	public string spriteName = string.Empty;

	public tk2dSpriteCollectionData spriteCollectionData;

	public List<GameSkinSpriteData> gameSkinSpriteData = new List<GameSkinSpriteData>();

	public int uid
	{
		get
		{
			return _uid;
		}
	}
}
