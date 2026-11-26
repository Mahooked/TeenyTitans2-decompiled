using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombineScreen : MonoBehaviour
{
	public Animator animator;

	public PageController pageController;

	public tk2dUIToggleButtonGroup sortButtons;

	public Transform modelTargetPosition;

	public tk2dUIItem feedButton;

	public GameObject rankUpScreen;

	public Animator slotAnimator;

	public tk2dUIItem slotCharButton;

	public TextMeshPro slotCharLevelText;

	public tk2dSprite slotCharPortrait;

	public tk2dSprite slotChip;

	public tk2dSprite abilityBackingSprite;

	public tk2dSprite abilityIconSprite;

	public tk2dSlicedSprite currentShininess;

	public tk2dSlicedSprite previewShininess;

	public tk2dUIItem silkieButton;

	public AEControllerTk2d silkieAnimator;

	public AEControllerTk2d silkieBurpAnimator;

	public ParticleSystem silkieSleepParticle;

	public Transform silkieHelpBubble;

	public tk2dUIItem backButton;

	public Material rankNormalFontMaterial;

	public Material rankUpFontMaterial;

	public List<RankUpAbilityButton> rankUpAbilityButtons;

	public TextMeshPro rankUpAbilityName;

	public TextMeshPro rankUpAbilityDescription;

	public TextMeshPro rankUpAbilityEffectDescription;

	public TextMeshPro rankUpAbilityShinyDescription;

	public GameObject shinyBar;

	public Transform shinyBarTopExtent;

	public Transform shinyBarBottomExtent;

	[HideInInspector]
	public float timeEnabled;

	public SimpleTextButton fillBarButton;

	private float fullShininess = 212f;

	private float targetFullness;

	private float targetPreviewFullness;

	private float lastRankUpTime;

	private int shinyPointsRank1 = 100;

	private int shinyPointsRank2 = 150;

	private int shinyPointsRank3 = 200;

	private int okShinyPoints = 15;

	private int goodShinyPoints = 25;

	private int greatShinyPoints = 50;

	private int previewPoints;

	private int currentPoints;

	private int startingRank;

	private const int OK = 0;

	private const int GOOD = 1;

	private const int GREAT = 2;

	private Vector3 modelPreviousPosition;

	private Vector3 modelPreviousRotation;

	private Camera silkieButtonCamera;

	private bool _showWarning = true;

	private Character _character;

	private Player _player;

	public Character childCombineCharacter { get; private set; }

	public SilkieCheatSheet silkieCheatSheet { get; private set; }

	public bool rankUpSequenceActive { get; private set; }

	public bool showWarning
	{
		get
		{
			return _showWarning;
		}
		set
		{
			_showWarning = value;
		}
	}

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			_character = value;
			UpdateCharacter();
		}
	}

	public Player player
	{
		get
		{
			return _player;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		pageController.Init(InitPortrait, InitFolderPortrait);
		silkieButtonCamera = CameraUtility.GetCameraForLayer(1 << silkieButton.gameObject.layer);
	}

	private void OnEnable()
	{
		pageController.DragFinished += HandleDragFinished;
		pageController.CharacterSelected += HandleCharacterSelected;
		pageController.CharacterPortraitCreated += HandleCharacterPortraitCreated;
		if (_player != null)
		{
			HandleCharacterCollectionChanged();
		}
		silkieButton.OnClick += HandleSilkieClick;
		feedButton.gameObject.SetActive(false);
		feedButton.OnClick += HandleCombineClicked;
		slotCharPortrait.gameObject.SetActive(false);
		slotChip.transform.parent.gameObject.SetActive(false);
		slotAnimator.Play("empty");
		slotCharPortrait.GetComponent<MeshRenderer>().enabled = true;
		previewShininess.dimensions = new Vector2(0f, previewShininess.dimensions.y);
		currentShininess.dimensions = new Vector2(0f, currentShininess.dimensions.y);
		silkieAnimator.PlayFromFrame("sleep", 0);
		silkieSleepParticle.emissionRate = 1f;
		previewPoints = 0;
		childCombineCharacter = null;
		Vector3 worldPositionInCamera = CameraUtility.GetWorldPositionInCamera(silkieButton.transform.position + new Vector3(24f, 10f, 0f), silkieButtonCamera, CameraUtility.GetCameraForLayer(1 << silkieHelpBubble.gameObject.layer));
		worldPositionInCamera.z = -100f;
		silkieHelpBubble.position = worldPositionInCamera;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_enterscreensnore", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void Start()
	{
		shinyBar.transform.localPosition = GetShinyBarStartPosition();
	}

	public Vector3 GetShinyBarStartPosition()
	{
		float num = Vector3.Distance(shinyBarTopExtent.position, shinyBarBottomExtent.position);
		return new Vector3(shinyBar.transform.localPosition.x, shinyBarBottomExtent.position.y + num / 2f, shinyBar.transform.localPosition.z);
	}

	public void MoveShinyBarToUpgradePosition()
	{
		shinyBar.transform.DOLocalMove(new Vector3(shinyBar.transform.localPosition.x, -12.5f, shinyBar.transform.localPosition.z), 0.6f).SetEase(Ease.OutSine).SetDelay(0.5f);
	}

	public void MoveShinyBarToStartPosition()
	{
		shinyBar.transform.DOLocalMove(GetShinyBarStartPosition(), 0.6f).SetEase(Ease.OutSine).SetDelay(0.2f);
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.CharacterCollectionChanged -= HandleCharacterCollectionChanged;
		}
		_player = null;
	}

	private void OnDisable()
	{
		pageController.DragFinished -= HandleDragFinished;
		pageController.CharacterSelected -= HandleCharacterSelected;
		pageController.CharacterPortraitCreated -= HandleCharacterPortraitCreated;
		silkieButton.OnClick -= HandleSilkieClick;
		feedButton.OnClick -= HandleCombineClicked;
	}

	private void Update()
	{
		if (silkieCheatSheet != null && Input.GetMouseButtonUp(0))
		{
			Collider2D collider2D = Physics2D.OverlapPoint(silkieButtonCamera.ScreenToWorldPoint(Input.mousePosition));
			if (collider2D != null)
			{
				tk2dUIItem component = collider2D.GetComponent<tk2dUIItem>();
				if (component == null || component != silkieButton)
				{
					HideCheatSheet();
				}
			}
			else
			{
				HideCheatSheet();
			}
		}
		if (_character == null || rankUpScreen.activeSelf)
		{
			return;
		}
		targetFullness = fullShininess * ((float)currentPoints / (float)CurrentRankShinyPointsNeeded());
		if (currentShininess.dimensions.x < targetFullness)
		{
			currentShininess.dimensions = new Vector2(Mathf.Clamp(EaseOut(currentShininess.dimensions.x, targetFullness, -6f), 0f, targetFullness), currentShininess.dimensions.y);
		}
		else if (currentShininess.dimensions.x > targetFullness)
		{
			currentShininess.dimensions = new Vector2(Mathf.Max(0f, EaseOut(currentShininess.dimensions.x, targetFullness - 1f, -6f)), currentShininess.dimensions.y);
		}
		targetPreviewFullness = fullShininess * ((float)Mathf.Min(CurrentRankShinyPointsNeeded(), previewPoints) / (float)CurrentRankShinyPointsNeeded());
		if (previewShininess.dimensions.x < targetPreviewFullness)
		{
			previewShininess.dimensions = new Vector2(Mathf.Clamp(EaseOut(previewShininess.dimensions.x, targetPreviewFullness, -6f), 0f, targetPreviewFullness), previewShininess.dimensions.y);
		}
		else if (previewShininess.dimensions.x > targetPreviewFullness)
		{
			previewShininess.dimensions = new Vector2(Mathf.Max(0f, EaseOut(previewShininess.dimensions.x, targetPreviewFullness - 1f, -6f)), previewShininess.dimensions.y);
		}
		if (currentShininess.dimensions.x < 26f)
		{
			currentShininess.SetBorder(0f, 0f, 0f, 0f);
		}
		else
		{
			currentShininess.SetBorder(1890f / 4095f, 0f, 15f / 26f, 0f);
		}
		if (previewShininess.dimensions.x < 26f)
		{
			previewShininess.SetBorder(0f, 0f, 0f, 0f);
		}
		else
		{
			previewShininess.SetBorder(1890f / 4095f, 0f, 2520f / 4095f, 0f);
		}
		if (startingRank != _character.shinyRank && _character.shinyRank > startingRank)
		{
			rankUpSequenceActive = true;
			if (currentShininess.dimensions.x >= targetFullness)
			{
				Camera.main.depth = 5f;
				previewPoints = 0;
				lastRankUpTime = Time.time;
				startingRank = _character.shinyRank;
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.orderedSlots[_character.shinyRank - 1].abilityId);
				rankUpAbilityName.text = Language.Get(ability.abilityNameKey, "ABILS,CHIPS,ACHVS,LDBS");
				rankUpAbilityDescription.text = Language.Get(ability.abilityDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS");
				rankUpAbilityEffectDescription.text = TTGParseHelper.GetAbilityEffectText(ability, _character);
				rankUpAbilityShinyDescription.text = TTGParseHelper.GetShinyEffectDescription(ability, _character);
				rankUpScreen.SetActive(true);
				animator.Play((_character.shinyRank == 1) ? "CombineScreen_RankUp_Intro" : ((_character.shinyRank != 2) ? "CombineScreen_RankUp3_Intro" : "CombineScreen_RankUp2_Intro"));
				MoveShinyBarToUpgradePosition();
				modelPreviousPosition = TitanTowerController.instance.modelViewer.transform.position;
				modelPreviousRotation = TitanTowerController.instance.modelViewer.transform.localRotation.eulerAngles;
				Sequence sequence = DOTween.Sequence();
				sequence.Insert(0f, TitanTowerController.instance.modelViewer.transform.DOMove(new Vector3(0f, 1.32f, 1.83f), 0.75f).SetEase(Ease.InOutSine));
				sequence.Insert(0f, TitanTowerController.instance.modelViewer.transform.DOLocalRotate(new Vector3(347f, 0f, 0f), 0.75f).SetEase(Ease.InOutSine));
				sequence.Play();
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_rankupsequence", GFAudioManager.SFX_GROUP, string.Empty, 0.85f);
			}
		}
	}

	private int ShinyPointsForTier(int tier, int level)
	{
		switch (tier)
		{
		case 0:
			return okShinyPoints + (level - 1);
		case 1:
			return goodShinyPoints + (level - 1);
		case 2:
			return greatShinyPoints + (level - 1);
		default:
			return okShinyPoints + (level - 1);
		}
	}

	private int CurrentRankShinyPointsNeeded()
	{
		if (startingRank <= 0)
		{
			return shinyPointsRank1;
		}
		if (startingRank == 1)
		{
			return shinyPointsRank2;
		}
		if (startingRank >= 2)
		{
			return shinyPointsRank3;
		}
		return shinyPointsRank1;
	}

	public void Init(Player player)
	{
		if (player != _player)
		{
			if (_player != null)
			{
				_player.CharacterCollectionChanged -= HandleCharacterCollectionChanged;
			}
			_player = player;
			if (_player != null)
			{
				_player.CharacterCollectionChanged += HandleCharacterCollectionChanged;
				HandleCharacterCollectionChanged();
			}
		}
	}

	public void EnableSortButtons(bool enable = true)
	{
		int num = sortButtons.ToggleBtns.Length;
		for (int i = 0; i < num; i++)
		{
			sortButtons.ToggleBtns[i].uiItem.enabled = enable;
		}
	}

	private void HandleCharacterCollectionChanged()
	{
		List<Character> list = new List<Character>(_player.characters);
		list.Remove(_character);
		list.Sort((Character x, Character y) =>
		{
			int uid = _character.characterDefinition.uid;
			CharacterClass characterClass = _character.characterClass;
			if (x.characterDefinition.uid == uid && y.characterDefinition.uid != uid)
			{
				return -1;
			}
			if (x.characterDefinition.uid != uid && y.characterDefinition.uid == uid)
			{
				return 1;
			}
			if (x.characterDefinition.uid != uid && y.characterDefinition.uid != uid)
			{
				if (x.characterClass == characterClass && y.characterClass != characterClass)
				{
					return -1;
				}
				if (x.characterClass == characterClass && y.characterClass == characterClass)
				{
					return x.level.CompareTo(y.level);
				}
				if (x.characterClass != characterClass && y.characterClass == characterClass)
				{
					return 1;
				}
			}
			return x.level.CompareTo(y.level);
		});
		pageController.SetList(list, PageController.ViewMode.Single);
		pageController.UpdatePages();
	}

	public void ForceUpdatePageControllerList()
	{
		HandleCharacterCollectionChanged();
	}

	private void HandleSilkieClick()
	{
		ShowCheatSheet();
	}

	private void ShowCheatSheet()
	{
		if (silkieCheatSheet != null)
		{
			HideCheatSheet();
			return;
		}
		silkieCheatSheet = ObjectPool.GetObjectFromResource<SilkieCheatSheet>("Prefabs/GUI/SilkieCheatSheet", default(Vector3), false);
		silkieCheatSheet.Init(_character);
		Vector3 worldPositionInCamera = CameraUtility.GetWorldPositionInCamera(silkieButton.transform.position + new Vector3(24f, 10f, 0f), silkieButtonCamera, CameraUtility.GetCameraForLayer(1 << silkieCheatSheet.gameObject.layer));
		worldPositionInCamera.z = -100f;
		silkieCheatSheet.transform.position = worldPositionInCamera;
		silkieCheatSheet.gameObject.SetActive(true);
		silkieCheatSheet.transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.15f).From().SetEase(Ease.OutBack);
		silkieHelpBubble.gameObject.SetActive(false);
	}

	private void HideCheatSheet()
	{
		if (silkieCheatSheet != null)
		{
			DOTween.Kill(silkieCheatSheet.transform);
			ObjectPool.PoolObject(silkieCheatSheet.gameObject);
			silkieCheatSheet = null;
			silkieHelpBubble.gameObject.SetActive(true);
		}
	}

	private void UpdateCharacter()
	{
		if (_character == null)
		{
			return;
		}
		List<AbilitySlot> orderedSlots = _character.characterDefinition.orderedSlots;
		if (orderedSlots.Count > 0)
		{
			AbilityData abilityData = null;
			int count = rankUpAbilityButtons.Count;
			for (int i = 0; i < count; i++)
			{
				abilityData = ((orderedSlots.Count <= i) ? null : GameManager.instance.abilityDatabase.GetAbility(orderedSlots[i].abilityId));
				rankUpAbilityButtons[i].Init(abilityData);
			}
		}
		startingRank = _character.shinyRank;
		if (_character.shinyRank >= 3)
		{
			currentPoints = CurrentRankShinyPointsNeeded();
		}
		else
		{
			currentPoints = _character.shinyPoints;
		}
		previewPoints = 0;
		if (_character.shinyRank < 3)
		{
			if (_character.characterDefinition.orderedSlots.Count > _character.shinyRank)
			{
				AbilityData ability = GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.orderedSlots[_character.shinyRank].abilityId);
				abilityIconSprite.SetSprite(ability.iconSprite);
				if (ability.castTime > 0f)
				{
					abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_goldcast");
				}
				else if (ability.numChargesRequired > 0)
				{
					abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple_gold");
				}
				else if (ability.channelingData.speed > 0f)
				{
					abilityBackingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel_gold");
				}
				else
				{
					abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_gold");
				}
			}
		}
		else
		{
			AbilityData ability2 = GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.orderedSlots.GetLastItem().abilityId);
			abilityIconSprite.SetSprite(ability2.iconSprite);
			if (ability2.castTime > 0f)
			{
				abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_goldcast");
			}
			else if (ability2.numChargesRequired > 0)
			{
				abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple_gold");
			}
			else if (ability2.channelingData.speed > 0f)
			{
				abilityBackingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel_gold");
			}
			else
			{
				abilityBackingSprite.SetSprite("TTG_battlebar_abilityicon_backing_gold");
			}
		}
	}

	private void UpdateCombineSlotWithCharacter(Character character)
	{
		if (_character != character)
		{
			if (_character.shinyRank >= 3)
			{
				currentPoints = CurrentRankShinyPointsNeeded();
			}
			else if (character != childCombineCharacter)
			{
				slotAnimator.Play("combinescreen_fig_intro", 0, 0f);
				slotCharPortrait.gameObject.SetActive(true);
				slotCharPortrait.GetComponent<MeshRenderer>().enabled = true;
				slotCharLevelText.text = Language.Get("LVL", "MENUS,GUI") + " " + character.level;
				slotCharLevelText.color = ((character.level < character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
				slotCharPortrait.SetSprite(character.characterDefinition.portraitSprite);
				slotChip.transform.parent.gameObject.SetActive(true);
				feedButton.gameObject.SetActive(true);
				silkieSleepParticle.emissionRate = 0f;
				UpdatePreviewBarWithCombineCharacter(character);
			}
		}
	}

	private void PlaySilkieIdle(string idleName)
	{
		if (!silkieAnimator.currentClip.name.Contains("idle"))
		{
			silkieAnimator.Play(idleName + "_intro", true);
			silkieAnimator.AddToQueue(idleName);
		}
		else
		{
			silkieAnimator.Play(idleName);
		}
	}

	private void UpdatePreviewBarWithCombineCharacter(Character character)
	{
		childCombineCharacter = character;
		if (_character.characterDefinition.characterClass.Equals(character.characterDefinition.characterClass))
		{
			if (_character.characterDefinition.Equals(character.characterDefinition))
			{
				previewPoints = _character.shinyPoints + ShinyPointsForTier(2, childCombineCharacter.level);
				PlaySilkieIdle("idle3");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_figureplacelevel3", GFAudioManager.SFX_GROUP, string.Empty);
			}
			else
			{
				previewPoints = _character.shinyPoints + ShinyPointsForTier(1, childCombineCharacter.level);
				PlaySilkieIdle("idle2");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_figureplacelevel2", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
		else
		{
			previewPoints = _character.shinyPoints + ShinyPointsForTier(0, childCombineCharacter.level);
			PlaySilkieIdle("idle1");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_figureplacelevel1", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private IEnumerator StartCombineRoutine()
	{
		rankUpSequenceActive = true;
		backButton.enabled = false;
		pageController.dragEnabled = false;
		pageController.portraitSelectionEnabled = false;
		pageController.dragNavigationEnabled = false;
		pageController.dotNavigationEnabled = false;
		CityCharacterVOSet voSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
		GFAudioManager.instance.PlayClipFromResources(voSet.enhanceFeedClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		if (silkieAnimator.currentClip.name.Contains("idle1"))
		{
			silkieBurpAnimator.PlayFromFrame("burp1", 0, true);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_burplevel1", GFAudioManager.SFX_GROUP, string.Empty);
		}
		if (silkieAnimator.currentClip.name.Contains("idle2"))
		{
			silkieBurpAnimator.PlayFromFrame("burp2", 0, true);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_burplevel2", GFAudioManager.SFX_GROUP, string.Empty);
		}
		if (silkieAnimator.currentClip.name.Contains("idle3"))
		{
			silkieBurpAnimator.PlayFromFrame("burp3", 0, true);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_burplevel3", GFAudioManager.SFX_GROUP, string.Empty);
		}
		yield return new WaitForSeconds(1f);
		slotCharPortrait.gameObject.SetActive(false);
		_player.RemoveCharacterFromAll(childCombineCharacter);
		TitanTowerController.instance.characterSelect.ForceUpdatePageControllerList();
		yield return new WaitForSeconds(1f);
		int currentRankShinyPointsNeeded = CurrentRankShinyPointsNeeded();
		currentPoints = Mathf.Min(currentRankShinyPointsNeeded, previewPoints);
		_character.shinyPoints = previewPoints;
		if (_character.shinyPoints >= currentRankShinyPointsNeeded)
		{
			_character.shinyPoints = Mathf.Max(0, _character.shinyPoints - currentRankShinyPointsNeeded);
			_character.shinyRank = Mathf.Min(3, _character.shinyRank + 1);
			if (_character.shinyRank >= 3)
			{
				AchievementsDatabase.UnlockAchievementId("TT2_SECONDSFORSILKIE");
			}
			CommunicatorManager.instance.UpdateMissionWithTargetMessage("EnhanceFig");
			GFAudioManager.instance.PlayClipFromResources(voSet.enhanceShinyClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		else
		{
			backButton.enabled = true;
			pageController.dragEnabled = true;
			pageController.portraitSelectionEnabled = true;
			pageController.dragNavigationEnabled = true;
			pageController.dotNavigationEnabled = true;
		}
		_player.Save();
		silkieSleepParticle.emissionRate = 1f;
		rankUpSequenceActive = false;
	}

	private IEnumerator ShowWarningDialog()
	{
		if (_showWarning)
		{
			PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default(Vector3), false);
			dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
			dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
			dialog.descriptionText.text = string.Format(Language.Get("FEEDTOSILKIE", "MENUS,GUI"), Language.Get(childCombineCharacter.characterDefinition.characterNameKey, "NPCS,FIGS"), childCombineCharacter.level);
			dialog.gameObject.SetActive(true);
			while (dialog.gameObject.activeSelf)
			{
				yield return null;
			}
			if (dialog.isConfirmed)
			{
				feedButton.gameObject.SetActive(false);
				StartCoroutine("StartCombineRoutine");
				silkieAnimator.Play("combine");
				silkieAnimator.AddToQueue("sleep");
				slotAnimator.Play("combinescreen_fig_suckup");
				if (Application.isPlaying)
				{
					CommunicatorManager.instance.UpdateMissionWithTargetMessage("FeedSilkie");
				}
			}
			else
			{
				ObjectPool.PoolObject(dialog.gameObject);
			}
		}
		else
		{
			feedButton.gameObject.SetActive(false);
			StartCoroutine("StartCombineRoutine");
			silkieAnimator.Play("combine");
			silkieAnimator.AddToQueue("sleep");
			slotAnimator.Play("combinescreen_fig_suckup");
		}
	}

	private void HandleCombineClicked()
	{
		StartCoroutine(ShowWarningDialog());
	}

	private void ClearCharacterToCombine()
	{
		childCombineCharacter = null;
		slotCharPortrait.gameObject.SetActive(false);
		slotChip.transform.parent.gameObject.SetActive(false);
		feedButton.gameObject.SetActive(false);
		silkieSleepParticle.emissionRate = 1f;
		silkieAnimator.Play("sleep", true);
		previewPoints = 0;
	}

	private void HandleDragFinished(CharacterSelectPortrait portrait)
	{
		Collider2D[] array = Physics2D.OverlapPointAll(pageController.upperCamera.ScreenToWorldPoint(pageController.lowerCamera.WorldToScreenPoint(portrait.transform.position)), 1 << LayerMask.NameToLayer("UI"));
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			if (array[i].gameObject.name.Equals("main_slot"))
			{
				HandleCharacterSelected(portrait.character);
			}
		}
	}

	private void HandleCharacterPortraitCreated(CharacterSelectPortrait portait)
	{
		portait.transform.position += new Vector3(0f, 0f, -100f);
	}

	private void HandleCharacterSelected(Character character)
	{
		if (_character != character)
		{
			UpdateCombineSlotWithCharacter(character);
		}
	}

	private void HandleOnSortChange(tk2dUIToggleButtonGroup group)
	{
		switch (group.SelectedIndex)
		{
		case 0:
			pageController.filteredCharacters.Sort((Character x, Character y) => y.uid.CompareTo(x.uid));
			break;
		case 1:
			pageController.filteredCharacters.Sort((Character x, Character y) => y.level.CompareTo(x.level));
			break;
		}
		pageController.UpdatePages();
	}

	private void HandleSlotClicked()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_silkiescreen_figureplaceundo", GFAudioManager.SFX_GROUP, string.Empty);
		ClearCharacterToCombine();
	}

	private void RankUpScreenNextClicked()
	{
		if (_character.shinyRank >= 3)
		{
			currentPoints = CurrentRankShinyPointsNeeded();
		}
		else
		{
			currentPoints = _character.shinyPoints;
		}
		previewPoints = 0;
		animator.Play((_character.shinyRank == 1) ? "CombineScreen_RankUp_Outro" : ((_character.shinyRank != 2) ? "CombineScreen_RankUp3_Outro" : "CombineScreen_RankUp2_Outro"));
		StartCoroutine("RankUpOutroCheck");
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, TitanTowerController.instance.modelViewer.transform.DOMove(modelPreviousPosition, 0.75f).SetEase(Ease.InOutSine));
		sequence.Insert(0f, TitanTowerController.instance.modelViewer.transform.DOLocalRotate(modelPreviousRotation, 0.75f).SetEase(Ease.InOutSine));
		sequence.Play();
	}

	private IEnumerator RankUpOutroCheck()
	{
		yield return new WaitForEndOfFrame();
		string stateName = ((_character.shinyRank == 1) ? "CombineScreen_RankUp_Outro" : ((_character.shinyRank != 2) ? "CombineScreen_RankUp3_Outro" : "CombineScreen_RankUp2_Outro"));
		MoveShinyBarToStartPosition();
		while (animator.IsName(stateName) && animator.IsPlaying())
		{
			yield return new WaitForEndOfFrame();
		}
		UpdateCharacter();
		rankUpScreen.SetActive(false);
		Camera.main.depth = 3f;
		backButton.enabled = true;
		pageController.dragEnabled = true;
		pageController.portraitSelectionEnabled = true;
		pageController.dragNavigationEnabled = true;
		pageController.dotNavigationEnabled = true;
		rankUpSequenceActive = false;
	}

	private void InitPortrait(CharacterSelectPortrait portrait, Character character, bool animate)
	{
		portrait.Init(character);
		portrait.mode = (_player.IsCharacterInParty(character) ? CharacterSelectPortraitMode.Equipped : CharacterSelectPortraitMode.Normal);
		portrait.animator.Play("Intro", 0, (!animate) ? 1 : 0);
	}

	private void InitFolderPortrait(CharacterSelectPortrait portrait, CharacterDefinition characterDefinition, int characterCount, bool animate)
	{
		portrait.InitAsFolder(characterDefinition, characterCount);
		portrait.mode = CharacterSelectPortraitMode.Normal;
		portrait.animator.Play("Intro", 0, (!animate) ? 1 : 0);
	}

	public bool IsCurrentShininessAtTarget()
	{
		return Mathf.Approximately(currentShininess.dimensions.x, targetFullness);
	}

	private float EaseOut(float current, float target, float coeff)
	{
		return target - (target - current) * Mathf.Exp(coeff * Time.deltaTime) + 0.01f;
	}
}
