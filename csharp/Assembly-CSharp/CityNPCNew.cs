using UnityEngine;

public class CityNPCNew : MonoBehaviour
{
	public Animator animator;

	public AEAnimatorTk2d animatorTk2d;

	public tk2dSprite staticSprite;

	public Landmark landmark;

	public Transform iconHolder;

	public BoxCollider boxCollider;

	private AEControllerTk2d _animatorController;

	private Renderer _iconRenderer;

	private Renderer _staticSpriteRenderer;

	public AEControllerTk2d animatorController
	{
		get
		{
			if (_animatorController == null)
			{
				_animatorController = GetComponentInChildren<AEControllerTk2d>();
			}
			return _animatorController;
		}
	}

	public Renderer iconRenderer
	{
		get
		{
			if (_iconRenderer == null && iconHolder != null)
			{
				_iconRenderer = iconHolder.GetComponentInChildren<Renderer>(includeInactive: true);
			}
			return _iconRenderer;
		}
	}

	public Renderer staticSpriteRenderer
	{
		get
		{
			if (_staticSpriteRenderer == null && staticSprite != null)
			{
				_staticSpriteRenderer = staticSprite.GetComponent<Renderer>();
			}
			return _staticSpriteRenderer;
		}
	}

	private void Awake()
	{
		if (animatorTk2d == null)
		{
			animatorTk2d = GetComponentInChildren<AEAnimatorTk2d>();
		}
		if (boxCollider == null)
		{
			boxCollider = GetComponent<BoxCollider>();
		}
		if (boxCollider != null)
		{
			boxCollider.enabled = false;
		}
	}

	private void Start()
	{
		if (animatorController != null)
		{
			DayNightCard.instance.AddAnimationControllerToTint(animatorController);
		}
		if (staticSprite != null)
		{
			DayNightCard.instance.AddSpriteToTint(staticSprite);
		}
	}
}
