using UnityEngine;

public class AESpriteTk2d : AEAbstractSprite
{
	public tk2dSprite sprite;

	private Vector3 offsetThing = new Vector3(0f, 0f, 1f);

	protected float offsetAmount = -0.1f;

	[SerializeField]
	[HideInInspector]
	protected float _layerIndexScale = 1f;

	public override void Init(AEAnimation newAnimAsset, AELayer newLayer, int newPixelsPerMeter, bool updateSprite, params object[] args)
	{
		base.Init(newAnimAsset, newLayer, newPixelsPerMeter, updateSprite, args);
		if (updateSprite && args.Length > 0 && !sprite.SetSprite(args[0] as tk2dSpriteCollectionData, base.layer.sourceNoExt))
		{
			base.spriteRenderer.enabled = false;
		}
	}

	public override void GoToFrame(int frameIndex)
	{
		if (frameIndex < base.layer.inFrame || frameIndex >= base.layer.outFrame)
		{
			base.spriteRenderer.enabled = false;
			return;
		}
		AEFrame frame = base.layer.GetFrame(frameIndex);
		if (frame == null)
		{
			return;
		}
		if (frame.isPositionChanged)
		{
			base.cachedTransform.localPosition = frame.positionUnity / _pixelsPerMeter;
		}
		if (frame.isPivotChanged)
		{
			Vector3 vector = frame.pivot / _pixelsPerMeter;
			spriteTransform.localPosition = new Vector3(0f - vector.x, vector.y, (float)base.layer.index * 0.001f * _layerIndexScale);
			if (_childAnchor != null)
			{
				_childAnchor.transform.localPosition = new Vector3(spriteTransform.localPosition.x, spriteTransform.localPosition.y, 0f);
			}
		}
		if (frame.isScaleChanged)
		{
			sprite.scale = frame.scale + offsetThing;
			offsetThing.z += offsetAmount;
			offsetAmount = 0f - offsetAmount;
		}
		if (frame.isRotationChanged)
		{
			base.cachedTransform.localRotation = Quaternion.Euler(0f, 0f, 0f - frame.rotation);
		}
		if (frame.isOpacityChanged)
		{
			Color color = sprite.color;
			color.a = frame.opacity;
			sprite.color = color;
		}
		if (frame.isVisibilityChanged)
		{
			base.spriteRenderer.enabled = base.isRendererEnabled && frame.isVisible;
		}
	}

	public override void GoToFrameForced(int frameIndex)
	{
		if (frameIndex < base.layer.inFrame || frameIndex >= base.layer.outFrame)
		{
			base.spriteRenderer.enabled = false;
			return;
		}
		AEFrame frame = base.layer.GetFrame(frameIndex);
		if (frame != null)
		{
			base.cachedTransform.localPosition = frame.positionUnity / _pixelsPerMeter;
			Vector3 vector = frame.pivot / _pixelsPerMeter;
			spriteTransform.localPosition = new Vector3(0f - vector.x, vector.y, (float)base.layer.index * 0.001f * _layerIndexScale);
			if (_childAnchor != null)
			{
				_childAnchor.localPosition = new Vector3(spriteTransform.localPosition.x, spriteTransform.localPosition.y, 0f);
			}
			if (sprite.scale != frame.scale + offsetThing)
			{
				sprite.scale = frame.scale + offsetThing;
			}
			offsetThing.z += offsetAmount;
			offsetAmount = 0f - offsetAmount;
			base.cachedTransform.localRotation = Quaternion.Euler(0f, 0f, 0f - frame.rotation);
			Color color = sprite.color;
			color.a = frame.opacity;
			sprite.color = color;
			base.spriteRenderer.enabled = base.isRendererEnabled && frame.isVisible;
		}
	}

	public override void SetColor(Color newColor)
	{
		sprite.color = newColor;
	}

	public override void SetSortingLayerName(string newSortingLayerName)
	{
		sprite.GetComponent<Renderer>().sortingLayerName = newSortingLayerName;
	}

	public override void SetSortingOrder(int newSortingOrder)
	{
		sprite.SortingOrder = newSortingOrder;
	}

	public override void SetFlipped(bool isFlipped)
	{
		_layerIndexScale = ((!isFlipped) ? 1 : (-1));
	}
}
