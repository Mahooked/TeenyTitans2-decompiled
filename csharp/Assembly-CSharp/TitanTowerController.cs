using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TouchScript.Debugging;
using UnityEngine;

public class TitanTowerController : MonoBehaviour
{
	public const string TOP_MENU = "topMenu";

	public const string CHARACTER_SELECT = "characterSelect";

	public const string CHARACTER_VIEW = "characterView";

	public const string THE_LIBRARY = "theLibrary";

	public const string THE_LIBRARY_CHARACTER_VIEW = "theLibraryCharacterView";

	public const string AWARDS_VIEW = "awardsView";

	public const string COMBINE_SCREEN = "combineScreen";

	public const string MOD_CHIP_SCREEN = "modChipScreen";

	public const string ACCESSORIES_SELECT = "accessoriesSelect";

	public const string ACCESSORIES_VIEW = "accessoriesView";

	public static bool fromChallengeScene;

	public static bool fromLegionOfDoomScene;

	public TouchDebugger touchDebugger;

	public tk2dSprite backgroundSprite;

	public GameObject topMenu;

	public Animator topMenuAnimator;

	public tk2dUIItem toyboxButton;

	public tk2dUIItem libraryButton;

	public tk2dUIItem awardsButton;

	public tk2dUIItem accessoriesButton;

	public CharacterSelect characterSelect;

	public CharacterView characterView;

	public TheLibrary theLibrary;

	public TheLibraryCharacterView theLibraryCharacterView;

	public AwardsView awardsView;

	public AccessorySelect accessorySelect;

	public AccessoriesView accessoriesView;

	public AccessoryEquipBar accessoryEquipBar;

	public CombineScreen combineScreen;

	public ModChipScreen modChipScreen;

	public AbilityCustomizationGUI abilityCustomization;

	public MySquad mySquad;

	public tk2dUIItem exitButton;

	public tk2dUIItem riddlerShopButton;

	public GameObject achievementsButton;

	public AchievementScreen achievementScreen;

	public CurrencyGUI currencyGUI;

	public ModelViewer modelViewer;

	public ModelViewerCamera modelViewerCamera;

	public tk2dCamera guiCamera2;

	public tk2dCamera guiCamera3;

	public tk2dCamera guiCamera4;

	public tk2dCamera guiCamera4b;

	public AudioSource viewerTheme;

	public float speed = 5f;

	public float gravity = -0.5f;

	public float rotationSpeed = 1f;

	public float lengthMultiplier = 0.75f;

	private string curPage;

	private string prevPage = string.Empty;

	private List<string> prevPages;

	private Vector3 prevModelRotation = Vector3.zero;

	private bool startedExitToCity;

	private static TitanTowerController _instance;

	public bool ignoreExitButtonFunction { get; set; }

	public string CurPage => curPage;

	public static TitanTowerController instance => _instance;

