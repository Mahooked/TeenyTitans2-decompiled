using UnityEngine;

public class GameSkinTexture : MonoBehaviour
{
	private string startingSpriteName = string.Empty;

	private Vector3 startingLocalPosition = Vector3.zero;

	private tk2dBaseSprite _sprite;

	private GameSkinDatabase _skinDatabase;

	private tk2dBaseSprite sprite
	{
		get
		{
			if (_sprite == null)
			{
				_sprite = GetComponent<tk2dBaseSprite>();
			}
			return _sprite;
		}
	}

	private GameSkinDatabase skinDatabase
	{
		get
		{
			if (_skinDatabase == null)
			{
				_skinDatabase = GameManager.instance.gameSkinDatabase;
			}
			return _skinDatabase;
		}
	}

	private void Awake()
	{
		if (sprite != null)
		{
			startingSpriteName = sprite.CurrentSprite.name;
			startingLocalPosition = transform.localPosition;
		}
		UpdateSprite();
	}

	private void OnEnable()
	{
		GameSkinManager.instance.OnSkinChange += HandleSkinChange;
		HandleSkinChange();
	}

	private void OnDisable()
	{
		if (GameSkinManager.instance != null)
		{
			GameSkinManager.instance.OnSkinChange -= HandleSkinChange;
		}
	}

	private void HandleSkinChange()
	{
		UpdateSprite();
	}

	private void UpdateSprite()
	{
		if (!(sprite != null))
		{
			return;
		}
		transform.localPosition = startingLocalPosition;
		for (int i = 0; i < skinDatabase.skinData.Count; i++)
		{
			GameSkinData gameSkinData = skinDatabase.skinData[i];
			if (!gameSkinData.spriteName.Equals(startingSpriteName))
			{
				continue;
			}
			for (int j = 0; j < gameSkinData.gameSkinSpriteData.Count; j++)
			{
				GameSkinSpriteData gameSkinSpriteData = gameSkinData.gameSkinSpriteData[j];
				if (gameSkinSpriteData.skinKey.Equals(GameSkinManager.instance.CurrentSkinName))
				{
					sprite.SetSprite(gameSkinSpriteData.spriteCollectionData, gameSkinSpriteData.spriteName);
					transform.localPosition = startingLocalPosition + gameSkinSpriteData.offsetLocalPosition;
				}
			}
		}
	}
}
