using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
	private static Dictionary<CharacterClass, string> classIconSpriteNames = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"menu_characterview_classicon_beast"
		},
		{
			CharacterClass.Cute,
			"menu_characterview_classicon_cute"
		},
		{
			CharacterClass.DarkArts,
			"menu_characterview_classicon_darkarts"
		},
		{
			CharacterClass.MartialArts,
			"menu_characterview_classicon_martialarts"
		},
		{
			CharacterClass.Super,
			"menu_characterview_classicon_super"
		},
		{
			CharacterClass.Tech,
			"menu_characterview_classicon_tech"
		},
		{
			CharacterClass.None,
			"menu_characterview_classicon_none"
		}
	};

	private static Dictionary<CharacterRarity, string> rarityIconSpriteNames = new Dictionary<CharacterRarity, string>
	{
		{
			CharacterRarity.Tier1White,
			"menu_characterview_rarity_white"
		},
		{
			CharacterRarity.Tier2Green,
			"menu_characterview_rarity_green"
		},
		{
			CharacterRarity.Tier3Blue,
			"menu_characterview_rarity_blue"
		},
		{
			CharacterRarity.Tier4Purple,
			"menu_characterview_rarity_purple"
		},
		{
			CharacterRarity.Tier5Gold,
			"menu_characterview_rarity_gold"
		},
		{
			CharacterRarity.Tier6GoldPlus,
			"menu_characterview_rarity_ultrarare"
		}
	};

	public SimpleTextButton customizeButton;

	public SimpleTextButton combineButton;

	public SimpleTextButton modChipButton;

	public SimpleTextButton levelUpButton;

	public TextMeshPro nameText;

	public tk2dSprite classIconSprite;

	public tk2dUIItem classIconButton;

	public ClassBonusBox classBonusBox;

	public TextMeshPro levelText;

	public tk2dSprite xpBackingSprite;

	public tk2dSlicedSpriteHelper xpFillSprite;

	public CharacterViewShinyRank shinyRank;

	public CharacterViewAbilities abilities;

	public CharacterViewStat healthStat;

	public CharacterViewStat attackStat;

	public CharacterViewStat speedStat;

	public CharacterViewStat luckStat;

	public CharacterViewStat dodgeStat;

	public CharacterViewModChipSlot modChipSlot;

	public FavoriteButton favoriteButton;

	public tk2dUIItem viewInLibraryButton;

	public tk2dUIItem repaintButton;

	public tk2dUIItem scrapButton;

	public tk2dUIItem leftNavButton;

	public tk2dUIItem rightNavButton;

	public tk2dUIItem equipButton;

	public tk2dSprite equipArrowSprite;

	public TextMeshPro equipText;

	public tk2dSprite raritySprite;

	public ModelViewer modelViewer;

	public ModelViewerCamera modelViewerCamera;

	public float xpFillMaxWidth = 65f;

	public GameObject characterInfoGroup;

	public AbilityCustomizationGUI abilityCustomization;

	private ShopFigureModel shopFigure;

	private CharacterStatInfoBox characterStatInfoBox;

	private Camera classButtonCamera;

	private tk2dSprite repaintButtonSprite;

	public CharacterViewRepaintWindow repaintWindow;

	private AudioSource showFigSource;

	private bool playedshowFigSFX;

	private Texture currentFigureDefaultTexture;

	private Dictionary<string, Texture> characterTextureCache = new Dictionary<string, Texture>();

	private Character _character;

	private Player _player;

	private List<Character> _characterList;

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			Init(value, _characterList, _player);
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
			Init(_character, _characterList, value);
		}
	}

	public event Action<Character> CharacterChanged;

	private void Awake()
	{
		if (classBonusBox.gameObject.activeSelf)
		{
			classBonusBox.gameObject.SetActive(value: false);
		}
	}

	private void Start()
	{
		classButtonCamera = tk2dCamera.CameraForLayer(classIconButton.gameObject.layer).ScreenCamera;
	}

	private void OnEnable()
	{
		viewInLibraryButton.OnClick += HandleViewInLibraryClick;
		customizeButton.uiItem.OnClick += HandleCustomizeClick;
		repaintButton.OnClick += HandleViewRepaintColorsClick;
		scrapButton.OnClick += HandleScrapClick;
		classIconButton.OnClick += HandleClassIconClick;
		healthStat.uiItem.OnClickUIItem += HandleStatClick;
		attackStat.uiItem.OnClickUIItem += HandleStatClick;
		dodgeStat.uiItem.OnClickUIItem += HandleStatClick;
		luckStat.uiItem.OnClickUIItem += HandleStatClick;
		leftNavButton.OnClickUIItem += HandleOnNavClick;
		rightNavButton.OnClickUIItem += HandleOnNavClick;
		equipButton.OnClick += HandleOnEquipClick;
		repaintWindow.ColorSelected += HandleRepaintColorSelected;
		repaintWindow.closeButton.OnClick += HandleRepaintCloseButtonClick;
		modelViewer.xSpeed = 150f;
		modelViewer.ySpeed = 0f;
		modelViewer.useHitArea = true;
		modelViewerCamera.enabled = false;
		if (_character != null)
		{
			ShowFigure();
			HandleUpdated();
		}
	}

	private void OnDisable()
	{
		viewInLibraryButton.OnClick -= HandleViewInLibraryClick;
		customizeButton.uiItem.OnClick -= HandleCustomizeClick;
		repaintButton.OnClick -= HandleViewRepaintColorsClick;
		scrapButton.OnClick -= HandleScrapClick;
		leftNavButton.OnClickUIItem -= HandleOnNavClick;
		rightNavButton.OnClickUIItem -= HandleOnNavClick;
		equipButton.OnClick -= HandleOnEquipClick;
		repaintWindow.ColorSelected -= HandleRepaintColorSelected;
		repaintWindow.closeButton.OnClick -= HandleRepaintCloseButtonClick;
		classIconButton.OnClick -= HandleClassIconClick;
		healthStat.uiItem.OnClickUIItem -= HandleStatClick;
		attackStat.uiItem.OnClickUIItem -= HandleStatClick;
		dodgeStat.uiItem.OnClickUIItem -= HandleStatClick;
		luckStat.uiItem.OnClickUIItem -= HandleStatClick;
		repaintWindow.gameObject.SetActive(value: false);
		characterInfoGroup.SetActive(value: true);
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
	}

	private void Update()
	{
		if ((!classBonusBox.gameObject.activeSelf && !(characterStatInfoBox != null)) || !Input.GetMouseButtonUp(0))
		{
			return;
		}
		Collider2D collider2D = Physics2D.OverlapPoint(classButtonCamera.ScreenToWorldPoint(Input.mousePosition));
		if (collider2D != null)
		{
			tk2dUIItem component = collider2D.GetComponent<tk2dUIItem>();
			if (component != null)
			{
				if (component != classIconButton)
				{
					classBonusBox.gameObject.SetActive(value: false);
				}
				if (component != healthStat.uiItem && component != attackStat.uiItem && component != dodgeStat.uiItem && component != luckStat.uiItem)
				{
					HideCharacterStatInfoBox();
				}
			}
			else
			{
				classBonusBox.gameObject.SetActive(value: false);
				HideCharacterStatInfoBox();
			}
		}
		else
		{
			classBonusBox.gameObject.SetActive(value: false);
			HideCharacterStatInfoBox();
		}
	}

	private void HandleViewInLibraryClick()
	{
		Debug.Log("Open this character in library");
	}

	private void HandleViewRepaintColorsClick()
	{
		if (repaintWindow.gameObject.activeSelf)
		{
			repaintWindow.gameObject.SetActive(value: false);
			characterInfoGroup.SetActive(value: true);
		}
		else
		{
			characterInfoGroup.SetActive(value: false);
			repaintWindow.gameObject.SetActive(value: true);
		}
	}

	private void HandleRepaintColorSelected(RepaintColorButton repaintColorButton)
	{
		bool flag = _character.repaintColorsUnlocked.Contains(repaintColorButton.id) || repaintColorButton.id == -1;
		if (flag || Player.instance.HasFreeRepaint(repaintColorButton.id))
		{
			if (!flag)
			{
				Player.instance.AddToRepaintColorsPurchased(_character, repaintColorButton.id);
				Player.instance.SpendFreeRepaintSlot(repaintColorButton.id);
			}
			Player.instance.SetRepaintColorPreference(_character, repaintColorButton.id);
			RepaintFigureModel(shopFigure);
			repaintWindow.ForceRefreshButtons();
		}
	}

	private void HandleRepaintCloseButtonClick()
	{
		repaintWindow.gameObject.SetActive(value: false);
		characterInfoGroup.SetActive(value: true);
	}

	private void HandleCustomizeClick()
	{
		abilityCustomization.SetCharacter(_character);
		abilityCustomization.gameObject.SetActive(value: true);
	}

	private void HandleCombineClick()
	{
		Debug.Log("Open the Combine GUI for this character");
	}

	private void HandleScrapClick()
	{
		StartCoroutine(ShowPawnWindow());
	}

	public void Init(Character character, List<Character> characterList, Player player)
	{
		if (character == _character && characterList == _characterList && player == _player)
		{
			return;
		}
		if (character != _character)
		{
			if (_character != null)
			{
				_character.Updated -= HandleUpdated;
				RemoveFigure();
			}
			_character = character;
			favoriteButton.character = _character;
			if (_character != null)
			{
				_character.Updated += HandleUpdated;
				HandleUpdated();
				ShowFigure();
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_opencharinfopage", GFAudioManager.SFX_GROUP, string.Empty);
			}
			OnCharacterChanged(character);
		}
		if (characterList != _characterList)
		{
			_characterList = characterList;
		}
		if (player != _player)
		{
			if (_player != null)
			{
				_player.PartyChanged -= HandlePartyChanged;
			}
			_player = player;
			if (_player != null)
			{
				_player.PartyChanged += HandlePartyChanged;
				HandlePartyChanged();
			}
		}
	}

	public void EnableButtons(bool enable = true)
	{
		leftNavButton.enabled = enable;
		rightNavButton.enabled = enable;
		equipButton.enabled = enable;
		favoriteButton.toggleButton.uiItem.enabled = enable;
		repaintButton.enabled = enable;
		scrapButton.enabled = enable;
		customizeButton.uiItem.enabled = enable;
		combineButton.uiItem.enabled = enable;
		modChipButton.uiItem.enabled = enable;
		modChipSlot.uiItem.enabled = enable;
		viewInLibraryButton.enabled = enable;
		abilities.EnableButtons(enable);
		healthStat.uiItem.enabled = enable;
		attackStat.uiItem.enabled = enable;
		dodgeStat.uiItem.enabled = enable;
		luckStat.uiItem.enabled = enable;
		classIconButton.enabled = enable;
	}

	public void MoveFigureToLibraryPos()
	{
		if (shopFigure != null)
		{
			shopFigure.transform.DOLocalMove(new Vector3(0f, -1.075f, 0f), 0.25f).SetEase(Ease.OutSine);
		}
	}

	public void MoveFigureToCharViewPos()
	{
		if (shopFigure != null)
		{
			shopFigure.transform.DOLocalMove(new Vector3(0f, -0.86f, 0f), 0.25f).SetEase(Ease.OutSine);
		}
	}

	private void ShowFigure()
	{
		if (shopFigure != null)
		{
			return;
		}
		shopFigure = ObjectPool.GetObjectFromResource<ShopFigureModel>(_character.characterDefinition.shopFigurePath);
		if (shopFigure != null)
		{
			modelViewer.transform.localRotation = Quaternion.identity;
			shopFigure.Init(_character);
			shopFigure.transform.parent = modelViewer.transform;
			shopFigure.transform.localPosition = new Vector3(0f, -0.86f, 0f);
			shopFigure.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			shopFigure.transform.localScale = Vector3.one * 0.9f;
			Texture characterTexture = GetCharacterTexture(shopFigure.character.characterDefinition.characterName);
			if (characterTexture == null)
			{
				currentFigureDefaultTexture = shopFigure.renderer.material.mainTexture;
				characterTextureCache.Add(shopFigure.character.characterDefinition.characterName, currentFigureDefaultTexture);
			}
			RepaintFigureModel(shopFigure);
		}
		if (!playedshowFigSFX && (showFigSource == null || !showFigSource.isPlaying))
		{
			string clipPath = string.Empty;
			playedshowFigSFX = true;
			CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
			if (characterVOSet != null)
			{
				clipPath = ((_character.characterDefinition.characterRarity != CharacterRarity.Tier5Gold) ? characterVOSet.towerFigsClipSet.GetRandom() : characterVOSet.towerFigsRareClipSet.GetRandom());
			}
			showFigSource = GFAudioManager.instance.PlayClipFromResources(clipPath, GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
	}

	private Texture GetCharacterTexture(string characterKey)
	{
		Texture value = null;
		characterTextureCache.TryGetValue(characterKey, out value);
		return value;
	}

	private void RepaintFigureModel(ShopFigureModel model)
	{
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(model.character.uid);
		if (repaintColorPreferenceForCharacterUID <= 0)
		{
			Texture characterTexture = GetCharacterTexture(model.character.characterDefinition.characterName);
			if (characterTexture == null)
			{
				model.UpdateTexture(currentFigureDefaultTexture);
				characterTextureCache.Add(model.character.characterDefinition.characterName, currentFigureDefaultTexture);
			}
			else
			{
				model.UpdateTexture(characterTexture);
			}
		}
		if (repaintColorPreferenceForCharacterUID <= 0)
		{
			return;
		}
		RepaintColorSlot repaintColorSlotForID = model.character.characterDefinition.GetRepaintColorSlotForID(repaintColorPreferenceForCharacterUID);
		if (repaintColorSlotForID != null)
		{
			string texturePathScaled = repaintColorSlotForID.texturePathScaled;
			Texture texture = Resources.Load<Texture>(texturePathScaled);
			if (texture != null)
			{
				model.UpdateTexture(texture);
			}
		}
	}

	private void RemoveFigure()
	{
		if (shopFigure != null)
		{
			shopFigure.Init(null);
			ObjectPool.PoolObject(shopFigure.gameObject);
			shopFigure = null;
		}
	}

	private IEnumerator ShowPawnWindow()
	{
		PopupDialogPawn dialog = ObjectPool.GetObjectFromResource<PopupDialogPawn>("Prefabs/GUI/PopUP_WindowPawn", default, isActive: false);
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.descriptionText.text = Language.Get("PAWNFIGDESC", "MENUS,GUI");
		dialog.InitForCharacter(this.character);
		dialog.gameObject.SetActive(value: true);
		dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			if (_characterList.Count == 1)
			{
				_characterList = _player.characters;
				TitanTowerController.instance.characterSelect.filterButtons.SelectedIndex = 0;
			}
			int num = _characterList.IndexOf(_character);
			CommunicatorManager.instance.UpdateMissionWithTargetMessage("ScrapFig");
			num++;
			if (num >= _characterList.Count)
			{
				num = 0;
			}
			Character character = _characterList[num];
			_characterList.Remove(_character);
			_player.AddCurrency(dialog.currencyReceived);
			_player.RemoveCharacterFromAll(_character);
			_player.Save();
			this.character = character;
			TitanTowerController.instance.characterSelect.ForceUpdatePageControllerList();
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_coincollect", GFAudioManager.SFX_GROUP, string.Empty);
		}
		ObjectPool.PoolObject(dialog.gameObject);
	}

	private void HandleUpdated()
	{
		CharacterDefinition characterDefinition = _character.characterDefinition;
		nameText.text = ((!string.IsNullOrEmpty(characterDefinition.characterNameKey)) ? Language.Get(characterDefinition.characterNameKey, "NPCS,FIGS") : characterDefinition.characterName);
		classIconSprite.SetSprite(classIconSpriteNames[characterDefinition.characterClass]);
		levelText.text = Language.Get("LVL", "MENUS,GUI") + " " + _character.level;
		levelText.color = ((_character.level < _character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
		DOTween.To(() => xpFillSprite.width, (float x) =>
		{
			xpFillSprite.width = x;
		}, (!_character.isMaxLevel) ? (xpFillMaxWidth * _character.currentLevelProgress) : xpFillMaxWidth, 0.25f).SetEase(Ease.OutSine);
		if (shinyRank != null)
		{
			shinyRank.UpdateWithCharacter(_character);
		}
		abilities.Init(_character);
		healthStat.Init(_character);
		attackStat.Init(_character);
		luckStat.Init(_character);
		dodgeStat.Init(_character);
		modChipSlot.Init(_character.modChip);
		repaintWindow.Init(_character);
		HandlePartyChanged();
		customizeButton.isEnabled = _character.abilityCustomizationUnlocked;
		customizeButton.textMesh.text = ((!customizeButton.isEnabled) ? string.Format(Language.Get("UNLOCKSFIVE", "MENUS,GUI"), 10) : Language.Get("TOWER_BUTTON_CUSTOM", "MENUS,GUI"));
		combineButton.isEnabled = _character.level >= 5;
		combineButton.textMesh.text = ((!combineButton.isEnabled) ? string.Format(Language.Get("UNLOCKSFIVE", "MENUS,GUI"), 5) : Language.Get("ENHANCE", "MENUS,GUI"));
		modChipButton.isEnabled = _character.modChipUnlocked;
		modChipButton.textMesh.text = ((!modChipButton.isEnabled) ? string.Format(Language.Get("UNLOCKSFIVE", "MENUS,GUI"), 5) : Language.Get("MODCHIP", "MENUS,GUI"));
		raritySprite.SetSprite(rarityIconSpriteNames[_character.characterDefinition.characterRarity]);
		if (repaintButtonSprite == null)
		{
			repaintButtonSprite = repaintButton.GetComponentInChildren<tk2dSprite>(includeInactive: true);
		}
		if (_character.repaintColorsUnlocked.Count >= _character.characterDefinition.repaintSlots.Count)
		{
			repaintButtonSprite.SetSprite("menu_characterselect_button_repaint_golden");
		}
		else
		{
			repaintButtonSprite.SetSprite("menu_characterselect_button_repaint");
		}
	}

	private void HandlePartyChanged()
	{
		if (_player != null && !_player.activeParty.Contains(_character))
		{
			equipArrowSprite.scale = new Vector3(0.9f, 0.9f, 1f);
			equipText.text = Language.Get("EQUIP", "MENUS,GUI");
		}
		else
		{
			equipArrowSprite.scale = new Vector3(0.9f, -0.9f, 1f);
			equipText.text = Language.Get("UNEQUIP", "MENUS,GUI");
		}
	}

	private void HandleClassIconClick()
	{
		if (_character != null)
		{
			classBonusBox.Init(_character);
			classBonusBox.gameObject.SetActive(!classBonusBox.gameObject.activeSelf);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void HandleStatClick(tk2dUIItem uiItem)
	{
		CharacterViewStat component = uiItem.GetComponent<CharacterViewStat>();
		if (component != null)
		{
			ShowCharacterStatInfoBox(component);
		}
	}

	private void ShowCharacterStatInfoBox(CharacterViewStat stat)
	{
		if (characterStatInfoBox == null)
		{
			characterStatInfoBox = ObjectPool.GetObjectFromResource<CharacterStatInfoBox>("Prefabs/GUI/CharacterStatInfoBox_CharacterView");
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		if (characterStatInfoBox.characterViewStat == stat)
		{
			HideCharacterStatInfoBox();
			return;
		}
		characterStatInfoBox.Init(stat);
		characterStatInfoBox.transform.parent = stat.transform;
		characterStatInfoBox.transform.localPosition = Vector3.zero;
		characterStatInfoBox.transform.localRotation = Quaternion.identity;
		characterStatInfoBox.transform.localScale = Vector3.one;
	}

	public void HideCharacterStatInfoBox()
	{
		if (characterStatInfoBox != null)
		{
			characterStatInfoBox.Init(null);
			ObjectPool.PoolObject(characterStatInfoBox.gameObject);
			characterStatInfoBox = null;
		}
	}

	private void HandleOnEquipClick()
	{
		StartCoroutine(DoEquip());
	}

	private IEnumerator DoEquip()
	{
		if (!_character.characterDefinition.canUseInBattle || (!_player.IsCharacterInParty(_character) && _player.isActivePartyFull))
		{
			PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
			dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
			dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
			dialog.descriptionText.text = (_character.characterDefinition.canUseInBattle ? Language.Get("SQUADFULL", "MENUS,GUI") : "This character is not yet set up for battle, and therefore cannot be added to your party.");
			dialog.gameObject.SetActive(value: true);
			yield return new WaitForInactive(dialog.gameObject);
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else if (_player.IsCharacterInParty(_character))
		{
			if (_player.activeParty.Count == 1)
			{
				PopupDialog dialog2 = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
				dialog2.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
				dialog2.transform.parent = GUICamera.instance.anchorMiddleCenter;
				dialog2.transform.localPosition = new Vector3(0f, 0f, -200f);
				dialog2.headerText.text = Language.Get("WARNING", "MENUS,GUI");
				dialog2.descriptionText.text = Language.Get("ATLEASTONE", "MENUS,GUI");
				dialog2.gameObject.SetActive(value: true);
				yield return new WaitForInactive(dialog2.gameObject);
				ObjectPool.PoolObject(dialog2.gameObject);
			}
			else
			{
				_player.RemoveFromParty(_character);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_unequipchar", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
		else
		{
			_player.AddToParty(_character);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_equipcharacter", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void HandleOnNavClick(tk2dUIItem button)
	{
		int num = _characterList.IndexOf(_character);
		if (button == leftNavButton)
		{
			num--;
			if (num < 0)
			{
				num = _characterList.Count - 1;
			}
		}
		else
		{
			num++;
			if (num >= _characterList.Count)
			{
				num = 0;
			}
		}
		character = _characterList[num];
		TitanTowerController.instance.ShowPage("characterView", isBack: true);
	}

	protected void OnCharacterChanged(Character character)
	{
		if (CharacterChanged != null)
		{
			CharacterChanged(character);
		}
	}
}
