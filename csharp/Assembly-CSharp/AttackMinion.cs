using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackMinion : StatusEffectMinion
{
	public enum MinionType
	{
		DarkMinion,
		Lutherbot
	}

	private static readonly List<Vector3> lexBotPositions = new List<Vector3>
	{
		new Vector3(-1.49f, 0f, 1.04f),
		new Vector3(0.96f, 0f, -0.9f)
	};

	private static readonly List<Vector3> darkMinionPositions = new List<Vector3>
	{
		new Vector3(-1.18f, 0f, 0.18f),
		new Vector3(0.66f, 0f, -1.07f)
	};

	private DarkMinion darkMinion;

	private MinionType minionType;

	private BattleCharacter caster;

	private bool queueAttack;

	private ActionMeter otherMeter;

	private PortraitSwitcher otherPortaits;

	private BattleController battleController;

	private int attackDamage;

	private bool continueSequence;

	private Coroutine luthorBotIntroRoutine;

	protected override List<Vector3> minionPositions => minionType switch
	{
		MinionType.Lutherbot => lexBotPositions, 
		_ => darkMinionPositions, 
	};

	protected override Vector3 minionLocalPosition => (!(darkMinion != null)) ? Vector3.zero : darkMinion.transform.position;

	public AttackMinion(MinionType minionType, BattleCharacter caster, ActionMeter targetMeter, float duration, int attackDamage, Accessory casterAccessory = null)
		: base(targetMeter, duration, casterAccessory)
	{
		this.minionType = minionType;
		this.caster = caster;
		this.attackDamage = attackDamage;
	}

	protected override void InitMinionEffect()
	{
		string path;
		string attackAnim;
		string outroAnim;
		string deathOutroAnim;
		switch (minionType)
		{
		case MinionType.Lutherbot:
			path = "Prefabs/FX/LexLuthor_Minion";
			attackAnim = "attack";
			outroAnim = "swap out";
			deathOutroAnim = "death";
			break;
		default:
			path = "Prefabs/FX/RadicalRaven_Minion";
			attackAnim = "rr_ghost_attack_edit";
			outroAnim = "rr_ghost_portalAway_edit";
			deathOutroAnim = "rr_ghost_death_edit";
			break;
		}
		darkMinion = ObjectPool.GetObjectFromResource<DarkMinion>(path, caster.model.position);
		darkMinion.Init(attackAnim, outroAnim, deathOutroAnim);
		darkMinion.transform.localScale = caster.transform.localScale;
		battleController = BattleController.instance;
		battleController.WaitingEnded += HandleWaitingEnded;
		target.OutgoingDamage += HandleOutgoingDamage;
		targetMeter.CharacterChanged += HandleCharacterChanged;
		queueAttack = false;
		bool flag = targetMeter == battleController.playerActionMeter;
		otherMeter = ((!flag) ? battleController.playerActionMeter : battleController.enemyActionMeter);
		otherPortaits = ((!flag) ? battleController.playerPortraits : battleController.enemyPortraits);
		Vector3 position = targetMinionPosition;
		position.z = ((!flag) ? (0f - position.z) : position.z);
		darkMinion.transform.position = position;
		if (minionType == MinionType.Lutherbot)
		{
			luthorBotIntroRoutine = darkMinion.StartCoroutine(LuthorBotIntoDelay());
		}
	}

	private IEnumerator LuthorBotIntoDelay()
	{
		darkMinion.animator.speed = 0f;
		yield return new WaitForSeconds(0.6f);
		darkMinion.animator.speed = 1f;
	}

	protected override void UpdateMinionEffect()
	{
	}

	protected override void RemoveMinionEffect()
	{
		if (diedFromDamage)
		{
			darkMinion.PlayDeathOutro();
		}
		else
		{
			darkMinion.PlayOutro();
		}
		if (luthorBotIntroRoutine != null)
		{
			darkMinion.StopCoroutine(luthorBotIntroRoutine);
		}
		luthorBotIntroRoutine = null;
	}

	protected override void CleanUp()
	{
		battleController.WaitingEnded -= HandleWaitingEnded;
		target.OutgoingDamage -= HandleOutgoingDamage;
	}

	private void HandleCharacterChanged()
	{
		target.OutgoingDamage -= HandleOutgoingDamage;
		target = targetMeter.character.character;
		target.OutgoingDamage += HandleOutgoingDamage;
	}

	private void HandleWaitingEnded()
	{
		if (queueAttack)
		{
			queueAttack = false;
			battleController.StartCoroutine(AttackSequence());
		}
	}

	private void HandleOutgoingDamage(DamageResult damageResult)
	{
		if (damageResult.damage > 0 && damageResult.abilityData != null && !damageResult.isDodged && damageResult.abilityData.channelingData.speed <= 0f)
		{
			queueAttack = true;
		}
	}

	private IEnumerator AttackSequence()
	{
		if (otherMeter.character != null && otherMeter.character.character != null)
		{
			while (battleController.waiting)
			{
				yield return null;
			}
			battleController.ForceStartWaiting();
			continueSequence = false;
			Action continueCallback = () =>
			{
				continueSequence = true;
			};
			darkMinion.PlayAttack(continueCallback);
			while (!continueSequence)
			{
				yield return null;
			}
			continueSequence = false;
			if (minionType == MinionType.DarkMinion)
			{
				darkMinion.projectileObj.SetActive(value: true);
				darkMinion.projectileObj.transform.position = darkMinion.projectileSpawnPoint.position;
				Sequence tween = DOTween.Sequence();
				tween.Append(darkMinion.projectileObj.transform.DOMoveX((!(otherMeter.character.center == null)) ? otherMeter.character.center.position.x : otherMeter.character.transform.position.x, 0.2f));
				tween.Insert(0f, darkMinion.projectileObj.transform.DOMoveZ((!(otherMeter.character.center == null)) ? otherMeter.character.center.position.z : otherMeter.character.transform.position.z, 0.2f));
				yield return tween.WaitForCompletion();
				darkMinion.projectileObj.SetActive(value: false);
				darkMinion.projectileObj.transform.localPosition = Vector3.zero;
			}
			DamageResult damageResult = new DamageResult(attackDamage, ImpactType.Small)
			{
				source = target,
				target = otherMeter.character.character
			};
			otherMeter.character.character.DoDamage(damageResult);
			yield return darkMinion.WaitForAttackAnimationFnished();
			if (otherMeter.character.character.isDead)
			{
				battleController.CheckCharacterDeath();
			}
			else
			{
				battleController.ForceEndWaiting();
			}
		}
	}

	protected override void ResetEffect()
	{
	}
}
