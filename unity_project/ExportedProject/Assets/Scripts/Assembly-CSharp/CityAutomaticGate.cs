using System.Collections;
using UnityEngine;

public class CityAutomaticGate : MonoBehaviour
{
	[SerializeField]
	private float _checkDistance = 1f;

	[SerializeField]
	private float _checkDelay = 0.5f;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private string _openStateName;

	[SerializeField]
	private string _closeStateName;

	[SerializeField]
	private AudioClip openSound;

	[SerializeField]
	private AudioClip closeSound;

	private CityCharacter _cityCharacter;

	private int instanceId;

	public bool isInRange { get; private set; }

	private void Start()
	{
		_cityCharacter = CityController.instance.cityCharacter;
		instanceId = GetInstanceID();
		float num = Vector3.Distance(_cityCharacter.transform.position, base.transform.position);
		if (num <= _checkDistance)
		{
			isInRange = true;
			_animator.Play(_openStateName, 0, 1f);
		}
		else
		{
			isInRange = false;
			_animator.Play(_closeStateName, 0, 1f);
		}
	}

	private void OnBecameVisible()
	{
		StartCoroutine("CheckForCharacter");
	}

	private void OnBecameInvisible()
	{
		StopCoroutine("CheckForCharacter");
	}

	private IEnumerator CheckForCharacter()
	{
		while (true)
		{
			float dist = Vector3.Distance(_cityCharacter.transform.position, base.transform.position);
			if (dist <= _checkDistance && !isInRange)
			{
				isInRange = true;
				_animator.Play(_openStateName, 0, (!_animator.IsPlaying()) ? 0f : (1f - _animator.NormalizedTime()));
				GFAudioManager.instance.StopById("automaticGate_" + instanceId);
				if (openSound != null)
				{
					GFAudioManager.instance.PlayClip(openSound, "automaticGate_" + instanceId, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
				}
			}
			else if (dist > _checkDistance && isInRange)
			{
				isInRange = false;
				_animator.Play(_closeStateName, 0, (!_animator.IsPlaying()) ? 0f : (1f - _animator.NormalizedTime()));
				GFAudioManager.instance.StopById("automaticGate_" + instanceId);
				if (closeSound != null)
				{
					GFAudioManager.instance.PlayClip(closeSound, "automaticGate_" + instanceId, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
				}
			}
			yield return new WaitForSeconds(_checkDelay);
		}
	}
}
