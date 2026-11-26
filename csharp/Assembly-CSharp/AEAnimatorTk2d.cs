using UnityEngine;

[SelectionBase]
public class AEAnimatorTk2d : AEAnimator
{
	[SerializeField]
	private tk2dSpriteCollectionData _spriteCollection;

	public tk2dSpriteCollectionData spriteCollection
	{
		get
		{
			return _spriteCollection;
		}
		set
		{
			if (!(value == _spriteCollection))
			{
				_spriteCollection = value;
				Rebuild();
			}
		}
	}

	public override void Rebuild()
	{
		if (Application.isPlaying && !isInitialized)
		{
			doRebuild = true;
		}
		else if (animationAsset != null && _spriteCollection != null)
		{
			Vector3 localScale = transform.localScale;
			transform.localScale = Vector3.one;
			if (useCachedSprites)
			{
				InitCachedSprites();
			}
			else
			{
				InitSprites();
			}
			RefreshPivot();
			transform.localScale = localScale;
		}
		else if (spritesHolder != null)
		{
			Object.DestroyImmediate(spritesHolder.gameObject);
			spritesHolder = null;
			sprites.Clear();
		}
	}

	protected override AEAbstractSprite CreateSprite()
	{
		return (Object.Instantiate(Resources.Load("AESpriteTk2d")) as GameObject).GetComponent<AESpriteTk2d>();
	}

	protected override void InitSprite(AEAbstractSprite sprite, AELayer layer, bool updateSprite = true)
	{
		sprite.Init(animationAsset, layer, pixelsPerMeter, updateSprite, _spriteCollection);
	}
}
