using UnityEngine;

public class tk2dUIAnimationItem : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public Animator animator;

	public string downStateName;

	public string upStateName;

	public string clickStateName;

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
	}

	private void Reset()
	{
		uiItem = GetComponent<tk2dUIItem>();
	}

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
		uiItem.OnUp += HandleOnUp;
		uiItem.OnClick += HandleOnClick;
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnUp -= HandleOnUp;
		uiItem.OnClick -= HandleOnClick;
	}

	private void HandleOnDown()
	{
		if (animator != null)
		{
			animator.Play(downStateName);
		}
	}

	private void HandleOnUp()
	{
		if (animator != null)
		{
			animator.Play(upStateName);
		}
	}

	private void HandleOnClick()
	{
		if (animator != null)
		{
			animator.Play(clickStateName);
		}
	}
}
