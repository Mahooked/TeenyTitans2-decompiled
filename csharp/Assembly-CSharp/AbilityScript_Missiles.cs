using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript_Missiles : AbilityScript
{
	private Dictionary<string, ParticleSystem> activeSmoke = new Dictionary<string, ParticleSystem>();

	public override IEnumerator Play()
	{
		_caster.Play("missile", isAbilityAnimation: true);
		_casterCamera.Play("missile", isAbilityAnimation: true);
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/RocketsExplosion_Gizmo").transform;
		transform.localScale = _target.transform.localScale;
		yield break;
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		switch (evt.stringParameter)
		{
		case "smallHit":
			ShakeCamera(_casterCamera, 0.2f);
			_target.PlaySmallHit();
			break;
		case "hit":
			ProcessAbility();
			ShakeCamera(_casterCamera, 0.4f);
			break;
		}
	}

	protected override void HandleExtrasEventTriggered(AnimationEvent evt)
	{
		base.HandleExtrasEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		string text = array[0];
		ParticleSystem particleSystem = null;
		switch (text)
		{
		case "addSmoke":
		{
			Transform parent = _caster.extrasAnimator.transform.Find(array[1]);
			particleSystem = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/rockets_smoke_particle");
			particleSystem.transform.parent = parent;
			particleSystem.transform.localPosition = new Vector3(0f, 0f, -0.175f);
			activeSmoke.Add(array[1], particleSystem);
			break;
		}
		case "removeSmoke":
		{
			particleSystem = activeSmoke[array[1]];
			activeSmoke.Remove(array[1]);
			particleSystem.transform.parent = null;
			particleSystem.Stop();
			SelfDestruct selfDestruct = particleSystem.gameObject.AddComponent<SelfDestruct>();
			selfDestruct.destructType = SelfDestructType.Pool;
			selfDestruct.destroyComponent = true;
			break;
		}
		}
	}
}
