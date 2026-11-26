using TMPro;
using UnityEngine;

public class AbilityInfoBox : MonoBehaviour
{
	public tk2dSprite tailSprite;

	public Transform box;

	public TextMeshPro nameText;

	public TextMeshPro effectText;

	public TextMeshPro descriptionText;

	public TextMeshPro shinyText;

	public float descriptionYWithoutShiny = 37f;

	public float descriptionYWithShiny = 31f;

	private AbilityData _abilityData;

	private Character _character;

	public AbilityData abilityData
	{
		get
		{
			return _abilityData;
		}
	}

	public Character character
	{
		get
		{
			return _character;
		}
	}

	private void OnEnable()
	{
		Refresh();
	}

	public void Init(AbilityData abilityData, Character character)
	{
		if (!(abilityData == _abilityData) || character != _character)
		{
			_abilityData = abilityData;
			_character = character;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_abilityData == null || _character == null)
		{
			return;
		}
		Vector3 localPosition = descriptionText.transform.localPosition;
		localPosition.y = descriptionYWithoutShiny;
		if (_abilityData != null && _character != null)
		{
			nameText.text = ((!string.IsNullOrEmpty(_abilityData.abilityNameKey)) ? Language.Get(_abilityData.abilityNameKey, "ABILS,CHIPS,ACHVS,LDBS") : ((!_abilityData.abilityName.Contains("(")) ? _abilityData.abilityName : _abilityData.abilityName.Split('(')[0]));
			effectText.text = TTGParseHelper.GetAbilityEffectText(_abilityData, _character, _character.GetAbilityPosition(_abilityData));
			descriptionText.text = ((!string.IsNullOrEmpty(_abilityData.abilityDescriptionKey)) ? Language.Get(_abilityData.abilityDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS") : _abilityData.abilityDescription);
			if (shinyText != null)
			{
				if (_character.IsAbilityShiny(_abilityData))
				{
					shinyText.text = TTGParseHelper.GetShinyEffectDescription(_abilityData, _character);
					localPosition.y = descriptionYWithShiny;
				}
				else
				{
					shinyText.text = string.Empty;
				}
			}
		}
		else if (_abilityData != null)
		{
			nameText.text = ((!string.IsNullOrEmpty(_abilityData.abilityNameKey)) ? Language.Get(_abilityData.abilityNameKey, "ABILS,CHIPS,ACHVS,LDBS") : _abilityData.abilityName);
			effectText.text = TTGParseHelper.GetAbilityEffectText(_abilityData, _character, _character.GetAbilityPosition(_abilityData));
			descriptionText.text = ((!string.IsNullOrEmpty(_abilityData.abilityDescriptionKey)) ? Language.Get(_abilityData.abilityDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS") : _abilityData.abilityDescription);
			if (shinyText != null)
			{
				shinyText.text = string.Empty;
			}
		}
		else
		{
			string text = Language.Get("UNLOCKSFIVE", "MENUS,GUI").Trim();
			nameText.text = ((!text.Contains("{0}")) ? "Unlocks at Level 3" : string.Format(text, 3));
			effectText.text = string.Empty;
			descriptionText.text = string.Empty;
			if (shinyText != null)
			{
				shinyText.text = string.Empty;
			}
		}
		descriptionText.transform.localPosition = localPosition;
	}
}
