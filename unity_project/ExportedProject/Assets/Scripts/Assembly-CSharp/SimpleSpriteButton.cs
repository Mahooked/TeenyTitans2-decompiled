using UnityEngine;

public class SimpleSpriteButton : MonoBehaviour
{
	[SerializeField]
	private tk2dSprite sprite;

	[SerializeField]
	private Collider2D collider2D;

	[SerializeField]
	private Color colorEnabled;

	[SerializeField]
	private Color colorDisabled;

	private Renderer _spriteRenderer;

	[SerializeField]
	private bool _isEnabled = true;

	private Renderer spriteRenderer
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

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			_isEnabled = value;
			UpdateEnabledState();
		}
	}

	private void UpdateEnabledState()
	{
		if (_isEnabled)
		{
			spriteRenderer.enabled = true;
			sprite.color = colorEnabled;
			collider2D.enabled = true;
			return;
		}
		if (colorDisabled.a <= 0f)
		{
			spriteRenderer.enabled = false;
		}
		else
		{
			sprite.color = colorDisabled;
		}
		collider2D.enabled = false;
	}
}
