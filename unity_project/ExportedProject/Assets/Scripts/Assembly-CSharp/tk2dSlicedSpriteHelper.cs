using UnityEngine;

[RequireComponent(typeof(tk2dSlicedSprite))]
public class tk2dSlicedSpriteHelper : MonoBehaviour
{
	public float minWidth = 50f;

	public float minHeight = 50f;

	private float _width;

	private float _height;

	private Vector3 _baseScale;

	private bool isInitialized;

	[SerializeField]
	private tk2dSlicedSprite _slicedSprite;

	public tk2dSlicedSprite slicedSprite
	{
		get
		{
			if (_slicedSprite == null)
			{
				_slicedSprite = GetComponent<tk2dSlicedSprite>();
			}
			return _slicedSprite;
		}
	}

	public float width
	{
		get
		{
			return _width;
		}
		set
		{
			if (!isInitialized)
			{
				Init();
			}
			if (value != _width)
			{
				_width = value;
				Vector2 dimensions = slicedSprite.dimensions;
				Vector3 scale = slicedSprite.scale;
				if (_width < minWidth)
				{
					dimensions.x = minWidth;
					slicedSprite.dimensions = dimensions;
					scale.x = _baseScale.x * (_width / minWidth);
					slicedSprite.scale = scale;
				}
				else
				{
					scale.x = _baseScale.x;
					slicedSprite.scale = scale;
					dimensions.x = _width;
					slicedSprite.dimensions = dimensions;
				}
			}
		}
	}

	public float height
	{
		get
		{
			return _height;
		}
		set
		{
			if (!isInitialized)
			{
				Init();
			}
			if (value != _height)
			{
				_height = value;
				Vector2 dimensions = slicedSprite.dimensions;
				Vector3 scale = slicedSprite.scale;
				if (_height < minHeight)
				{
					dimensions.y = minHeight;
					slicedSprite.dimensions = dimensions;
					scale.y = _baseScale.y * (_height / minHeight);
					slicedSprite.scale = scale;
				}
				else
				{
					scale.y = _baseScale.y;
					slicedSprite.scale = scale;
					dimensions.y = _height;
					slicedSprite.dimensions = dimensions;
				}
			}
		}
	}

	private void Awake()
	{
		Init();
	}

	private void Reset()
	{
		_slicedSprite = GetComponent<tk2dSlicedSprite>();
		if (_slicedSprite != null)
		{
			width = (minWidth = _slicedSprite.dimensions.x);
			height = (minHeight = _slicedSprite.dimensions.y);
		}
	}

	private void Init()
	{
		if (!isInitialized)
		{
			_baseScale = slicedSprite.scale;
			_width = slicedSprite.dimensions.x;
			_height = slicedSprite.dimensions.y;
			isInitialized = true;
		}
	}
}
