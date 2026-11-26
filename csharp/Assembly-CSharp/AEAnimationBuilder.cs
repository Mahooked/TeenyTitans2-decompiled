using System.Collections.Generic;
using UnityEngine;

public class AEAnimationBuilder : MonoBehaviour
{
	private List<AEAnimationBuilderSprite> sprites = new List<AEAnimationBuilderSprite>();

	private Transform spritesHolder;

	private AEAnimation _animAsset;

	private int _currentFrame;

	private AEAnimationBuilderSprite GetSpriteByLayerIndex(int layerIndex)
	{
		int count = sprites.Count;
		for (int i = 0; i < count; i++)
		{
			if (sprites[i].layerIndex == layerIndex)
			{
				return sprites[i];
			}
		}
		return null;
	}

	private void GoToAndStop(int frame)
	{
		if (frame < 0)
		{
			frame = 0;
		}
		else if (frame >= _animAsset.totalFrames)
		{
			frame = _animAsset.lastFrame;
		}
		_currentFrame = frame;
		GoToCurrentFrameForced();
	}

	public void GoToCurrentFrameForced()
	{
		Vector3 localScale = transform.localScale;
		transform.localScale = Vector3.one;
		int count = sprites.Count;
		for (int i = 0; i < count; i++)
		{
			sprites[i].GoToFrameForced(_currentFrame);
		}
		transform.localScale = localScale;
	}

	public void Init(AEAnimation animAsset)
	{
		_animAsset = animAsset;
		sprites.Clear();
		spritesHolder = new GameObject("SpritesHolder").transform;
		spritesHolder.parent = transform;
		spritesHolder.localScale = Vector3.one;
		spritesHolder.localPosition = Vector3.zero;
		spritesHolder.localRotation = Quaternion.identity;
		AEAnimationBuilderSprite aEAnimationBuilderSprite = null;
		foreach (AELayer layer in animAsset.composition.layers)
		{
			if (layer != null && layer.index != -1)
			{
				switch (layer.type)
				{
				case AELayerType.FOOTAGE:
					aEAnimationBuilderSprite = AEResources.CreateAnimationBuilderSprite();
					break;
				default:
					Debug.LogError("Unsupported layer type: " + layer.type);
					break;
				case AELayerType.COMPOSITION:
					break;
				}
				aEAnimationBuilderSprite.transform.parent = spritesHolder;
				aEAnimationBuilderSprite.Init(layer);
				sprites.Add(aEAnimationBuilderSprite);
			}
		}
		AEAnimationBuilderSprite aEAnimationBuilderSprite2 = null;
		AEAnimationBuilderSprite aEAnimationBuilderSprite3 = null;
		foreach (AELayer layer2 in animAsset.composition.layers)
		{
			if (layer2 != null && layer2.index != -1 && layer2.parent != 0)
			{
				aEAnimationBuilderSprite2 = GetSpriteByLayerIndex(layer2.parent);
				aEAnimationBuilderSprite3 = GetSpriteByLayerIndex(layer2.index);
				aEAnimationBuilderSprite2.AddChild(aEAnimationBuilderSprite3);
			}
		}
		Dictionary<AEAnimationBuilderSprite, List<AEFrame>> dictionary = new Dictionary<AEAnimationBuilderSprite, List<AEFrame>>();
		foreach (AEAnimationBuilderSprite sprite in sprites)
		{
			Transform parent = sprite.transform.parent;
			dictionary.Add(sprite, new List<AEFrame>());
			AEFrame aEFrame = null;
			AEFrame lastFrame = null;
			int totalFrames = sprite.layer.totalFrames;
			for (int i = 0; i < totalFrames; i++)
			{
				AEFrame frame = sprite.layer.GetFrame(i);
				GoToAndStop(i);
				sprite.transform.parent = spritesHolder;
				aEFrame = new AEFrame();
				aEFrame.positionUnity = sprite.transform.localPosition;
				aEFrame.rotation = 0f - sprite.transform.localRotation.eulerAngles.z;
				aEFrame.opacity = frame.opacity;
				aEFrame.pivot = frame.pivot;
				aEFrame.scale = frame.scale;
				aEFrame.isVisible = frame.isVisible;
				aEFrame.index = frame.index;
				aEFrame.CompareToFrame(lastFrame);
				lastFrame = aEFrame;
				dictionary[sprite].Add(aEFrame);
				sprite.transform.parent = parent;
			}
		}
		foreach (AEAnimationBuilderSprite sprite2 in sprites)
		{
			if (sprite2.transform.parent != spritesHolder)
			{
				sprite2.transform.parent = spritesHolder;
			}
			_animAsset.composition.GetLayer(sprite2.layerIndex).SetFrames(dictionary[sprite2]);
			_animAsset.composition.GetLayer(sprite2.layerIndex).parent = 0;
		}
		foreach (AEAnimationBuilderSprite sprite3 in sprites)
		{
			if (sprite3.childAnchor != null)
			{
				Object.DestroyImmediate(sprite3.childAnchor.gameObject);
			}
			sprite3.transform.localScale = Vector3.one;
		}
	}
}
