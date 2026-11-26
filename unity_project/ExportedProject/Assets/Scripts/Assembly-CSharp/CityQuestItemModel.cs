using UnityEngine;

public class CityQuestItemModel : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public BoxCollider boxCollider;

	public TransformLink transformLink;

	public GameObject glint;

	private Camera _mainCamera;

	private Camera mainCamera
	{
		get
		{
			if (_mainCamera == null)
			{
				_mainCamera = ((!(CityController.instance != null)) ? Camera.main : CityController.instance.pinchZoomer.thisCamera);
			}
			return _mainCamera;
		}
	}

	private void Start()
	{
		if (transformLink == null)
		{
			transformLink = GetComponent<TransformLink>();
		}
		if (transformLink != null && transformLink.linkTo == null)
		{
			transformLink.LinkTo = CityController.instance.cameraHolder;
		}
	}

	public void Enable()
	{
		if (glint != null)
		{
			glint.SetActive(true);
		}
		if (boxCollider != null)
		{
			boxCollider.enabled = true;
		}
	}

	public void Disable()
	{
		if (glint != null)
		{
			glint.SetActive(false);
		}
		if (boxCollider != null)
		{
			boxCollider.enabled = false;
		}
	}
}
