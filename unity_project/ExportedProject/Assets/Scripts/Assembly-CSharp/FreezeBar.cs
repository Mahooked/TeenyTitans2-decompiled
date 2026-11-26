using UnityEngine;

public class FreezeBar : MonoBehaviour, IPoolable
{
	public tk2dSprite leftCap;

	public tk2dTiledSprite middlePiece;

	public tk2dSprite rightCap;

	public tk2dSprite[] sheens;

	private float middlePieceWidth;

	private float _width;

	private float _alpha = 1f;

	public float width
	{
		get
		{
			return _width;
		}
		set
		{
			SetWidth(value);
		}
	}

	public float alpha
	{
		get
		{
			return _alpha;
		}
		set
		{
			SetAlpha(value);
		}
	}

	private void Awake()
	{
		middlePieceWidth = middlePiece.CurrentSprite.GetUntrimmedBounds().size.x;
		SetWidth(middlePieceWidth);
		SetAlpha(1f);
	}

	public void SetWidth(float width)
	{
		if (width != _width)
		{
			_width = (float)Mathf.RoundToInt(width / middlePieceWidth) * middlePieceWidth;
			Vector2 dimensions = middlePiece.dimensions;
			dimensions.x = _width;
			middlePiece.dimensions = dimensions;
			Vector3 localPosition = rightCap.transform.localPosition;
			localPosition.x = _width;
			rightCap.transform.localPosition = localPosition;
			int num = sheens.Length;
			for (int i = 0; i < num; i++)
			{
				sheens[i].gameObject.SetActive(sheens[i].transform.localPosition.x < _width);
			}
		}
	}

	public void SetAlpha(float alpha)
	{
		if (alpha != _alpha)
		{
			_alpha = Mathf.Clamp01(alpha);
			Color color = leftCap.color;
			color.a = _alpha;
			leftCap.color = color;
			color = middlePiece.color;
			color.a = _alpha;
			middlePiece.color = color;
			color = rightCap.color;
			color.a = _alpha;
			rightCap.color = color;
			int num = sheens.Length;
			for (int i = 0; i < num; i++)
			{
				color = sheens[i].color;
				color.a = _alpha;
				sheens[i].color = color;
			}
		}
	}

	public void ResetObject()
	{
		alpha = 1f;
	}
}
