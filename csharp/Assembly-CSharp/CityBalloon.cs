using System.Collections;
using UnityEngine;

public class CityBalloon : MonoBehaviour
{
	[SerializeField]
	private NewGUID _guid;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private GameObject _balloonGO;

	[SerializeField]
	private AEController _aeController;

	[SerializeField]
	private AudioClip _popSound;

	[SerializeField]
	private float _popSoundDelay;

	public bool isPopped { get; private set; }

	private void Start()
	{
		if (Communicator.instance != null)
		{
			Communicator.instance.StartDayTime += Refresh;
			Communicator.instance.StartNightTime += Refresh;
		}
		TransformLink component = GetComponent<TransformLink>();
		if (component != null && component.linkTo == null && CityController.instance != null)
		{
			component.LinkTo = CityController.instance.cameraHolder;
		}
		_animator.transform.localPosition = new Vector3(0f, 0f, -0.294f);
		Refresh();
	}

	private void OnDestroy()
	{
		if (Communicator.instance != null)
		{
			Communicator.instance.StartDayTime -= Refresh;
			Communicator.instance.StartNightTime -= Refresh;
		}
	}

	public void Pop()
	{
		if (!isPopped)
		{
			Player.instance.AddToDefeatedNPCs(_guid.guid);
			_aeController.Play("pop");
			GFAudioManager.instance.PlayClip(_popSound, 1f, loop: false, _popSoundDelay, 128, GFAudioManager.SFX_GROUP);
			Refresh();
		}
	}

	private void Refresh()
	{
		isPopped = Player.instance.HasDefeatedNPC(_guid.guid);
		if (isPopped)
		{
			if (_aeController != null)
			{
				if (_aeController.currentClip.name != "pop" || !_aeController.animator.isPlaying)
				{
					if (_balloonGO != null)
					{
						_balloonGO.gameObject.SetActive(value: false);
					}
				}
				else
				{
					StartCoroutine(WaitForPop());
				}
			}
			else if (_balloonGO != null)
			{
				_balloonGO.gameObject.SetActive(value: false);
			}
		}
		else
		{
			if (_balloonGO != null)
			{
				_balloonGO.gameObject.SetActive(value: true);
			}
			if (_aeController != null && _aeController.currentClip.name != "idle")
			{
				_aeController.Play("idle");
			}
		}
	}

	private IEnumerator WaitForPop()
	{
		yield return new WaitWhile(() => _aeController.currentClip.name == "pop" && _aeController.animator.isPlaying);
		Refresh();
	}
}
