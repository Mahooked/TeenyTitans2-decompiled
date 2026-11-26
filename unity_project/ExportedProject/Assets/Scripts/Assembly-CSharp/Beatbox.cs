using UnityEngine;

public class Beatbox : MonoBehaviour, IPoolable
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public Transform spawner;

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
	}

	public void PlayOutro()
	{
		ObjectPool.GetObjectFromResource("Prefabs/FX/OmniExplosion", spawner.position);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_beatboxexplode", GFAudioManager.SFX_GROUP, string.Empty);
		animator.Play("intro", 0, 0f);
		ObjectPool.PoolObject(base.gameObject, 0.01f);
	}

	private void HandleEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter.Equals("complete"))
		{
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	public void ResetObject()
	{
		animator.transform.localPosition = Vector3.zero;
	}
}
