using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using TouchScript.Gestures;
using UnityEngine;

public class FlashSaleController : MonoBehaviour
{
	public static FlashSaleController instance;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FlashSaleStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FlashSaleIntroStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FlashSaleEnded__BackingField;

	public GameObject flashSaleTitle;

	public GameObject flashSaleFailedTitle;

	public AudioClip flashSaleMusic;

	private float flashSaleTimer;

	private float originalMoveSpeed;

	private List<GameObject> ticketObjs;

	private List<GameObject> boostObjs;

	private GameObject countdownObj;

	private BattleCountdown battleCountdown;

	private float originalApproachFactor;

	private GameObject endCountdown;

	private TextMeshPro endCountdownText;

	private Animator endCountdownAnimator;

	private bool endingCountdownStarted;

	private bool headBackMessagePlayed;

	private int entryFee;

	private string[] flashSaleVONames = new string[7] { "sfx_vo_robin_flashsale1_kpr1", "sfx_vo_robin_flashsale1_kpr2", "sfx_vo_robin_flashsale2_kpr1", "sfx_vo_robin_flashsale2_kpr2", "sfx_vo_robin_flashsale3_kpr", "sfx_vo_robin_flashsale4_kpr", "sfx_vo_robin_flashsale4_kpr2" };

	private bool hasBoughtFlashSale;

	private float elapsedTime;

	private float sfxTimer;

	private bool introSequenceActive;

	private bool flashSaleActive;

	private bool failureObjActive;

	private float timeFlashSaleStarted;

	public bool IntroSequenceActive
	{
		get
		{
			return introSequenceActive;
		}
	}

	public bool FlashSaleActive
	{
		get
		{
			return flashSaleActive;
		}
	}

	public bool FailureObjActive
	{
		get
		{
			return failureObjActive;
		}
	}

	public bool EnterShopDelayed
	{
		get
		{
			if (!flashSaleActive)
			{
				return false;
			}
			return Time.time < timeFlashSaleStarted + 5f;
		}
	}

