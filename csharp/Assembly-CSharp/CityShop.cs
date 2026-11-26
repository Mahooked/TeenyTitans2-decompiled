using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class CityShop : MonoBehaviour, IInteractiveBuilding
{
	[HideInInspector]
	public int shopUID;

	[HideInInspector]
	public TimeOfDay timeOfDay;

	[HideInInspector]
	public bool handleBreakingNews;

	[HideInInspector]
	public int newsFlashUID = -1;

	public PathNode entranceNode;

	private bool isDayTime;

	private bool isOpen;

	private bool isSetup;

	private bool isForceClosed;

	private ShopDefinition shopDef;

	private CityShopSign shopSign;

	private float previousMinScale = 0.4f;

	private GameObject landmarkIconTimerObj;

	private AnimatorDisabler _animatorDisabler;

	private Animator _animator;

	private AnimatorDisabler animatorDisabler
	{
		get
		{
			if (_animatorDisabler == null)
			{
				_animatorDisabler = animator.gameObject.GetComponent<AnimatorDisabler>();
			}
			return _animatorDisabler;
		}
	}

	private Animator animator
	{
		get
		{
			if (_animator == null)
			{
				_animator = transform.GetComponent<Animator>();
			}
			return _animator;
		}
	}

	public Action interactionFunction => HandleOnShopDown;

	public event Action<bool> OpenStatusUpdated;

	private void OnEnable()
	{
		GetComponent<tk2dUIItem>().OnClick += HandleOnShopDown;
		shopSign = GetComponentInChildren<CityShopSign>(includeInactive: true);
		StartCoroutine(Start());
	}

	private void OnDisable()
	{
		GetComponent<tk2dUIItem>().OnClick -= HandleOnShopDown;
		entranceNode = null;
		if (isSetup)
		{
			isSetup = false;
			if (CityController.instance != null)
			{
				CityController.instance.communicator.NewsFlashStarted -= HandleNewsFlashStarted;
				CityController.instance.communicator.NewsFlashEnded -= HandleNewsFlashEnded;
				CityController.instance.communicator.StartDayTime -= HandleStartDayTime;
				CityController.instance.communicator.StartNightTime -= HandleStartNightTime;
				CityController.instance.cityShops.Remove(this);
			}
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		SetupCityShop();
	}

	private void SetupCityShop()
	{
		if (isSetup)
		{
			return;
		}
		isSetup = true;
		Landmark component = GetComponent<Landmark>();
		if (component != null)
		{
			component.zoomedInSettings.hideAtMinScale = true;
			component.zoomedInSettings.minScale = 0.4f;
			component.UpdateZoomSettings();
			if (component.icon == null)
			{
				component.ResetLandmarkIcon();
			}
		}
		if (CityController.instance != null)
		{
			CityController.instance.communicator.NewsFlashStarted += HandleNewsFlashStarted;
			CityController.instance.communicator.NewsFlashEnded += HandleNewsFlashEnded;
			CityController.instance.communicator.StartDayTime += HandleStartDayTime;
			CityController.instance.communicator.StartNightTime += HandleStartNightTime;
			CityController.instance.cityShops.Add(this);
			isDayTime = CityController.instance.communicator.isDayTime;
		}
		shopDef = GameManager.instance.shopDatabase.GetShopDef(shopUID);
		timeOfDay = shopDef.timeOfDay;
		isOpen = false;
		CheckShouldBeOpen();
		if (entranceNode == null)
		{
			List<PathNode> list = new List<PathNode>(UnityEngine.Object.FindObjectsOfType<PathNode>());
			PathNode pathNode = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < list.Count; i++)
			{
				PathNode pathNode2 = list[i];
				float num2 = Vector3.Distance(pathNode2.transform.position, transform.position);
				if (num2 < num)
				{
					num = num2;
					pathNode = pathNode2;
				}
			}
			if (pathNode != null)
			{
				entranceNode = pathNode;
			}
		}
		CheckShouldHandleNewsFlash();
	}

	private void CheckShouldHandleNewsFlash()
	{
		if (CityController.instance == null)
		{
			return;
		}
		handleBreakingNews = false;
		for (int i = 0; i < shopDef.newsFlashes.Count; i++)
		{
			if (CityController.instance.communicator.IsActiveNewsFlash(shopDef.newsFlashes[i]))
			{
				handleBreakingNews = true;
				break;
			}
		}
	}

	private void HandleStartDayTime()
	{
		isDayTime = true;
		CheckShouldBeOpen();
	}

	private void HandleStartNightTime()
	{
		isDayTime = false;
		CheckShouldBeOpen();
	}

	private void HandleNewsFlashEnded(NewsFlash obj)
	{
		if (handleBreakingNews)
		{
			Landmark component = GetComponent<Landmark>();
			if (component != null)
			{
				component.ResetSpriteIcon();
				component.zoomedInSettings.hideAtMinScale = true;
				component.zoomedInSettings.minScale = previousMinScale;
				component.UpdateZoomSettings();
			}
			if (landmarkIconTimerObj != null)
			{
				UnityEngine.Object.Destroy(landmarkIconTimerObj);
			}
		}
		CheckShouldHandleNewsFlash();
	}

	private void HandleNewsFlashStarted(NewsFlash obj)
	{
		CheckShouldHandleNewsFlash();
		if (shopDef.opensWithNewsFlash)
		{
			CheckShouldBeOpen();
		}
		if (handleBreakingNews)
		{
			Landmark component = GetComponent<Landmark>();
			component.UpdateSpriteIcon("gui_citynotifiers_newsflash");
			component.zoomedInSettings.hideAtMinScale = false;
			previousMinScale = component.zoomedInSettings.minScale;
			component.zoomedInSettings.minScale = 0.85f;
			component.UpdateZoomSettings();
			if (obj.showTimer)
			{
				landmarkIconTimerObj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/LandmarkIconCountdownText") as GameObject);
				landmarkIconTimerObj.transform.parent = component.icon.sprite.transform;
				landmarkIconTimerObj.transform.localPosition = new Vector3(0f, -14.8f, -0.01f);
				landmarkIconTimerObj.transform.localScale = Vector3.one;
				StartCoroutine(UpdateLandmarkIconTimer(obj));
			}
		}
	}

	private IEnumerator UpdateLandmarkIconTimer(NewsFlash newsFlash)
	{
		TextMeshPro timerText = landmarkIconTimerObj.GetComponent<TextMeshPro>();
		NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(newsFlash.uid);
		Communicator communicator = CityController.instance.communicator;
		float displayTime = newsFlashData.timeActive;
		float currentTime = communicator.GetCurrentTime();
		while (landmarkIconTimerObj != null)
		{
			currentTime = communicator.GetCurrentTime();
			float timeLeft = (float)newsFlash.durationTime - (currentTime - displayTime);
			if (timeLeft < 0f)
			{
				timeLeft = 0f;
			}
			int mins = (int)timeLeft / 60;
			int sec = (int)(timeLeft % 60f);
			if (mins < 0)
			{
				mins = 0;
			}
			if (sec < 0)
			{
				sec = 0;
			}
			string minStr = ((mins >= 10) ? mins.ToString() : ("0" + mins));
			string secStr = ((sec >= 10) ? sec.ToString() : ("0" + sec));
			timerText.text = minStr + ":" + secStr;
			yield return new WaitForEndOfFrame();
		}
	}

	private void HandleFlashSaleStarted()
	{
		Landmark component = GetComponent<Landmark>();
		if (component != null)
		{
			component.icon.hideAtMinScale = false;
		}
	}

	private void HandleFlashSaleEnded()
	{
		Landmark component = GetComponent<Landmark>();
		if (component != null)
		{
			component.icon.hideAtMinScale = true;
		}
	}

	private void CheckShouldBeOpen()
	{
		if (shopDef.opensWithNewsFlash)
		{
			if (Player.instance.GetNewsFlashData(shopDef.opensWithNewsFlashUID) == null && !Player.instance.HasCompletedNewsFlash(shopDef.opensWithNewsFlashUID))
			{
				isOpen = false;
				ShowComingSoonSign();
				StartCoroutine("HideLandmarkIcon");
				if (OpenStatusUpdated != null)
				{
					OpenStatusUpdated(isOpen);
				}
				return;
			}
			isOpen = true;
		}
		int num = -1;
		bool flag = false;
		if (num > -1)
		{
			MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(num);
			if (missionCompletedData != null)
			{
				flag = true;
			}
		}
		if (!flag || isForceClosed)
		{
			isOpen = false;
			ShowClosedSign();
			StartCoroutine("HideLandmarkIcon");
			if (OpenStatusUpdated != null)
			{
				OpenStatusUpdated(isOpen);
			}
			return;
		}
		if (timeOfDay != TimeOfDay.Both)
		{
			if (timeOfDay == TimeOfDay.Day && !isDayTime)
			{
				ShowClosedUntilDaySign();
				isOpen = false;
				StartCoroutine("HideLandmarkIcon");
				if (OpenStatusUpdated != null)
				{
					OpenStatusUpdated(isOpen);
				}
				return;
			}
			if (timeOfDay == TimeOfDay.Night && isDayTime)
			{
				ShowClosedUntilNightSign();
				isOpen = false;
				StartCoroutine("HideLandmarkIcon");
				if (OpenStatusUpdated != null)
				{
					OpenStatusUpdated(isOpen);
				}
				return;
			}
		}
		HideShopSign();
		isOpen = true;
		StartCoroutine("ShowLandmarkIcon");
		if (OpenStatusUpdated != null)
		{
			OpenStatusUpdated(isOpen);
		}
	}

	private IEnumerator ShowLandmarkIcon()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Landmark landmark = GetComponent<Landmark>();
		if (landmark != null && landmark.icon != null)
		{
			landmark.icon.visible = true;
			landmark.zoomedInSettings.visible = true;
			landmark.zoomedOutSettings.visible = true;
			landmark.icon.gameObject.SetActive(value: true);
		}
	}

	private IEnumerator HideLandmarkIcon()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Landmark landmark = GetComponent<Landmark>();
		if (landmark != null && landmark.icon != null)
		{
			landmark.icon.visible = false;
			landmark.zoomedInSettings.visible = false;
			landmark.zoomedOutSettings.visible = false;
			landmark.icon.gameObject.SetActive(value: false);
		}
	}

	public void ForceClosed()
	{
		ShowClosedSign();
		isOpen = false;
		isForceClosed = true;
		StopShopAnimation();
		if (OpenStatusUpdated != null)
		{
			OpenStatusUpdated(isOpen);
		}
		StartCoroutine("HideLandmarkIcon");
	}

	public void ForceOpen()
	{
		HideShopSign();
		isOpen = true;
		isForceClosed = false;
		if (OpenStatusUpdated != null)
		{
			OpenStatusUpdated(isOpen);
		}
		StartCoroutine("ShowLandmarkIcon");
	}

	public void ForceUpdateOpenStatus()
	{
		CheckShouldBeOpen();
	}

	private void HideShopSign()
	{
		if (shopSign != null)
		{
			shopSign.gameObject.SetActive(value: false);
		}
		PlayShopAnimation();
	}

	private void ShowClosedSign()
	{
		if (shopSign != null)
		{
			shopSign.gameObject.SetActive(value: true);
			shopSign.SetState(CityShopSign.State.Closed);
			StopShopAnimation();
		}
	}

	private void ShowComingSoonSign()
	{
		if (shopSign != null)
		{
			shopSign.gameObject.SetActive(value: true);
			shopSign.SetState(CityShopSign.State.ComingSoon);
			StopShopAnimation();
		}
	}

	private void ShowClosedUntilDaySign()
	{
		if (shopSign != null)
		{
			shopSign.gameObject.SetActive(value: true);
			shopSign.SetState(CityShopSign.State.ClosedUntilDay);
			StopShopAnimation();
		}
	}

	private void ShowClosedUntilNightSign()
	{
		if (shopSign != null)
		{
			shopSign.gameObject.SetActive(value: true);
			shopSign.SetState(CityShopSign.State.ClosedUntilNight);
			StopShopAnimation();
		}
	}

	private void PlayShopAnimation()
	{
		if (animator != null)
		{
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = true;
			}
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
		}
	}

	private void StopShopAnimation()
	{
		if (animator != null)
		{
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
			animator.StopPlayback();
			animator.enabled = false;
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = false;
			}
			animator.transform.localScale = Vector3.one;
		}
	}

	private void HandleOnShopDown()
	{
		if ((!(CoreGameTutorial.instance != null) || CoreGameTutorial.instance.currentStep != CoreGameTutorial.Step.CityTutorialViewMapStart) && (!(TitleScreenController.instance != null) || !TitleScreenController.instance.titleScreenActive) && (!(CityReward.instance != null) || !CityReward.instance.showingRewards) && isOpen && CityController.instance.cityCharacter.AllowInput)
		{
			if (CityController.instance.cityCharacter.CurrentNode == entranceNode)
			{
				PreEnterShop();
			}
			else
			{
				CityController.instance.cityCharacter.MoveToInteractiveNode(entranceNode, PreEnterShop);
				CityController.instance.pinchZoomer.ZoomIn();
			}
			if (animator != null && animator.enabled)
			{
				animator.Play("ShopBuilding_AnimTap");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
	}

	private void PreEnterShop()
	{
		CityController.instance.cityCharacter.StopMove();
		if ((!(TitleScreenController.instance != null) || !TitleScreenController.instance.titleScreenActive) && !CityReward.instance.showingRewards && !DayNightCard.instance.isActive)
		{
			EnterShop();
		}
	}

	private void EnterShop()
	{
		if (CityController.instance.exitingCityScene)
		{
			return;
		}
		CityController.prevPosition = CityController.instance.cityCharacter.transform.position;
		CityController.prevCameraAngleIndex = CityController.instance.curCameraAngleIndex;
		CityController.usePrevPosition = true;
		CityController.instance.communicator.SaveDayAndTime();
		SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.gameObject.transform.position.ToString(), save: false);
		SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
		ShopController.inventory = shopDef.defaultInventory;
		int num = -1;
		if (shopDef.additionalInventories.Count > 0)
		{
			for (int i = 0; i < shopDef.additionalInventories.Count; i++)
			{
				ShopInventorySetup shopInventorySetup = shopDef.additionalInventories[i];
				if (shopInventorySetup.newsFlashInitiated && shopInventorySetup.newsFlashUID > -1 && Player.instance.GetNewsFlashData(shopInventorySetup.newsFlashUID) != null && shopInventorySetup.inventoryFile != null && shopInventorySetup.priority > num)
				{
					num = shopInventorySetup.priority;
					ShopController.inventory = shopInventorySetup.inventoryFile;
				}
			}
		}
		ShopController.shopDef = shopDef;
		CheckShouldHandleNewsFlash();
		if (handleBreakingNews)
		{
			newsFlashUID = -1;
			for (int j = 0; j < shopDef.newsFlashes.Count; j++)
			{
				if (CityController.instance.communicator.IsActiveNewsFlash(shopDef.newsFlashes[j]))
				{
					newsFlashUID = shopDef.newsFlashes[j];
				}
			}
			ShopController.newsFlashUID = newsFlashUID;
		}
		else
		{
			ShopController.newsFlashUID = -1;
		}
		CityController.instance.ExitCityScene();
		LoadingScreenCover.LoadScene("ShopTest");
	}
}
