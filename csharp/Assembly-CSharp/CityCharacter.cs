using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using TouchScript.Gestures;
using UnityEngine;

[SelectionBase]
public class CityCharacter : MonoBehaviour
{
	public static CityCharacterType characterType = CityCharacterType.Robin;

	public static bool waitForTitanChoice;

	public Transform model;

	public PathNode startNode;

	public MoveSwipeType moveSwipeType = MoveSwipeType.Screen;

	public float angleThreshold = 45f;

	public float turningForgiveness = 0.35f;

	public float turningForgivenessMoving = 0.15f;

	public float minInputDistance = 5f;

	public float moveSpeed = 2f;

	public bool useMinInputDistance = true;

	public TapGesture tapGesture;

	public CameraPinchZoomer pinchZoomer;

	public string[] districtChangedVOBeach;

	public string[] districtChangedVODowntown;

	public string[] districtChangedVOPrison;

	public string[] districtChangedVOShady;

	public string[] districtChangedVOUptown;

	public string[] dayStartedVO;

	public string[] nightStartedVO;

	private float extraRayDist = 18.5f;

	private float baseMoveSpeed;

	private PathNode _currentNode;

	private PathNode _prevNode;

	private List<PathConnection> _curPath;

	private List<PathNode> allNodes;

	private bool pathingToTarget;

	private Tweener moveTween;

	private Tweener moveTween2;

	private bool isZooming;

	private tk2dUIItem touchStartedOnButton;

	private GameObject flashSaleParticle;

	private bool interactiveDestinationQueued;

	private Action interactiveDestinationCallback;

	private PathNode interactiveDestinationNode;

	private bool pathingToTemporaryWaypoint;

	private List<PathNode> waypointGroup = new List<PathNode>(2);

	private string[] tapSFXRegular = new string[4] { "sfx_controls_tap_regualr_1", "sfx_controls_tap_regualr_2", "sfx_controls_tap_regualr_3", "sfx_controls_tap_regualr_4" };

	private string[] tapSFXSand = new string[4] { "sfx_controls_tap_sand_1", "sfx_controls_tap_sand_2", "sfx_controls_tap_sand_3", "sfx_controls_tap_sand_4" };

	private string[] tapSFXWater = new string[4] { "sfx_controls_tap_water_1", "sfx_controls_tap_water_2", "sfx_controls_tap_water_3", "sfx_controls_tap_water_4" };

	private string[] robinIdleVO = new string[10] { "sfx_vo_robin_city_idle1_kpr", "sfx_vo_robin_city_idle1_kpr", "sfx_vo_robin_city_idle2_kpr", "sfx_vo_robin_city_idle3_kpr", "sfx_vo_robin_city_idle4_kpr", "sfx_vo_robin_city_idle5_kpr", "sfx_vo_robin_city_idle6_kpr", "sfx_vo_robin_city_idle7_kpr1", "sfx_vo_robin_city_idle7_kpr2", "sfx_vo_robin_city_idle8_kpr" };

	private string[] beastBoyIdleVO = new string[0];

	private float delayOnIdleVO = 120f;

	private CameraFollow cameraFollow;

	private float prevTargetCameraFOV = 12f;

	private float nextTargetCameraFOV = 12f;

	private CityController _cityController;

	private FlashSaleController _flashSaleController;

	private Collider[] hitResults = new Collider[10];

	private float lastHitCheck;

	private float hitCheckDT = 0.1f;

	private float idleSFXTimer;

	private IEnumerator boostDurationCoroutine;

	private bool allowInput = true;

	private bool allowTapToMove = true;

	[HideInInspector]
	public AEControllerTk2d animator;

	private List<GameObject> animatorsAddedToScene = new List<GameObject>();

	private AEControllerTk2d _robinAnimator;

	private AEControllerTk2d _beastBoyAnimator;

	private AEControllerTk2d _ravenAnimator;

	private AEControllerTk2d _starfireAnimator;

	private AEControllerTk2d _cyborgAnimator;

	private static bool firstCheck;

	private CityController cityController
	{
		get
		{
			if (_cityController == null)
			{
				_cityController = CityController.instance;
			}
			return _cityController;
		}
	}

	private FlashSaleController flashSaleController
	{
		get
		{
			if (_flashSaleController == null && FlashSaleController.instance != null)
			{
				_flashSaleController = FlashSaleController.instance;
			}
			return _flashSaleController;
		}
	}

	public static CharacterName RobinType
	{
		get
		{
			if (RobinIsDogbin())
			{
				return CharacterName.DogRobin;
			}
			if (characterType == CityCharacterType.BeastBoy)
			{
				return CharacterName.BeastBoy;
			}
			if (characterType == CityCharacterType.Raven)
			{
				return CharacterName.Raven;
			}
			if (characterType == CityCharacterType.Starfire)
			{
				return CharacterName.Starfire;
			}
			if (characterType == CityCharacterType.Cyborg)
			{
				return CharacterName.Cyborg;
			}
			return CharacterName.Robin;
		}
	}

	private string[] CityCharacterVO
	{
		get
		{
			if (characterType == CityCharacterType.Robin)
			{
				return robinIdleVO;
			}
			if (characterType == CityCharacterType.BeastBoy)
			{
				return beastBoyIdleVO;
			}
			return robinIdleVO;
		}
	}

	public PathNode CurrentNode => _currentNode;

	public bool IsMoving => moveTween != null;

	public bool AllowInput
	{
		get
		{
			return allowInput;
		}
		set
		{
			allowInput = value;
		}
	}

	public bool AllowTapToMove
	{
		get
		{
			return allowTapToMove;
		}
		set
		{
			allowTapToMove = value;
		}
	}

	public AEControllerTk2d robinAnimator
	{
		get
		{
			if (_robinAnimator == null)
			{
				Transform transform = base.transform.Find("Robin");
				if (transform != null)
				{
					_robinAnimator = transform.gameObject.GetComponent<AEControllerTk2d>();
				}
				if (_robinAnimator == null)
				{
					ClearAnimatorsAddedToScene();
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Characters/CityTitans/Robin") as GameObject);
					gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
					Vector3 position = gameObject.transform.position;
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = position;
					gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					_robinAnimator = gameObject.GetComponent<AEControllerTk2d>();
					animatorsAddedToScene.Add(gameObject);
				}
			}
			return _robinAnimator;
		}
	}

	public AEControllerTk2d beastBoyAnimator
	{
		get
		{
			if (_beastBoyAnimator == null)
			{
				Transform transform = base.transform.Find("BeastBoy");
				if (transform != null)
				{
					_beastBoyAnimator = transform.gameObject.GetComponent<AEControllerTk2d>();
				}
				if (_beastBoyAnimator == null)
				{
					ClearAnimatorsAddedToScene();
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Characters/CityTitans/BeastBoy") as GameObject);
					gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
					Vector3 position = gameObject.transform.position;
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = position;
					gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					_beastBoyAnimator = gameObject.GetComponent<AEControllerTk2d>();
					animatorsAddedToScene.Add(gameObject);
				}
			}
			return _beastBoyAnimator;
		}
	}

	public AEControllerTk2d ravenAnimator
	{
		get
		{
			if (_ravenAnimator == null)
			{
				Transform transform = base.transform.Find("Raven");
				if (transform != null)
				{
					_ravenAnimator = transform.gameObject.GetComponent<AEControllerTk2d>();
				}
				if (_ravenAnimator == null)
				{
					ClearAnimatorsAddedToScene();
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Characters/CityTitans/Raven") as GameObject);
					gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
					Vector3 position = gameObject.transform.position;
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = position;
					gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					_ravenAnimator = gameObject.GetComponent<AEControllerTk2d>();
					animatorsAddedToScene.Add(gameObject);
				}
			}
			return _ravenAnimator;
		}
	}

