using UnityEngine;

public class Drone : MonoBehaviour
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public string attackSoundPath = string.Empty;

	public float playAttackOffset;

	public Transform explosionPosition;

	public bool useRandomExplosionRotation;

	protected virtual void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	protected virtual void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
	}

	public virtual void PlayAttack()
	{
		if (!string.IsNullOrEmpty(attackSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(attackSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public virtual void PlayOutro()
	{
		if (animator.HasState(0, "outro"))
		{
			animator.Play("outro");
		}
		else
		{
			Remove();
		}
	}

	private void HandleEventTriggered(AnimationEvent obj)
	{
		if (obj.stringParameter == "complete")
		{
			Remove();
		}
	}

	private void Remove()
	{
		ObjectPool.PoolObject(gameObject);
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/OmniExplosion", (!(explosionPosition != null)) ? base.transform.position : explosionPosition.position).transform;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_beatboxexplode", GFAudioManager.SFX_GROUP, string.Empty);
		if (useRandomExplosionRotation)
		{
			transform.localRotation = Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
		}
	}
}
