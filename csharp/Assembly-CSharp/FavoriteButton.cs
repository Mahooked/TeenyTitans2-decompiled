using UnityEngine;

public class FavoriteButton : MonoBehaviour
{
	public tk2dUIToggleButton toggleButton;

	private Character _character;

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

	private void Awake()
	{
		if (toggleButton == null)
		{
			toggleButton = GetComponent<tk2dUIToggleButton>();
		}
	}

	private void OnEnable()
	{
		toggleButton.OnToggle += HandleOnToggle;
	}

	private void OnDisable()
	{
		toggleButton.OnToggle -= HandleOnToggle;
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
	}

	public void Init(Character character)
	{
		if (character != _character)
		{
			if (_character != null)
			{
				_character.Updated -= HandleUpdated;
			}
			_character = character;
			if (_character != null)
			{
				_character.Updated += HandleUpdated;
				HandleUpdated();
			}
		}
	}

	private void HandleOnToggle(tk2dUIToggleButton button)
	{
		if (_character != null)
		{
			_character.isFavorite = button.IsOn;
		}
	}

	private void HandleUpdated()
	{
		toggleButton.IsOn = _character.isFavorite;
	}
}
