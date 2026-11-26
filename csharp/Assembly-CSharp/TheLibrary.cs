using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheLibrary : MonoBehaviour
{
	public PageController2 pageController;

	public TextMeshPro countText;

	public float timeEnabled;

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
		pageController.Init(InitPortrait);
		List<CharacterDefinition> list = GameManager.instance.characterDatabase.characters.FindAll((CharacterDefinition obj) => obj.isActive && (!obj.isSecret || Player.instance.HasAcquiredCharacter(obj)));
		list.Sort((CharacterDefinition x, CharacterDefinition y) =>
		{
			if (x.isSecret == y.isSecret)
			{
				return x.libraryNumber.CompareTo(y.libraryNumber);
			}
			return x.isSecret ? 1 : (-1);
		});
		pageController.SetList(list);
		countText.text = $"{_player.charactersAcquired.Count}/{pageController.filteredCharacterDefinitions.Count}";
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.CharacterAdded -= HandleCharacterAdded;
		}
		_player = null;
	}

	public void Init(Player player)
	{
		if (player != _player)
		{
			if (_player != null)
			{
				_player.CharacterAdded -= HandleCharacterAdded;
			}
			_player = player;
			if (_player != null)
			{
				_player.CharacterAdded += HandleCharacterAdded;
				HandleCharacterAdded(null);
			}
		}
	}

	private void HandleCharacterAdded(Character obj)
	{
		pageController.ForceUpdate();
		countText.text = $"{_player.charactersAcquired.Count}/{pageController.filteredCharacterDefinitions.Count}";
	}

	private void InitPortrait(CharacterSelectPortrait portrait, CharacterDefinition character)
	{
		portrait.Init(character);
		portrait.mode = ((!_player.charactersAcquired.Contains(character.uid)) ? CharacterSelectPortraitMode.Unknown : CharacterSelectPortraitMode.Normal);
		if (Time.unscaledTime - timeEnabled > 0.25f)
		{
			portrait.animator.Play("Intro", 0, 1f);
		}
	}
}
