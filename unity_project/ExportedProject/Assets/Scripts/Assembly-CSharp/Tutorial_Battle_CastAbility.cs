using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Battle_CastAbility : MonoBehaviour
{
	public BattleController battleController;

	public List<string> triggersRequired;

	private void Start()
	{
		if (Player.instance.HasTriggered("tutorialCastAbility") || Time.frameCount == 1)
		{
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			battleController.playerActionMeter.CastStartedAbility += HandleCastStartedAbility;
		}
	}

	private void HandleCastStartedAbility(ActionMeter meter, AbilityButton button)
	{
		battleController.playerActionMeter.CastStartedAbility -= HandleCastStartedAbility;
		StartCoroutine(WaitForHalfCast(meter, button));
	}

	private IEnumerator WaitForHalfCast(ActionMeter meter, AbilityButton button)
	{
		battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = false;
		yield return null;
		float maxCastWidth = meter.maxWidth * ((float)button.position * 0.01f);
		float halfCastWidth = maxCastWidth * 0.5f;
		yield return new WaitWhile(() => meter.fillCastSprite.width < halfCastWidth);
		PauseManager.instance.isEnabled = false;
		Time.timeScale = 0f;
		Vector3 position = meter.fillCastSprite.transform.position + new Vector3(meter.fillCastSprite.width, 0f, 0f);
		TutorialPopup popup = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		popup.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		popup.SetMessage(Language.Get("PURPLECHARGE", "TUTORIALS"));
		popup.transform.position = position + new Vector3(0f, 10f, 0f);
		yield return new WaitForTap();
		popup.SetMessage(Language.Get("PURPLECHARGE2", "TUTORIALS"));
		yield return new WaitForTap();
		ObjectPool.PoolObject(popup.gameObject);
		Time.timeScale = 1f;
		PauseManager.instance.isEnabled = true;
		bool abilityUsed = false;
		Action<ActionMeter, Character, AbilityData> abilityUsedDelegate = null;
		abilityUsedDelegate = (ActionMeter actionMeter, Character character, AbilityData abilityData) =>
		{
			abilityUsed = true;
		};
		meter.AbilityUsed += abilityUsedDelegate;
		yield return new WaitWhile(() => !abilityUsed);
		meter.AbilityUsed -= abilityUsedDelegate;
		battleController.enemyActionMeter.GetComponent<PlayerAI>().isEnabled = true;
		Player.instance.SetTrigger("tutorialCastAbility");
	}
}
