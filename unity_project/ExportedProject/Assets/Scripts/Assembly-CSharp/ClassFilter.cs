using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ClassFilter : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CharacterClass> ClassChanged__BackingField;

	public tk2dUIToggleButtonGroup buttons;

	public Transform portraitsRightExtents;

	private CharacterClass _characterClass = CharacterClass.MartialArts;

	public CharacterClass characterClass
	{
		get
		{
			return _characterClass;
		}
	}

	public event Action<CharacterClass> ClassChanged
	{
		add
		{
			Action<CharacterClass> action = ClassChanged__BackingField;
			Action<CharacterClass> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ClassChanged__BackingField, (Action<CharacterClass>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CharacterClass> action = ClassChanged__BackingField;
			Action<CharacterClass> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ClassChanged__BackingField, (Action<CharacterClass>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		Vector3 position = TitanTowerController.instance.guiCamera2.GetComponent<Camera>().WorldToScreenPoint(portraitsRightExtents.position);
		Vector3 position2 = base.transform.position;
		position2.x = GUICamera.instance.camera.ScreenToWorldPoint(position).x;
		base.transform.position = position2;
	}

	private void OnEnable()
	{
		buttons.OnChange += HandleOnChange;
	}

	private void OnDisable()
	{
		buttons.OnChange -= HandleOnChange;
	}

	private void HandleOnChange(tk2dUIToggleButtonGroup group)
	{
		CharacterClass characterClass = _characterClass;
		switch (group.SelectedToggleButton.name)
		{
		case "DarkArts":
			characterClass = CharacterClass.DarkArts;
			break;
		case "Super":
			characterClass = CharacterClass.Super;
			break;
		case "Beast":
			characterClass = CharacterClass.Beast;
			break;
		case "Tech":
			characterClass = CharacterClass.Tech;
			break;
		case "Cute":
			characterClass = CharacterClass.Cute;
			break;
		default:
			characterClass = CharacterClass.MartialArts;
			break;
		}
		if (characterClass != _characterClass)
		{
			_characterClass = characterClass;
			OnClassChanged(_characterClass);
		}
	}

	protected void OnClassChanged(CharacterClass characterClass)
	{
		if (ClassChanged__BackingField != null)
		{
			ClassChanged__BackingField(characterClass);
		}
	}
}
