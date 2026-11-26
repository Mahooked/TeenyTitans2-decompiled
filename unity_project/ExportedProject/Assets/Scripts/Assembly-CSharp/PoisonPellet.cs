using DG.Tweening;
using UnityEngine;

public class PoisonPellet : MonoBehaviour, IPoolable
{
	public AnimatorEventListener animatorEventListener;

	public Transform pellet;

	public Transform emitTransform;

	public Vector3 offset;

	private ParticleSystem pSystem;

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
		pSystem = null;
	}

	public void TurnOff()
	{
		if (pSystem != null)
		{
			pSystem.EnableEmission(false);
			pSystem.gameObject.AddComponent<SelfDestruct>().Init(pSystem.duration, SelfDestructType.Pool, null, true);
		}
		ObjectPool.GetObjectFromResource("Prefabs/FX/OmniExplosion", pellet.position);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_beatboxexplode", GFAudioManager.SFX_GROUP, string.Empty);
		ObjectPool.PoolObject(base.gameObject);
	}

	private void HandleEventTriggered(AnimationEvent evt)
	{
		switch (evt.stringParameter)
		{
		case "startOffset":
		{
			Vector3 endValue = offset + new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
			animatorEventListener.transform.DOLocalMove(endValue, (float)evt.intParameter / 60f).SetEase(Ease.Linear);
			break;
		}
		case "open":
			pSystem = ObjectPool.GetObjectFromResource<ParticleSystem>("Prefabs/FX/PoisonPellet_ParticleSystem", (!(emitTransform == null)) ? emitTransform.position : pellet.position);
			pSystem.EnableEmission(true);
			break;
		}
	}

	public void ResetObject()
	{
		animatorEventListener.transform.localPosition = Vector3.zero;
	}
}
