using UnityEngine;

public class tk2dTintAdapter : MonoBehaviour
{
	public tk2dBaseSprite sprite;

	private Color _tintColor = new Color(1f, 1f, 1f, 0f);

	private Renderer _spriteRenderer;

	public Color tintColor
	{
		get
		{
			return _tintColor;
		}
		set
		{
			if (!(value == _tintColor))
			{
				_tintColor = value;
				if (spriteRenderer != null)
				{
					spriteRenderer.material.SetColor("_TintColor", _tintColor);
				}
			}
		}
	}

	public Renderer spriteRenderer
	{
		get
		{
			if (_spriteRenderer == null)
			{
				_spriteRenderer = sprite.GetComponent<Renderer>();
			}
			return _spriteRenderer;
		}
	}

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
		}
	}

	private void Reset()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
		}
	}
}
