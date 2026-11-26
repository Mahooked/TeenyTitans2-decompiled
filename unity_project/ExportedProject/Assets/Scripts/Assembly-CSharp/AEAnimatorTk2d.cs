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
		else if (base.animationAsset != null && _spriteCollection != null)
		{
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = Vector3.one;
			if (base.useCachedSprites)
			{
				InitCachedSprites();
			}
			else
			{
				InitSprites();
			}
			RefreshPivot();
			base.transform.localScale = localScale;
		}
		else if (base.spritesHolder != null)
		{
			Object.DestroyImmediate(base.spritesHolder.gameObject);
			base.spritesHolder = null;
			base.sprites.Clear();
		}
	}

	protected override AEAbstractSprite CreateSprite()
	{
		return (Object.Instantiate(Resources.Load("AESpriteTk2d")) as GameObject).GetComponent<AESpriteTk2d>();
	}

	protected override void InitSprite(AEAbstractSprite sprite, AELayer layer, bool updateSprite = true)
	{
		sprite.Init(base.animationAsset, layer, pixelsPerMeter, updateSprite, _spriteCollection);
	}
}
