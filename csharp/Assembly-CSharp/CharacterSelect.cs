using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
	public PageController pageController;

	public tk2dUIToggleButtonGroup filterButtons;

	public tk2dUIToggleButtonGroup sortButtons;

	public ClassFilter classFilter;

	public tk2dUIItem allFilterBtn;

	public tk2dUIItem classFilterBtn;

	[Space]
	public tk2dUIItem lvlButton;

	public tk2dUIItem newButton;

	private bool flipLvlSort;

	private bool flipNewSort;

	private int inFolderCharacterUID = -1;

	private Player _player;

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

	private void Start()
	{
		pageController.Init(InitPortrait, InitFolderPortrait);
		HandleOnFilterChange(filterButtons);
	}

	private void OnEnable()
	{
		pageController.DragFinished += HandleDragFinished;
		pageController.CharacterPortraitCreated += HandleCharacterPortraitCreated;
		pageController.CharacterFolderSelected += HandleCharacterFolderSelected;
		filterButtons.OnChange += HandleOnFilterChange;
		classFilter.ClassChanged += HandleOnClassChanged;
		allFilterBtn.OnClickUIItem += HandleFolderBtnClicked;
		classFilterBtn.OnClickUIItem += HandleFolderBtnClicked;
	}

	private void OnDisable()
	{
		pageController.DragFinished -= HandleDragFinished;
		pageController.CharacterPortraitCreated -= HandleCharacterPortraitCreated;
		pageController.CharacterFolderSelected -= HandleCharacterFolderSelected;
		filterButtons.OnChange -= HandleOnFilterChange;
		classFilter.ClassChanged -= HandleOnClassChanged;
		allFilterBtn.OnClickUIItem -= HandleFolderBtnClicked;
		classFilterBtn.OnClickUIItem -= HandleFolderBtnClicked;
		lvlButton.OnClickUIItem -= HandleLevelButtonClicked;
		newButton.OnClickUIItem -= HandleNewButtonClicked;
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.PartyChanged -= HandlePartyChanged;
		}
		_player = null;
	}

	public void Init(Player player)
	{
		ProcessFilterChange(filterButtons);
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

	public void EnableFilterButtons(bool enable = true)
	{
		int num = filterButtons.ToggleBtns.Length;
		for (int i = 0; i < num; i++)
		{
			filterButtons.ToggleBtns[i].uiItem.enabled = enable;
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

	private void InitPortrait(CharacterSelectPortrait portrait, Character character, bool animate)
	{
		portrait.Init(character);
		portrait.mode = (_player.IsCharacterInParty(character) ? CharacterSelectPortraitMode.Equipped : CharacterSelectPortraitMode.Normal);
		if (animate)
		{
			StartCoroutine(DelayedIntroAnim(portrait));
		}
		else
		{
			portrait.animator.Play("Intro", 0, 1f);
		}
	}

	private void InitFolderPortrait(CharacterSelectPortrait portrait, CharacterDefinition characterDefinition, int characterCount, bool animate)
	{
		portrait.InitAsFolder(characterDefinition, characterCount);
		portrait.mode = CharacterSelectPortraitMode.Normal;
		if (animate)
		{
			StartCoroutine(DelayedIntroAnim(portrait));
		}
		else
		{
			portrait.animator.Play("Intro", 0, 1f);
		}
	}

	private IEnumerator DelayedIntroAnim(CharacterSelectPortrait portrait)
	{
		Color clear = Color.clear;
		portrait.portraitSprite.gameObject.SetActive(value: false);
		portrait.equippedSprite.color = clear;
		portrait.xpBackSprite.color = clear;
		portrait.xpFillSprite.slicedSprite.color = clear;
		portrait.levelText.color = clear;
		portrait.questionMarkText.color = clear;
		portrait.favoriteIcon.color = clear;
		portrait.repaintIcon.color = clear;
		portrait.newIcon.color = clear;
		portrait.folderIcon.color = clear;
		portrait.folderNumText.color = clear;
		portrait.xText.color = clear;
		yield return null;
		Color white = Color.white;
		portrait.portraitSprite.gameObject.SetActive(value: true);
		portrait.equippedSprite.color = white;
		portrait.xpBackSprite.color = white;
		portrait.xpFillSprite.slicedSprite.color = white;
		portrait.levelText.color = white;
		portrait.questionMarkText.color = white;
		portrait.favoriteIcon.color = white;
		portrait.repaintIcon.color = white;
		portrait.newIcon.color = white;
		portrait.folderIcon.color = white;
		portrait.folderNumText.color = white;
		portrait.xText.color = white;
		portrait.animator.Play("Intro", 0, 0f);
	}

	private void HandleDragFinished(CharacterSelectPortrait portrait)
	{
		if (!portrait.character.characterDefinition.canUseInBattle)
		{
			return;
		}
		Collider2D[] array = Physics2D.OverlapPointAll(pageController.upperCamera.ScreenToWorldPoint(pageController.lowerCamera.WorldToScreenPoint(portrait.transform.position)), 1 << LayerMask.NameToLayer("UI"));
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			if (array[i].GetComponent<MySquad>() != null && _player.activeParty.Count < 6)
			{
				_player.AddToParty(portrait.character);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_equipcharacter", GFAudioManager.SFX_GROUP, string.Empty);
				break;
			}
		}
	}

	private void HandleCharacterPortraitCreated(CharacterSelectPortrait obj)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_generalbuttons", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.2f);
	}

	private void HandleOnFilterChange(tk2dUIToggleButtonGroup group)
	{
		ProcessFilterChange(group);
	}

	private void ProcessFilterChange(tk2dUIToggleButtonGroup group, bool flipSort = false)
	{
		if (_player == null)
		{
			return;
		}
		classFilter.gameObject.SetActive(value: false);
		lvlButton.OnClickUIItem -= HandleLevelButtonClicked;
		newButton.OnClickUIItem -= HandleNewButtonClicked;
		switch (group.SelectedIndex)
		{
		case 0:
		{
			List<Character> characters = new List<Character>(_player.characters);
			characters.Sort((Character x, Character y) => x.characterDefinition.libraryNumber.CompareTo(y.characterDefinition.libraryNumber));
			if (inFolderCharacterUID != -1)
			{
				pageController.SetList(characters.FindAll((Character x) => x.characterDefinition.uid == inFolderCharacterUID), PageController.ViewMode.Single);
				pageController.OverrideExitButtonFunction();
			}
			else
			{
				pageController.SetList(characters, PageController.ViewMode.Folder, animateButtonsIn: true, recordAsMasterList: true);
			}
			flipLvlSort = false;
			flipNewSort = false;
			break;
		}
		case 1:
		{
			classFilter.gameObject.SetActive(value: true);
			List<Character> characters = _player.characters.FindAll((Character obj) => obj.characterDefinition.characterClass == classFilter.characterClass);
			characters.Sort((Character x, Character y) => x.characterDefinition.libraryNumber.CompareTo(y.characterDefinition.libraryNumber));
			if (inFolderCharacterUID != -1)
			{
				pageController.SetList(characters.FindAll((Character x) => x.characterDefinition.uid == inFolderCharacterUID), PageController.ViewMode.Single);
				pageController.OverrideExitButtonFunction();
			}
			else
			{
				pageController.SetList(characters, PageController.ViewMode.Folder, animateButtonsIn: true, recordAsMasterList: true);
			}
			flipLvlSort = false;
			flipNewSort = false;
			break;
		}
		case 2:
		{
			List<Character> characters = new List<Character>(_player.characters);
			if (!flipSort)
			{
				characters.Sort((Character x, Character y) =>
				{
					int num = x.isViewed.CompareTo(y.isViewed);
					return (num != 0) ? num : y.uid.CompareTo(x.uid);
				});
			}
			else
			{
				characters.Sort((Character x, Character y) =>
				{
					int num = y.isViewed.CompareTo(x.isViewed);
					return (num != 0) ? num : x.uid.CompareTo(y.uid);
				});
			}
			pageController.SetList(characters, PageController.ViewMode.Single, animateButtonsIn: true, recordAsMasterList: true);
			StartCoroutine(OnClickNewButtonLateSub());
			flipLvlSort = false;
			break;
		}
		case 3:
		{
			List<Character> characters = new List<Character>(_player.characters);
			if (!flipSort)
			{
				characters.Sort((Character x, Character y) => y.level.CompareTo(x.level));
			}
			else
			{
				characters.Sort((Character x, Character y) => x.level.CompareTo(y.level));
			}
			pageController.SetList(characters, PageController.ViewMode.Single, animateButtonsIn: true, recordAsMasterList: true);
			StartCoroutine(OnClickLvlButtonLateSub());
			flipNewSort = false;
			break;
		}
		case 4:
		{
			List<Character> characters = _player.characters.FindAll((Character obj) => obj.isFavorite);
			pageController.SetList(characters, PageController.ViewMode.Single, animateButtonsIn: true, recordAsMasterList: true);
			flipLvlSort = false;
			flipNewSort = false;
			break;
		}
		}
		pageController.UpdatePages();
	}

	private IEnumerator OnClickLvlButtonLateSub()
	{
		yield return null;
		lvlButton.OnClickUIItem += HandleLevelButtonClicked;
	}

	private IEnumerator OnClickNewButtonLateSub()
	{
		yield return null;
		newButton.OnClickUIItem += HandleNewButtonClicked;
	}

	private void HandleLevelButtonClicked(tk2dUIItem obj)
	{
		flipLvlSort = !flipLvlSort;
		Debug.Log("Handle Level Button Clicked, while active");
		ProcessFilterChange(filterButtons, flipLvlSort);
	}

	private void HandleNewButtonClicked(tk2dUIItem obj)
	{
		flipNewSort = !flipNewSort;
		Debug.Log("Handle New Button Clicked, while active");
		ProcessFilterChange(filterButtons, !flipNewSort);
	}

	private void HandleOnClassChanged(CharacterClass characterClass)
	{
		if (_player != null)
		{
			List<Character> list = _player.characters.FindAll((Character obj) => obj.characterDefinition.characterClass == classFilter.characterClass);
			list.Sort((Character x, Character y) => x.characterDefinition.libraryNumber.CompareTo(y.characterDefinition.libraryNumber));
			pageController.SetList(list, PageController.ViewMode.Folder, animateButtonsIn: true, recordAsMasterList: true);
		}
	}

	private void HandlePartyChanged()
	{
		pageController.ForceUpdate();
	}

	public void ForceUpdatePageControllerList()
	{
		inFolderCharacterUID = -1;
		HandleOnFilterChange(filterButtons);
	}

	private void HandleFolderBtnClicked(tk2dUIItem folderUIItem)
	{
		if (folderUIItem == filterButtons.SelectedToggleButton.uiItem && pageController.currentViewMode == PageController.ViewMode.Single)
		{
			pageController.ForceReturnExitButtonControl();
			ForceUpdatePageControllerList();
		}
	}

	private void HandleCharacterFolderSelected(int characterUID)
	{
		inFolderCharacterUID = characterUID;
	}
}
