using System;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class CharacterViewRepaintWindow : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<RepaintColorButton> ColorSelected__BackingField;

	[SerializeField]
	private RepaintColorButton[] _colorButtons;

	[SerializeField]
	private tk2dUIItem _closeButton;

	[SerializeField]
	private TextMeshPro rainbowRaiderTextMesh;

	private Character _character;

	public tk2dUIItem closeButton
	{
		get
		{
			return _closeButton;
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
			Init(value);
		}
	}

	public event Action<RepaintColorButton> ColorSelected
	{
		add
		{
			Action<RepaintColorButton> action = ColorSelected__BackingField;
			Action<RepaintColorButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ColorSelected__BackingField, (Action<RepaintColorButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<RepaintColorButton> action = ColorSelected__BackingField;
			Action<RepaintColorButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ColorSelected__BackingField, (Action<RepaintColorButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < _colorButtons.Length; i++)
		{
			_colorButtons[i].tk2dUIItem.OnClickUIItem += HandleColorButtonClick;
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < _colorButtons.Length; i++)
		{
			_colorButtons[i].tk2dUIItem.OnClickUIItem -= HandleColorButtonClick;
		}
	}

	public void Init(Character character)
	{
		_character = character;
		rainbowRaiderTextMesh.text = string.Format(Language.Get("TOWER_LABEL_REPAINTS_HOWTO", "MENUS,GUI"), "<sprite name=\"icon_notifier_rainbowRaider\">");
		RefreshButtons();
	}

	public void ForceRefreshButtons()
	{
		RefreshButtons();
	}

	private void RefreshButtons()
	{
		for (int i = 0; i < _colorButtons.Length; i++)
		{
			_colorButtons[i].gameObject.SetActive(false);
		}
		RepaintColorButton repaintColorButton = _colorButtons[0];
		repaintColorButton.gameObject.SetActive(true);
		repaintColorButton.InitAsNoColorButton(true);
		int num = 0;
		CharacterDefinition characterDefinition = _character.characterDefinition;
		for (int j = 1; j < _colorButtons.Length; j++)
		{
			RepaintColorButton repaintColorButton2 = _colorButtons[j];
			if (num < characterDefinition.repaintSlots.Count)
			{
				RepaintColorSlot repaintColorSlot = characterDefinition.repaintSlots[num];
				repaintColorButton2.Init(repaintColorSlot, _character, true);
				repaintColorButton2.gameObject.SetActive(true);
			}
			num++;
		}
	}

	private void HandleColorButtonClick(tk2dUIItem uiItem)
	{
		if (ColorSelected__BackingField != null)
		{
			ColorSelected__BackingField(uiItem.GetComponent<RepaintColorButton>());
		}
	}
}
