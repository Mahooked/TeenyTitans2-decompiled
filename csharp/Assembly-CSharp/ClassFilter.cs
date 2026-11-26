using System;
using UnityEngine;

public class ClassFilter : MonoBehaviour
{
	public tk2dUIToggleButtonGroup buttons;

	public Transform portraitsRightExtents;

	private CharacterClass _characterClass = CharacterClass.MartialArts;

	public CharacterClass characterClass => _characterClass;

	public event Action<CharacterClass> ClassChanged;

	private void Awake()
	{
		Vector3 position = TitanTowerController.instance.guiCamera2.GetComponent<Camera>().WorldToScreenPoint(portraitsRightExtents.position);
		Vector3 position2 = transform.position;
		position2.x = GUICamera.instance.camera.ScreenToWorldPoint(position).x;
		transform.position = position2;
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
		characterClass = group.SelectedToggleButton.name switch
		{
			"DarkArts" => CharacterClass.DarkArts, 
			"Super" => CharacterClass.Super, 
			"Beast" => CharacterClass.Beast, 
			"Tech" => CharacterClass.Tech, 
			"Cute" => CharacterClass.Cute, 
			_ => CharacterClass.MartialArts, 
		};
		if (characterClass != _characterClass)
		{
			_characterClass = characterClass;
			OnClassChanged(_characterClass);
		}
	}

	protected void OnClassChanged(CharacterClass characterClass)
	{
		if (ClassChanged != null)
		{
			ClassChanged(characterClass);
		}
	}
}
