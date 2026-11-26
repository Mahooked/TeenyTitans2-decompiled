using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class MySquad : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Character> CharacterSelected__BackingField;

	public Animator animator;

	public tk2dSlicedSprite backingSprite;

	public TextMeshPro titleText;

	public CharacterInfoBox characterInfoBox;

	public tk2dUIItem toggleButton;

	public List<MySquadSlot> slots;

	private bool cityMySquad = true;

	private bool tournamentMySquad;

	private MySquadSlot selectedSlot;

	private MySquadSlot prevSlotClick;

	private tk2dSprite toggleBtnBgSprite;

	private tk2dSprite toggleBtnArrowSprite;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnHide__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OnShow__BackingField;

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

	public event Action<Character> CharacterSelected
	{
		add
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnHide
	{
		add
		{
			Action action = OnHide__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnHide__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnHide__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnHide__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OnShow
	{
		add
		{
			Action action = OnShow__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnShow__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnShow__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnShow__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].uiItem.OnClickUIItem += HandleOnClickUIItem;
		}
		if (characterInfoBox != null && characterInfoBox.gameObject.activeSelf)
		{
			characterInfoBox.gameObject.SetActive(false);
		}
		if (toggleButton != null)
		{
			toggleBtnBgSprite = toggleButton.GetComponent<tk2dSprite>();
			toggleBtnArrowSprite = toggleButton.transform.GetChild(0).GetComponent<tk2dSprite>();
		}
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.PartyChanged -= HandlePartyChanged;
		}
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].uiItem.OnClickUIItem -= HandleOnClickUIItem;
		}
		if (cityMySquad && CityButtonEventHelper.instance != null)
		{
			CityButtonEventHelper.instance.tk2dUIItemOnDown -= Handletk2dUIItemOnDown;
		}
		_player = null;
		CharacterSelected__BackingField = null;
		OnHide__BackingField = null;
		OnShow__BackingField = null;
	}

	private void HandleOnClickUIItem(tk2dUIItem obj)
	{
		MySquadSlot component = obj.GetComponent<MySquadSlot>();
		if ((component.character == null || cityMySquad || tournamentMySquad) && component.character != null && cityMySquad)
		{
			selectedSlot = component;
			if (characterInfoBox.gameObject.activeSelf && selectedSlot == prevSlotClick)
			{
				characterInfoBox.gameObject.SetActive(false);
				selectedSlot = null;
			}
			else
			{
				Vector3 position = GUICamera.instance.camera.WorldToScreenPoint(component.transform.position + new Vector3(0f, 32f));
				position = GUICamera.instance.camera.ScreenToWorldPoint(position);
				position.z = characterInfoBox.transform.position.z;
				characterInfoBox.transform.position = position;
				characterInfoBox.Init(component.character);
				characterInfoBox.gameObject.SetActive(true);
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
			prevSlotClick = selectedSlot;
		}
		if (component.character != null)
		{
			OnCharacterSelected(component.character);
		}
	}

	public void Init(Player player, bool isCity = false, bool isTournament = false)
	{
		if (player != _player)
		{
			cityMySquad = isCity;
			tournamentMySquad = isTournament;
			if (cityMySquad)
			{
				CityButtonEventHelper.instance.tk2dUIItemOnDown += Handletk2dUIItemOnDown;
			}
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

	public void EnableSlotButtons(bool enable = true)
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].uiItem.enabled = enable;
		}
	}

	public void SetInteractive(bool interactive)
	{
		EnableSlotButtons(interactive);
		Color color = ((!interactive) ? Color.gray : Color.white);
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = slots[i];
			mySquadSlot.portraitSprite.color = color;
			mySquadSlot.levelText.color = color;
			mySquadSlot.xpFillSprite.slicedSprite.color = color;
			mySquadSlot.repaintIcon.color = color;
		}
		backingSprite.color = color;
		if (toggleButton != null)
		{
			toggleButton.enabled = interactive;
			toggleBtnArrowSprite.color = color;
			toggleBtnBgSprite.color = color;
		}
	}

	private void Handletk2dUIItemOnDown(tk2dUIItem uiItem)
	{
		if (!uiItem.gameObject.name.Contains("MySquadSlot") && !uiItem.gameObject.name.Contains("Tab_Button") && !uiItem.gameObject.name.Contains("AbilityButton") && !uiItem.gameObject.name.Contains("ClassIcon"))
		{
			HideNow();
		}
	}

	private void HandlePartyChanged()
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].Init((i >= _player.activeParty.Count) ? null : _player.activeParty[i]);
			slots[i].showHealth = tournamentMySquad;
		}
	}

	private void ShowPortraits()
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].portraitRenderer.enabled = true;
		}
	}

	private void HidePortraits()
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].portraitRenderer.enabled = false;
		}
	}

	public void ToggleShowHide()
	{
		if (characterInfoBox.gameObject.activeSelf)
		{
			characterInfoBox.gameObject.SetActive(false);
			selectedSlot = null;
		}
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("MySquad_Intro") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
		{
			HideNow();
		}
		else
		{
			ShowNow();
		}
	}

	public void HideNow()
	{
		if (characterInfoBox.gameObject.activeSelf)
		{
			characterInfoBox.gameObject.SetActive(false);
			selectedSlot = null;
		}
		if (animator.GetCurrentAnimatorStateInfo(0).IsName((!tournamentMySquad) ? "MySquad_Intro" : "MySquad_Intro_big"))
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_partybar_close", GFAudioManager.SFX_GROUP, string.Empty);
			animator.Play((!tournamentMySquad) ? "MySquad_outro" : "MySquad_outro_big");
			if (OnHide__BackingField != null)
			{
				OnHide__BackingField();
			}
		}
	}

	public void ShowNow()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName((!tournamentMySquad) ? "MySquad_outro" : "MySquad_outro_big"))
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_partybar_open", GFAudioManager.SFX_GROUP, string.Empty);
			ShowPortraits();
			animator.Play((!tournamentMySquad) ? "MySquad_Intro" : "MySquad_Intro_big");
			if (OnShow__BackingField != null)
			{
				OnShow__BackingField();
			}
		}
	}

	protected void OnCharacterSelected(Character character)
	{
		if (CharacterSelected__BackingField != null)
		{
			CharacterSelected__BackingField(character);
		}
	}
}
