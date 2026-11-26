using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AEController : MonoBehaviour
{
	public AEAnimator animator;

	public bool playAutomatically;

	public List<AEClip> clips = new List<AEClip>();

	private Dictionary<string, AEClip> _clipDictionary;

	private List<AEClip> _clipQueue;

	[SerializeField]
	private int _nextID = -1;

	[SerializeField]
	private float _pivotX = 0.5f;

	[SerializeField]
	private float _pivotY = 0.5f;

	[SerializeField]
	private string _defaultClipName;

	private AEClip _defaultClip;

	private AEClip _currentClip;

	[SerializeField]
	private float _timeScale = 1f;

	public int nextID
	{
		get
		{
			_nextID++;
			return _nextID;
		}
	}

	public float pivotX
	{
		get
		{
			return _pivotX;
		}
		set
		{
			if (value != _pivotX)
			{
				_pivotX = value;
			}
		}
	}

	public float pivotY
	{
		get
		{
			return _pivotY;
		}
		set
		{
			if (value != _pivotY)
			{
				_pivotY = value;
			}
		}
	}

	public string defaultClipName => _defaultClipName;

	public AEClip defaultClip
	{
		get
		{
			if (_defaultClip == null)
			{
				_defaultClip = GetClipByName(_defaultClipName);
			}
			return _defaultClip;
		}
	}

	public AEClip currentClip => _currentClip;

	public float timeScale
	{
		get
		{
			return _timeScale;
		}
		set
		{
			if (value != _timeScale)
			{
				_timeScale = value;
			}
		}
	}

	public bool useUnscaledTime
	{
		get
		{
			return animator.useUnscaledTime;
		}
		set
		{
			animator.useUnscaledTime = value;
		}
	}

	public bool isFlipped
	{
		get
		{
			return animator.isFlipped;
		}
		set
		{
			animator.isFlipped = value;
			animator.GoToCurrentFrameForced();
		}
	}

	public event Action<AEController> AnimationChanged;

	public event Action<AEClip> LoopComplete;

	private void Awake()
	{
		_clipDictionary = new Dictionary<string, AEClip>();
		_clipQueue = new List<AEClip>();
		int count = clips.Count;
		for (int i = 0; i < count; i++)
		{
			_clipDictionary.Add(clips[i].name, clips[i]);
		}
		animator.AnimationComplete += HandleAnimationComplete;
		animator.LoopComplete += HandleLoopComplete;
		SetCurrentClip(defaultClip);
		OnAnimationChanged();
	}

	private void Reset()
	{
		animator = GetComponent<AEAnimator>();
		if (animator != null && animator.animationAsset != null)
		{
			AEClip aEClip = new AEClip();
			aEClip.id = nextID;
			aEClip.animation = animator.animationAsset;
			aEClip.name = "animation_" + aEClip.id;
			AddClip(aEClip);
		}
	}

	public void Play()
	{
		if (_currentClip != null)
		{
			animator.GoToAndPlay(0);
		}
	}

	public void Play(string clipName)
	{
		if (_clipDictionary == null)
		{
			return;
		}
		if (!_clipDictionary.ContainsKey(clipName))
		{
			Debug.LogError($"No clip by the name of {clipName} exists in this AEController");
			return;
		}
		AEClip aEClip = _clipDictionary[clipName];
		if (aEClip != _currentClip)
		{
			SetCurrentClip(aEClip);
			animator.GoToAndPlay(0);
			OnAnimationChanged();
		}
	}

	public void Play(string clipName, bool clearQueue)
	{
		if (clearQueue)
		{
			ClearClipQueue();
		}
		Play(clipName);
	}

	public void PlayFromFrame(string clipName, int frame, bool clearQueue = false)
	{
		Play(clipName, clearQueue);
		animator.GoToAndPlay(frame);
	}

	public void Stop()
	{
		animator.Stop();
	}

	public void AddToQueue(string clipName)
	{
		if (_clipDictionary.ContainsKey(clipName))
		{
			_clipQueue.Add(_clipDictionary[clipName]);
		}
	}

	public void ClearClipQueue()
	{
		_clipQueue.Clear();
	}

	public void AddClip(AEClip newClip)
	{
		clips.Add(newClip);
		if (string.IsNullOrEmpty(_defaultClipName))
		{
			_defaultClipName = newClip.name;
			animator.animationAsset = newClip.animation;
		}
	}

	public void RemoveClip(AEClip clip)
	{
		if (clip != null && clips.Contains(clip))
		{
			clips.Remove(clip);
			if (clip.name.Equals(_defaultClipName) && clips.Count > 0)
			{
				SetDefaultClip(clips[0]);
			}
			else if (clips.Count == 0)
			{
				_defaultClipName = string.Empty;
				animator.Rebuild();
			}
		}
	}

	public void RemoveClip(string clipName)
	{
		RemoveClip(GetClipByName(clipName));
	}

	public void SetClipName(AEClip clip, string newName)
	{
		if (clips.Contains(clip))
		{
			if (clip.name.Equals(_defaultClipName))
			{
				_defaultClipName = newName;
			}
			clips[clips.IndexOf(clip)].name = newName;
		}
	}

	public void SetDefaultClip(AEClip clip)
	{
		if (clips.Contains(clip) && !clip.name.Equals(_defaultClipName))
		{
			_defaultClipName = clip.name;
		}
	}

	public bool ContainsClip(string clipName)
	{
		return _clipDictionary.ContainsKey(clipName);
	}

	public AEClip GetClipByName(string clipName)
	{
		if (_clipDictionary.ContainsKey(clipName))
		{
			return _clipDictionary[clipName];
		}
		return null;
	}

	private void HandleAnimationComplete(AEAnimator animator)
	{
		CheckQueue();
	}

	private void HandleLoopComplete()
	{
		OnLoopComplete();
	}

	private void CheckQueue()
	{
		if (_clipQueue.Count > 0)
		{
			AEClip aEClip = _clipQueue[0];
			_clipQueue.RemoveAt(0);
			Play(aEClip.name);
		}
	}

	private void SetCurrentClip(AEClip clip)
	{
		if (clip != null && clips.Contains(clip) && clip != _currentClip)
		{
			_currentClip = clip;
			if (_currentClip.useUniquePivot)
			{
				animator.SetAnimation(_currentClip.animation, _currentClip.pivotX, _currentClip.pivotY);
			}
			else
			{
				animator.SetAnimation(_currentClip.animation, _pivotX, _pivotY);
			}
			animator.looping = _currentClip.looping;
			animator.timeScale = _timeScale * _currentClip.timeScale;
		}
	}

	protected void OnAnimationChanged()
	{
		if (AnimationChanged != null)
		{
			AnimationChanged(this);
		}
	}

	protected void OnLoopComplete()
	{
		if (LoopComplete != null)
		{
			LoopComplete(_currentClip);
		}
	}
}
