using UnityEngine;

public class ClassChart : MonoBehaviour
{
	public tk2dUIItem closeButton;

	public tk2dUIItem backingButton;

	private void OnEnable()
	{
		closeButton.OnClick += HandleOnCloseClick;
		backingButton.OnClick += HandleOnCloseClick;
	}

	private void OnDisable()
	{
		closeButton.OnClick -= HandleOnCloseClick;
		backingButton.OnClick -= HandleOnCloseClick;
	}

	private void HandleOnCloseClick()
	{
		ObjectPool.PoolObject(base.gameObject);
	}
}
