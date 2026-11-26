using System.Collections.Generic;
using UnityEngine;

public class PlayerAI_Network : PlayerAI
{
	public int NextAbilitySeed { get; set; }

	protected override void Update()
	{
	}

	public void TofuUsed(int tofuTypeID, int seed = -1)
	{
		if (battleController != null && battleController.isBattleRunning)
		{
			TofuEffect tofuEffect = new GameObject("TofuEffect - " + battleController.enemyCharacter.characterDefinition.characterName).AddComponent<TofuEffect>();
			tofuEffect.StartEffectForTarget((TofuType)tofuTypeID, battleController.enemyCharacter, null, seed);
			Debug.Log("Use Tofu type " + (TofuType)tofuTypeID);
		}
	}

	public void SwapCharacter(int characterUID)
	{
		Debug.Log("Swap character");
		Character character = party.Find((Character x) => x.characterDefinition.uid == characterUID);
		if (character != null)
		{
			portraits.SelectCharacter(character);
		}
	}

	public void MakeDecision(int abilityUID, bool isCast, float finalDamageMultiplier)
	{
		List<AbilityButton> activatedAbilityButtons = actionMeter.GetActivatedAbilityButtons();
		AbilityButton abilityButton = null;
		if (activatedAbilityButtons.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < activatedAbilityButtons.Count; i++)
		{
			if (activatedAbilityButtons[i].abilityData.uid == abilityUID)
			{
				abilityButton = activatedAbilityButtons[i];
				break;
			}
		}
		if (!(abilityButton != null))
		{
			return;
		}
		if (isCast)
		{
			actionMeter.StartNetworkCasting();
			PressButton(abilityButton);
			return;
		}
		battleCharacter.character.finalDamageMultiplier = finalDamageMultiplier;
		if (actionMeter.isNetworkCasting)
		{
			actionMeter.FinishNetworkCasting();
		}
		else
		{
			PressButton(abilityButton);
		}
	}

	public override void HandleIncomingDamage(DamageResult damageResult)
	{
	}

	public override void HandleDamageReceived(DamageResult damageResult)
	{
	}
}
