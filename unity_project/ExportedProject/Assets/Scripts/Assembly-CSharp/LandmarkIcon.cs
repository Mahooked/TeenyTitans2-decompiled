using System.Collections;
using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class LandmarkIcon : MonoBehaviour
{
	public tk2dSprite sprite;

	public tk2dSprite tailSprite;

	public Renderer[] arrows;

	public Camera mainCamera;

	public Camera guiCamera;

	public Transform landmark;

	public Vector3 landmarkOffset = Vector3.zero;

	[Range(0.01f, 0.9f)]
	public float scaleFactor = 0.003f;

	public float minScale = 0.2f;

	public float maxScale = 0.5f;

	public bool snapToEdge = true;

	public bool useDirectPath = true;

	public bool hideWhenInView;

	public bool hideAtMinScale;

	public bool showTail;

	public bool visible = true;

	private Renderer tailSpriteRenderer;

	private bool isHidden = true;

	private Sequence fadeInSequence;

	private Sequence fadeOutSequence;

	private float tailLength;

	private bool isDestroyed;

	public tk2dSprite waypointArrow;

	public tk2dUIItem uiItem;

	public BoxCollider2D boxCollider;

	private float colliderSelectedSize = 60f;

	private float colliderUnSelectedSize = 35f;

	private bool isCorrectParent = true;

	private bool checkedForCorrectParent;

	private float _screenWidth = -1f;

	private float _screenHeight = -1f;

	private TitleScreenController _titleScreenController;

	public bool IsManualWayPoint;

	public bool isSelectedLandmarkWayPoint;

	private Animator _animator;

	private float screenWidth
	{
		get
		{
			if (_screenWidth <= 0f)
			{
				_screenWidth = Screen.width;
			}
			return _screenWidth;
		}
	}

	private float screenHeight
	{
		get
		{
			if (_screenHeight <= 0f)
			{
				_screenHeight = Screen.height;
			}
			return _screenHeight;
		}
	}

	private TitleScreenController titleScreenController
	{
		get
		{
			if (_titleScreenController == null)
			{
				_titleScreenController = TitleScreenController.instance;
			}
			return _titleScreenController;
		}
	}

	public Animator animator
	{
		get
		{
			if (_animator == null)
			{
				_animator = GetComponent<Animator>();
			}
			return _animator;
		}
	}

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponentInChildren<tk2dSprite>();
		}
		if (mainCamera == null)
		{
			mainCamera = Camera.main;
		}
		if (guiCamera == null)
		{
			guiCamera = GUICamera.instance.camera;
		}
		Color color = sprite.color;
		color.a = 0f;
		sprite.color = color;
		color = tailSprite.color;
		color.a = 0f;
		tailSprite.color = color;
		sprite.gameObject.SetActive(false);
		tailSprite.gameObject.SetActive(false);
	}

	private void Start()
	{
		useDirectPath = PlayerPrefs.GetInt("UseDirectPath", 1) == 1;
		tailLength = tailSprite.GetBounds().min.y;
		tailSpriteRenderer = tailSprite.GetComponent<Renderer>();
		tailSpriteRenderer.enabled = showTail;
	}

	private void OnEnable()
	{
		if (CityController.instance != null)
		{
			CityController.instance.OnLandmarkWayPointClicked += SelectLandmarkWayPoint;
		}
		uiItem.OnDown += HandleOnDown;
		uiItem.OnClick += HandleOnClick;
		uiItem.OnRelease += HandleOnRelease;
		checkedForCorrectParent = false;
		isCorrectParent = true;
	}

	private void OnDisable()
	{
		if (CityController.instance != null)
		{
			CityController.instance.OnLandmarkWayPointClicked -= SelectLandmarkWayPoint;
		}
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnClick -= HandleOnClick;
		uiItem.OnRelease -= HandleOnRelease;
	}

	private void Reset()
	{
		sprite = GetComponentInChildren<tk2dSprite>();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus && PlayerPrefs.HasKey("UseDirectPath"))
		{
			useDirectPath = PlayerPrefs.GetInt("UseDirectPath", 1) == 1;
		}
	}

	private void OnDestroy()
	{
		isDestroyed = true;
	}

	private void LateUpdate()
	{
		if (isDestroyed || landmark == null)
		{
			return;
		}
		if (!visible)
		{
			if (!isHidden)
			{
				FadeOut();
				isHidden = true;
			}
			return;
		}
		if (titleScreenController != null && titleScreenController.titleScreenActive)
		{
			if (!isHidden)
			{
				FadeOut();
				isHidden = true;
			}
			return;
		}
		Transform transform = sprite.transform;
		Transform transform2 = null;
		Vector3 vector = mainCamera.WorldToScreenPoint(landmark.position + landmarkOffset);
		Vector3 vector2 = mainCamera.WorldToViewportPoint(landmark.position + landmarkOffset);
		Vector3 vector3 = new Vector3(screenWidth / 2f, screenHeight / 2f, vector.z);
		Vector3 vector4 = guiCamera.WorldToScreenPoint(transform.position);
		if (showTail)
		{
			transform2 = tailSprite.transform;
			float z = Mathf.Atan2(vector.y - vector3.y, vector.x - vector3.x) * 57.29578f + 90f;
			Vector3 eulerAngles = transform2.localRotation.eulerAngles;
			eulerAngles.z = z;
			transform2.localRotation = Quaternion.Euler(eulerAngles);
		}
		float num = Vector3.Distance(mainCamera.ScreenToWorldPoint(vector), mainCamera.ScreenToWorldPoint(vector3));
		Vector3 scale = sprite.scale;
		scale.x = (scale.y = Mathf.Clamp(maxScale - num * scaleFactor, minScale, maxScale));
		sprite.scale = scale;
		tailSprite.scale = scale;
		bool flag = false;
		if (vector2.x >= 0f && vector2.x <= 1f && vector2.y >= 0f && vector2.y <= 1f)
		{
			flag = true;
		}
		if (hideAtMinScale)
		{
			if (scale.x == minScale && !isHidden)
			{
				FadeOut();
				isHidden = true;
			}
			else if (scale.x > minScale && isHidden && (!hideWhenInView || (hideWhenInView && !flag)))
			{
				FadeIn();
				isHidden = false;
			}
		}
		else if (isHidden && (!hideWhenInView || (hideWhenInView && !flag)))
		{
			FadeIn();
			isHidden = false;
		}
		Bounds bounds = sprite.GetBounds();
		Vector3 vector5 = guiCamera.WorldToScreenPoint(new Vector3(transform.position.x + bounds.center.x + bounds.extents.x, transform.position.y + bounds.center.y + bounds.extents.y, transform.position.z));
		Vector3 vector6 = guiCamera.WorldToScreenPoint(new Vector3(transform.position.x + bounds.center.x - bounds.extents.x, transform.position.y + bounds.center.y - bounds.extents.y, transform.position.z));
		if (showTail)
		{
			Vector3 position = transform2.TransformPoint(new Vector3(0f, tailLength * scale.x, 0f));
			Vector3 vector7 = guiCamera.WorldToScreenPoint(position);
			if (vector7.x > vector5.x)
			{
				vector5.x = vector7.x;
			}
			if (vector7.y > vector5.y)
			{
				vector5.y = vector7.y;
			}
			if (vector7.x < vector6.x)
			{
				vector6.x = vector7.x;
			}
			if (vector7.y < vector6.y)
			{
				vector6.y = vector7.y;
			}
		}
		vector5.x -= vector4.x;
		vector5.y -= vector4.y;
		vector5.z = 0f;
		vector6.x -= vector4.x;
		vector6.y -= vector4.y;
		vector6.z = 0f;
		if (!snapToEdge || (vector.x >= 0f - vector6.x && vector.x <= screenWidth - vector5.x && vector.y >= 0f - vector6.y && vector.y <= screenHeight - vector5.y))
		{
			if (hideWhenInView && !isHidden)
			{
				FadeOut();
				isHidden = true;
			}
			else if (!hideWhenInView && isHidden)
			{
				FadeIn();
				isHidden = false;
			}
			tailSpriteRenderer.enabled = false;
			if (animator != null && !animator.enabled)
			{
				return;
			}
			Vector3 position2 = guiCamera.ScreenToWorldPoint(vector);
			position2.z = ((!IsManualWayPoint) ? 0f : 10f);
			if (!checkedForCorrectParent)
			{
				checkedForCorrectParent = true;
				if (base.transform.parent.GetComponent<tk2dCameraAnchor>() != null)
				{
					isCorrectParent = false;
				}
				else
				{
					isCorrectParent = true;
				}
			}
			if (isCorrectParent)
			{
				base.transform.parent.position = position2;
			}
			else
			{
				base.transform.position = position2;
			}
			return;
		}
		if ((!hideAtMinScale || (hideWhenInView && scale.x > minScale)) && isHidden)
		{
			FadeIn();
			isHidden = false;
		}
		tailSpriteRenderer.enabled = showTail;
		if (animator != null && !animator.enabled)
		{
			return;
		}
		if (useDirectPath)
		{
			Vector3 lineVec = vector - vector3;
			Vector3 intersection;
			if ((Math3d.LineLineIntersection(out intersection, vector3, lineVec, new Vector3(screenWidth - vector5.x, 0f, vector.z), new Vector3(0f, 1f, 0f) * screenHeight) && intersection.y >= 0f - vector6.y && intersection.y <= screenHeight - vector5.y) || (Math3d.LineLineIntersection(out intersection, vector3, lineVec, new Vector3(0f - vector6.x, 0f, vector.z), new Vector3(0f, 1f, 0f) * screenHeight) && intersection.y >= 0f - vector6.y && intersection.y <= screenHeight - vector5.y) || (Math3d.LineLineIntersection(out intersection, vector3, lineVec, new Vector3(0f, screenHeight - vector5.y, vector.z), new Vector3(1f, 0f, 0f) * screenWidth) && intersection.x >= 0f - vector6.x && intersection.x <= screenWidth - vector5.x) || !Math3d.LineLineIntersection(out intersection, vector3, lineVec, new Vector3(0f, 0f - vector6.y, vector.z), new Vector3(1f, 0f, 0f) * screenWidth) || !(intersection.x >= 0f - vector6.x) || intersection.x <= screenWidth - vector5.x)
			{
			}
			if (intersection.x < 0f - vector6.x)
			{
				intersection.x = 0f - vector6.x;
			}
			else if (intersection.x > screenWidth - vector5.x)
			{
				intersection.x = screenWidth - vector5.x;
			}
			if (intersection.y < 0f - vector6.y)
			{
				intersection.y = 0f - vector6.y;
			}
			else if (intersection.y > screenHeight - vector5.y)
			{
				intersection.y = screenHeight - vector5.y;
			}
			Vector3 position3 = guiCamera.ScreenToWorldPoint(intersection);
			position3.z = 0f - scale.x;
			if (!checkedForCorrectParent)
			{
				checkedForCorrectParent = true;
				if (base.transform.parent.GetComponent<tk2dCameraAnchor>() != null)
				{
					isCorrectParent = false;
				}
				else
				{
					isCorrectParent = true;
				}
			}
			if (isCorrectParent)
			{
				base.transform.parent.position = position3;
			}
			else
			{
				base.transform.position = position3;
			}
			return;
		}
		if (vector.x < 0f - vector6.x)
		{
			vector.x = 0f - vector6.x;
		}
		else if (vector.x > screenWidth - vector5.x)
		{
			vector.x = screenWidth - vector5.x;
		}
		if (vector.y < 0f - vector6.y)
		{
			vector.y = 0f - vector6.y;
		}
		else if (vector.y > screenHeight - vector5.y)
		{
			vector.y = screenHeight - vector5.y;
		}
		Vector3 position4 = guiCamera.ScreenToWorldPoint(vector);
		position4.z = ((!isSelectedLandmarkWayPoint) ? (0f - scale.x) : 10f);
		if (!checkedForCorrectParent)
		{
			checkedForCorrectParent = true;
			if (base.transform.parent.GetComponent<tk2dCameraAnchor>() != null)
			{
				isCorrectParent = false;
			}
			else
			{
				isCorrectParent = true;
			}
		}
		if (isCorrectParent)
		{
			base.transform.parent.position = position4;
		}
		else
		{
			base.transform.position = position4;
		}
	}

	private void FadeIn()
	{
		if (animator != null)
		{
			animator.enabled = true;
		}
		if (!isDestroyed && !(sprite.color.a >= 0.99f) && !LoadingScreenCover.isLoading && fadeInSequence == null)
		{
			if (fadeOutSequence != null)
			{
				fadeOutSequence.Kill();
				fadeOutSequence = null;
			}
			if (sprite != null)
			{
				sprite.gameObject.SetActive(true);
			}
			if (tailSprite != null)
			{
				tailSprite.gameObject.SetActive(true);
			}
			fadeInSequence = DOTween.Sequence().OnComplete(OnFadeInComplete);
			fadeInSequence.Append(DOTween.ToAlpha(() => sprite.color, (Color x) =>
			{
				sprite.color = x;
			}, 1f, 0.25f));
			fadeInSequence.Insert(0f, DOTween.ToAlpha(() => tailSprite.color, (Color x) =>
			{
				tailSprite.color = x;
			}, 1f, 0.25f));
			fadeInSequence.Play();
			for (int num = 0; num < arrows.Length; num++)
			{
				arrows[num].enabled = true;
			}
		}
	}

	private void FadeOut()
	{
		if (!isDestroyed && !(sprite.color.a <= 0.01f) && !LoadingScreenCover.isLoading && fadeOutSequence == null)
		{
			if (fadeInSequence != null)
			{
				fadeInSequence.Kill();
				fadeInSequence = null;
			}
			fadeOutSequence = DOTween.Sequence().OnComplete(OnFadeOutComplete);
			fadeOutSequence.Append(DOTween.ToAlpha(() => sprite.color, (Color x) =>
			{
				sprite.color = x;
			}, 0f, 0.25f));
			fadeOutSequence.Insert(0f, DOTween.ToAlpha(() => tailSprite.color, (Color x) =>
			{
				tailSprite.color = x;
			}, 0f, 0.25f));
			fadeOutSequence.Play();
			for (int num = 0; num < arrows.Length; num++)
			{
				arrows[num].enabled = false;
			}
		}
	}

	private void OnFadeInComplete()
	{
		fadeInSequence = null;
	}

	private void OnFadeOutComplete()
	{
		fadeOutSequence = null;
		if (sprite != null)
		{
			sprite.gameObject.SetActive(false);
		}
		if (tailSprite != null)
		{
			tailSprite.gameObject.SetActive(false);
		}
		if (animator != null)
		{
			animator.enabled = false;
		}
	}

	private IEnumerator DelayRenableCollider()
	{
		yield return new WaitForSeconds(0.02f);
		boxCollider.enabled = true;
	}

	private IEnumerator DelayDisableGameObject()
	{
		yield return new WaitForSeconds(0.3f);
		base.gameObject.SetActive(false);
		boxCollider.enabled = false;
	}

	private void HandleOnDown()
	{
		if (CityController.instance.zoomType == ZoomType.Normal)
		{
			CityController.instance.LandmarkWayPointDown(this);
		}
		CityController.instance.PreventManualWayPoint(true);
	}

	private void HandleOnClick()
	{
		if (CityController.instance.pinchZoomer.isZoomedOut)
		{
			if (CityController.instance.zoomType == ZoomType.Normal)
			{
				CityController.instance.LandmarkWayPointClicked(this);
			}
			boxCollider.enabled = false;
			if (CityController.instance.zoomType != ZoomType.Tunnel)
			{
				StartCoroutine("DelayRenableCollider");
			}
		}
	}

	private void HandleOnRelease()
	{
		if (CityController.instance.zoomType == ZoomType.Normal)
		{
			CityController.instance.LandmarkWayPointReleased(this);
		}
		StartCoroutine(DelayRenableManualWayPoint());
	}

	private IEnumerator DelayRenableManualWayPoint()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		CityController.instance.PreventManualWayPoint(false);
	}

	private void SelectLandmarkWayPoint(LandmarkIcon icon, bool unselectSelected = true, bool manualWayPointClick = false)
	{
		if (icon == this)
		{
			if (isSelectedLandmarkWayPoint && unselectSelected && (!IsManualWayPoint || (IsManualWayPoint && !manualWayPointClick && base.gameObject.activeInHierarchy)))
			{
				isSelectedLandmarkWayPoint = false;
				if (!animator.IsName("Landmarkicon_select_outro"))
				{
					animator.Play("Landmarkicon_select_outro");
				}
				if (IsManualWayPoint)
				{
					StopCoroutine("DelayDisableGameObject");
					StartCoroutine("DelayDisableGameObject");
				}
				boxCollider.size = new Vector2(colliderUnSelectedSize, colliderUnSelectedSize);
			}
			else
			{
				isSelectedLandmarkWayPoint = true;
				if (IsManualWayPoint)
				{
					boxCollider.enabled = true;
					StopCoroutine("DelayDisableGameObject");
					base.gameObject.SetActive(true);
				}
				if (!animator.IsNamed("Landmarkicon_select", "Landmarkicon_select_loop"))
				{
					animator.Play("Landmarkicon_select");
				}
				boxCollider.size = new Vector2(colliderSelectedSize, colliderSelectedSize);
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_markerplace", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			isSelectedLandmarkWayPoint = false;
			if (!animator.IsName("Landmarkicon_hold"))
			{
				animator.Play("Landmarkicon_hold");
			}
			if (IsManualWayPoint && !manualWayPointClick)
			{
				boxCollider.enabled = false;
				base.gameObject.SetActive(false);
			}
			boxCollider.size = new Vector2(colliderUnSelectedSize, colliderUnSelectedSize);
		}
	}
}
