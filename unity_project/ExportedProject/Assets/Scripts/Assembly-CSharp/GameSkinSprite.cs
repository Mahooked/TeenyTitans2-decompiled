using UnityEngine;

public class GameSkinSprite : MonoBehaviour
{
	[Tooltip("If TRUE, the local position of this Transform will be reset to its starting value when the sprite is updated, and the offset from the game skin data will be applied.")]
	public bool updatePosition = true;

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
			startingLocalPosition = base.transform.localPosition;
		}
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
		if (updatePosition)
		{
			base.transform.localPosition = startingLocalPosition;
		}
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
				if (!gameSkinSpriteData.skinKey.Equals(GameSkinManager.instance.CurrentSkinName))
				{
					continue;
				}
				sprite.SetSprite(gameSkinSpriteData.spriteCollectionData, gameSkinSpriteData.spriteName);
				if (updatePosition)
				{
					base.transform.localPosition = startingLocalPosition + gameSkinSpriteData.offsetLocalPosition;
				}
				if (gameSkinSpriteData.spriteName == "gui_communicator_antenna")
				{
					if (GameSkinManager.instance.CurrentSkinName == "BEASTBOY")
					{
						sprite.GetComponent<BoxCollider2D>().offset = new Vector2(-67.74f, 45.7f);
					}
					else
					{
						sprite.GetComponent<BoxCollider2D>().offset = new Vector2(-54.5f, 91.7f);
					}
				}
			}
		}
	}
}
