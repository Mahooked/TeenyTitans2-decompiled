using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour
{
	public string spriteName = string.Empty;

	[Tooltip("If left empty, the tail sprite name is assumed to be the above sprite name with \"_tail\" appended")]
	public string tailSpriteNameOverride = string.Empty;

	public tk2dSpriteCollectionData spriteCollectionData;

	public bool disableIconCollider;

	public Vector3 landmarkOffset = Vector3.zero;

	public List<ZoomType> supportedViewTypes = new List<ZoomType>();

	public LandmarkSettings zoomedInSettings = new LandmarkSettings();

	public LandmarkSettings zoomedOutSettings = new LandmarkSettings();

	private CameraPinchZoomer _pinchZoomer;

	private bool _didApplicationQuit;

	private bool isSetup;

	private string TailSpriteName
	{
		get
		{
			return (!string.IsNullOrEmpty(tailSpriteNameOverride)) ? tailSpriteNameOverride : (icon.sprite.CurrentSprite.name + "_tail");
		}
	}

	public LandmarkIcon icon { get; private set; }

	public bool IsIconSetup
	{
		get
		{
			return isSetup;
		}
	}

	private void OnEnable()
	{
		if (icon != null)
		{
			icon.visible = !_pinchZoomer.isZoomedOut;
		}
		StartCoroutine("DelayStart");
	}

	private void OnDisable()
	{
		if (CityController.instance != null)
		{
			CityController.instance.OnExitCityScene -= HandleOnCitySceneExit;
		}
		if (icon != null)
		{
			icon.visible = false;
		}
	}

	private void Start()
	{
		CreateLandmarkIcon();
	}

	private void OnApplicationQuit()
	{
		_didApplicationQuit = true;
	}

	private IEnumerator DelayStart()
	{
		yield return new WaitForEndOfFrame();
		if (CityController.instance != null)
		{
			CityController.instance.OnExitCityScene += HandleOnCitySceneExit;
		}
		CreateLandmarkIcon();
	}

	private void OnDestroy()
	{
		if (_pinchZoomer != null)
		{
			_pinchZoomer.ZoomedIn -= HandleZoomedIn;
			_pinchZoomer.ZoomedOut -= HandleZoomedOut;
		}
		if (!isSetup)
		{
			return;
		}
		isSetup = false;
		_pinchZoomer = null;
		if (icon != null)
		{
			if (CityController.instance != null)
			{
				CityController.instance.landmarkIcons.Remove(icon);
			}
			Object.Destroy(icon.transform.parent.gameObject);
		}
	}

	private void HandleOnCitySceneExit()
	{
		if (icon != null)
		{
			if (CityController.instance != null)
			{
				CityController.instance.landmarkIcons.Remove(icon);
			}
			Object.Destroy(icon.transform.parent.gameObject);
		}
	}

	public void Show()
	{
		if (!isSetup)
		{
			CreateLandmarkIcon();
		}
		if (icon != null)
		{
			icon.sprite.gameObject.SetActive(true);
			icon.tailSprite.gameObject.SetActive(true);
		}
	}

	public void Show(string spriteIconName)
	{
		UpdateSpriteIcon(spriteIconName);
		Show();
	}

	public void Hide()
	{
		if (!isSetup)
		{
			CreateLandmarkIcon();
		}
		if (icon != null)
		{
			icon.sprite.gameObject.SetActive(false);
			icon.tailSprite.gameObject.SetActive(false);
		}
	}

	public void ResetLandmarkIcon()
	{
		isSetup = false;
		if (icon != null)
		{
			if (CityController.instance != null)
			{
				CityController.instance.landmarkIcons.Remove(icon);
			}
			Object.Destroy(icon.transform.parent.gameObject);
		}
		CreateLandmarkIcon();
	}

	public void CreateLandmarkIcon()
	{
		if (!isSetup)
		{
			if (CityController.instance != null)
			{
				_pinchZoomer = CityController.instance.pinchZoomer;
				_pinchZoomer.ZoomedIn += HandleZoomedIn;
				_pinchZoomer.ZoomedOut += HandleZoomedOut;
			}
			icon = ObjectPool.GetObjectFromResource("Prefabs/GUI/LandmarkIcon").GetComponentInChildren<LandmarkIcon>();
			icon.landmark = base.transform;
			icon.transform.parent.SetParent(GUICamera.instance.anchorMiddleCenter, true);
			if (spriteCollectionData != null)
			{
				icon.sprite.SetSprite(spriteCollectionData, spriteName);
			}
			else
			{
				icon.sprite.SetSprite(spriteName);
			}
			icon.tailSprite.SetSprite(TailSpriteName);
			icon.landmarkOffset = landmarkOffset;
			if (CityController.instance != null && !CityController.instance.landmarkIcons.Contains(icon))
			{
				CityController.instance.landmarkIcons.Add(icon);
			}
			if (disableIconCollider)
			{
				icon.boxCollider.enabled = false;
			}
			UpdateZoomSettings();
			isSetup = true;
		}
	}

	public void UpdateSpriteIcon(string spriteIconName)
	{
		if (!isSetup)
		{
			CreateLandmarkIcon();
		}
		if (icon != null)
		{
			icon.sprite.SetSprite(spriteIconName);
			icon.tailSprite.SetSprite(TailSpriteName);
		}
	}

	public void ResetSpriteIcon()
	{
		if (!isSetup)
		{
			CreateLandmarkIcon();
		}
		if (icon != null)
		{
			icon.sprite.SetSprite(spriteName);
			icon.tailSprite.SetSprite(TailSpriteName);
		}
	}

	public void UpdateZoomSettings()
	{
		if (_pinchZoomer != null && _pinchZoomer.isZoomedOut)
		{
			HandleZoomedOut();
		}
		else
		{
			HandleZoomedIn();
		}
	}

	private void HandleZoomedIn()
	{
		if (icon == null || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (CityController.instance != null && CityController.instance.zoomType != ZoomType.Normal && !supportedViewTypes.Contains(CityController.instance.zoomType) && CityController.instance.zoomType != ZoomType.Tunnel)
		{
			icon.visible = false;
			return;
		}
		icon.visible = zoomedInSettings.visible;
		icon.snapToEdge = zoomedInSettings.snapToEdge;
		icon.showTail = zoomedInSettings.showTail;
		icon.hideWhenInView = zoomedInSettings.hideWhenInView;
		icon.hideAtMinScale = zoomedInSettings.hideAtMinScale;
		icon.scaleFactor = zoomedInSettings.scaleFactor;
		icon.minScale = zoomedInSettings.minScale;
		icon.maxScale = zoomedInSettings.maxScale;
		if (icon.isSelectedLandmarkWayPoint)
		{
			icon.minScale = zoomedInSettings.minScale * 2f;
			icon.hideAtMinScale = false;
		}
		icon.boxCollider.enabled = false;
		if (icon.IsManualWayPoint)
		{
			icon.waypointArrow.scale = new Vector3(1f, 1f, 1f);
		}
		if (icon.animator.IsNamed("Landmarkicon_intro_waypoint", "Landmarkicon_select_loop", "Landmarkicon_select"))
		{
			icon.animator.Play("Landmarkicon_select_outro");
		}
		if (icon.isSelectedLandmarkWayPoint)
		{
			icon.animator.enabled = true;
			icon.animator.Play("Landmarkicon_selected_loop");
		}
	}

	private void HandleZoomedOut()
	{
		if (icon == null || !base.gameObject.activeInHierarchy || icon.sprite.CurrentSprite.name.Equals("gui_citynotifiers_newsflash"))
		{
			return;
		}
		if (CityController.instance.zoomType != ZoomType.Normal && !supportedViewTypes.Contains(CityController.instance.zoomType) && CityController.instance.zoomType != ZoomType.Tunnel)
		{
			icon.visible = false;
			return;
		}
		icon.visible = zoomedOutSettings.visible;
		icon.snapToEdge = zoomedOutSettings.snapToEdge;
		icon.showTail = zoomedOutSettings.showTail;
		icon.hideWhenInView = zoomedOutSettings.hideWhenInView;
		icon.hideAtMinScale = zoomedOutSettings.hideAtMinScale;
		icon.scaleFactor = zoomedOutSettings.scaleFactor;
		icon.minScale = zoomedOutSettings.minScale;
		icon.maxScale = zoomedOutSettings.maxScale;
		if (!icon.IsManualWayPoint)
		{
			if (!disableIconCollider)
			{
				icon.boxCollider.enabled = true;
			}
		}
		else
		{
			if (!disableIconCollider)
			{
				icon.boxCollider.enabled = true;
			}
			icon.waypointArrow.scale = new Vector3(0.7f, 0.7f, 1f);
			icon.tailSprite.gameObject.transform.localEulerAngles = Vector3.zero;
		}
		if (icon.isSelectedLandmarkWayPoint)
		{
			icon.animator.Play("Landmarkicon_select");
		}
	}
}
