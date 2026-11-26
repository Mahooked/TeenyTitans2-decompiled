using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AEController))]
public class AERareAnimation : MonoBehaviour
{
	public string clipToWatch;

	public List<string> clipsToPlay;

	public float probability = 0.25f;

	public int minLoops;

	private AEClip _activeClip;

	private int _loopCounter;

	private bool _clipToWatchActive;

	[SerializeField]
	private AEController _controller;

	public AEController controller => _controller;

	private void Awake()
	{
		if (_controller == null)
		{
			_controller = GetComponent<AEController>();
		}
		_controller.AnimationChanged += HandleAnimationChanged;
		_controller.LoopComplete += HandleLoopComplete;
	}

	private void Start()
	{
		HandleAnimationChanged(_controller);
	}

	private void OnEnable()
	{
		if (_controller.currentClip == null)
		{
			return;
		}
		for (int i = 0; i < clipsToPlay.Count; i++)
		{
			if (clipsToPlay[i].Equals(_controller.currentClip.name))
			{
				StartCoroutine(WaitForClip(clipsToPlay[i]));
				break;
			}
		}
	}

	private void Reset()
	{
		_controller = GetComponent<AEController>();
		clipToWatch = string.Empty;
		if (clipsToPlay != null)
		{
			clipsToPlay.Clear();
		}
		probability = 0.25f;
	}

	private void HandleAnimationChanged(AEController controller)
	{
		_clipToWatchActive = controller.currentClip.name.Equals(clipToWatch);
		_loopCounter = 0;
	}

	private void HandleLoopComplete(AEClip clip)
	{
		if (_clipToWatchActive && clipsToPlay.Count > 0)
		{
			_loopCounter++;
			if (_loopCounter > minLoops && Random.value <= probability)
			{
				StartCoroutine(WaitForClip(clipsToPlay[Random.Range(0, clipsToPlay.Count)]));
			}
		}
	}

	private IEnumerator WaitForClip(string clipName)
	{
		_controller.Play(clipName);
		_activeClip = _controller.currentClip;
		while (_controller.currentClip == _activeClip)
		{
			if (!_controller.animator.isPlaying)
			{
				_controller.Play(clipToWatch);
				_activeClip = null;
				break;
			}
			yield return null;
		}
	}
}