	public AEControllerTk2d starfireAnimator
	{
		get
		{
			if (_starfireAnimator == null)
			{
				Transform transform = base.transform.Find("Starfire");
				if (transform != null)
				{
					_starfireAnimator = transform.gameObject.GetComponent<AEControllerTk2d>();
				}
				if (_starfireAnimator == null)
				{
					ClearAnimatorsAddedToScene();
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Characters/CityTitans/Starfire") as GameObject);
					gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
					Vector3 position = gameObject.transform.position;
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = position;
					gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					_starfireAnimator = gameObject.GetComponent<AEControllerTk2d>();
					animatorsAddedToScene.Add(gameObject);
				}
			}
			return _starfireAnimator;
		}
	}

	public AEControllerTk2d cyborgAnimator
	{
		get
		{
			if (_cyborgAnimator == null)
			{
				Transform transform = base.transform.Find("Cyborg");
				if (transform != null)
				{
					_cyborgAnimator = transform.gameObject.GetComponent<AEControllerTk2d>();
				}
				if (_cyborgAnimator == null)
				{
					ClearAnimatorsAddedToScene();
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Characters/CityTitans/Cyborg") as GameObject);
					gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
					Vector3 position = gameObject.transform.position;
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = position;
					gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					_cyborgAnimator = gameObject.GetComponent<AEControllerTk2d>();
					animatorsAddedToScene.Add(gameObject);
				}
			}
			return _cyborgAnimator;
		}
	}

	public static event Action<CityCharacterType> CharacterTypeChanged;

	private void Awake()
	{
		_curPath = new List<PathConnection>();
		characterType = CityCharacterTypeFromSkinName(GameSkinManager.instance.CurrentSkinName);
	}

	private IEnumerator Start()
	{
		UpdateCityCharacter();
		baseMoveSpeed = moveSpeed;
		if (cityController != null)
		{
			allNodes = cityController.allNodes;
		}
		else
		{
			allNodes = new List<PathNode>(UnityEngine.Object.FindObjectsOfType<PathNode>());
		}
		cameraFollow = Camera.main.GetComponentInParent<CameraFollow>();
		prevTargetCameraFOV = (nextTargetCameraFOV = cameraFollow.targetFOV);
		GameObject tapEffect = ObjectPool.GetObjectFromResource("Prefabs/City/Tap_Particle_01");
		ObjectPool.PoolObject(tapEffect);
		SwipeManager.Instance.SwipeOccurred += HandleSwipeOccurred;
		CityButtonEventHelper.instance.tk2dUIItemOnDown += Handletk2dUIItemOnDown;
		CityButtonEventHelper.instance.tk2dUIItemOnRelease += Handletk2dUIItemOnRelease;
		cityController.communicator.StartDayTime += HandleDayNightChanged;
		cityController.communicator.StartNightTime += HandleDayNightChanged;
		idleSFXTimer = delayOnIdleVO;
		UpdateStartNode();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		while (waitForTitanChoice)
		{
			yield return null;
		}
		UpdateCityCharacter();
		if (animator != null)
		{
			animator.Play("idle");
		}
		GetComponent<TransformLink>().ignoreDisable = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		UpdateStartNode();
	}

	private void OnDestroy()
	{
		if (SwipeManager.Instance != null)
		{
			SwipeManager.Instance.SwipeOccurred -= HandleSwipeOccurred;
		}
		if (CityButtonEventHelper.instance != null)
		{
			CityButtonEventHelper.instance.tk2dUIItemOnDown -= Handletk2dUIItemOnDown;
			CityButtonEventHelper.instance.tk2dUIItemOnRelease -= Handletk2dUIItemOnRelease;
		}
		if (CityController.instance != null)
		{
			cityController.communicator.StartDayTime -= HandleDayNightChanged;
			cityController.communicator.StartNightTime -= HandleDayNightChanged;
		}
	}

	public static bool RobinIsDogbin()
	{
		return SaveUtility.GetInt("ROBIN_IS_DOGBIN") == 1;
	}

	public void FaceTarget(Transform target)
	{
		Vector3 vector = Camera.main.WorldToViewportPoint(transform.position);
		if (Camera.main.WorldToViewportPoint(target.position).x < vector.x)
		{
			if (animator != null)
			{
				animator.isFlipped = false;
			}
		}
		else if (animator != null)
		{
			animator.isFlipped = true;
		}
	}

	public void SwitchToCharacter(CityCharacterType updatedCharacterType)
	{
		GameSkinManager.instance.ChangeGameSkin(updatedCharacterType.ToString().ToUpper());
		UpdateCityCharacter();
	}

	private void DisableAnimatorExcept(AEControllerTk2d controller)
	{
		if (_robinAnimator != null && controller != _robinAnimator)
		{
			_robinAnimator.gameObject.SetActive(value: false);
		}
		if (_beastBoyAnimator != null && controller != _beastBoyAnimator)
		{
			_beastBoyAnimator.gameObject.SetActive(value: false);
		}
		if (_ravenAnimator != null && controller != _ravenAnimator)
		{
			_ravenAnimator.gameObject.SetActive(value: false);
		}
		if (_starfireAnimator != null && controller != _starfireAnimator)
		{
			_starfireAnimator.gameObject.SetActive(value: false);
		}
		if (_cyborgAnimator != null && controller != _cyborgAnimator)
		{
			_cyborgAnimator.gameObject.SetActive(value: false);
		}
		controller.gameObject.SetActive(value: true);
	}

	public static CityCharacterType CityCharacterTypeFromSkinName(string skinName)
	{
		return (CityCharacterType)Enum.Parse(typeof(CityCharacterType), skinName, ignoreCase: true);
	}

	public void UpdateCityCharacter()
	{
		characterType = CityCharacterTypeFromSkinName(GameSkinManager.instance.CurrentSkinName);
		bool isFlipped = animator != null && animator.isFlipped;
		if (animator != null)
		{
			DayNightCard.instance.RemoveAnimationControllerToTint(animator);
		}
		switch (characterType)
		{
		case CityCharacterType.Robin:
			animator = robinAnimator;
			DisableAnimatorExcept(animator);
			break;
		case CityCharacterType.BeastBoy:
			animator = beastBoyAnimator;
			DisableAnimatorExcept(animator);
			break;
		case CityCharacterType.Raven:
			animator = ravenAnimator;
			DisableAnimatorExcept(animator);
			break;
		case CityCharacterType.Starfire:
			animator = starfireAnimator;
			DisableAnimatorExcept(animator);
			break;
		case CityCharacterType.Cyborg:
			animator = cyborgAnimator;
			DisableAnimatorExcept(animator);
			break;
		}
		animator.isFlipped = isFlipped;
		animator.animator.RefreshFlipped();
		animator.Play("idle");
		DayNightCard.instance.AddAnimationControllerToTint(animator);
		if (CharacterTypeChanged != null)
		{
			CharacterTypeChanged(characterType);
		}
	}

	public void UpdateCurrentNode(PathNode pathNode)
	{
		_currentNode = pathNode;
	}

	public void UpdateStartNode()
	{
		startNode = null;
		PathNode pathNode = null;
		float num = float.PositiveInfinity;
		for (int i = 0; i < allNodes.Count; i++)
		{
			PathNode pathNode2 = allNodes[i];
			float num2 = Vector3.Distance(pathNode2.transform.position, transform.position);
			if (num2 < num && pathNode2.isActive)
			{
				num = num2;
				pathNode = pathNode2;
			}
		}
		if (pathNode != null)
		{
			startNode = pathNode;
		}
		if (startNode != null)
		{
			SetPosition(startNode);
		}
	}

	private void OnEnable()
	{
		tapGesture.Tapped += HandleTapped;
		pinchZoomer.ZoomStarted += HandleZoomStarted;
		pinchZoomer.ZoomFinished += HandleZoomFinished;
	}

	private void OnDisable()
	{
		tapGesture.Tapped -= HandleTapped;
		pinchZoomer.ZoomStarted -= HandleZoomStarted;
		pinchZoomer.ZoomFinished -= HandleZoomFinished;
		isZooming = false;
	}

	private void Handletk2dUIItemOnDown(tk2dUIItem uiItem)
	{
		touchStartedOnButton = uiItem;
	}

	private void Handletk2dUIItemOnRelease(tk2dUIItem uiItem)
	{
		StartCoroutine("DelayTouchStartNull");
	}

