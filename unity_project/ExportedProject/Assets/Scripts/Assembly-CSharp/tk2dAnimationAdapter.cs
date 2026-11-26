using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class tk2dAnimationAdapter : MonoBehaviour
{
	public Color color = Color.white;

	public Color colorChildren = Color.white;

	public Vector3 scale = Vector3.one;

	public Vector2 dimensions = Vector2.zero;

	public Color tintColor = Color.clear;

	public Rect clipRect = new Rect(0f, 0f, 1f, 1f);

	private tk2dBaseSprite _sprite;

	private tk2dSlicedSprite slicedSprite;

	private tk2dClippedSprite _clippedSprite;

	private tk2dTextMesh text;

	private tk2dTintAdapter tintAdapter;

	private tk2dBaseSprite[] childSprites;

	public tk2dBaseSprite sprite
	{
		get
		{
			return _sprite;
		}
	}

	private void Awake()
	{
		if (_sprite == null)
		{
			_sprite = GetComponent<tk2dBaseSprite>();
		}
		if (slicedSprite == null)
		{
			slicedSprite = GetComponent<tk2dSlicedSprite>();
		}
		if (text == null)
		{
			text = GetComponent<tk2dTextMesh>();
		}
		if (tintAdapter == null)
		{
			tintAdapter = GetComponent<tk2dTintAdapter>();
		}
		if (_clippedSprite == null)
		{
			_clippedSprite = GetComponent<tk2dClippedSprite>();
		}
	}

	private void Start()
	{
		if (_sprite != null)
		{
			_sprite.color = color;
			_sprite.scale = scale;
		}
		if (slicedSprite != null)
		{
			slicedSprite.color = color;
			slicedSprite.scale = scale;
			dimensions = slicedSprite.dimensions;
		}
		if (_clippedSprite != null)
		{
			_clippedSprite.ClipRect = clipRect;
		}
		if (text != null)
		{
			text.color = color;
		}
		if (tintAdapter != null)
		{
			tintAdapter.tintColor = tintColor;
		}
		List<tk2dBaseSprite> list = new List<tk2dBaseSprite>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			list.AddRange(base.transform.GetChild(i).GetComponentsInChildren<tk2dBaseSprite>(true));
		}
		childSprites = list.ToArray();
	}

	private void LateUpdate()
	{
		if (_sprite != null && _sprite.CurrentSprite == null)
		{
			return;
		}
		if (_sprite != null && _sprite.color != color)
		{
			_sprite.color = color;
		}
		if (_sprite != null && _sprite.scale != scale)
		{
			_sprite.scale = scale;
		}
		if (slicedSprite != null)
		{
			if (color != slicedSprite.color)
			{
				slicedSprite.color = color;
			}
			if (scale != slicedSprite.scale)
			{
				slicedSprite.scale = scale;
			}
			if (dimensions != slicedSprite.dimensions)
			{
				slicedSprite.dimensions = dimensions;
			}
		}
		if (_clippedSprite != null && _clippedSprite.ClipRect != clipRect)
		{
			_clippedSprite.ClipRect = clipRect;
		}
		if (text != null)
		{
			if (color != text.color)
			{
				text.color = color;
			}
			if (scale != text.scale)
			{
				text.scale = scale;
			}
		}
		int num = childSprites.Length;
		for (int i = 0; i < num; i++)
		{
			childSprites[i].color = colorChildren;
		}
		if (Application.isPlaying && tintAdapter != null && tintAdapter.tintColor != tintColor)
		{
			tintAdapter.tintColor = tintColor;
		}
	}
}
