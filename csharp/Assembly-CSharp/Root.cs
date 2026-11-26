using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : StatusEffect
{
	private ActionMeter actionMeter;

	private PortraitSwitcher portraits;

	private Character character;

	private float duration;

	private string rootPrefabPath;

	private float timer;

	private GameObject rootGO;

	private List<AbilityButton> rootedButtons;

	private string btnRootSpriteName;

	private IEnumerator waitForExpireConfirmation;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => true;

	public string rootSpriteName => btnRootSpriteName;

	public Root(ActionMeter meter, PortraitSwitcher portraits, float duration, string prefabPath, string btnRootSpriteName, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		actionMeter = meter;
		this.portraits = portraits;
		character = actionMeter.character.character;
		this.duration = duration;
		rootedButtons = new List<AbilityButton>();
		rootPrefabPath = prefabPath;
		this.btnRootSpriteName = btnRootSpriteName;
	}

	private IEnumerator WaitForExpireConfirmation(float prevTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectExpire(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			timer = prevTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Expire();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		timer = duration;
		character.IncomingDamage += HandleIncomingDamage;
		character.Death += HandleDeath;
		actionMeter.CharacterChanged += HandleCharacterChanged;
		portraits.Enabled += HandlePortraitsEnabled;
		portraits.isEnabled = false;
		int count = actionMeter.abilityButtons.Count;
		BattleDisplayManager.instance.ShowMessage(Language.Get("ROOTED", "ABILS,CHIPS,ACHVS,LDBS"), actionMeter.character);
		if (!string.IsNullOrEmpty(rootPrefabPath))
		{
			rootGO = ObjectPool.GetObjectFromResource(rootPrefabPath, actionMeter.character.transform.position);
			rootGO.transform.localScale = actionMeter.character.transform.localScale;
		}
	}

	private void HandleCharacterChanged()
	{
		rootedButtons.Clear();
		int count = actionMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			AbilityButton abilityButton = actionMeter.abilityButtons[i];
			abilityButton.canActivate = !StatusEffectManager.instance.CharacterHasAbilityBlockerOnButton(actionMeter.character.character, abilityButton);
		}
	}

	private void HandleIncomingDamage(DamageResult result)
	{
		result.isDodged = false;
	}

	private void HandleDeath()
	{
		Remove();
	}

	private void HandlePortraitsEnabled()
	{
		portraits.isEnabled = false;
	}

	private void HandleAccessoryDestroyed()
	{
		Remove();
	}

	public bool IsButtonRooted(AbilityButton button)
	{
		return rootedButtons != null && rootedButtons.Contains(button);
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting || !(duration >= 0f) || !(timer > 0f))
		{
			return;
		}
		float prevTimer = timer;
		timer -= DeltaTime;
		if (!(timer <= 0f))
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForExpireConfirmation == null)
			{
				waitForExpireConfirmation = WaitForExpireConfirmation(prevTimer);
				BattleController.instance.StartCoroutine(waitForExpireConfirmation);
			}
		}
		else
		{
			Expire();
		}
	}

	protected override void ResetEffect()
	{
		timer = duration;
		BattleDisplayManager.instance.ShowMessage(Language.Get("ROOTED", "ABILS,CHIPS,ACHVS,LDBS"), actionMeter.character);
	}

	protected override void RemoveEffect()
	{
		if (waitForExpireConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForExpireConfirmation);
			waitForExpireConfirmation = null;
		}
		character.IncomingDamage -= HandleIncomingDamage;
		character.Death -= HandleDeath;
		actionMeter.CharacterChanged -= HandleCharacterChanged;
		portraits.Enabled -= HandlePortraitsEnabled;
		if (!BattleController.instance.waiting)
		{
			portraits.isEnabled = true;
		}
		int count = actionMeter.abilityButtons.Count;
		rootedButtons.Clear();
		rootedButtons = null;
		if (rootGO != null)
		{
			ObjectPool.PoolObject(rootGO);
			rootGO = null;
		}
	}

	protected override void CleanUp()
	{
	}
}