	private IEnumerator DelayTouchStartNull()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		touchStartedOnButton = null;
	}

	private void HandleTapped(object sender, EventArgs e)
	{
		if (touchStartedOnButton != null && !touchStartedOnButton.gameObject.name.Equals("MagnifyingGlass_Button"))
		{
			return;
		}
		if (cityController != null && cityController.mySquad != null)
		{
			cityController.mySquad.HideNow();
		}
		CameraPinchZoomer cameraPinchZoomer = ((!(cityController != null)) ? Camera.main.GetComponentInParent<CameraPinchZoomer>() : cityController.pinchZoomer);
		bool flag = false;
		Ray ray = cameraPinchZoomer.thisCamera.ScreenPointToRay(tapGesture.ScreenPosition);
		float enter = 0f;
		new Plane(Vector3.up, transform.position).Raycast(ray, out enter);
		RaycastHit[] array = Physics.RaycastAll(ray, enter);
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (!(raycastHit.collider != null))
			{
				continue;
			}
			tk2dUIItem component = raycastHit.collider.GetComponent<tk2dUIItem>();
			if (component != null && component.enabled)
			{
				flag = true;
				if (raycastHit.collider.GetComponent<CityGlint>() != null)
				{
					StopMove();
				}
				break;
			}
		}
		Collider2D collider2D = null;
		Camera camera = null;
		for (int j = 0; j < 2; j++)
		{
			switch (j)
			{
			case 0:
				camera = GUICamera.instance.camera;
				goto default;
			case 1:
				if (cityController != null)
				{
					camera = cityController.guiCamera2;
					goto default;
				}
				continue;
			default:
			{
				collider2D = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(tapGesture.ScreenPosition), LayerMask.GetMask("UI"));
				if (!(collider2D != null))
				{
					continue;
				}
				tk2dUIItem component2 = collider2D.GetComponent<tk2dUIItem>();
				if (!(component2 != null) || !component2.enabled)
				{
					continue;
				}
				break;
			}
			}
			flag = true;
			break;
		}
		bool flag2 = false;
		if (touchStartedOnButton != null && touchStartedOnButton.gameObject.name == "MagnifyingGlass_Button")
		{
			flag = false;
			flag2 = true;
		}
		if (!allowInput || flag)
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		if (Vector3.Distance(point, transform.position) <= 0.35f || cameraPinchZoomer.isZoomedOut || flag2 || (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy) || !allowTapToMove || (cityController != null && cityController.communicator.PopUpActive) || (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive) || (CityReward.instance != null && CityReward.instance.showingRewards))
		{
			StopMove();
			return;
		}
		GameObject gameObject = null;
		string text = "Tap_Particle_01";
		Vector3 position = point;
		float maxDistance = enter + extraRayDist;
		Ray ray2 = cameraPinchZoomer.thisCamera.ScreenPointToRay(tapGesture.ScreenPosition);
		if (Physics.Raycast(ray2, out var hitInfo, maxDistance, LayerMask.GetMask("Buildings", "TapSurface")) && hitInfo.collider != null)
		{
			bool flag3 = hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("TapSurface");
			if (flag3)
			{
				TapSurface component3 = hitInfo.collider.GetComponent<TapSurface>();
				if (component3 != null)
				{
					switch (component3.surfaceType)
					{
					case TapSurfaceType.Beach:
						text = "Tap_Particle_Beach";
						break;
					case TapSurfaceType.Water:
						text = "Tap_Particle_Water";
						break;
					case TapSurfaceType.City:
						text = "Tap_Particle_01";
						break;
					}
				}
			}
			gameObject = ObjectPool.GetObjectFromResource("Prefabs/City/" + text);
			if (!flag3)
			{
				position = hitInfo.point;
				Vector3 forward = ((!(hitInfo.normal == Vector3.back)) ? Vector3.forward : Vector3.up);
				gameObject.transform.rotation = Quaternion.LookRotation(forward, hitInfo.normal);
			}
			else
			{
				gameObject.transform.rotation = Quaternion.identity;
			}
		}
		if (gameObject == null)
		{
			gameObject = ObjectPool.GetObjectFromResource("Prefabs/City/" + text);
			gameObject.transform.rotation = Quaternion.identity;
		}
		gameObject.transform.position = position;
		string text2 = string.Empty;
		if (hitInfo.collider != null)
		{
			TapSurface component4 = hitInfo.collider.GetComponent<TapSurface>();
			if (component4 != null)
			{
				switch (component4.surfaceType)
				{
				case TapSurfaceType.Beach:
					text2 = tapSFXSand[UnityEngine.Random.Range(0, tapSFXSand.Length)];
					break;
				case TapSurfaceType.Water:
					text2 = tapSFXWater[UnityEngine.Random.Range(0, tapSFXWater.Length)];
					break;
				case TapSurfaceType.City:
					text2 = tapSFXRegular[UnityEngine.Random.Range(0, tapSFXRegular.Length)];
					break;
				}
			}
			else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Buildings"))
			{
				text2 = tapSFXRegular[UnityEngine.Random.Range(0, tapSFXRegular.Length)];
			}
		}
		else
		{
			text2 = tapSFXRegular[UnityEngine.Random.Range(0, tapSFXRegular.Length)];
		}
		if (!string.IsNullOrEmpty(text2))
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + text2, GFAudioManager.SFX_GROUP, string.Empty);
		}
		MoveToTappedPoint(point);
	}

	private void HandleZoomStarted()
	{
		isZooming = true;
	}

	private void HandleZoomFinished()
	{
		isZooming = false;
	}

	public void PlayCityLocationVO(CityLocation curDistrict)
	{
		if (!(GFAudioManager.instance == null) && !cityController.IsCityVOPlaying() && UnityEngine.Random.value <= GameManager.instance.gameSettings.enterCityVOProbability && (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive) && !flashSaleController.FlashSaleActive && !flashSaleController.IntroSequenceActive && !DayNightCard.instance.isActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !cityController.communicator.PopUpActive && !GFAudioManager.instance.IsGroupMuted(GFAudioManager.VOICEOVER_GROUP))
		{
			CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(characterType);
			string text = string.Empty;
			switch (curDistrict)
			{
			case CityLocation.JumpCity:
				text = characterVOSet.enterJumpCityClipSet.GetRandom();
				break;
			case CityLocation.Gotham:
				text = characterVOSet.enterGothamCityClipSet.GetRandom();
				break;
			case CityLocation.Metropolis:
				text = characterVOSet.enterMetropolisClipSet.GetRandom();
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				AudioSource voSource = GFAudioManager.instance.PlayClipFromResources(text, GFAudioManager.VOICEOVER_GROUP, string.Empty);
				cityController.RegisterCityVO(voSource);
			}
		}
	}

	private void HandleDayNightChanged()
	{
		if (!(GFAudioManager.instance == null) && UnityEngine.Random.value <= 0.5f && !cityController.IsCityVOPlaying() && (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive) && !flashSaleController.FlashSaleActive && !flashSaleController.IntroSequenceActive && (DialogueTree.instance == null || !DialogueTree.instance.isConverstationPlaying) && !cityController.communicator.PopUpActive)
		{
			bool isDayTime = cityController.communicator.isDayTime;
			string empty = string.Empty;
			CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(characterType);
			empty = ((!isDayTime) ? characterVOSet.newNightClipSet.GetRandom() : characterVOSet.newDayClipSet.GetRandom());
			AudioSource voSource = GFAudioManager.instance.PlayClipFromResources(empty, GFAudioManager.VOICEOVER_GROUP, string.Empty);
			cityController.RegisterCityVO(voSource);
		}
	}

	private void Update()
	{
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			return;
		}
		if (Time.time - lastHitCheck > hitCheckDT)
		{
			lastHitCheck = Time.time;
			int num = Physics.OverlapBoxNonAlloc(transform.position + new Vector3(0f, 0.2f, 0f), new Vector3(0.1f, 0.2f, 0.1f), hitResults, Quaternion.identity, LayerMask.GetMask("Pickup"), QueryTriggerInteraction.Collide);
			for (int i = 0; i < num; i++)
			{
				CityPickup component = hitResults[i].GetComponent<CityPickup>();
				if (component != null)
				{
					component.Collected();
				}
			}
		}
		idleSFXTimer -= Time.deltaTime;
		if (idleSFXTimer <= 0f)
		{
			idleSFXTimer = delayOnIdleVO;
			if (cityController != null && !cityController.IsCityVOPlaying() && (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive) && !flashSaleController.FlashSaleActive && !flashSaleController.IntroSequenceActive && !DayNightCard.instance.isActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !cityController.communicator.PopUpActive && !GFAudioManager.instance.IsGroupMuted(GFAudioManager.VOICEOVER_GROUP) && GameManager.instance.characterVODatabase.GetCharacterVOSet(characterType).idleClipSet.Length > 0)
			{
				AudioSource voSource = GFAudioManager.instance.PlayClipFromResources(GameManager.instance.characterVODatabase.GetCharacterVOSet(characterType).idleClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
				cityController.RegisterCityVO(voSource);
			}
		}
		if (interactiveDestinationQueued && !IsNodeOnPath(interactiveDestinationNode))
		{
			interactiveDestinationQueued = false;
			interactiveDestinationCallback = null;
		}
	}

	private void LateUpdate()
	{
		if (pathingToTemporaryWaypoint)
		{
			CheckTempWaypointStatus();
		}
	}

	private void CheckTempWaypointStatus()
	{
		PathNode pathNode = waypointGroup[0];
		if (!(pathNode == null) && !IsNodeOnPath(pathNode))
		{
			for (int i = 0; i < pathNode.connectedNodes.Count; i++)
			{
				pathNode.connectedNodes[i].RemoveConnectedNode(pathNode);
			}
			if (allNodes.Contains(pathNode))
			{
				allNodes.Remove(pathNode);
			}
			UnityEngine.Object.Destroy(pathNode.gameObject);
			waypointGroup.Remove(pathNode);
			pathingToTemporaryWaypoint = false;
		}
	}

	private void HandleSwipeOccurred(SwipeResult result)
	{
		if (isZooming || pinchZoomer.isZoomedOut || touchStartedOnButton != null || !allowInput || (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy) || (cityController != null && (cityController.communicator.PopUpActive || cityController.pauseScreen.gameObject.activeSelf)) || (CityReward.instance != null && CityReward.instance.showingRewards))
		{
			return;
		}
		if (cityController != null && cityController.mySquad != null)
		{
			cityController.mySquad.HideNow();
		}
		string[] swipeSFX = GameManager.instance.gameSettings.swipeSFX;
		if (swipeSFX != null && swipeSFX.Length > 0)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + swipeSFX[UnityEngine.Random.Range(0, swipeSFX.Length)], GFAudioManager.SFX_GROUP, string.Empty);
		}
		float num = 0f;
		switch (moveSwipeType)
		{
		case MoveSwipeType.Screen:
			num = Mathf.Atan2(result.end2D.y - result.start2D.y, result.end2D.x - result.start2D.x);
			Move(num * 57.29578f, fromSwipe: true);
			return;
		case MoveSwipeType.World:
			num = Mathf.Atan2(result.worldEnd.z - result.worldStart.z, result.worldEnd.x - result.worldStart.x);
			Move(num * 57.29578f, fromSwipe: true);
			return;
		}
		num = Mathf.Atan2(result.end2D.y - result.start2D.y, result.end2D.x - result.start2D.x);
		if (!Move(num * 57.29578f, fromSwipe: true))
		{
			num = Mathf.Atan2(result.worldEnd.z - result.worldStart.z, result.worldEnd.x - result.worldStart.x);
			Move(num * 57.29578f, fromSwipe: true);
		}
	}

	private bool Move(float angle, bool fromSwipe = false)
	{
		PathConnection pathConnection = FindConnectedNode(_currentNode, angle);
		if (pathConnection.end == null && !fromSwipe)
		{
			return false;
		}
		if (moveTween == null)
		{
			if (!(pathConnection.end == null))
			{
				_curPath.Clear();
				_curPath.Add(pathConnection);
				CheckPath();
				return true;
			}
			PathNode pathNode = null;
			PathNode pathNode2 = null;
			PathConnection item = null;
			int count = _currentNode.connectedNodes.Count;
			for (int i = 0; i < count; i++)
			{
				pathNode = _currentNode.connectedNodes[i];
				float num = turningForgiveness;
				if (_currentNode.overrideTurningForgiveness)
				{
					PathNode possibleNodeTemp = pathNode;
					TurningForgivenessOverrideData turningForgivenessOverrideData = _currentNode.turningForgivenessOverrideData.Find((TurningForgivenessOverrideData x) => x.pathNode == possibleNodeTemp);
					if (turningForgivenessOverrideData != null)
					{
						num = turningForgivenessOverrideData.turningForgiveness;
					}
				}
				if (_currentNode.GetDistanceToConnectedNode(pathNode) <= num)
				{
					PathConnection pathConnection2 = FindConnectedNode(pathNode, angle);
					if (pathConnection2.end != null && (pathNode2 == null || _currentNode.GetDistanceToConnectedNode(pathNode) < _currentNode.GetDistanceToConnectedNode(pathNode2)))
					{
						pathNode2 = pathNode;
						item = pathConnection2;
					}
				}
			}
			if (pathNode2 != null)
			{
				_curPath.Clear();
				_curPath.Add(new PathConnection(pathNode2));
				_curPath.Add(item);
				CheckPath();
				return true;
			}
		}
		else
		{
			PathConnection connection = FindConnectedNode(_prevNode, angle);
			if (connection.end != null && connection.end != _currentNode)
			{
				float num2 = turningForgivenessMoving;
				if (_prevNode.overrideTurningForgiveness)
				{
					TurningForgivenessOverrideData turningForgivenessOverrideData2 = _prevNode.turningForgivenessOverrideData.Find((TurningForgivenessOverrideData x) => x.pathNode == connection.end);
					if (turningForgivenessOverrideData2 != null)
					{
						num2 = turningForgivenessOverrideData2.turningForgiveness;
					}
				}
				if (Vector3.Distance(transform.position, _prevNode.transform.position) <= num2)
				{
					moveTween.Kill();
					moveTween = null;
					_curPath.Clear();
					_curPath.Add(new PathConnection(_prevNode));
					_curPath.Add(connection);
					CheckPath();
					return true;
				}
			}
			if (!(pathConnection.end == null))
			{
				if (pathConnection.end == _prevNode)
				{
					moveTween.Kill();
					moveTween = null;
					_curPath.Clear();
					_curPath.Add(pathConnection);
					CheckPath();
					return true;
				}
				if ((!useMinInputDistance || Vector3.Distance(transform.position, _currentNode.transform.position) <= minInputDistance) && !_curPath.Contains(pathConnection))
				{
					float num3 = Mathf.Atan2(_currentNode.transform.position.z - transform.position.z, _currentNode.transform.position.x - transform.position.x) * 57.29578f;
					float num4 = Mathf.Atan2(pathConnection.end.transform.position.z - _currentNode.transform.position.z, pathConnection.end.transform.position.x - _currentNode.transform.position.x) * 57.29578f;
					float num5 = Mathf.Abs(num4 - num3);
					if (fromSwipe)
					{
						pathingToTarget = false;
						_curPath.Clear();
					}
					if (_currentNode != pathConnection.end && num5 > angleThreshold / 2f)
					{
						_curPath.Add(pathConnection);
					}
					return true;
				}
			}
		}
		return false;
	}

	private void OnMoveUpdate()
	{
		float num = moveTween.ElapsedPercentage();
		if (!float.IsNaN(num))
		{
			cameraFollow.targetFOV = Mathf.Lerp(prevTargetCameraFOV, nextTargetCameraFOV, num);
		}
		if (num > 0.98f || float.IsNaN(num))
		{
			if (moveTween2 != null)
			{
				moveTween2.Kill();
			}
			moveTween2 = transform.DOMove(_currentNode.transform.position, moveSpeed).SetSpeedBased().SetEase(Ease.Linear)
				.OnComplete(() =>
				{
					moveTween2 = null;
				});
			moveTween.Kill();
			OnMoveComplete();
		}
	}

	private IEnumerator WaitToEnableCameraApproach()
	{
		yield return new WaitForEndOfFrame();
		pinchZoomer.cameraFollow.approach = true;
	}

	private void LoadSceneOnNode(string sceneName)
	{
		if (!CityController.instance.exitingCityScene)
		{
			allowInput = false;
			SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.gameObject.transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
			CityController.instance.communicator.SaveDayAndTime();
			CityController.instance.communicator.ResolveActiveNewsFlash();
			Player.instance.IncreaseCurrentTime(20);
			CityController.instance.ExitCityScene();
			LoadingScreenCover.LoadScene(sceneName);
		}
	}

	private void OnMoveComplete()
	{
		moveTween = null;
		if (!CoreGameTutorial.ignoreNotifyReachedNode && CoreGameTutorial.instance != null)
		{
			CoreGameTutorial.instance.NotifyReachedNode(_currentNode);
		}
		PathNode currentNode = _currentNode;
		prevTargetCameraFOV = ((!_currentNode.adjustCameraFOV) ? cameraFollow.targetFOV : _currentNode.targetCameraFOV);
		cameraFollow.targetFOV = prevTargetCameraFOV;
		if (_currentNode.setTrigger)
		{
			Player.instance.SetTrigger(_currentNode.triggerName);
		}
		if (!string.IsNullOrEmpty(_currentNode.soundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(_currentNode.soundPath, _currentNode.soundGroup, string.Empty);
		}
		if (_currentNode.triggersConvo != null && (flashSaleController == null || !flashSaleController.FlashSaleActive))
		{
			StopMove();
			animator.Play("idle");
			CityController.instance.HandleShowDialogue(_currentNode.triggersConvo, showExitButton: false, null, null, trackCompletion: true);
			return;
		}
		if (_currentNode.teleportToNode)
		{
			EventMessenger.Instance.Raise(new TeleportEvent(_currentNode.teleportNode, stopMovement: false));
		}
		else if (_currentNode.loadScene && !string.IsNullOrEmpty(_currentNode.loadSceneName))
		{
			CityController.startFromNodeNamed = _currentNode.startAtNodeNamed;
			CityController.startFacingRight = _currentNode.startFacingRight;
			LoadSceneOnNode(_currentNode.loadSceneName);
		}
		if (_currentNode.forceStop && (!pathingToTarget || _curPath.Count == 0))
		{
			bool flag = false;
			if (_currentNode.uiItem != null)
			{
				CityNPC component = _currentNode.uiItem.gameObject.GetComponent<CityNPC>();
				if (component != null)
				{
					if (component.dayOnly && !cityController.communicator.isDayTime)
					{
						flag = true;
					}
					if (component.nightOnly && cityController.communicator.isDayTime)
					{
						flag = true;
					}
				}
			}
			if (flashSaleController != null && flashSaleController.FlashSaleActive)
			{
				flag = true;
			}
			if (!flag)
			{
				_curPath.Clear();
				if (moveTween2 != null)
				{
					moveTween2.Kill();
				}
				animator.Play("idle");
				SaveUtility.SetString("PlayerPosition", transform.position.ToString(), save: false);
				SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
				if (_currentNode.uiItem != null)
				{
					_currentNode.uiItem.enabled = true;
				}
			}
			else if (!CheckPath())
			{
				Move(Mathf.Atan2(_currentNode.transform.position.z - _prevNode.transform.position.z, _currentNode.transform.position.x - _prevNode.transform.position.x) * 57.29578f);
			}
		}
		else if (!CheckPath())
		{
			bool flag2 = Move(Mathf.Atan2(_currentNode.transform.position.z - _prevNode.transform.position.z, _currentNode.transform.position.x - _prevNode.transform.position.x) * 57.29578f);
			if (!flag2)
			{
				bool flag3 = false;
				int count = _currentNode.connectedNodes.Count;
				if (count == 2)
				{
					for (int i = 0; i < count; i++)
					{
						PathNode pathNode = _currentNode.connectedNodes[i];
						if (pathNode != _prevNode)
						{
							flag3 = true;
							flag2 = Move(_currentNode.angles[i]);
							break;
						}
					}
				}
				if (!flag3 || !flag2)
				{
					animator.Play("idle");
				}
			}
		}
		if (interactiveDestinationQueued && currentNode == interactiveDestinationNode)
		{
			interactiveDestinationQueued = false;
			if (interactiveDestinationCallback != null)
			{
				interactiveDestinationCallback();
			}
			interactiveDestinationCallback = null;
		}
	}

	public void StopMove(bool animate = true, bool savePosition = true)
	{
		if (moveTween == null)
		{
			return;
		}
		moveTween.Kill();
		moveTween = null;
		if (moveTween2 != null)
		{
			moveTween2.Kill();
		}
		if (animate)
		{
			animator.Play("idle");
		}
		PathNode pathNode = new GameObject("StopNode").AddComponent<PathNode>();
		pathNode.transform.position = transform.position;
		pathNode.isTemporary = true;
		pathNode.distToSource = float.PositiveInfinity;
		pathNode.visited = false;
		int num = _prevNode.connectedNodes.IndexOf(_currentNode);
		int num2 = _currentNode.connectedNodes.IndexOf(_prevNode);
		if (num == -1 || num2 == -1 || num2 > _currentNode.angles.Count - 1 || num > _prevNode.angles.Count - 1)
		{
			return;
		}
		pathNode.AddConnectedNode(_currentNode, _prevNode.angles[num], Vector3.Distance(_currentNode.transform.position, pathNode.transform.position));
		pathNode.AddConnectedNode(_prevNode, _currentNode.angles[num2], Vector3.Distance(_prevNode.transform.position, pathNode.transform.position));
		_currentNode.AddConnectedNode(pathNode, _currentNode.angles[_currentNode.connectedNodes.IndexOf(_prevNode)], Vector3.Distance(pathNode.transform.position, _currentNode.transform.position));
		_prevNode.AddConnectedNode(pathNode, _prevNode.angles[_prevNode.connectedNodes.IndexOf(_currentNode)], Vector3.Distance(pathNode.transform.position, _prevNode.transform.position));
		if (_currentNode.isTemporary)
		{
			pathNode.SetBackupNode(_currentNode.connectedNodes.Find((PathNode x) => x != _prevNode));
		}
		allNodes.Add(pathNode);
		_currentNode = pathNode;
		_curPath.Clear();
		prevTargetCameraFOV = (nextTargetCameraFOV = cameraFollow.targetFOV);
		if (savePosition)
		{
			SaveUtility.SetString("PlayerPosition", transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
		}
	}

	public void PlayIdleAnimation()
	{
		animator.Play("idle");
	}

	private bool CheckPath()
	{
		if (moveTween != null)
		{
			return false;
		}
		if (_curPath.Count > 0)
		{
			PathConnection pathConnection = _curPath.Shift();
			if (_currentNode.isTemporary)
			{
				if (waypointGroup.Contains(_currentNode) && pathingToTemporaryWaypoint)
				{
					pathingToTemporaryWaypoint = false;
				}
				if (_currentNode.connectedNodes.Count >= 2)
				{
					int num = -1;
					num = ((_currentNode.connectedNodes[0] == pathConnection.end) ? 1 : 0);
					PathNode pathNode = _currentNode.connectedNodes[num];
					if (pathNode.isTemporary)
					{
						float angle = _currentNode.angles[num];
						int indexOfConnectedNodeAtAngle = pathNode.GetIndexOfConnectedNodeAtAngle(angle, 0.1f);
						if (indexOfConnectedNodeAtAngle != -1)
						{
							_prevNode = pathNode.connectedNodes[indexOfConnectedNodeAtAngle];
						}
					}
					else
					{
						_prevNode = pathNode;
					}
				}
				else if (_currentNode.connectedNodes.Count > 0 && _currentNode.connectedNodes[0] == pathConnection.end)
				{
					float item = Mathf.Atan2(transform.position.z - pathConnection.end.transform.position.z, transform.position.x - pathConnection.end.transform.position.x) * 57.29578f;
					int num2 = pathConnection.end.angles.IndexOf(item);
					if (num2 != -1)
					{
						_prevNode = pathConnection.end.connectedNodes[num2];
					}
					else
					{
						Debug.LogFormat("***Recovered from error but could not find a replacement prevNode. CharacterPos: {0}  Next Connection End: {1} (Relevant to above Temporary Node Custom Error)", transform.position, pathConnection.end);
					}
				}
				else
				{
					Debug.Log("***Recovered from error but current node, " + _currentNode.gameObject.name + ", is not connected to ANY nodes.", _currentNode.gameObject);
				}
				for (int i = 0; i < _currentNode.connectedNodes.Count; i++)
				{
					_currentNode.connectedNodes[i].RemoveConnectedNode(_currentNode);
				}
				allNodes.Remove(_currentNode);
				if (_currentNode.gameObject != null && !waypointGroup.Contains(_currentNode))
				{
					UnityEngine.Object.Destroy(_currentNode.gameObject);
				}
			}
			else
			{
				if (_currentNode.forceStop && _currentNode.uiItem != null)
				{
					_currentNode.uiItem.enabled = false;
				}
				_prevNode = _currentNode;
			}
			_currentNode = pathConnection.end;
			prevTargetCameraFOV = cameraFollow.targetFOV;
			nextTargetCameraFOV = ((!_currentNode.adjustCameraFOV) ? cameraFollow.targetFOV : _currentNode.targetCameraFOV);
			if (_currentNode.triggersMusic != null && (flashSaleController == null || !flashSaleController.FlashSaleActive) && cityController != null)
			{
				AudioClip clip = _currentNode.triggersMusic;
				if (!cityController.communicator.isDayTime && _currentNode.triggersMusicNighttime != null)
				{
					clip = _currentNode.triggersMusicNighttime;
				}
				cityController.PlayMusic(clip, crossFade: true);
			}
			float num3 = Mathf.Abs(pathConnection.end.transform.position.x - transform.position.x);
			float num4 = Mathf.Abs(pathConnection.end.transform.position.z - transform.position.z);
			float num5 = 0.1f;
			if (pathConnection.end.transform.position.x < transform.position.x && num3 > num5)
			{
				animator.Play("run_side");
				if (animator.isFlipped)
				{
					animator.isFlipped = false;
				}
			}
			else if (pathConnection.end.transform.position.x > transform.position.x && num3 > num5)
			{
				animator.Play("run_side");
				if (!animator.isFlipped)
				{
					animator.isFlipped = true;
				}
			}
			else if (pathConnection.end.transform.position.z > transform.position.z && num4 > num5)
			{
				animator.Play("run_back");
				if (animator.isFlipped)
				{
					animator.isFlipped = false;
				}
			}
			else if (pathConnection.end.transform.position.z < transform.position.z && num4 > num5)
			{
				animator.Play("run_front");
				if (animator.isFlipped)
				{
					animator.isFlipped = false;
				}
			}
			if (pathConnection.curveWaypoints.Length > 0)
			{
				moveTween = transform.DOPath(pathConnection.curveWaypoints, moveSpeed, PathType.CatmullRom, PathMode.Ignore, 5).SetLookAt(0.01f).SetSpeedBased()
					.SetEase(Ease.Linear)
					.OnComplete(OnMoveComplete);
			}
			else
			{
				moveTween = transform.DOMove(_currentNode.transform.position, moveSpeed).SetSpeedBased().SetEase(Ease.Linear)
					.OnUpdate(OnMoveUpdate);
			}
			return true;
		}
		return false;
	}

	private PathConnection FindConnectedNode(PathNode startNodeCheck, float angle)
	{
		if (startNodeCheck == null)
		{
			return new PathConnection(null);
		}
		PathNode pathNode = null;
		float num = 0f;
		int count = startNodeCheck.connectedNodes.Count;
		for (int i = 0; i < count && i <= startNodeCheck.angles.Count - 1; i++)
		{
			float num2 = startNodeCheck.angles[i] - angle;
			if (num2 >= 180f)
			{
				num2 = 360f - num2;
			}
			else if (num2 <= -180f)
			{
				num2 += 360f;
			}
			else if (num2 < 0f)
			{
				num2 = 0f - num2;
			}
			if (num2 <= angleThreshold / 2f && (pathNode == null || num2 < num) && startNodeCheck.connectedNodes[i].isActive)
			{
				pathNode = startNodeCheck.connectedNodes[i];
				num = num2;
			}
		}
		return new PathConnection(pathNode);
	}

	private void MoveToTappedPoint(Vector3 tapPoint)
	{
		StopMove(animate: false, savePosition: false);
		PathNode nearestPathNodeToPosition = GetNearestPathNodeToPosition(tapPoint, ignoreDisabledNodes: true);
		List<KeyValuePair<Vector3, PathNode>> list = new List<KeyValuePair<Vector3, PathNode>>();
		for (int i = 0; i < nearestPathNodeToPosition.connectedNodes.Count; i++)
		{
			PathNode pathNode = nearestPathNodeToPosition.connectedNodes[i];
			if (!pathNode.isTemporary && pathNode.isActive)
			{
				Vector3 position = nearestPathNodeToPosition.transform.position;
				Vector3 position2 = pathNode.transform.position;
				float num = Vector3.Distance(position, position2);
				float num2 = Vector3.Distance(tapPoint, position2);
				float num3 = Vector3.Distance(tapPoint, position);
				Vector3 key = Vector3.zero;
				bool flag = false;
				float num4 = Mathf.Acos((Mathf.Pow(num, 2f) + Mathf.Pow(num3, 2f) - Mathf.Pow(num2, 2f)) / (2f * num * num3)) * 57.29578f;
				float num5 = Mathf.Acos((Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f) - Mathf.Pow(num3, 2f)) / (2f * num * num2)) * 57.29578f;
				if (num4 > 90f)
				{
					key = position;
					flag = true;
				}
				if (num5 > 90f)
				{
					key = position2;
					flag = true;
				}
				if (!flag)
				{
					float num6 = (num + num2 + num3) / 2f;
					float f = 2f * Mathf.Sqrt(num6 * (num6 - num) * (num6 - num2) * (num6 - num3)) / num;
					float num7 = Mathf.Sqrt(Mathf.Pow(num3, 2f) - Mathf.Pow(f, 2f));
					key = num7 * (position2 - position).normalized + position;
				}
				list.Add(new KeyValuePair<Vector3, PathNode>(key, pathNode));
			}
		}
		float num8 = float.PositiveInfinity;
		Vector3 vector = Vector3.zero;
		PathNode pathNode2 = null;
		foreach (KeyValuePair<Vector3, PathNode> item in list)
		{
			Vector3 key2 = item.Key;
			float num9 = Vector3.Distance(tapPoint, key2);
			if (num9 < num8)
			{
				num8 = num9;
				vector = key2;
				pathNode2 = item.Value;
			}
		}
		if (pathNode2 == null)
		{
			StopMove();
			return;
		}
		PathNode pathNode3 = null;
		if (!V3Equal(nearestPathNodeToPosition.transform.position, vector))
		{
			waypointGroup.Insert(0, new GameObject("TemporaryWaypointNode").AddComponent<PathNode>());
			Debug.Log("Distance from nearest point: " + Vector3.Distance(nearestPathNodeToPosition.transform.position, vector));
			pathNode3 = waypointGroup[0];
			pathNode3.transform.position = vector;
			pathNode3.isTemporary = true;
			pathNode3.distToSource = float.PositiveInfinity;
			pathNode3.visited = false;
			pathNode3.SetForceStop(shouldForceStop: true);
			if (_currentNode.isTemporary && _currentNode.IsNodeConnectedOrBackup(nearestPathNodeToPosition) && _currentNode.IsNodeConnectedOrBackup(pathNode2))
			{
				if (Vector3.Distance(_currentNode.transform.position, nearestPathNodeToPosition.transform.position) < Vector3.Distance(pathNode3.transform.position, nearestPathNodeToPosition.transform.position))
				{
					pathNode3.AddConnectedNode(_currentNode);
					pathNode3.AddConnectedNode(pathNode2);
					_currentNode.AddConnectedNode(pathNode3);
					pathNode2.AddConnectedNode(pathNode3);
					pathNode3.SetBackupNode(nearestPathNodeToPosition);
				}
				else if (Vector3.Distance(_currentNode.transform.position, pathNode2.transform.position) < Vector3.Distance(pathNode3.transform.position, pathNode2.transform.position))
				{
					pathNode3.AddConnectedNode(_currentNode);
					pathNode3.AddConnectedNode(nearestPathNodeToPosition);
					_currentNode.AddConnectedNode(pathNode3);
					nearestPathNodeToPosition.AddConnectedNode(pathNode3);
					pathNode3.SetBackupNode(pathNode2);
				}
			}
			else
			{
				pathNode3.AddConnectedNode(nearestPathNodeToPosition);
				pathNode3.AddConnectedNode(pathNode2);
				nearestPathNodeToPosition.AddConnectedNode(pathNode3);
				pathNode2.AddConnectedNode(pathNode3);
			}
			allNodes.Add(pathNode3);
			pathingToTemporaryWaypoint = true;
			for (int num10 = waypointGroup.Count - 1; num10 > 0; num10--)
			{
				PathNode pathNode4 = waypointGroup[num10];
				if (pathNode4 != null && pathNode4 != _currentNode)
				{
					foreach (PathNode connectedNode in pathNode4.connectedNodes)
					{
						connectedNode.RemoveConnectedNode(pathNode4);
					}
					if (allNodes.Contains(pathNode4))
					{
						allNodes.Remove(pathNode4);
					}
					UnityEngine.Object.Destroy(pathNode4.gameObject);
				}
				waypointGroup.RemoveAt(num10);
			}
		}
		else
		{
			pathNode3 = nearestPathNodeToPosition;
		}
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/City/Tap_WaypointIndicator");
		objectFromResource.GetComponent<TapWaypointIndicator>().Init(this, pathNode3);
		MoveToNode(pathNode3);
	}

	public bool MoveToNode(PathNode targetNode)
	{
		StopMove(animate: false, savePosition: false);
		PathNode currentNode = _currentNode;
		if (currentNode == null)
		{
			return false;
		}
		currentNode.distToSource = 0f;
		bool flag = false;
		bool flag2 = false;
		int count = allNodes.Count;
		while (!flag && !flag2)
		{
			PathNode pathNode = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < count; i++)
			{
				PathNode pathNode2 = allNodes[i];
				float distToSource = pathNode2.distToSource;
				if (distToSource < num && !pathNode2.visited && pathNode2.isActive)
				{
					num = distToSource;
					pathNode = pathNode2;
					if (num == 0f)
					{
						break;
					}
				}
			}
			if (pathNode == null)
			{
				flag2 = true;
				break;
			}
			pathNode.visited = true;
			for (int j = 0; j < pathNode.connectedNodes.Count; j++)
			{
				PathNode pathNode3 = pathNode.connectedNodes[j];
				float num2 = pathNode.distToSource + pathNode.distances[j];
				if (num2 < pathNode3.distToSource && pathNode3.isActive)
				{
					pathNode3.distToSource = num2;
					if (pathNode3 == targetNode)
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (flag2)
		{
			animator.Play("idle");
			ResetAllNodePathingFields();
			return false;
		}
		for (int k = 0; k < count; k++)
		{
			allNodes[k].visited = false;
		}
		bool flag3 = false;
		List<PathNode> list = new List<PathNode>();
		list.Add(targetNode);
		PathNode pathNode4 = targetNode;
		pathNode4.visited = true;
		while (!flag3)
		{
			PathNode pathNode5 = null;
			float num3 = float.PositiveInfinity;
			for (int l = 0; l < pathNode4.connectedNodes.Count; l++)
			{
				PathNode pathNode6 = pathNode4.connectedNodes[l];
				float distToSource2 = pathNode6.distToSource;
				if (distToSource2 < num3 && !pathNode6.visited)
				{
					num3 = distToSource2;
					pathNode5 = pathNode6;
				}
			}
			if (pathNode5 == null)
			{
				break;
			}
			pathNode5.visited = true;
			list.Add(pathNode5);
			if (pathNode5 == currentNode)
			{
				if (pathNode5.isTemporary)
				{
					list.Remove(pathNode5);
				}
				flag3 = true;
			}
			pathNode4 = pathNode5;
		}
		int count2 = list.Count;
		if (count2 == 0)
		{
			ResetAllNodePathingFields();
			return false;
		}
		list.Reverse();
		_curPath.Clear();
		for (int m = 0; m < count2; m++)
		{
			if (list[m] != _currentNode)
			{
				_curPath.Add(new PathConnection(list[m]));
			}
		}
		ResetAllNodePathingFields();
		pathingToTarget = true;
		CheckPath();
		return true;
	}

	public bool MoveToNode2(PathNode targetNode)
	{
		StopMove();
		PathNode currentNode = _currentNode;
		if (currentNode == null)
		{
			return false;
		}
		currentNode.distToSource = 0f;
		bool flag = false;
		bool flag2 = false;
		int count = allNodes.Count;
		List<PathNode> list = new List<PathNode>();
		list.Add(currentNode);
		while (!flag && !flag2)
		{
			PathNode pathNode = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < list.Count; i++)
			{
				PathNode pathNode2 = list[i];
				float distToSource = pathNode2.distToSource;
				if (distToSource < num && !pathNode2.visited && pathNode2.isActive)
				{
					num = distToSource;
					pathNode = pathNode2;
				}
			}
			if (pathNode == null)
			{
				flag2 = true;
				break;
			}
			pathNode.visited = true;
			for (int j = 0; j < pathNode.connectedNodes.Count; j++)
			{
				PathNode pathNode3 = pathNode.connectedNodes[j];
				float num2 = pathNode.distToSource + pathNode.distances[j];
				list.Add(pathNode3);
				if (num2 < pathNode3.distToSource)
				{
					pathNode3.distToSource = num2;
				}
				if (pathNode3 == targetNode)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag2)
		{
			ResetAllNodePathingFields();
			return false;
		}
		bool flag3 = false;
		List<PathNode> list2 = new List<PathNode>();
		list2.Add(targetNode);
		PathNode pathNode4 = targetNode;
		while (!flag3)
		{
			PathNode pathNode5 = null;
			float num3 = float.PositiveInfinity;
			for (int k = 0; k < pathNode4.connectedNodes.Count; k++)
			{
				PathNode pathNode6 = pathNode4.connectedNodes[k];
				float distToSource2 = pathNode6.distToSource;
				if (distToSource2 < num3)
				{
					num3 = distToSource2;
					pathNode5 = pathNode6;
				}
			}
			if (pathNode5 == null)
			{
				break;
			}
			list2.Add(pathNode5);
			if (pathNode5 == currentNode)
			{
				if (pathNode5.isTemporary)
				{
					list2.Remove(pathNode5);
				}
				flag3 = true;
			}
			pathNode4 = pathNode5;
		}
		int count2 = list2.Count;
		if (count2 == 0)
		{
			ResetAllNodePathingFields();
			return false;
		}
		list2.Reverse();
		_curPath.Clear();
		for (int l = 0; l < count2; l++)
		{
			if (list2[l] != _currentNode)
			{
				_curPath.Add(new PathConnection(list2[l]));
			}
		}
		ResetAllNodePathingFields();
		pathingToTarget = true;
		CheckPath();
		return true;
	}

	public bool MoveToInteractiveNode(PathNode targetNode, Action onReachedDestination)
	{
		if (targetNode == null)
		{
			Debug.LogError("MoveToInteractiveNode was given a NULL target node!");
			return false;
		}
		interactiveDestinationQueued = true;
		interactiveDestinationCallback = onReachedDestination;
		interactiveDestinationNode = targetNode;
		return MoveToNode(targetNode);
	}

	public bool CanReachNode(PathNode targetNode, bool allowInactiveNodes)
	{
		PathNode currentNode = _currentNode;
		if (currentNode == null)
		{
			return false;
		}
		bool flag = false;
		Queue<PathNode> queue = new Queue<PathNode>();
		queue.Enqueue(currentNode);
		while (!flag)
		{
			PathNode pathNode = null;
			if (queue.Count > 0)
			{
				pathNode = queue.Dequeue();
			}
			if (pathNode == null)
			{
				break;
			}
			pathNode.visited = true;
			int count = pathNode.connectedNodes.Count;
			for (int i = 0; i < count; i++)
			{
				PathNode pathNode2 = pathNode.connectedNodes[i];
				if (!pathNode2.visited && (pathNode2.isActive || allowInactiveNodes))
				{
					if (pathNode2 == targetNode)
					{
						flag = true;
						break;
					}
					queue.Enqueue(pathNode2);
				}
			}
		}
		int count2 = allNodes.Count;
		for (int j = 0; j < count2; j++)
		{
			allNodes[j].visited = false;
		}
		return flag;
	}

	public List<PathNode> GetReachableNodes()
	{
		PathNode currentNode = _currentNode;
		if (currentNode == null)
		{
			return null;
		}
		currentNode.distToSource = 0f;
		bool flag = false;
		int count = allNodes.Count;
		List<PathNode> list = new List<PathNode>();
		while (!flag)
		{
			PathNode pathNode = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < count; i++)
			{
				PathNode pathNode2 = allNodes[i];
				float distToSource = pathNode2.distToSource;
				if (distToSource < num && !pathNode2.visited && pathNode2.isActive)
				{
					num = distToSource;
					pathNode = pathNode2;
					if (num == 0f)
					{
						break;
					}
				}
			}
			if (pathNode == null)
			{
				flag = true;
				break;
			}
			pathNode.visited = true;
			list.Add(pathNode);
			for (int j = 0; j < pathNode.connectedNodes.Count; j++)
			{
				PathNode pathNode3 = pathNode.connectedNodes[j];
				float num2 = pathNode.distToSource + pathNode.distances[j];
				if (num2 < pathNode3.distToSource)
				{
					pathNode3.distToSource = num2;
				}
			}
		}
		ResetAllNodePathingFields();
		return list;
	}

	private void ResetAllNodePathingFields()
	{
		for (int i = 0; i < allNodes.Count; i++)
		{
			PathNode pathNode = allNodes[i];
			pathNode.distToSource = float.PositiveInfinity;
			pathNode.visited = false;
		}
	}

	public bool IsNodeOnPath(PathNode targetNode)
	{
		if (targetNode == null)
		{
			return false;
		}
		if (_currentNode == targetNode)
		{
			return true;
		}
		PathConnection pathConnection = _curPath.Find((PathConnection connection) => connection.end == targetNode);
		if (pathConnection != null)
		{
			return true;
		}
		return false;
	}

	public void SetPosition(PathNode targetNode)
	{
		_currentNode = (_prevNode = targetNode);
		transform.position = _currentNode.transform.position;
		if (_currentNode.forceStop && _currentNode.uiItem != null)
		{
			_currentNode.uiItem.enabled = true;
		}
		prevTargetCameraFOV = (nextTargetCameraFOV = ((!_currentNode.adjustCameraFOV) ? cameraFollow.targetFOV : _currentNode.targetCameraFOV));
		cameraFollow.targetFOV = prevTargetCameraFOV;
	}

	public void SetMoveSpeed(float newSpeed)
	{
		moveSpeed = newSpeed;
		if (moveTween != null && moveTween.IsPlaying())
		{
			moveTween.Kill();
			moveTween = transform.DOMove(_currentNode.transform.position, moveSpeed).SetSpeedBased().SetEase(Ease.Linear)
				.OnUpdate(OnMoveUpdate);
			if (moveTween2 != null)
			{
				moveTween2.Kill();
			}
		}
		animator.timeScale = baseMoveSpeed * moveSpeed;
		string clipName = animator.currentClip.name;
		animator.Play("idle");
		animator.Play(clipName);
	}

	public void CollectedFlashSaleBoost(FlashSaleBoostPickup.BoostType boostType)
	{
		float delay = 0f;
		switch (boostType)
		{
		case FlashSaleBoostPickup.BoostType.Normal:
			if (flashSaleParticle == null || !flashSaleParticle.gameObject.activeInHierarchy)
			{
				flashSaleParticle = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSale_ParticleTrail_Particle System");
				flashSaleParticle.transform.parent = transform;
				flashSaleParticle.transform.localPosition = animator.transform.localPosition;
			}
			if (!cityController.IsCityVOPlaying())
			{
				AudioSource voSource = GFAudioManager.instance.PlayClipFromResources("Audio/VO/sfx_vo_robin_flashsale5_kpr", GFAudioManager.VOICEOVER_GROUP, string.Empty);
				cityController.RegisterCityVO(voSource);
			}
			delay = GameManager.instance.flashSaleSettings.boostDuration;
			SetMoveSpeed(GameManager.instance.flashSaleSettings.moveSpeedBoost);
			break;
		case FlashSaleBoostPickup.BoostType.Negative:
			if (flashSaleParticle != null && flashSaleParticle.gameObject.activeInHierarchy)
			{
				ObjectPool.PoolObject(flashSaleParticle);
			}
			delay = GameManager.instance.flashSaleSettings.slowDownDuration;
			SetMoveSpeed(GameManager.instance.flashSaleSettings.moveSpeedSlowDown);
			break;
		}
		if (boostDurationCoroutine != null)
		{
			StopCoroutine(boostDurationCoroutine);
		}
		boostDurationCoroutine = SetMoveSpeedDelayed(GameManager.instance.flashSaleSettings.moveSpeedBase, delay);
		StartCoroutine(boostDurationCoroutine);
	}

	private IEnumerator SetMoveSpeedDelayed(float newMoveSpeed, float delay)
	{
		float timer = delay;
		while (timer > 0f)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		ObjectPool.PoolObject(flashSaleParticle);
		if (flashSaleController.FlashSaleActive || flashSaleController.IntroSequenceActive)
		{
			SetMoveSpeed(newMoveSpeed);
		}
	}

	public void FlashSaleEnded(float originalMoveSpeed)
	{
		if (boostDurationCoroutine != null)
		{
			StopCoroutine(boostDurationCoroutine);
		}
		ObjectPool.PoolObject(flashSaleParticle);
		SetMoveSpeed(originalMoveSpeed);
		StopMove();
	}

	private void ClearAnimatorsAddedToScene()
	{
		int count = animatorsAddedToScene.Count;
		for (int i = 0; i < count; i++)
		{
			UnityEngine.Object.Destroy(animatorsAddedToScene[i]);
		}
		animatorsAddedToScene.Clear();
	}

	public static void FirstTimeCheck()
	{
		if (!firstCheck)
		{
			firstCheck = true;
			characterType = CityCharacterTypeFromSkinName(GameSkinManager.instance.CurrentSkinName);
			if (CharacterTypeChanged != null)
			{
				CharacterTypeChanged(characterType);
			}
		}
	}

	public static string GetCityCharacterConvoName(string convoName)
	{
		return convoName;
	}

	private PathNode GetNearestPathNodeToPosition(Vector3 position, bool ignoreDisabledNodes = false)
	{
		PathNode result = null;
		float num = float.PositiveInfinity;
		PathNode pathNode = null;
		for (int i = 0; i < allNodes.Count; i++)
		{
			pathNode = allNodes[i];
			if (!pathNode.isTemporary && (pathNode.isActive || !ignoreDisabledNodes))
			{
				float num2 = Vector3.Distance(pathNode.transform.position, position);
				if (num2 < num)
				{
					num = num2;
					result = pathNode;
				}
			}
		}
		return result;
	}

	private static bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 0.0001f;
	}
}
