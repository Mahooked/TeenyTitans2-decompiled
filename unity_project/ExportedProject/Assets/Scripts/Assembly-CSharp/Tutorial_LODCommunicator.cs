using System.Collections;
using UnityEngine;

public class Tutorial_LODCommunicator : Tutorial
{
	public Vector3 communicatorRightSpriteBubbleOffset;

	public Vector3 communicatorLODBubbleOffset;

	public Vector3 communicatorLODScreenOffset;

	private TutorialStep[] _tutorialSteps = new TutorialStep[1]
	{
		new TutorialStep("LOD Com")
	};

	public override TutorialStep[] tutorialSteps
	{
		get
		{
			return _tutorialSteps;
		}
		set
		{
			_tutorialSteps = value;
		}
	}

	public override bool isGameStateValid
	{
		get
		{
			return CityController.instance != null && !TitleScreenController.instance.titleScreenActive && (DialogueTree.instance == null || !DialogueTree.instance.gameObject.activeInHierarchy) && !CityController.instance.communicator.MissionScrollerActive() && !CityController.instance.pauseScreen.gameObject.activeSelf && !GameUI.instance.showingCinemaBars && !CityReward.instance.showingRewards && CityController.instance.cityCharacter.AllowInput;
		}
	}

	public override IEnumerator PlayTutorial(int stepIndex)
	{
		Debug.Log("Play LOD Communicator Tutorial...");
		yield return null;
		CityController cityController = CityController.instance;
		cityController.cityCharacter.AllowInput = false;
		cityController.PreventManualWayPoint(true);
		cityController.communicator.pauseTime = true;
		cityController.cityCharacter.StopMove();
		cityController.pinchZoomer.allowPinchZoom = false;
		cityController.pinchZoomer.allowPan = false;
		cityController.pinchZoomer.allowDoubleClickZoomIn = false;
		cityController.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		GameUI.instance.HideMySquad();
		GameUI.instance.HideAllLandmarksBut(null);
		GameUI.instance.pauseButton.SetActive(false);
		GameUI.instance.magnifyingGlass.SetActive(false);
		GameUI.instance.titanTowerButton.SetActive(false);
		GameUI.instance.wallet.SetActive(false);
		tk2dUIItem dayNightBtn = Communicator.instance.dayNight.GetComponentInChildren<tk2dUIItem>();
		tk2dUIItem mailBtn = Communicator.instance.mail.GetComponentInChildren<tk2dUIItem>();
		tk2dUIItem missionBtn = Communicator.instance.missions.GetComponentInChildren<tk2dUIItem>();
		tk2dUIItem lodBtn = Communicator.instance.legionOfDoom.GetComponentInChildren<tk2dUIItem>();
		tk2dUIItem leftNavBtn = Communicator.instance.leftBtnSprite.GetComponent<tk2dUIItem>();
		tk2dUIItem rightNavBtn = Communicator.instance.rightBtnSprite.GetComponent<tk2dUIItem>();
		dayNightBtn.enabled = false;
		mailBtn.enabled = false;
		missionBtn.enabled = false;
		PauseManager.instance.isEnabled = false;
		PopUpBubble popUpBubble = ObjectPool.GetObjectFromResource<PopUpBubble>("Prefabs/GUI/PopUpBubble");
		popUpBubble.SetLayer(LayerMask.NameToLayer("UI"));
		Camera guiCamera2 = GameUI.instance.guiCamera2;
		if (!Communicator.instance.legionOfDoom.activeInHierarchy)
		{
			popUpBubble.Init(targetWorldPosition: Communicator.instance.rightBtnSprite.transform.position + communicatorRightSpriteBubbleOffset, message: Language.Get("TUTORIAL_LOD_1", "TUTORIALS"), targetCamera: guiCamera2, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
			popUpBubble.transform.parent = Communicator.instance.rightBtnSprite.transform;
			yield return new WaitUntil(() => Communicator.instance.legionOfDoom.activeSelf);
			leftNavBtn.enabled = false;
			rightNavBtn.enabled = false;
			yield return popUpBubble.WaitUntilIntroComplete();
			popUpBubble.Init(targetWorldPosition: Communicator.instance.legionOfDoom.transform.position + communicatorLODBubbleOffset, message: Language.Get("TUTORIAL_LOD_2", "TUTORIALS"), targetCamera: guiCamera2, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
			popUpBubble.transform.parent = Communicator.instance.legionOfDoom.transform;
		}
		else
		{
			leftNavBtn.enabled = false;
			rightNavBtn.enabled = false;
			popUpBubble.Init(targetWorldPosition: Communicator.instance.legionOfDoom.transform.position + communicatorLODBubbleOffset, message: Language.Get("TUTORIAL_LOD_1_1", "TUTORIALS"), targetCamera: guiCamera2, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
			popUpBubble.transform.parent = Communicator.instance.legionOfDoom.transform;
		}
		yield return popUpBubble.WaitUntilIntroComplete();
		yield return new WaitUntil(() => Communicator.instance.lodPopup.activeInHierarchy);
		popUpBubble.OutroBubble();
		lodBtn.enabled = false;
		yield return new WaitUntil(() => !Communicator.instance.lodCityMenu.mainMenuAnimator.IsName("communicator_emailpopup_intro"));
		popUpBubble.InitWithoutTail(targetWorldPosition: Communicator.instance.lodPopup.transform.position + communicatorLODScreenOffset, message: Language.Get("TUTORIAL_LOD_3", "TUTORIALS"), targetCamera: guiCamera2, forcedBubbleType: PopUpBubble.BubbleType.Normal, flipTailX: true);
		popUpBubble.transform.parent = Communicator.instance.lodPopup.transform;
		yield return new WaitForTap();
		popUpBubble.OutroBubble();
		yield return popUpBubble.WaitUntilOutroComplete();
		ObjectPool.PoolObject(popUpBubble.gameObject);
		cityController.cityCharacter.AllowInput = true;
		cityController.PreventManualWayPoint(false);
		cityController.communicator.pauseTime = false;
		cityController.pinchZoomer.allowPinchZoom = true;
		cityController.pinchZoomer.allowPan = true;
		cityController.pinchZoomer.allowDoubleClickZoomIn = true;
		cityController.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		GameUI.instance.ShowMySquad();
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.pauseButton.SetActive(true);
		GameUI.instance.magnifyingGlass.SetActive(true);
		GameUI.instance.titanTowerButton.SetActive(true);
		GameUI.instance.wallet.SetActive(true);
		dayNightBtn.enabled = true;
		mailBtn.enabled = true;
		missionBtn.enabled = true;
		lodBtn.enabled = true;
		leftNavBtn.enabled = true;
		rightNavBtn.enabled = true;
		PauseManager.instance.isEnabled = true;
	}

	public override void CleanUp()
	{
	}
}
