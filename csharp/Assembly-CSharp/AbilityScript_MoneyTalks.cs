using System.Collections;
using UnityEngine;

public class AbilityScript_MoneyTalks : AbilityScript_Default
{
	private int currencyIncrement;

	private int leftover;

	protected override void HandleExtrasEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		string text = array[0];
		if (text != null && text == "spawnCoins" && _caster == BattleController.instance.playerCharacter)
		{
			Transform transform = _caster.extrasAnimator.transform.Find(array[1]);
			if (transform != null)
			{
				BattleController.instance.StartCoroutine(PlayCoinsplosion(10, transform));
			}
		}
	}

	private IEnumerator PlayCoinsplosion(int count, Transform target)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_coinsadd", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.1f);
		ParticleSystem pSystem = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/GUI/VictoryCoin_Particle System", default, isActive: false);
		WalletGUI walletGUI = BattleController.instance.walletGUI;
		currencyIncrement = Mathf.Max(1, Mathf.FloorToInt(count / pSystem.maxParticles));
		leftover = ((count > pSystem.maxParticles) ? (count % pSystem.maxParticles) : 0);
		pSystem.emission.SetBursts(new ParticleSystem.Burst[1]
		{
			new ParticleSystem.Burst(0f, (short)count)
		});
		Vector3 newPosition = BattleDisplayManager.instance.GetPositionInGUICamera(_caster, target);
		newPosition.z = -300f;
		pSystem.transform.position = newPosition;
		pSystem.gameObject.SetActive(value: true);
		walletGUI.particleAttractor.ParticleDestroyed += HandleOnParticleDestroyed;
		walletGUI.particleAttractor.AttractParticleSystem(pSystem);
		yield return new WaitWhile(() => walletGUI.particleAttractor.numParticleSystems > 0);
		walletGUI.particleAttractor.ParticleDestroyed -= HandleOnParticleDestroyed;
		ObjectPool.PoolObject(pSystem.gameObject);
	}

	private void HandleOnParticleDestroyed()
	{
		int num = currencyIncrement;
		if (leftover > 0)
		{
			num++;
			leftover--;
		}
		BattleController.instance.walletGUI.AddToWallet(num);
	}
}