	public event Action FlashSaleStarted
	{
		add
		{
			Action action = FlashSaleStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FlashSaleStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action FlashSaleIntroStarted
	{
		add
		{
			Action action = FlashSaleIntroStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleIntroStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FlashSaleIntroStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleIntroStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action FlashSaleEnded
	{
		add
		{
			Action action = FlashSaleEnded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleEnded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FlashSaleEnded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FlashSaleEnded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	private void OnEnable()
	{
		Player.instance.RemoveCurrencyTickets(Player.instance.currencyTickets);
	}

	private void OnDisable()
	{
		if (CityController.instance != null)
		{
			CityController.instance.pinchZoomer.CamReachedTargetPos -= HandleCameraZoomIn_StartGame;
		}
	}

	public void ShowFlashSaleDialogBox(bool freeFlashSale = false)
	{
	}

	private void BuyFlashSale()
	{
		hasBoughtFlashSale = true;
		CityController.instance.walletGUI.AddToWallet(-entryFee);
	}

	private void BuyFlashSaleDenied()
	{
		CityController.instance.DisplayNotEnoughLooseChange(0.5f);
	}

	private void CancelledFlashSaleDialog()
	{
		if (hasBoughtFlashSale)
		{
			hasBoughtFlashSale = false;
			CityController.instance.walletGUI.AddToWallet(entryFee);
		}
	}

	public void StartFlashSale()
	{
		if (FlashSaleIntroStarted__BackingField != null)
		{
			FlashSaleIntroStarted__BackingField();
		}
		StartCoroutine(IntroSequence());
	}

	private IEnumerator IntroSequence()
	{
		introSequenceActive = true;
		headBackMessagePlayed = false;
		endingCountdownStarted = false;
		CityController.instance.cityCharacter.StopMove();
		CityController.instance.cityCharacter.AllowInput = false;
		GUICamera.instance.anchorUpperLeft.gameObject.SetActive(false);
		yield return new WaitForEndOfFrame();
		CityController.instance.StopMusic();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_logointro", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.instance.pauseScreen.SetTitleButtonAllowed(false);
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
		GUICamera.instance.anchorUpperRight.gameObject.SetActive(false);
		Communicator.instance.SetCommunicatorShown(false);
		GameUI.instance.HideMySquad();
		GameUI.instance.mySquad.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		if (flashSaleTitle != null)
		{
			flashSaleTitle.SetActive(true);
			Animator flashSaleAnimator = flashSaleTitle.GetComponentInChildren<Animator>();
			while (flashSaleAnimator.NormalizedTime() < 0.99f)
			{
				yield return null;
			}
		}
		CityController.instance.PlayMusic(flashSaleMusic);
		if (flashSaleTitle != null)
		{
			flashSaleTitle.SetActive(false);
		}
		originalApproachFactor = CityController.instance.pinchZoomer.approachFactor;
		CityController.instance.zoomType = ZoomType.FlashSale;
		CityController.instance.pinchZoomer.approachFactor = 3f;
		CityController.instance.pinchZoomer.ZoomOut(225f);
		StartCoroutine(LerpCameraToCenterMap());
		PanGesture panGesture = CityController.instance.cameraHolder.GetComponentInChildren<PanGesture>();
		panGesture.enabled = false;
		if (GameManager.instance.flashSaleSettings.useRandomItemSetups)
		{
			SetUpFlashSaleObjs();
		}
		else
		{
			FlashSaleSetupManager.instance.CreateRandomSaleSetup(out boostObjs, out ticketObjs);
		}
		yield return StartCoroutine(CascadeLandmarkIcons());
		Communicator.instance.SetCommunicatorShown(true);
		GameUI.instance.ShowMySquad();
		CityController.instance.communicator.ShowFlashSaleTimer(GameManager.instance.flashSaleSettings.duration);
		countdownObj = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/Battle_CountDown");
		countdownObj.transform.parent = GUICamera.instance.anchorMiddleCenter;
		countdownObj.transform.localPosition = Vector3.zero;
		countdownObj.GetComponent<BattleCountdown>().allowBonusTapping = false;
		battleCountdown = countdownObj.GetComponent<BattleCountdown>();
		for (int i = 3; i > 0; i--)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_countdown", GFAudioManager.SFX_GROUP, string.Empty);
			battleCountdown.ShowNumber(i);
			yield return new WaitForSeconds(1.05f);
		}
		GUICamera.instance.anchorUpperLeft.gameObject.SetActive(true);
		GUICamera.instance.anchorUpperRight.gameObject.SetActive(true);
		panGesture.enabled = true;
		CityController.instance.walletGUI.Show();
		CityController.instance.SetMagnifyingGlassActive(false);
		CityController.instance.SetTitanTowerBtnActive(false);
		CityController.instance.pinchZoomer.approachFactor = originalApproachFactor;
		CityController.instance.pinchZoomer.ZoomIn(20f);
		CityController.instance.pinchZoomer.CamReachedTargetPos += HandleCameraZoomIn_StartGame;
	}

	private void HandleCameraZoomIn_StartGame()
	{
		CityController.instance.pinchZoomer.CamReachedTargetPos -= HandleCameraZoomIn_StartGame;
		battleCountdown.ShowGo();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_go", GFAudioManager.SFX_GROUP, string.Empty);
		introSequenceActive = false;
		originalMoveSpeed = CityController.instance.cityCharacter.moveSpeed;
		CityController.instance.cityCharacter.SetMoveSpeed(GameManager.instance.flashSaleSettings.moveSpeedBase);
		CityController.instance.communicator.StartFlashSale();
		CityController.instance.cityCharacter.AllowInput = true;
		flashSaleActive = true;
		flashSaleTimer = GameManager.instance.flashSaleSettings.duration;
		UnityEngine.Debug.Log("Set Start Time!");
		timeFlashSaleStarted = Time.time;
		if (FlashSaleStarted__BackingField != null)
		{
			FlashSaleStarted__BackingField();
		}
	}

	private IEnumerator LerpCameraToCenterMap()
	{
		float currentLerp = 0f;
		float lerpTime = 0.5f;
		float t = 0f;
		Transform cameraHolder = CityController.instance.cameraHolder;
		Vector3 startingPos = cameraHolder.position;
		Vector3 endPos = new Vector3(3.6f, 0f, -11.5f);
		while (t < 1f)
		{
			currentLerp += Time.deltaTime;
			currentLerp = ((!(currentLerp > lerpTime)) ? currentLerp : lerpTime);
			t = currentLerp / lerpTime;
			cameraHolder.position = Vector3.Lerp(startingPos, endPos, t);
			yield return null;
		}
		cameraHolder.position = endPos;
	}

	private void Update()
	{
		if (!flashSaleActive)
		{
			return;
		}
		flashSaleTimer -= Time.deltaTime;
		if (flashSaleTimer <= 13f && !endingCountdownStarted)
		{
			endingCountdownStarted = true;
			elapsedTime = 0f;
			if (endCountdown == null || !endCountdown.activeInHierarchy)
			{
				endCountdown = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSale_Countdown");
				endCountdown.transform.position = GUICamera.instance.anchorMiddleCenter.transform.position;
				endCountdownText = endCountdown.transform.Find("Anim_GRP/CountdownText").GetComponent<TextMeshPro>();
				endCountdownAnimator = endCountdown.GetComponentInChildren<Animator>();
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_getbacktothestore", GFAudioManager.SFX_GROUP, string.Empty);
			}
			if (!headBackMessagePlayed)
			{
				headBackMessagePlayed = true;
				endCountdownAnimator.Play("FlashSale_CountdownHeadBack_Anim", 0, 0f);
			}
		}
		else if (flashSaleTimer > 13f && endingCountdownStarted)
		{
			endingCountdownStarted = false;
			ObjectPool.PoolObject(endCountdown);
		}
		if (flashSaleTimer < 12f)
		{
			elapsedTime += Time.deltaTime;
		}
		if (flashSaleTimer < 11f && flashSaleTimer >= 1f && elapsedTime >= 1f && endCountdownText != null)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_timerunningoutcountdown", GFAudioManager.SFX_GROUP, string.Empty);
			endCountdownAnimator.Play("FlashSale_CountdownNumber_Anim", 0, 0f);
			endCountdownText.text = ((int)flashSaleTimer/*cast due to constrained. prefix*/).ToString();
			elapsedTime = 0f;
		}
		sfxTimer -= Time.deltaTime;
		if (sfxTimer <= 0f)
		{
			AudioSource voSource = GFAudioManager.instance.PlayClipFromResources("Audio/VO/" + flashSaleVONames[UnityEngine.Random.Range(0, flashSaleVONames.Length)], GFAudioManager.VOICEOVER_GROUP, string.Empty);
			CityController.instance.RegisterCityVO(voSource);
			sfxTimer = 5f;
		}
		if (flashSaleTimer <= 0f)
		{
			EndFlashSale(false);
		}
	}

	public void AddTime()
	{
		float timeAddedOnTicketPickup = GameManager.instance.flashSaleSettings.timeAddedOnTicketPickup;
		flashSaleTimer += timeAddedOnTicketPickup;
		CityController.instance.communicator.AddFlashSaleTime(timeAddedOnTicketPickup);
	}

	private void SetUpFlashSaleObjs()
	{
		List<PathNode> reachableNodes = CityController.instance.cityCharacter.GetReachableNodes();
		ticketObjs = new List<GameObject>();
		boostObjs = new List<GameObject>();
		int num = GameManager.instance.flashSaleSettings.numTicketsOnMap;
		int num2 = GameManager.instance.flashSaleSettings.numBoostsOnMap;
		for (int i = 0; i < GameManager.instance.flashSaleSettings.ticketsToAddOnMission.Count; i++)
		{
			FlashSaleTicketsToAddOnMission flashSaleTicketsToAddOnMission = GameManager.instance.flashSaleSettings.ticketsToAddOnMission[i];
			if (Player.instance.HasMissionCompleted(flashSaleTicketsToAddOnMission.missionUID))
			{
				num += flashSaleTicketsToAddOnMission.ticketsToAdd;
				num2 += flashSaleTicketsToAddOnMission.boostsToAdd;
			}
		}
		UnityEngine.Debug.LogFormat("Creating {0} tickets for Flash Sale", num);
		int num3 = num;
		while (num3 > 0 && reachableNodes.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, reachableNodes.Count);
			PathNode pathNode = reachableNodes[index];
			if (Vector3.Distance(pathNode.transform.position, CityController.instance.cityCharacter.transform.position) > 2f)
			{
				GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSaleTicket", pathNode.transform.position);
				ticketObjs.Add(objectFromResource);
				num3--;
			}
			reachableNodes.RemoveAt(index);
		}
		UnityEngine.Debug.LogFormat("Creating {0} boosts for Flash Sale", num2);
		int num4 = num2;
		while (num4 > 0 && reachableNodes.Count > 0)
		{
			int index2 = UnityEngine.Random.Range(0, reachableNodes.Count);
			PathNode pathNode2 = reachableNodes[index2];
			if (Vector3.Distance(pathNode2.transform.position, CityController.instance.cityCharacter.transform.position) > 2f)
			{
				GameObject objectFromResource2 = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSale_Boost", pathNode2.transform.position);
				objectFromResource2.GetComponent<FlashSaleBoostPickup>().boostType = ((!(UnityEngine.Random.value < 0.5f)) ? FlashSaleBoostPickup.BoostType.Negative : FlashSaleBoostPickup.BoostType.Normal);
				boostObjs.Add(objectFromResource2);
				num4--;
			}
			reachableNodes.RemoveAt(index2);
		}
	}

	private IEnumerator CascadeLandmarkIcons()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		List<Landmark> ticketLandmarks = new List<Landmark>();
		for (int i = 0; i < ticketObjs.Count; i++)
		{
			ticketLandmarks.Add(ticketObjs[i].GetComponentInChildren<Landmark>());
		}
		for (int j = 0; j < ticketLandmarks.Count; j++)
		{
			Landmark landmark = ticketLandmarks[j];
			if (landmark.IsIconSetup)
			{
				landmark.ResetLandmarkIcon();
			}
			landmark.icon.gameObject.SetActive(false);
		}
		ticketLandmarks.Sort(SortByX);
		yield return new WaitForSeconds(0.65f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_ticketsappear", GFAudioManager.SFX_GROUP, string.Empty);
		int count = ticketLandmarks.Count;
		for (int k = 0; k < count; k++)
		{
			ticketLandmarks[k].icon.gameObject.SetActive(true);
			yield return new WaitForSeconds(1f / (float)count);
		}
		yield return new WaitForSeconds(2f);
	}

	private int SortByX(Landmark a, Landmark b)
	{
		return a.transform.position.x.CompareTo(b.transform.position.x);
	}

	public void EndFlashSale(bool enteredStore)
	{
		flashSaleActive = false;
		CityController.instance.zoomType = ZoomType.Normal;
		CityController.instance.SetMagnifyingGlassActive(true);
		CityController.instance.SetTitanTowerBtnActive(true);
		CityController.instance.cityCharacter.FlashSaleEnded(originalMoveSpeed);
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		GameUI.instance.mySquad.SetActive(true);
		CityController.instance.StopMusic();
		CityController.instance.StopAllCityVO();
		if (!enteredStore)
		{
			CityController.instance.walletGUI.AddToWalletTickets(-Player.instance.currencyTickets, false);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_lose", GFAudioManager.SFX_GROUP, string.Empty);
			StartCoroutine(PlayFlashSaleFailedAnim());
			StartCoroutine(ResumeCityMusicAfterDelay(3.192f));
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_end", GFAudioManager.SFX_GROUP, string.Empty);
			CityController.instance.pinchZoomer.ZoomOut();
			CityController.instance.pinchZoomer.ZoomIn();
			CityController.instance.communicator.EndFlashSale();
			CityController.instance.pauseScreen.SetTitleButtonAllowed(true);
		}
		countdownObj.GetComponent<BattleCountdown>().allowBonusTapping = true;
		ObjectPool.PoolObject(countdownObj);
		battleCountdown = null;
		for (int num = ticketObjs.Count - 1; num >= 0; num--)
		{
			ObjectPool.PoolObject(ticketObjs[num]);
		}
		ticketObjs.Clear();
		ticketObjs = null;
		for (int num2 = boostObjs.Count - 1; num2 >= 0; num2--)
		{
			ObjectPool.PoolObject(boostObjs[num2]);
		}
		boostObjs.Clear();
		boostObjs = null;
		if (FlashSaleEnded__BackingField != null)
		{
			FlashSaleEnded__BackingField();
		}
	}

	private IEnumerator PlayFlashSaleFailedAnim()
	{
		if (flashSaleFailedTitle != null)
		{
			flashSaleFailedTitle.SetActive(true);
		}
		failureObjActive = true;
		yield return new WaitForEndOfFrame();
		if (flashSaleFailedTitle != null)
		{
			Animator failedAnimator = flashSaleFailedTitle.GetComponentInChildren<Animator>();
			while (failedAnimator.NormalizedTime() < 0.99f)
			{
				yield return null;
			}
		}
		CityController.instance.pinchZoomer.approachFactor = 3f;
		CityController.instance.pinchZoomer.ZoomOut(false);
		CityController.instance.pinchZoomer.ZoomIn(false);
		CityController.instance.pinchZoomer.ZoomedIn += ZoomInOutDuringFailAnim;
		CityController.instance.pinchZoomer.ZoomedOut += ZoomInOutDuringFailAnim;
		CityController.instance.communicator.EndFlashSale();
		CityController.instance.walletGUI.UpdateCurrencySprite();
		CityController.instance.pauseScreen.SetTitleButtonAllowed(true);
		if (flashSaleFailedTitle != null)
		{
			flashSaleFailedTitle.SetActive(false);
		}
		failureObjActive = false;
		yield return new WaitForSeconds(2f);
		CityController.instance.pinchZoomer.approachFactor = originalApproachFactor;
		CityController.instance.pinchZoomer.ZoomedIn -= ZoomInOutDuringFailAnim;
		CityController.instance.pinchZoomer.ZoomedOut -= ZoomInOutDuringFailAnim;
	}

	private IEnumerator ResumeCityMusicAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		CityController.instance.PlayCurrentLocationMusic();
	}

	private void ZoomInOutDuringFailAnim()
	{
		CityController.instance.pinchZoomer.approachFactor = originalApproachFactor;
		CityController.instance.pinchZoomer.ZoomedIn -= ZoomInOutDuringFailAnim;
		CityController.instance.pinchZoomer.ZoomedOut -= ZoomInOutDuringFailAnim;
	}
}
