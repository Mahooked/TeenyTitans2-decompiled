using System;
using System.Collections.Generic;
using UnityEngine;

public class DiminishingReturns
{
	public struct DiminishedAbility
	{
		public int abilityUID;

		public int uses;
	}

	public Action DiminishedAbilityUpdated;

	private DiminishedAbility _playerDiminishedAbility;

	private DiminishedAbility _enemyDiminishedAbility;

	private BattleController battleController;

	private static DiminishingReturns _instance;

	public static DiminishingReturns instance => _instance;

	public DiminishingReturns(BattleController battleController)
	{
		this.battleController = battleController;
		battleController.playerActionMeter.AbilityFinished += HandleAbilityFinished;
		battleController.playerActionMeter.ChannelFinished += HandleChannelAbilityFinished;
		battleController.enemyActionMeter.AbilityFinished += HandleAbilityFinished;
		battleController.enemyActionMeter.ChannelFinished += HandleChannelAbilityFinished;
		_instance = this;
	}

	public void Dispose()
	{
		battleController.playerActionMeter.AbilityFinished -= HandleAbilityFinished;
		battleController.playerActionMeter.ChannelFinished -= HandleChannelAbilityFinished;
		battleController.enemyActionMeter.AbilityFinished -= HandleAbilityFinished;
		battleController.enemyActionMeter.ChannelFinished -= HandleChannelAbilityFinished;
	}

	private void HandleAbilityFinished(ActionMeter meter, Character character, AbilityData abilityData)
	{
		if (!(abilityData.channelingData.speed > 0f))
		{
			UpdateDiminishedAbilityUse(abilityData, character);
		}
	}

	private void HandleChannelAbilityFinished(AbilityData abilityData, Character channelerCharacter)
	{
		if (!(abilityData.channelingData.speed <= 0f))
		{
			UpdateDiminishedAbilityUse(abilityData, channelerCharacter);
		}
	}

	private void UpdateDiminishedAbilityUse(AbilityData abilityData, Character character)
	{
		int uid = abilityData.uid;
		if (uid == 273)
		{
			return;
		}
		if (character == battleController.playerCharacter.character)
		{
			int abilityChargeCount = battleController.playerActionMeter.GetAbilityChargeCount(character, abilityData);
			if (abilityData.numChargesRequired == 0 || abilityChargeCount == 0)
			{
				if (_playerDiminishedAbility.abilityUID == uid && abilityData.useDiminishingReturns)
				{
					_playerDiminishedAbility.uses++;
				}
				else
				{
					_playerDiminishedAbility.abilityUID = uid;
					_playerDiminishedAbility.uses = (abilityData.useDiminishingReturns ? 1 : 0);
				}
			}
			else if (_playerDiminishedAbility.abilityUID != uid)
			{
				_playerDiminishedAbility.abilityUID = uid;
				_playerDiminishedAbility.uses = 0;
			}
			if (DiminishedAbilityUpdated != null)
			{
				DiminishedAbilityUpdated();
			}
		}
		else
		{
			if (character != battleController.enemyCharacter.character)
			{
				return;
			}
			int abilityChargeCount2 = battleController.enemyActionMeter.GetAbilityChargeCount(character, abilityData);
			if (abilityData.numChargesRequired == 0 || abilityChargeCount2 == 0)
			{
				if (_enemyDiminishedAbility.abilityUID == uid && abilityData.useDiminishingReturns)
				{
					_enemyDiminishedAbility.uses++;
				}
				else
				{
					_enemyDiminishedAbility.abilityUID = uid;
					_enemyDiminishedAbility.uses = (abilityData.useDiminishingReturns ? 1 : 0);
				}
			}
			else if (_enemyDiminishedAbility.abilityUID != uid)
			{
				_enemyDiminishedAbility.abilityUID = uid;
				_enemyDiminishedAbility.uses = 0;
			}
			if (DiminishedAbilityUpdated != null)
			{
				DiminishedAbilityUpdated();
			}
		}
	}

	public float GetDiminishedValue(float initialValue, Character character, AbilityData abilityData, float minValueReduction = 0f, float minPossibleValue = 0f)
	{
		float diminishingReturnsFactor = GetDiminishingReturnsFactor(character, abilityData);
		float num = Mathf.Max(minPossibleValue, initialValue * (1f - diminishingReturnsFactor));
		if (diminishingReturnsFactor > 0f && initialValue > 0f && Mathf.Approximately(num, initialValue))
		{
			num = Mathf.Max(minPossibleValue, num - minValueReduction);
		}
		return num;
	}

	public int GetDiminishedValue(int initialValue, Character character, AbilityData abilityData, int minValueReduction = 1, int minPossibleValue = 1)
	{
		float diminishingReturnsFactor = GetDiminishingReturnsFactor(character, abilityData);
		int num = Mathf.Max(minPossibleValue, Mathf.RoundToInt((float)initialValue * (1f - diminishingReturnsFactor)));
		if (diminishingReturnsFactor > 0f && initialValue > 0 && num == initialValue)
		{
			num = Mathf.Max(minPossibleValue, num - minValueReduction);
		}
		return num;
	}

	public float GetDiminishingReturnsFactor(Character character, AbilityData abilityData)
	{
		int num = abilityData.uid;
		if (num == 273)
		{
			num = 274;
		}
		DiminishedAbility? diminishedAbility = null;
		if (character == battleController.playerCharacter.character && _playerDiminishedAbility.abilityUID == num)
		{
			diminishedAbility = _playerDiminishedAbility;
		}
		else if (character == battleController.enemyCharacter.character && _enemyDiminishedAbility.abilityUID == num)
		{
			diminishedAbility = _enemyDiminishedAbility;
		}
		if (diminishedAbility.HasValue)
		{
			DiminishingReturnsSetting diminishingReturnsSetting = ((!abilityData.useDiminishingReturns) ? null : GetDiminishingReturnsSetting(diminishedAbility.Value));
			if (diminishingReturnsSetting != null)
			{
				return diminishingReturnsSetting.effectPowerReduction;
			}
		}
		return 0f;
	}

	private DiminishingReturnsSetting GetDiminishingReturnsSetting(DiminishedAbility diminishedAbility)
	{
		DiminishingReturnsSetting result = null;
		List<DiminishingReturnsSetting> diminishingReturnsSettings = GameManager.instance.gameSettings.diminishingReturnsSettings;
		for (int i = 0; i < diminishingReturnsSettings.Count; i++)
		{
			DiminishingReturnsSetting diminishingReturnsSetting = diminishingReturnsSettings[i];
			if (diminishedAbility.uses >= diminishingReturnsSetting.uses)
			{
				result = diminishingReturnsSetting;
			}
		}
		return result;
	}
}
