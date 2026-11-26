using UnityEngine;

public class RiddlerShopBaseWindow : MonoBehaviour
{
	public Animator animator;

	public tk2dUIItem closeButton;

	private AnimatorEventListener _animatorEventListener;

	public AnimatorEventListener animatorEventListener
	{
		get
		{
			if (_animatorEventListener == null)
			{
				_animatorEventListener = animator.GetComponent<AnimatorEventListener>();
			}
			return _animatorEventListener;
		}
	}

	protected virtual void OnEnable()
	{
		if (animatorEventListener != null)
		{
			animatorEventListener.EventTriggered += Handle_EventTriggered;
		}
	}

	protected virtual void OnDisable()
	{
		if (animatorEventListener != null)
		{
			animatorEventListener.EventTriggered -= Handle_EventTriggered;
		}
	}

	public virtual void Open()
	{
		base.gameObject.SetActive(true);
	}

	public virtual void Close()
	{
		base.gameObject.SetActive(false);
	}

	private void Handle_EventTriggered(AnimationEvent obj)
	{
		if (obj.stringParameter == "complete")
		{
			BoxCollider2D[] componentsInChildren = GetComponentsInChildren<BoxCollider2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
				componentsInChildren[i].enabled = true;
			}
		}
	}
}
