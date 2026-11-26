using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AbilityScript_Portal : AbilityScript_Default
{
	public override IEnumerator Play()
	{
		yield return base.Play();
		_target.character.IncomingDamage += HandleIncomingDamage;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string stringParameter = evt.stringParameter;
		if (stringParameter != null && stringParameter == "hit" && !_target.character.isDead && _target.character.HasStatusEffect<KnockedOut>())
		{
			_target.transform.DOLocalMoveY(-2.5f, 0.75f).SetEase(Ease.InCubic);
		}
	}

	private void HandleIncomingDamage(DamageResult damageResult)
	{
		damageResult.impactType = ImpactType.Small;
	}

	public override void CleanUp()
	{
		base.CleanUp();
		_target.character.IncomingDamage -= HandleIncomingDamage;
	}
}
