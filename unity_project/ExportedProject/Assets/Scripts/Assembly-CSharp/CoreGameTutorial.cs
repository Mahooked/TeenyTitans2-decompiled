using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class CoreGameTutorial : MonoBehaviour
{
	public enum Step
	{
		None = 0,
		FirstSwipe = 1,
		FindFirstShop = 2,
		ShopTutorialPart1 = 3,
		ShopTutorialPart2 = 4,
		ShopTutorialPart3 = 5,
		ShopTutorialPart4 = 6,
		ShopTutorialPart5 = 7,
		ShopTutorialPart6 = 8,
		ShopTutorialPart7 = 9,
		ShopTutorialPart8 = 10,
		CityBattleNPC = 11,
		CityBattleNPCPart2 = 12,
		ShopTutorial2Start = 13,
		ShopTutorial2ChooseNextFigure = 14,
		ShopTutorial2SwipeToFront = 15,
		ShopTutorial2SwipeToBack = 16,
		ShopTutorial2PlaceInCart = 17,
		ShopTutorial2CheckOut = 18,
		ShopTutorial2Finished = 19,
		CityTutorialViewMapStart = 20,
		CityTutorialAfterMap = 21
	}

	public static CoreGameTutorial instance;

	public static bool ignoreNotifyReachedNode;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Step> StepComplete__BackingField;

	public Step currentStep;

	public GameObject tutorialMissionNPC;

	public GameObject tutorialGlintObject;

	public GameObject tutorialShopBuilding;

	public GameObject tutorialShopBuilding2;

	public GameObject tutorialGothamLandmark;

	public GameObject tutorialMetropolisLandmark;

	public bool cityScene;

	public bool shopScene;

	public List<PathNode> enterParkPathNodes = new List<PathNode>();

	public PathNode tutorialGlintMoveToNode;

	private bool playedGlintTutorial;

	private GameObject findShopTapHand;

	private GameObject tutorialBattleNPC1;

	private bool preventToyboxShelfReturn;

	private bool preventBuyButton;

	private bool preventMoveToCart;

	private bool hasTossedFigure;

	private string shopCharacterNameTarget = "Radical Beast Boy";

	public string shopCharacterNameAboveTarget = "Bumblebee";

	public string shopCharacterNameAboveTop = "See-More";

	private bool hasTossedTop;

	private TutorialHand _tutorialHand;

	private TutorialPopUp _tutorialPopUp;

	private TutorialPopUp _tutorialPopUpShop;

	public bool _isActive;

	private TutorialHand tutorialHand
	{
		get
		{
			if (_tutorialHand == null)
			{
				_tutorialHand = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_Hands") as GameObject).GetComponent<TutorialHand>();
			}
			return _tutorialHand;
		}
	}

	private TutorialPopUp tutorialPopUp
	{
		get
		{
			if (_tutorialPopUp == null)
			{
				_tutorialPopUp = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_PopUp") as GameObject).GetComponent<TutorialPopUp>();
			}
			return _tutorialPopUp;
		}
	}

	private TutorialPopUp tutorialPopUpShop
	{
		get
		{
			if (_tutorialPopUpShop == null)
			{
				_tutorialPopUpShop = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_PopUp_Shop") as GameObject).GetComponent<TutorialPopUp>();
			}
			return _tutorialPopUpShop;
		}
	}

	public bool isActive
	{
		get
		{
			return _isActive;
		}
	}

	public event Action<Step> StepComplete
	{
		add
		{
			Action<Step> action = StepComplete__BackingField;
			Action<Step> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StepComplete__BackingField, (Action<Step>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Step> action = StepComplete__BackingField;
			Action<Step> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StepComplete__BackingField, (Action<Step>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		instance = this;
		SetTutorial(Step.None);
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if (shopScene)
		{
			CompleteStep(Step.FindFirstShop);
		}
		if (TitleScreenController.instance == null || !TitleScreenController.instance.titleScreenActive)
		{
			ContinueFromPreviousTutorial();
		}
		else
		{
			StartCoroutine(WaitForTitleScreen());
		}
	}

	private IEnumerator WaitForTitleScreen()
	{
		while (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.3f);
		ContinueFromPreviousTutorial();
	}

	private void ContinueFromPreviousTutorial()
	{
		CheckCompletedShopTutorials();
		SetTutorial(GetPreviousTutorialStep());
	}

	public Step GetPreviousTutorialStep()
	{
		string[] names = Enum.GetNames(typeof(Step));
		for (int i = 1; i < names.Length; i++)
		{
			if (SaveUtility.GetInt(names[i] + "_Complete") == 0)
			{
				return (Step)i;
			}
		}
		return Step.None;
	}

	private void CheckCompletedShopTutorials()
	{
		MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(4);
		if (missionCompletedData != null)
		{
			CompleteStep(Step.ShopTutorial2Start);
			CompleteStep(Step.ShopTutorial2ChooseNextFigure);
			CompleteStep(Step.ShopTutorial2SwipeToFront);
			CompleteStep(Step.ShopTutorial2SwipeToBack);
			CompleteStep(Step.ShopTutorial2PlaceInCart);
			CompleteStep(Step.ShopTutorial2CheckOut);
			CompleteStep(Step.ShopTutorial2Finished);
		}
	}

	public static bool IsTutorialStepComplete(Step step)
	{
		return SaveUtility.GetInt(string.Concat(step, "_Complete")) == 1;
	}

	private void SetTutorial(Step step)
	{
		currentStep = step;
		switch (step)
		{
		case Step.FirstSwipe:
			if (cityScene)
			{
				StopCoroutine("SwipeMoveTutorial");
				StartCoroutine("SwipeMoveTutorial");
			}
			break;
		case Step.FindFirstShop:
			if (cityScene)
			{
				StopCoroutine("FindShopTutorial");
				StartCoroutine("FindShopTutorial");
			}
			break;
		case Step.ShopTutorialPart1:
			if (cityScene)
			{
				StopCoroutine("FindShopTutorial");
				StartCoroutine("FindShopTutorial");
			}
			if (shopScene)
			{
				StopCoroutine("ShopSwipeTutorial");
				StartCoroutine("ShopSwipeTutorial");
			}
			break;
		case Step.CityBattleNPC:
			if (cityScene)
			{
				StopCoroutine("CityBattleNPCTutorial");
				StartCoroutine("CityBattleNPCTutorial");
			}
			break;
		case Step.CityBattleNPCPart2:
			if (cityScene)
			{
				StopCoroutine("CityBattleNPCTutorialPart2");
				StartCoroutine("CityBattleNPCTutorialPart2");
			}
			break;
		case Step.ShopTutorial2Start:
			if (shopScene)
			{
				StopCoroutine("SecondShopTutorial");
				StartCoroutine("SecondShopTutorial");
			}
			if (cityScene)
			{
				StopCoroutine("SecondShopTutorialCity");
				StartCoroutine("SecondShopTutorialCity");
			}
			break;
		case Step.CityTutorialViewMapStart:
			if (cityScene)
			{
				StopCoroutine("CityTutorialViewMap");
				StartCoroutine("CityTutorialViewMap");
			}
			break;
		}
	}

	private void CompleteStep(Step step)
	{
		SaveUtility.SetInt(string.Concat(step, "_Complete"), 1, false);
		if (StepComplete__BackingField != null)
		{
			StepComplete__BackingField(step);
		}
	}

	public void CompleteAllTutorials()
	{
		Step[] array = (Step[])Enum.GetValues(typeof(Step));
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			CompleteStep(array[i]);
		}
	}

	public void NotifyReachedNode(PathNode node)
	{
		if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeSelf)
		{
			return;
		}
		if (SaveUtility.GetInt("GAME_PLAYED_GLINT_TUTORIAL") == 1)
		{
			ignoreNotifyReachedNode = true;
		}
		else if (!playedGlintTutorial)
		{
			MissionData missionData = Player.instance.GetMissionData(7);
			if (missionData != null && missionData.active && !missionData.completed && enterParkPathNodes.Contains(node))
			{
				StartCoroutine("GlintTutorial");
			}
		}
	}

	private IEnumerator GlintTutorial()
	{
		playedGlintTutorial = true;
		_isActive = true;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		tutorialGlintObject.GetComponent<BoxCollider>().enabled = false;
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.communicator.pauseTime = true;
		CityController.instance.cityCharacter.StopMove();
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.pinchZoomer.allowPan = false;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
		while (!CityController.instance.cityCharacter.animator.currentClip.name.Contains("idle"))
		{
			yield return new WaitForEndOfFrame();
		}
		GameUI.instance.HideAll();
		GameUI.instance.HideAllLandmarksBut(null);
		GameUI.instance.ShowCinemaBars(1f);
		tutorialGlintMoveToNode.forceStop = true;
		yield return new WaitForSeconds(1f);
		CameraFollow cameraFollow = CityController.instance.cameraHolder.GetComponent<CameraFollow>();
		CameraPinchZoomer pinchZoomer = CityController.instance.cameraHolder.GetComponent<CameraPinchZoomer>();
		CityCharacter cityCharacter = CityController.instance.cityCharacter;
		float previousApproachFactor = cameraFollow.approachFactor;
		cameraFollow.approachFactor = 2f;
		cameraFollow.follow = tutorialGlintObject.transform;
		cityCharacter.MoveToNode(tutorialGlintMoveToNode);
		yield return new WaitForSeconds(2f);
		tutorialGlintObject.GetComponent<BoxCollider>().enabled = true;
		GameObject tapGlintHand = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_Hands") as GameObject);
		tapGlintHand.transform.parent = tutorialGlintObject.transform;
		tapGlintHand.transform.localPosition = new Vector3(0.01f, 0.3f, 0f);
		tapGlintHand.transform.parent = null;
		tapGlintHand.transform.localEulerAngles = CityController.instance.cityCharacter.transform.localEulerAngles;
		tapGlintHand.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
		tapGlintHand.GetComponent<TutorialHand>().TapShopSetup();
		tapGlintHand.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		tutorialPopUp.text.text = Language.Get("TUTORIAL_GLINT_1", "TUTORIALS");
		Vector3 popUpPos = GameUI.instance.guiCamera2.ScreenToWorldPoint(pinchZoomer.thisCamera.WorldToScreenPoint(tapGlintHand.transform.position));
		popUpPos.x -= 95f;
		popUpPos.y -= 240f;
		popUpPos.z = -1200f;
		tutorialPopUp.transform.position = popUpPos;
		tutorialPopUp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		bool stayInLoop = true;
		CityController.instance.preventShowingCommunicatorAfterDialogue = true;
		CityController.instance.preventShowingGUIAfterDialogue = true;
		while (stayInLoop)
		{
			tapGlintHand.transform.localEulerAngles = CityController.instance.cityCharacter.transform.localEulerAngles;
			if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				stayInLoop = false;
			}
			yield return new WaitForEndOfFrame();
		}
		UnityEngine.Object.Destroy(tapGlintHand);
		tutorialPopUp.PlayOutro();
		CityController.instance.preventShowingCommunicatorAfterDialogue = false;
		CityController.instance.preventShowingGUIAfterDialogue = false;
		GameUI.instance.HideCinemaBars(0.6f);
		yield return new WaitForSeconds(0.6f);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (DialogueTree.instance == null || (DialogueTree.instance != null && !DialogueTree.instance.gameObject.activeInHierarchy))
			{
				stayInLoop = false;
			}
			yield return new WaitForEndOfFrame();
		}
		cameraFollow.approachFactor = previousApproachFactor;
		cameraFollow.follow = CityController.instance.cityCharacter.transform;
		tutorialGlintMoveToNode.forceStop = false;
		yield return new WaitForSeconds(0.1f);
		Player.instance.PinMission(43);
		Communicator.instance.SetCommunicatorShown(true);
		GameUI.instance.ShowMySquad();
		Communicator communicator = CityController.instance.communicator;
		while (communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(0.02f);
		if (communicator.isDayTime)
		{
			tutorialPopUp.text.text = Language.Get("TUTORIAL_COMM_5", "TUTORIALS");
			popUpPos = communicator.leftBtnSprite.transform.position;
			popUpPos.x -= 120f;
			popUpPos.y += 15f;
			tutorialPopUp.transform.position = popUpPos;
			tutorialPopUp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			tutorialPopUp.ShowBottomRightTail();
			tutorialPopUp.PlayIntro();
			while (!communicator.dayNight.activeSelf)
			{
				yield return new WaitForEndOfFrame();
			}
			tutorialPopUp.PlayOutro();
			communicator.leftBtnSprite.GetComponent<BoxCollider2D>().enabled = false;
			communicator.rightBtnSprite.GetComponent<BoxCollider2D>().enabled = false;
			yield return new WaitForSeconds(0.6f);
			tutorialPopUp.text.text = Language.Get("TUTORIAL_COMM_6", "TUTORIALS");
			popUpPos = communicator.leftBtnSprite.transform.position;
			popUpPos.x -= 120f;
			popUpPos.y -= 15f;
			tutorialPopUp.transform.position = popUpPos;
			tutorialPopUp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			tutorialPopUp.ShowBottomRightTail();
			tutorialPopUp.PlayIntro();
			CityController.instance.communicator.pauseTime = false;
			stayInLoop = true;
			while (stayInLoop)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
				{
					stayInLoop = false;
				}
				yield return null;
			}
			tutorialPopUp.PlayOutro();
			GameUI.instance.ShowAll();
		}
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowPan = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		CityController.instance.cityCharacter.AllowInput = true;
		communicator.leftBtnSprite.GetComponent<BoxCollider2D>().enabled = true;
		communicator.rightBtnSprite.GetComponent<BoxCollider2D>().enabled = true;
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.ShowAll();
		SaveUtility.SetInt("GAME_PLAYED_GLINT_TUTORIAL", 1, false);
		_isActive = false;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		CityController.instance.communicator.pauseTime = false;
		yield return new WaitForEndOfFrame();
	}

	public void StartCommunicatorTutorial()
	{
		StartCoroutine("CommunicatorTutorial");
	}

	private IEnumerator CommunicatorTutorial()
	{
		Communicator communicator = CityController.instance.communicator;
		_isActive = true;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		while (communicator.PopUpActive)
		{
			yield return null;
		}
		while (CityReward.instance.showingRewards)
		{
			yield return null;
		}
		GameUI.instance.HideAllButCommunicator();
		GameUI.instance.HideAllLandmarksBut(null);
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.cityCharacter.StopMove();
		while (!communicator.animator.IsNamed("communicator_hide_outro", "communicator_vibrate") || communicator.animator.IsPlaying())
		{
			yield return null;
		}
		Vector3 popUpPos = communicator.leftBtnSprite.transform.position;
		communicator.mailBtnCollider.enabled = false;
		communicator.newsFlashBtnCollider.enabled = false;
		communicator.antennaBtnCollider.enabled = false;
		communicator.pauseTime = true;
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		popUpBubble.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
		Camera popupCamera = GameUI.instance.guiCamera2;
		BoxCollider2D leftBtnSpriteCollider = communicator.leftBtnSprite.GetComponent<BoxCollider2D>();
		BoxCollider2D rightBtnSpriteCollider = communicator.rightBtnSprite.GetComponent<BoxCollider2D>();
		if (communicator.missions.activeSelf)
		{
			popUpBubble.Init(targetWorldPosition: communicator.dayNightCycle.position + new Vector3(-22.78f, 33.23f, 0f), message: Language.Get("TUTORIAL_COMM_3", "TUTORIALS"), targetCamera: popupCamera, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
		}
		else
		{
			popUpBubble.Init(targetWorldPosition: leftBtnSpriteCollider.bounds.center, message: Language.Get("TUTORIAL_COMM_1", "TUTORIALS"), targetCamera: popupCamera, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
			while (!communicator.missions.activeSelf)
			{
				yield return new WaitForEndOfFrame();
			}
			yield return popUpBubble.WaitUntilIntroComplete();
			popUpBubble.OutroBubble();
			leftBtnSpriteCollider.enabled = false;
			rightBtnSpriteCollider.enabled = false;
			popUpBubble.Init(targetWorldPosition: communicator.dayNightCycle.position + new Vector3(-22.78f, 33.23f, 0f), message: Language.Get("TUTORIAL_COMM_2", "TUTORIALS"), targetCamera: popupCamera, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
		}
		leftBtnSpriteCollider.enabled = false;
		rightBtnSpriteCollider.enabled = false;
		communicator.ActivateMissions();
		yield return new WaitForEndOfFrame();
		while (!communicator.missionsPopUpAnimator.IsName("communicator_missionpopup_intro") || !communicator.missionsPopUp.activeSelf)
		{
			yield return null;
		}
		communicator.missionsBtnCollider.enabled = false;
		yield return new WaitForSeconds(0.3f);
		communicator.missionsScroller.SetScrollPercentWithoutEvent(0f);
		popUpBubble.Init(targetWorldPosition: communicator.SideMissions[0].transform.position + new Vector3(-57.9f, 21.49f), message: Language.Get("TUTORIAL_COMM_4", "TUTORIALS"), targetCamera: popupCamera, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
		while (communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		popUpBubble.OutroBubble();
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.ShowAll();
		communicator.missionsBtnCollider.enabled = true;
		communicator.mailBtnCollider.enabled = true;
		communicator.newsFlashBtnCollider.enabled = true;
		communicator.antennaBtnCollider.enabled = true;
		leftBtnSpriteCollider.enabled = true;
		rightBtnSpriteCollider.enabled = true;
		communicator.pauseTime = false;
		CityController.instance.cityCharacter.AllowInput = true;
		_isActive = false;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		SaveUtility.SetInt("GAME_PLAYED_COMM_TUTORIAL", 1, false);
	}

	private IEnumerator SwipeMoveTutorial()
	{
		yield return new WaitUntil(() => LoadingScreenCover.outroFinished);
		yield return new WaitUntil(() => CityController.instance != null && CityController.instance.initialized);
		yield return new WaitForEndOfFrame();
		yield return new WaitWhile(() => DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy);
		yield return null;
		CityController.instance.communicator.pauseTime = true;
		GameUI.instance.HideAll(false);
		GameUI.instance.HideAllLandmarksBut(tutorialShopBuilding);
		tutorialShopBuilding.GetComponent<Landmark>().icon.hideAtMinScale = false;
		yield return new WaitForSeconds(0.1f);
		tutorialHand.PlaySwipeHandDown();
		tutorialHand.gameObject.AddComponent<DestroyOnTitleScreenActive>();
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
		yield return new WaitForSeconds(1.5f);
		while (CityController.instance.cityCharacter.animator.currentClip.name.Contains("idle"))
		{
			yield return new WaitForEndOfFrame();
		}
		tutorialHand.HideAllAnimators();
		CompleteStep(Step.FirstSwipe);
		SetTutorial(Step.FindFirstShop);
	}

	private IEnumerator FindShopTutorial()
	{
		yield return new WaitForEndOfFrame();
		CityController.instance.communicator.pauseTime = true;
		GameUI.instance.HideAll();
		yield return new WaitForEndOfFrame();
		GameUI.instance.HideAllLandmarksBut(tutorialShopBuilding);
		tutorialShopBuilding.GetComponent<Landmark>().icon.hideAtMinScale = false;
		yield return new WaitForEndOfFrame();
		GameObject findShopHand = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_Hands") as GameObject);
		findShopHand.name = "FIRST SHOP HAND";
		findShopHand.gameObject.AddComponent<DestroyOnTitleScreenActive>();
		findShopHand.transform.parent = tutorialShopBuilding.transform;
		findShopHand.transform.localPosition = new Vector3(0.092f, 1.3f, 0f);
		findShopHand.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
		findShopHand.transform.parent = null;
		findShopHand.transform.localEulerAngles = CityController.instance.cityCharacter.transform.localEulerAngles;
		findShopHand.GetComponent<TutorialHand>().TapShopSetup();
		findShopHand.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		findShopHand.SetActive(true);
	}

	private IEnumerator ShopSwipeTutorial()
	{
		ShopUI.instance.homeButton.SetActive(false);
		ShopUI.instance.retryButton.SetActive(false);
		yield return StartCoroutine(ShopController.instance.PlayTT2Dialogue("CONVO_TUTORIAL_FIRSTSHOP"));
		currentStep = Step.ShopTutorialPart2;
		tutorialHand.PlaySwipeHandFigureShop();
		tutorialHand.transform.position = Vector3.zero;
		tutorialHand.transform.localScale = new Vector3(-1f, 1f, 1f);
		bool playedFirstSwipeFX = false;
		bool stayInLoop = true;
		while (stayInLoop)
		{
			if (ShopController.instance.CurrentRack == 1 && !playedFirstSwipeFX)
			{
				playedFirstSwipeFX = true;
				CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
				GFAudioManager.instance.PlayClipFromResources(characterVOSet.storeTutorialFirstSwipeClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			if (ShopController.instance.CurrentRack == 2 && !Input.GetMouseButton(0))
			{
				stayInLoop = false;
			}
			yield return new WaitForEndOfFrame();
		}
		tutorialHand.HideAllAnimators();
		currentStep = Step.ShopTutorialPart3;
		yield return new WaitForSeconds(0.33f);
		yield return StartCoroutine(ShopController.instance.PlayTT2Dialogue("CONVO_TUTORIAL_FIRSTSHOP_ENDPAN"));
		currentStep = Step.ShopTutorialPart4;
		tutorialHand.PlaySwipeHandFigureShop();
		tutorialHand.transform.position = new Vector3(45f, -20f, 0f);
		tutorialHand.transform.localScale = new Vector3(1f, 1f, 1f);
		int characterUID = 94;
		switch (CityCharacter.characterType)
		{
		case CityCharacterType.Robin:
			shopCharacterNameTarget = "Radical Robin";
			characterUID = 93;
			break;
		case CityCharacterType.BeastBoy:
			shopCharacterNameTarget = "Radical Beast Boy";
			characterUID = 94;
			break;
		case CityCharacterType.Raven:
			shopCharacterNameTarget = "Radical Raven";
			characterUID = 98;
			break;
		case CityCharacterType.Starfire:
			shopCharacterNameTarget = "Radical Starfire";
			characterUID = 99;
			break;
		case CityCharacterType.Cyborg:
			shopCharacterNameTarget = "Radical Cyborg";
			characterUID = 97;
			break;
		}
		ShopToyBox firstFigBox = null;
		for (int i = 0; i < ShopController.instance.allToyBoxes.Count; i++)
		{
			firstFigBox = ShopController.instance.allToyBoxes[i];
			if (firstFigBox.charDef.characterName.Equals("Radical Beast Boy"))
			{
				CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(characterUID);
				if (character != null)
				{
					firstFigBox.UpdateBoxForCharacter(character);
					firstFigBox.GetComponent<BoxCollider>().enabled = false;
					break;
				}
			}
		}
		tutorialPopUpShop.text.text = string.Format(Language.Get("TUTORIAL_FIND_FIRST_FIGURE", "TUTORIALS"), Language.Get(firstFigBox.charDef.characterNameKey, "NPCS,FIGS"));
		tutorialPopUpShop.transform.parent = ShopUI.instance.AnchorUpperCenter;
		tutorialPopUpShop.transform.localPosition = new Vector3(0f, -100f, 10f);
		tutorialPopUpShop.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
		stayInLoop = true;
		int lastDirection = 1;
		while (stayInLoop)
		{
			if (lastDirection == -1 && tutorialHand.swipeHandAnimator.NormalizedTime() > 0.98f)
			{
				lastDirection = 1;
				tutorialHand.PlaySwipeHandFigureShop();
				tutorialHand.transform.position = new Vector3(45f, -20f, 0f);
				tutorialHand.transform.localScale = new Vector3(lastDirection, 1f, 1f);
			}
			else if (tutorialHand.swipeHandAnimator.NormalizedTime() > 0.98f)
			{
				lastDirection = -1;
				tutorialHand.PlaySwipeHandFigureShop();
				tutorialHand.transform.position = new Vector3(-45f, 75f, 0f);
				tutorialHand.transform.localScale = new Vector3(lastDirection, 1f, 1f);
			}
			if (currentStep != Step.ShopTutorialPart4)
			{
				stayInLoop = false;
			}
			yield return new WaitForEndOfFrame();
		}
		tutorialHand.HideAllAnimators();
		stayInLoop = true;
		while (stayInLoop)
		{
			if (currentStep != Step.ShopTutorialPart5)
			{
				stayInLoop = false;
			}
			yield return new WaitForEndOfFrame();
		}
		tutorialPopUpShop.PlayOutro();
		ShopUI.instance.ShowCinemaBars(1f);
		Vector3 previousPosition = ShopCart.instance.camera.transform.localPosition;
		ShopCart.instance.camera.transform.DOLocalMove(new Vector3(-1.223f, -1.111f, -10f), 1f);
		ShopCart.instance.transform.DOMove(new Vector3(0f, ShopCart.instance.transform.position.y, ShopCart.instance.transform.position.z), 0.5f);
		yield return new WaitForSeconds(1f);
		Camera uiCamera = ShopUI.instance.GetComponent<Camera>();
		Vector3 worldPoint = uiCamera.ScreenToWorldPoint(ShopCart.instance.camera.WorldToScreenPoint(firstFigBox.transform.position));
		worldPoint.z = 0f;
		tutorialHand.transform.position = worldPoint;
		tutorialHand.transform.localScale = new Vector3(-1f, 1f, 1f);
		tutorialHand.PlayTapHand();
		firstFigBox.GetComponent<BoxCollider>().enabled = true;
		while (currentStep == Step.ShopTutorialPart6)
		{
			yield return new WaitForEndOfFrame();
		}
		tutorialHand.HideAllAnimators();
		ShopUI.instance.HideCinemaBars(1f);
		ShopCart.instance.camera.gameObject.transform.DOLocalMove(previousPosition, 1f);
		while (currentStep == Step.ShopTutorialPart7)
		{
			yield return new WaitForEndOfFrame();
		}
		Player.instance.playerStats.numFiguresPurchased++;
		yield return StartCoroutine(ShopController.instance.PlayTT2Dialogue("CONVO_TUTORIAL_FIRSTSHOP_02"));
		CompleteStep(Step.ShopTutorialPart1);
		CompleteStep(Step.ShopTutorialPart2);
		CompleteStep(Step.ShopTutorialPart3);
		CompleteStep(Step.ShopTutorialPart4);
		CompleteStep(Step.ShopTutorialPart5);
		CompleteStep(Step.ShopTutorialPart6);
		CompleteStep(Step.ShopTutorialPart7);
		CompleteStep(Step.ShopTutorialPart8);
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}

	private IEnumerator CityBattleNPCTutorial()
	{
		UnityEngine.Debug.Log("City Battle Tutorial, 1");
		GameUI.instance.HideAll();
		GameUI.instance.HideAllLandmarksBut(null);
		CityController.instance.communicator.pauseTime = true;
		CityCharacter.FirstTimeCheck();
		NPCDefinition npcDef = GameManager.instance.npcDatabase.GetNPCDefByType((CityCharacter.characterType == CityCharacterType.Robin) ? CharacterName.BeastBoy : CharacterName.Robin);
		if (Player.instance.HasDefeatedNPC(npcDef.guid))
		{
			CompleteStep(Step.CityBattleNPC);
			SetTutorial(Step.CityBattleNPCPart2);
		}
		Player.instance.SetTrigger("SecondBattleTutorial");
		yield return new WaitForEndOfFrame();
	}

	private IEnumerator CityBattleNPCTutorialPart2()
	{
		UnityEngine.Debug.Log("Core Game Tutorial, City Battle NPC Tutorial Part 2");
		CityController.instance.communicator.pauseTime = false;
		GameUI.instance.HideAllButCommunicator();
		GameUI.instance.HideAllLandmarksBut(tutorialShopBuilding2);
		tutorialShopBuilding2.GetComponent<Landmark>().icon.hideAtMinScale = false;
		CityCharacter.FirstTimeCheck();
		yield return StartCoroutine(CityController.instance.PlayTT2Dialogue("CONVO_TUTORIAL_AFTERFIRSTBATTLE"));
		CompleteStep(Step.CityBattleNPCPart2);
		GameUI.instance.ShowAll();
		yield return new WaitForEndOfFrame();
		SetTutorial(Step.ShopTutorial2Start);
	}

	private IEnumerator SecondShopTutorialCity()
	{
		_isActive = true;
		GameUI.instance.pauseButton.SetActive(false);
		yield return new WaitForEndOfFrame();
		GameUI.instance.HideAllButCommunicator();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		GameUI.instance.HideAllLandmarksBut(tutorialShopBuilding2);
		tutorialShopBuilding2.GetComponent<Landmark>().icon.hideAtMinScale = false;
		CityCharacter.FirstTimeCheck();
		GameUI.instance.EnableAllLandmarksTunnels();
		if (SaveUtility.GetInt("GAME_PLAYED_FIRSTMAP_TUTORIAL") == 1)
		{
			GameUI.instance.ShowOnlyMapButton();
			_isActive = false;
			GameUI.instance.pauseButton.SetActive(!_isActive);
			yield break;
		}
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.communicator.pauseTime = true;
		GameUI.instance.ShowOnlyMapButton();
		while (CityController.instance.communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		tutorialHand.transform.position = new Vector3(CityController.instance.magnifyingGlassObj.transform.position.x, CityController.instance.magnifyingGlassObj.transform.position.y, -100f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		tutorialPopUp.text.text = Language.Get("CITY_TUTORIAL_1", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(tutorialHand.transform.position.x - 120f, tutorialHand.transform.position.y - 330f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		GameUI.instance.DisableAllLandmarks();
		CityController.instance.LandmarkWayPointDown(null);
		while (!CityController.instance.pinchZoomer.isZoomedOut)
		{
			yield return new WaitForEndOfFrame();
		}
		Communicator.instance.SetCommunicatorShown(false);
		GameUI.instance.HideMySquad();
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.pinchZoomer.allowPan = false;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
		GameUI.instance.DisableAllLandmarkCollidersExcept(null);
		CityController.instance.PreventManualWayPoint(true);
		tutorialPopUp.PlayOutro();
		CityController.instance.cameraHolder.transform.DOMove(tutorialShopBuilding2.transform.position, 0.6f);
		yield return new WaitForSeconds(0.6f);
		tutorialPopUp.text.text = Language.Get("TUTORIAL_FIRSTMAP_1", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x, 10f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowBottomLeftTail();
		tutorialPopUp.PlayIntro();
		tutorialHand.transform.position = new Vector3(GUICamera.instance.transform.position.x, 0f, -30f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		yield return new WaitForSeconds(0.8f);
		bool stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialPopUp.PlayOutro();
		tutorialHand.HideAllAnimators();
		Landmark shopLandMark = tutorialShopBuilding2.GetComponent<Landmark>();
		GameUI.instance.DisableAllLandmarkCollidersExcept(shopLandMark.icon);
		yield return new WaitForSeconds(0.1f);
		tutorialPopUp.text.text = Language.Get("TUTORIAL_FIRSTMAP_2", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x - 120f, -240f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		tutorialHand.transform.position = new Vector3(GUICamera.instance.transform.position.x, 75f, -30f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		tutorialHand.PlayTapHand();
		yield return new WaitForSeconds(0.8f);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (shopLandMark != null && shopLandMark.icon != null && shopLandMark.icon.animator.IsName("Landmarkicon_select"))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialPopUp.PlayOutro();
		tutorialHand.HideAllAnimators();
		GameUI.instance.DisableAllLandmarkCollidersExcept(null);
		yield return new WaitForEndOfFrame();
		CityController.instance.PreventManualWayPoint(true);
		shopLandMark.icon.boxCollider.enabled = false;
		yield return new WaitForSeconds(0.6f);
		shopLandMark.icon.boxCollider.enabled = false;
		tutorialHand.transform.position = new Vector3(CityController.instance.magnifyingGlassObj.transform.position.x, CityController.instance.magnifyingGlassObj.transform.position.y, -100f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		tutorialPopUp.text.text = Language.Get("TUTORIAL_FIRSTMAP_3", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(tutorialHand.transform.position.x - 120f, tutorialHand.transform.position.y - 330f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		while (CityController.instance.pinchZoomer.isZoomedOut)
		{
			yield return new WaitForEndOfFrame();
		}
		GameUI.instance.HideAllLandmarksBut(tutorialShopBuilding2);
		tutorialPopUp.PlayOutro();
		tutorialHand.HideAllAnimators();
		yield return new WaitForSeconds(0.6f);
		Communicator.instance.SetCommunicatorShown(true);
		GameUI.instance.ShowMySquad();
		while (CityController.instance.communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		Vector3 popUpPos = shopLandMark.icon.transform.position;
		popUpPos.x -= 121f;
		popUpPos.y -= 240f;
		popUpPos.z = -20f;
		tutorialPopUp.text.text = Language.Get("TUTORIAL_FIRSTMAP_4", "TUTORIALS");
		tutorialPopUp.transform.position = popUpPos;
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		yield return new WaitForSeconds(0.6f);
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowPan = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		CityController.instance.cityCharacter.animator.GetComponent<Landmark>().icon.transform.parent.gameObject.SetActive(true);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialPopUp.PlayOutro();
		CityController.instance.PreventManualWayPoint(false);
		shopLandMark.icon.boxCollider.enabled = true;
		CityController.instance.cityCharacter.AllowInput = true;
		_isActive = false;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		SaveUtility.SetInt("GAME_PLAYED_FIRSTMAP_TUTORIAL", 1, false);
	}

	private IEnumerator SecondShopTutorial()
	{
		currentStep = Step.ShopTutorial2CheckOut;
		UnityEngine.Debug.Log("Second Shop Tutorial Start");
		tk2dUIItem homeUIItem = ShopUI.instance.homeButton.GetComponentInChildren<tk2dUIItem>();
		tk2dUIItem retryUIItem = ShopUI.instance.retryButton.GetComponentInChildren<tk2dUIItem>();
		tk2dSprite[] homeSprites = ShopUI.instance.homeButton.GetComponentsInChildren<tk2dSprite>();
		tk2dSprite[] retrySprites = ShopUI.instance.retryButton.GetComponentsInChildren<tk2dSprite>();
		Color gray = Color.gray;
		Color white = Color.white;
		homeUIItem.enabled = false;
		retryUIItem.enabled = false;
		for (int i = 0; i < homeSprites.Length; i++)
		{
			homeSprites[i].color = gray;
		}
		for (int j = 0; j < retrySprites.Length; j++)
		{
			retrySprites[j].color = gray;
		}
		preventBuyButton = true;
		preventToyboxShelfReturn = true;
		preventMoveToCart = true;
		tutorialPopUpShop.transform.parent = ShopUI.instance.AnchorUpperCenter;
		tutorialPopUpShop.transform.localPosition = new Vector3(0f, -100f, 10f);
		tutorialPopUpShop.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
		tutorialPopUpShop.text.text = Language.Get("CHOOSE_NEXT_FIG", "TUTORIALS");
		tutorialPopUpShop.PlayIntro();
		ShopController.instance.FigureTossed += HandleFigureTossed;
		while (ShopController.instance.currentFigureObject == null)
		{
			yield return null;
		}
		while (!ShopController.instance.viewingFigure)
		{
			yield return null;
		}
		tutorialPopUpShop.text.text = Language.Get("COMPARE_FIGS", "TUTORIALS");
		if (!tutorialPopUpShop.IsActive)
		{
			tutorialPopUpShop.PlayIntro();
		}
		retryUIItem.enabled = false;
		for (int k = 0; k < retrySprites.Length; k++)
		{
			retrySprites[k].color = gray;
		}
		yield return new WaitForSeconds(0.5f);
		tutorialHand.transform.position = new Vector3(51f, -80f, -20f);
		tutorialHand.transform.localScale = new Vector3(-1f, 1f, 1f);
		tutorialHand.PlaySwipeHandFigureShop();
		bool continueSequence = false;
		while (!continueSequence)
		{
			if (ShopController.instance.currentFigureObject != null && ShopCart.instance.rotatingTransform.localEulerAngles.y > 170f && ShopCart.instance.rotatingTransform.localEulerAngles.y < 190f)
			{
				continueSequence = true;
			}
			yield return null;
		}
		tutorialHand.HideAllAnimators();
		tutorialPopUpShop.PlayOutro();
		preventBuyButton = false;
		preventToyboxShelfReturn = false;
		preventMoveToCart = false;
		ShopUI.instance.DisplayBuyButton();
		retryUIItem.enabled = true;
		for (int l = 0; l < retrySprites.Length; l++)
		{
			retrySprites[l].color = white;
		}
		continueSequence = false;
		Action continueAction = () =>
		{
			continueSequence = true;
		};
		ShopUI.instance.ReceiptShown += continueAction;
		yield return new WaitUntil(() => continueSequence);
		ShopUI.instance.ReceiptShown -= continueAction;
		ShopUI.instance.receiptTowerButton.GetComponent<tk2dUIItem>().enabled = false;
		tk2dSprite[] towerBtnSprites = ShopUI.instance.receiptTowerButton.GetComponentsInChildren<tk2dSprite>();
		for (int num = 0; num < towerBtnSprites.Length; num++)
		{
			towerBtnSprites[num].color = gray;
		}
		homeUIItem.enabled = true;
		retryUIItem.enabled = true;
		for (int num2 = 0; num2 < homeSprites.Length; num2++)
		{
			homeSprites[num2].color = white;
		}
		for (int num3 = 0; num3 < retrySprites.Length; num3++)
		{
			retrySprites[num3].color = white;
		}
	}

	private void HandleFigureTossed(ShopToyBox toyBox)
	{
		ShopController.instance.FigureTossed -= HandleFigureTossed;
		tutorialPopUpShop.PlayOutro();
		tk2dUIItem componentInChildren = ShopUI.instance.retryButton.GetComponentInChildren<tk2dUIItem>();
		tk2dSprite[] componentsInChildren = ShopUI.instance.retryButton.GetComponentsInChildren<tk2dSprite>();
		componentInChildren.enabled = true;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].color = Color.white;
		}
	}

	private IEnumerator CityTutorialViewMap()
	{
		yield return new WaitForEndOfFrame();
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		yield return new WaitForSeconds(0.25f);
		GameUI.instance.titanTowerButton.GetComponent<BoxCollider2D>().enabled = false;
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		CityController.instance.HandleShowDialogue((Conversation)Resources.Load("Data/TT2/CONVO_TUTORIAL_SECONDSHOP"), false);
		yield return new WaitForSeconds(0.4f);
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && (DialogueTree.instance.isConverstationPlaying || DialogueTree.instance.gameObject.activeInHierarchy))
			{
				yield return null;
			}
		}
		yield return new WaitForSeconds(1f);
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && (DialogueTree.instance.isConverstationPlaying || DialogueTree.instance.gameObject.activeInHierarchy))
			{
				yield return null;
			}
		}
		_isActive = true;
		GameUI.instance.pauseButton.SetActive(!_isActive);
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.communicator.pauseTime = true;
		CityController.instance.PreventManualWayPoint(true);
		GameUI.instance.titanTowerButton.GetComponent<BoxCollider2D>().enabled = false;
		tk2dBaseSprite[] towerSprites = GameUI.instance.titanTowerButton.transform.GetComponentsInChildren<tk2dBaseSprite>();
		tk2dBaseSprite[] array = towerSprites;
		foreach (tk2dBaseSprite tk2dBaseSprite2 in array)
		{
			tk2dBaseSprite2.color = new Color(0.6f, 0.6f, 0.6f, 1f);
		}
		while (CityController.instance.communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		Communicator.instance.SetCommunicatorShown(false);
		GameUI.instance.HideMySquad();
		tutorialHand.transform.position = new Vector3(CityController.instance.magnifyingGlassObj.transform.position.x, CityController.instance.magnifyingGlassObj.transform.position.y, -100f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		tutorialPopUp.text.text = Language.Get("CITY_TUTORIAL_1", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(tutorialHand.transform.position.x - 120f, tutorialHand.transform.position.y - 330f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowTopRightTail();
		tutorialPopUp.PlayIntro();
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		while (!CityController.instance.pinchZoomer.isZoomedOut)
		{
			yield return new WaitForEndOfFrame();
		}
		GameUI.instance.DisableAllLandmarkCollidersExcept(null);
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.pinchZoomer.allowPan = false;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
		tutorialPopUp.PlayOutro();
		CityController.instance.cameraHolder.transform.DOMove(tutorialGothamLandmark.transform.position, 0.6f);
		yield return new WaitForSeconds(0.6f);
		tutorialPopUp.text.text = Language.Get("TUTORIAL_MAP_IMPORTANT", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x, 10f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowBottomLeftTail();
		tutorialPopUp.PlayIntro();
		tutorialHand.transform.position = new Vector3(GUICamera.instance.transform.position.x, 0f, -30f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		yield return new WaitForSeconds(0.8f);
		bool stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialHand.HideAllAnimators();
		CityController.instance.cameraHolder.transform.DOMove(tutorialMetropolisLandmark.transform.position, 0.6f);
		yield return new WaitForSeconds(0.6f);
		tutorialPopUp.text.text = Language.Get("TUTORIAL_MAP_IMPORTANT", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x, 10f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowBottomLeftTail();
		tutorialHand.transform.position = new Vector3(GUICamera.instance.transform.position.x, 0f, -30f);
		tutorialHand.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		tutorialHand.PlayArrowBounce();
		yield return new WaitForSeconds(0.8f);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialPopUp.PlayOutro();
		tutorialHand.HideAllAnimators();
		CityController.instance.cameraHolder.transform.DOMove(tutorialShopBuilding.transform.position, 0.6f);
		yield return new WaitForSeconds(0.6f);
		tutorialPopUp.text.text = Language.Get("CITY_TUTORIAL_3", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x, 0f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowBottomLeftTail();
		tutorialPopUp.PlayIntro();
		yield return new WaitForSeconds(0.8f);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialHand.HideAllAnimators();
		tutorialPopUp.PlayOutro();
		if (tutorialMissionNPC != null)
		{
			CityController.instance.cameraHolder.transform.DOMove(tutorialMissionNPC.transform.position, 0.6f);
		}
		yield return new WaitForSeconds(0.6f);
		tutorialPopUp.text.text = Language.Get("CITY_TUTORIAL_4", "TUTORIALS");
		tutorialPopUp.transform.position = new Vector3(GUICamera.instance.transform.position.x, 0f, -20f);
		tutorialPopUp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		tutorialPopUp.ShowBottomLeftTail();
		tutorialPopUp.PlayIntro();
		yield return new WaitForSeconds(0.8f);
		stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		tutorialPopUp.PlayOutro();
		tutorialHand.HideAllAnimators();
		yield return new WaitForSeconds(0.6f);
		Communicator.instance.SetCommunicatorShown(true);
		GameUI.instance.ShowMySquad();
		CityController.instance.PreventManualWayPoint(false);
		GameUI.instance.EnableAllLandmarkColliders();
		GameUI.instance.titanTowerButton.GetComponent<BoxCollider2D>().enabled = true;
		tk2dBaseSprite[] array2 = towerSprites;
		foreach (tk2dBaseSprite tk2dBaseSprite3 in array2)
		{
			tk2dBaseSprite3.color = new Color(1f, 1f, 1f, 1f);
		}
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowPan = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		CityController.instance.magnifyingGlassObj.GetComponent<tk2dUIItem>().SimulateClick();
		CityController.instance.communicator.pauseTime = false;
		CompleteStep(Step.CityTutorialViewMapStart);
		CityController.instance.cityCharacter.AllowInput = true;
		SetTutorial(Step.CityTutorialAfterMap);
		CompleteStep(Step.CityTutorialAfterMap);
		_isActive = false;
		GameUI.instance.pauseButton.SetActive(!_isActive);
	}

	public bool CheckToyBoxTouched(string characterName)
	{
		if (characterName.Equals(shopCharacterNameTarget) && currentStep == Step.ShopTutorialPart6)
		{
			LockAllToyBoxes();
			currentStep = Step.ShopTutorialPart7;
			return true;
		}
		if (currentStep == Step.ShopTutorial2ChooseNextFigure)
		{
			currentStep = Step.ShopTutorial2SwipeToBack;
			return false;
		}
		return false;
	}

	public bool CheckRevealButtonNext()
	{
		if (currentStep == Step.ShopTutorialPart7)
		{
			currentStep = Step.ShopTutorialPart8;
			return true;
		}
		return false;
	}

	public void CheckToyBoxTossed(string characterName)
	{
		if (currentStep == Step.ShopTutorialPart4)
		{
			currentStep = Step.ShopTutorialPart5;
		}
		else if (currentStep == Step.ShopTutorialPart5)
		{
			if (characterName.Equals(shopCharacterNameAboveTop))
			{
				hasTossedTop = true;
			}
			if (characterName.Equals(shopCharacterNameAboveTarget) && hasTossedTop)
			{
				currentStep = Step.ShopTutorialPart6;
			}
		}
	}

	public bool PreventToyBoxTossed(string characterName)
	{
		if (currentStep == Step.ShopTutorialPart5)
		{
			if (characterName.Equals(shopCharacterNameTarget))
			{
				return true;
			}
			if (characterName.Equals(shopCharacterNameAboveTarget) && !hasTossedTop)
			{
				return true;
			}
		}
		return false;
	}

	public void NotifyHideToyBox(CharacterDefinition charDef)
	{
		if (currentStep == Step.ShopTutorialPart5)
		{
			if (charDef.characterName.Equals(shopCharacterNameAboveTarget))
			{
				currentStep = Step.ShopTutorialPart6;
			}
			if (charDef.characterName.Equals(shopCharacterNameAboveTop))
			{
				hasTossedTop = true;
			}
		}
	}

	public bool PreventShopToyBoxInitialTouch()
	{
		return currentStep == Step.ShopTutorialPart1 || currentStep == Step.ShopTutorialPart2 || currentStep == Step.ShopTutorial2Start;
	}

	public bool PreventShopToyBoxTouchOn(string characterName)
	{
		if (currentStep == Step.ShopTutorialPart6)
		{
			return !characterName.Equals(shopCharacterNameTarget);
		}
		if (currentStep == Step.ShopTutorialPart5 && characterName.Equals(shopCharacterNameTarget))
		{
			return true;
		}
		return false;
	}

	public bool PreventShopToyBoxTouch()
	{
		return currentStep == Step.ShopTutorialPart1 || currentStep == Step.ShopTutorialPart2 || currentStep == Step.ShopTutorialPart3 || currentStep == Step.ShopTutorialPart4 || currentStep == Step.ShopTutorialPart8 || currentStep == Step.ShopTutorial2Start;
	}

	public bool PreventShopToyBoxMovement()
	{
		return currentStep == Step.ShopTutorialPart1 || currentStep == Step.ShopTutorialPart2 || currentStep == Step.ShopTutorialPart3 || currentStep == Step.ShopTutorialPart6 || currentStep == Step.ShopTutorialPart8 || currentStep == Step.ShopTutorial2Start;
	}

	public bool PreventShopToyBoxToss()
	{
		return currentStep == Step.ShopTutorialPart1 || currentStep == Step.ShopTutorialPart2 || currentStep == Step.ShopTutorialPart3 || currentStep == Step.ShopTutorialPart6 || currentStep == Step.ShopTutorialPart7 || currentStep == Step.ShopTutorialPart8;
	}

	public bool PreventShopShelfMovement()
	{
		return currentStep == Step.ShopTutorialPart1 || currentStep == Step.ShopTutorialPart3 || currentStep == Step.ShopTutorialPart6 || currentStep == Step.ShopTutorialPart8 || currentStep == Step.ShopTutorial2Start || currentStep == Step.ShopTutorial2ChooseNextFigure || currentStep == Step.ShopTutorial2SwipeToFront || currentStep == Step.ShopTutorial2SwipeToBack || currentStep == Step.ShopTutorial2PlaceInCart || currentStep == Step.ShopTutorial2Finished;
	}

	public bool PreventShopToyBoxMoveBackToHanger()
	{
		if (currentStep == Step.ShopTutorial2SwipeToFront || currentStep == Step.ShopTutorial2SwipeToBack || preventToyboxShelfReturn)
		{
			return true;
		}
		return false;
	}

	public bool PreventShopToyBoxMoveToCart()
	{
		if (currentStep == Step.ShopTutorialPart5 || currentStep == Step.ShopTutorial2SwipeToFront || currentStep == Step.ShopTutorial2SwipeToBack || preventMoveToCart)
		{
			return true;
		}
		return false;
	}

	public bool preventBuyButtonDisplay()
	{
		if (currentStep == Step.ShopTutorialPart5 || currentStep == Step.ShopTutorial2ChooseNextFigure || currentStep == Step.ShopTutorial2SwipeToFront || currentStep == Step.ShopTutorial2SwipeToBack || preventBuyButton)
		{
			return true;
		}
		return false;
	}

	public void CheckOutButtonPressed()
	{
		if (currentStep == Step.ShopTutorial2CheckOut)
		{
			CompleteStep(Step.ShopTutorial2Start);
			CompleteStep(Step.ShopTutorial2ChooseNextFigure);
			CompleteStep(Step.ShopTutorial2SwipeToFront);
			CompleteStep(Step.ShopTutorial2SwipeToBack);
			CompleteStep(Step.ShopTutorial2PlaceInCart);
			CompleteStep(Step.ShopTutorial2CheckOut);
			CompleteStep(Step.ShopTutorial2Finished);
			currentStep = Step.ShopTutorial2Finished;
		}
	}

	private void LockAllToyBoxes()
	{
		for (int i = 0; i < ShopController.instance.allToyBoxes.Count; i++)
		{
			ShopToyBox shopToyBox = ShopController.instance.allToyBoxes[i];
			if (shopToyBox != null)
			{
				shopToyBox.DestroyHingeJoint();
			}
		}
	}

	public bool CheckShouldShowMissionIndicators()
	{
		MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(4);
		return missionCompletedData != null;
	}

	public bool CheckShouldAllowCoinSpawn()
	{
		MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(4);
		return missionCompletedData != null;
	}

	public void SkipAllTutorials()
	{
		string[] names = Enum.GetNames(typeof(Step));
		for (int i = 1; i < names.Length; i++)
		{
			SaveUtility.SetInt(names[i] + "_Complete", 1, false);
		}
		SaveUtility.Save();
	}
}