	private void Awake()
	{
		_instance = this;
		if (touchDebugger == null)
		{
			touchDebugger = UnityEngine.Object.FindObjectOfType<TouchDebugger>();
		}
		viewerTheme.Stop();
		curPage = "topMenu";
		prevPages = new List<string>();
		if (!mySquad.gameObject.activeSelf)
		{
			mySquad.gameObject.SetActive(value: true);
		}
		if (Application.platform != RuntimePlatform.IPhonePlayer && !Application.isEditor)
		{
			achievementsButton.SetActive(value: false);
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	private IEnumerator Start()
	{
		mySquad.gameObject.SetActive(value: false);
		accessoryEquipBar.gameObject.SetActive(value: false);
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		viewerTheme.Play();
		if (touchDebugger != null)
		{
			touchDebugger.gameObject.SetActive(value: false);
		}
		characterView.CharacterChanged += HandleCharacterChanged;
		characterView.viewInLibraryButton.OnClick += HandleOnViewInLibraryClick;
		characterView.modChipButton.uiItem.OnClick += HandleModChipButtonClick;
		characterView.modChipSlot.uiItem.OnClick += HandleModChipButtonClick;
		modChipScreen.lowerLeftAnchor.ForceUpdateTransform();
		modChipScreen.lowerCenterAnchor.ForceUpdateTransform();
		modChipScreen.lowerRightAnchor.ForceUpdateTransform();
		currencyGUI.Init(Player.instance);
		SwipeManager.Instance.SwipeOccurred += HandleSwipeOccurred;
	}

	private void HandleSwipeOccurred(SwipeResult swipeResult)
	{
		float num = Mathf.Atan2(swipeResult.end2D.y - swipeResult.start2D.y, swipeResult.end2D.x - swipeResult.start2D.x) * 57.29578f;
		if (num < 60f || num > 120f)
		{
			return;
		}
		Collider2D[] array = Physics2D.OverlapPointAll(GUICamera.instance.camera.ScreenToWorldPoint(swipeResult.start2D), 1 << LayerMask.NameToLayer("UI"));
		if (array.Length <= 0)
		{
			return;
		}
		int num2 = array.Length;
		MySquadSlot mySquadSlot = null;
		for (int i = 0; i < num2; i++)
		{
			mySquadSlot = array[i].GetComponent<MySquadSlot>();
			if (mySquadSlot != null)
			{
				break;
			}
		}
		if (mySquadSlot != null && mySquadSlot.uiItem.enabled && mySquadSlot.character != null && Player.instance.activeParty.Count > 1)
		{
			TossPortrait(mySquadSlot, swipeResult.direction2D, swipeResult.length);
			Player.instance.RemoveFromParty(mySquadSlot.character);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_unequipchar", GFAudioManager.SFX_GROUP, string.Empty);
		}
		AccessorySlot accessorySlot = null;
		for (int j = 0; j < num2; j++)
		{
			accessorySlot = array[j].GetComponent<AccessorySlot>();
			if (accessorySlot != null)
			{
				break;
			}
		}
		if (accessorySlot != null && accessorySlot.uiItem.enabled && accessorySlot.accessoryItemDefinition != null)
		{
			TossAccessory(accessorySlot, swipeResult.direction2D, swipeResult.length);
			Player.instance.RemoveFromEquippedAccessories(accessorySlot.accessoryItemDefinition.uid);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_unequipchar", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void TossPortrait(MySquadSlot slot, Vector2 direction, float length)
	{
		float xSpeed = direction.x * speed * length * lengthMultiplier;
		float ySpeed = direction.y * speed * length * lengthMultiplier;
		float rotationRate = direction.x * rotationSpeed;
		MySquadSlot mySquadSlot = UnityEngine.Object.Instantiate(slot);
		mySquadSlot.backingSprite.gameObject.SetActive(value: false);
		mySquadSlot.transform.position = slot.transform.position;
		mySquadSlot.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI4"));
		mySquadSlot.gameObject.AddComponent<TossAway>().Init(gravity, xSpeed, ySpeed, rotationRate, -500f);
	}

	private void TossAccessory(AccessorySlot slot, Vector2 direction, float length)
	{
		float xSpeed = direction.x * speed * length * lengthMultiplier;
		float ySpeed = direction.y * speed * length * lengthMultiplier;
		float rotationRate = direction.x * rotationSpeed;
		AccessorySlot accessorySlot = UnityEngine.Object.Instantiate(slot);
		accessorySlot.transform.position = slot.transform.position;
		accessorySlot.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI4"));
		accessorySlot.gameObject.AddComponent<TossAway>().Init(gravity, xSpeed, ySpeed, rotationRate, -500f);
	}

	private void OnEnable()
	{
		Input.multiTouchEnabled = false;
		toyboxButton.OnClickUIItem += HandleOnTopMenuClick;
		libraryButton.OnClickUIItem += HandleOnTopMenuClick;
		awardsButton.OnClickUIItem += HandleOnTopMenuClick;
		accessoriesButton.OnClickUIItem += HandleOnTopMenuClick;
		exitButton.OnClick += HandleOnExitClick;
		riddlerShopButton.OnClick += HandleOnRiddlerShopClick;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_firstbuttonsappear", GFAudioManager.SFX_GROUP, string.Empty);
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void OnDisable()
	{
		toyboxButton.OnClickUIItem -= HandleOnTopMenuClick;
		libraryButton.OnClickUIItem -= HandleOnTopMenuClick;
		awardsButton.OnClickUIItem -= HandleOnTopMenuClick;
		accessoriesButton.OnClickUIItem -= HandleOnTopMenuClick;
		exitButton.OnClick -= HandleOnExitClick;
		riddlerShopButton.OnClick -= HandleOnRiddlerShopClick;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
	}

	public void ShowPage(string pageName, bool isBack = false)
	{
		if (curPage == pageName)
		{
			return;
		}
		if (!isBack)
		{
			prevPages.Add(curPage);
		}
		switch (curPage)
		{
		case "topMenu":
			topMenu.SetActive(value: false);
			riddlerShopButton.gameObject.SetActive(value: false);
			break;
		case "characterSelect":
			characterSelect.gameObject.SetActive(value: false);
			break;
		case "characterView":
			characterView.gameObject.SetActive(value: false);
			if (pageName.Equals("characterSelect"))
			{
				characterView.Init(null, null, null);
			}
			break;
		case "theLibrary":
			theLibrary.gameObject.SetActive(value: false);
			break;
		case "theLibraryCharacterView":
			theLibraryCharacterView.gameObject.SetActive(value: false);
			break;
		case "awardsView":
			awardsView.gameObject.SetActive(value: false);
			break;
		case "combineScreen":
			combineScreen.gameObject.SetActive(value: false);
			break;
		case "modChipScreen":
			modChipScreen.gameObject.SetActive(value: false);
			break;
		case "accessoriesSelect":
			accessorySelect.gameObject.SetActive(value: false);
			break;
		case "accessoriesView":
			accessoriesView.gameObject.SetActive(value: false);
			break;
		}
		ShowPageName(pageName);
		prevPage = curPage;
		curPage = pageName;
	}

	public void HideAllPages()
	{
		topMenu.SetActive(value: false);
		characterSelect.gameObject.SetActive(value: false);
		characterView.gameObject.SetActive(value: false);
		theLibrary.gameObject.SetActive(value: false);
		theLibraryCharacterView.gameObject.SetActive(value: false);
		combineScreen.gameObject.SetActive(value: false);
		modChipScreen.gameObject.SetActive(value: false);
		awardsView.gameObject.SetActive(value: false);
		accessorySelect.gameObject.SetActive(value: false);
		accessoriesView.gameObject.SetActive(value: false);
		riddlerShopButton.gameObject.SetActive(value: false);
	}

	public void ShowPageName(string pageName, bool animate = true)
	{
		switch (pageName)
		{
		case "topMenu":
			topMenu.SetActive(value: true);
			riddlerShopButton.gameObject.SetActive(value: true);
			modelViewer.gameObject.SetActive(value: false);
			mySquad.gameObject.SetActive(value: false);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			if (Application.isPlaying)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_firstbuttonsappear", GFAudioManager.SFX_GROUP, string.Empty);
			}
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, Color.white, 0.35f).SetEase(Ease.InOutSine);
			}
			break;
		case "characterSelect":
			characterSelect.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: true);
			modelViewer.gameObject.SetActive(value: false);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			characterSelect.Init(Player.instance);
			characterSelect.pageController.CharacterSelected -= HandleCharacterSelected;
			characterSelect.pageController.CharacterSelected += HandleCharacterSelected;
			mySquad.Init(Player.instance);
			mySquad.CharacterSelected -= HandleCharacterSelected;
			mySquad.CharacterSelected += HandleCharacterSelected;
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, Color.white, 0.35f).SetEase(Ease.InOutSine);
			}
			break;
		case "characterView":
			characterView.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: true);
			modelViewer.gameObject.SetActive(value: true);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			mySquad.Init(Player.instance);
			mySquad.CharacterSelected -= HandleCharacterSelected;
			mySquad.CharacterSelected += HandleCharacterSelected;
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1.25f, 1.25f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(-90f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, Color.white, 0.35f).SetEase(Ease.InOutSine);
			}
			DOTween.Kill(modelViewer.transform);
			DOTween.Kill(modelViewerCamera.transform);
			if (curPage != null && (curPage.Equals("modChipScreen") || curPage.Equals("theLibraryCharacterView") || curPage.Equals("combineScreen")))
			{
				modelViewer.transform.DOLocalMove(new Vector3(-0.82f, 0.115f, 1.15f), 0.25f).SetEase(Ease.OutSine);
				modelViewer.transform.DOLocalRotate(prevModelRotation, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					modelViewer.autoRotate = true;
				});
				modelViewerCamera.transform.DOMove(new Vector3(0f, 0f, -3.5f), 0.25f).SetEase(Ease.OutSine);
				characterView.MoveFigureToCharViewPos();
			}
			else
			{
				modelViewer.transform.localPosition = new Vector3(-0.82f, 0.115f, 1.15f);
				modelViewer.transform.localRotation = Quaternion.identity;
				modelViewer.autoRotate = true;
				modelViewerCamera.transform.position = new Vector3(0f, 0f, -3.5f);
			}
			break;
		case "theLibrary":
			theLibrary.timeEnabled = Time.unscaledTime;
			theLibrary.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			modelViewer.gameObject.SetActive(value: false);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			theLibrary.Init(Player.instance);
			theLibrary.pageController.CharacterDefinitionSelected -= HandleCharacterDefinitionSelected;
			theLibrary.pageController.CharacterDefinitionSelected += HandleCharacterDefinitionSelected;
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, new Color(0.5f, 0.5f, 0.5f), 0.35f).SetEase(Ease.InOutSine);
			}
			break;
		case "theLibraryCharacterView":
			theLibraryCharacterView.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			modelViewer.gameObject.SetActive(value: true);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, new Color(0.5f, 0.5f, 0.5f), 0.35f).SetEase(Ease.InOutSine);
			}
			DOTween.Kill(modelViewer.transform);
			DOTween.Kill(modelViewerCamera.transform);
			if (curPage != null && curPage.Equals("characterView"))
			{
				prevModelRotation = modelViewer.transform.localRotation.eulerAngles;
				modelViewer.transform.DOMove(Vector3.zero, 0.25f).SetEase(Ease.OutSine);
				modelViewer.transform.DOLocalRotate(new Vector3(352f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
				modelViewerCamera.transform.DOMove(new Vector3(0f, -0.6f, -4.18f), 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					modelViewerCamera.enabled = true;
				});
				characterView.MoveFigureToLibraryPos();
			}
			else
			{
				modelViewer.transform.position = Vector3.zero;
				modelViewer.transform.localRotation = Quaternion.Euler(352f, 0f, 0f);
				modelViewerCamera.enabled = true;
			}
			break;
		case "awardsView":
			awardsView.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			modelViewer.gameObject.SetActive(value: true);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, new Color(0.5f, 0.5f, 0.5f), 0.35f).SetEase(Ease.InOutSine);
			}
			DOTween.Kill(modelViewer.transform);
			DOTween.Kill(modelViewerCamera.transform);
			modelViewer.transform.position = Vector3.zero;
			modelViewer.transform.localRotation = Quaternion.Euler(352f, 0f, 0f);
			modelViewerCamera.enabled = true;
			awardsView.Init();
			break;
		case "combineScreen":
			combineScreen.timeEnabled = Time.unscaledTime;
			combineScreen.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			topMenu.SetActive(value: false);
			modelViewer.gameObject.SetActive(value: true);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			if (Application.isPlaying)
			{
				combineScreen.Init(Player.instance);
				DOTween.Kill(modelViewer.transform);
				DOTween.Kill(modelViewerCamera.transform);
				Camera cameraForLayer = CameraUtility.GetCameraForLayer(1 << combineScreen.feedButton.gameObject.layer);
				Vector3 position = cameraForLayer.WorldToScreenPoint(combineScreen.modelTargetPosition.position);
				Plane plane = new Plane(Vector3.back, new Vector3(0f, 0f, 3.93f));
				Ray ray = modelViewerCamera.camera.ScreenPointToRay(position);
				plane.Raycast(ray, out var enter);
				Vector3 point = ray.GetPoint(enter);
				if (curPage.Equals("characterView"))
				{
					prevModelRotation = modelViewer.transform.localRotation.eulerAngles;
					modelViewer.autoRotate = false;
					modelViewer.transform.DOMove(point, 0.25f).SetEase(Ease.OutSine);
					modelViewer.transform.DOLocalRotate(new Vector3(339f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
				}
				else
				{
					modelViewer.autoRotate = false;
					modelViewer.transform.position = point;
					modelViewer.transform.localRotation = Quaternion.Euler(339f, 0f, 0f);
				}
			}
			break;
		case "modChipScreen":
			modChipScreen.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			modelViewer.gameObject.SetActive(value: true);
			this.accessoryEquipBar.gameObject.SetActive(value: false);
			modChipScreen.Init(Player.instance, characterView.character);
			if (animate)
			{
				DOTween.Kill(modelViewer.transform);
				modelViewer.autoRotate = false;
				modelViewer.transform.DOLocalMove(new Vector3(0f, 0.75f, 1.15f), 0.25f).SetEase(Ease.OutSine);
				modelViewer.transform.DOLocalRotate(new Vector3(90f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
			}
			prevModelRotation = modelViewer.transform.localRotation.eulerAngles;
			break;
		case "accessoriesSelect":
			accessorySelect.gameObject.SetActive(value: true);
			modelViewer.gameObject.SetActive(value: false);
			mySquad.gameObject.SetActive(value: false);
			this.accessoryEquipBar.gameObject.SetActive(value: true);
			if (Application.isPlaying)
			{
				accessorySelect.Init(Player.instance);
				accessorySelect.accessoryPageController.AccessorySelected -= HandleAccessorySelected;
				accessorySelect.accessoryPageController.AccessorySelected += HandleAccessorySelected;
				this.accessoryEquipBar.Init(Player.instance);
				AccessoryEquipBar accessoryEquipBar3 = this.accessoryEquipBar;
				accessoryEquipBar3.AccessorySelected = (Action<AccessoryItemDefinition>)Delegate.Remove(accessoryEquipBar3.AccessorySelected, new Action<AccessoryItemDefinition>(HandleAccessorySelected));
				AccessoryEquipBar accessoryEquipBar4 = this.accessoryEquipBar;
				accessoryEquipBar4.AccessorySelected = (Action<AccessoryItemDefinition>)Delegate.Combine(accessoryEquipBar4.AccessorySelected, new Action<AccessoryItemDefinition>(HandleAccessorySelected));
			}
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, Color.white, 0.35f).SetEase(Ease.InOutSine);
			}
			break;
		case "accessoriesView":
		{
			accessoriesView.gameObject.SetActive(value: true);
			modelViewer.gameObject.SetActive(value: true);
			mySquad.gameObject.SetActive(value: false);
			this.accessoryEquipBar.gameObject.SetActive(value: true);
			this.accessoryEquipBar.Init(Player.instance);
			AccessoryEquipBar accessoryEquipBar = this.accessoryEquipBar;
			accessoryEquipBar.AccessorySelected = (Action<AccessoryItemDefinition>)Delegate.Remove(accessoryEquipBar.AccessorySelected, new Action<AccessoryItemDefinition>(HandleAccessorySelected));
			AccessoryEquipBar accessoryEquipBar2 = this.accessoryEquipBar;
			accessoryEquipBar2.AccessorySelected = (Action<AccessoryItemDefinition>)Delegate.Combine(accessoryEquipBar2.AccessorySelected, new Action<AccessoryItemDefinition>(HandleAccessorySelected));
			if (animate)
			{
				DOTween.To(() => backgroundSprite.transform.parent.localScale, (Vector3 x) =>
				{
					backgroundSprite.transform.parent.localScale = x;
				}, new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
				backgroundSprite.transform.parent.DOLocalMoveY(0f, 0.25f).SetEase(Ease.OutSine);
				DOTween.To(() => backgroundSprite.color, (Color x) =>
				{
					backgroundSprite.color = x;
				}, Color.white, 0.35f).SetEase(Ease.InOutSine);
			}
			modelViewer.transform.localPosition = Vector3.zero;
			modelViewer.transform.localRotation = Quaternion.identity;
			modelViewer.autoRotate = true;
			modelViewerCamera.transform.position = new Vector3(0f, -0.7f, -3.5f);
			break;
		}
		}
	}

	private void HandleCharacterSelected(Character character)
	{
		characterView.Init(character, characterSelect.pageController.masterListCharacters, Player.instance);
		ShowPage("characterView");
	}

	private void HandleAccessorySelected(AccessoryItemDefinition accessoryItemDefinition)
	{
		ShowPage("accessoriesView");
		List<AccessoryItemDefinition> list = Player.instance.accessories.ConvertAll((int x) => (AccessoryItemDefinition)GameManager.instance.itemDatabase.GetItem(x));
		list.RemoveAll((AccessoryItemDefinition x) => !x.showInTower);
		accessoriesView.Init(accessoryItemDefinition, list, Player.instance);
	}

	private void HandleCharacterChanged(Character character)
	{
		if (character != null)
		{
			character.isViewed = true;
		}
		combineScreen.character = character;
		modChipScreen.character = character;
	}

	private void HandleCharacterDefinitionSelected(CharacterDefinition character)
	{
		theLibraryCharacterView.Init(character);
		ShowPage("theLibraryCharacterView");
	}

	private void HandleOnTopMenuClick(tk2dUIItem button)
	{
		if (button == toyboxButton)
		{
			ShowPage("characterSelect");
		}
		else if (button == libraryButton)
		{
			ShowPage("theLibrary");
		}
		else if (button == awardsButton)
		{
			ShowPage("awardsView");
		}
		else if (button == accessoriesButton)
		{
			ShowPage("accessoriesSelect");
		}
	}

	private void HandleOnViewInLibraryClick()
	{
		theLibraryCharacterView.Init(characterView.character.characterDefinition, TheLibraryCharacterView.ViewType.FromCharacterView, characterView.character.uid);
		ShowPage("theLibraryCharacterView");
	}

	private void HandleModChipButtonClick()
	{
		if (characterView.character.modChipUnlocked)
		{
			ShowPage("modChipScreen");
		}
	}

	private void HandleCombineScreenClick()
	{
		ShowPage("combineScreen");
	}

	private void HandleOnExitClick()
	{
		if (ignoreExitButtonFunction)
		{
			return;
		}
		if (prevPages.Count == 0)
		{
			if (!startedExitToCity)
			{
				startedExitToCity = true;
				Player.instance.Save();
				if (fromChallengeScene)
				{
					fromChallengeScene = false;
					LoadingScreenCover.LoadScene("Challenge");
				}
				else if (fromLegionOfDoomScene)
				{
					fromLegionOfDoomScene = false;
					LoadingScreenCover.LoadScene("LegionOfDoomMode");
				}
				else
				{
					LoadingScreenCover.LoadScene(CityController.previousCityScene);
				}
			}
		}
		else
		{
			ShowPage(prevPages.Pop(), isBack: true);
		}
	}

	private void HandleOnRiddlerShopClick()
	{
		if (!startedExitToCity)
		{
			startedExitToCity = true;
			Player.instance.Save();
			LoadingScreenCover.LoadScene("RiddlerShop");
		}
	}

	public void HandleAchievementsClicked()
	{
		SocialSystem.ShowAchievementsUI();
		exitButton.gameObject.SetActive(value: false);
		modelViewer.gameObject.SetActive(value: false);
		modelViewerCamera.enabled = false;
		modelViewerCamera.camera.enabled = false;
		this.achievementScreen.gameObject.SetActive(value: true);
		this.achievementScreen.Init();
		AchievementScreen achievementScreen = this.achievementScreen;
		achievementScreen.Closed = (Action)Delegate.Combine(achievementScreen.Closed, new Action(HandleAchievementScreenClosed));
	}

	private void HandleAchievementScreenClosed()
	{
		AchievementScreen achievementScreen = this.achievementScreen;
		achievementScreen.Closed = (Action)Delegate.Remove(achievementScreen.Closed, new Action(HandleAchievementScreenClosed));
		exitButton.gameObject.SetActive(value: true);
		modelViewer.gameObject.SetActive(value: true);
		modelViewerCamera.enabled = false;
		modelViewerCamera.camera.enabled = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			Debug.Log("Create Test Battle Request...");
			if (Player.instance.pvpData.friends.Count > 0)
			{
				BattleRequest battleRequest = new BattleRequest();
				battleRequest.playerId = Player.instance.pvpData.friends[0].playerID;
				battleRequest.region = "US";
				battleRequest.roomName = "TestRoom";
				BattleRequest battleRequest2 = battleRequest;
				Player.instance.pvpData.AddBattleRequest(battleRequest2);
			}
			else
			{
				Debug.LogError("You need at least 1 friend to test a battle request...");
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			Debug.Log("Create Test Friend Request...");
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.playerId = FriendCodeGenerator.Generate();
			friendRequest.playerName = "TestFriend" + UnityEngine.Random.Range(0, 101);
			friendRequest.level = UnityEngine.Random.Range(1, GameManager.instance.legionOfDoomLevelTable.MaxLevel + 1);
			friendRequest.portraitSpriteName = GameManager.instance.legionOfDoomSettings.GetRandomPortraitSprite();
			friendRequest.skillRating = UnityEngine.Random.Range(1000, 4001);
			FriendRequest friendRequest2 = friendRequest;
			Player.instance.pvpData.AddFriendRequest(friendRequest2);
		}
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		startedExitToCity = true;
		Player.instance.Save();
		fromChallengeScene = false;
		fromLegionOfDoomScene = false;
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
