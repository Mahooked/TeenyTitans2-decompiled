using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class GFAudioManager : MonoBehaviour
{
	public static string MUSIC_GROUP = "musicGroup";

	public static string SFX_GROUP = "sfxGroup";

	public static string VOICEOVER_GROUP = "voiceOverGroup";

	private static bool _applicationQuit;

	private static bool _destroyed;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SoundGroupMuteChanged__BackingField;

	public int numPooledAtStart = 5;

	public float loopDelay = 0.5f;

	public float threshold = 0.1f;

	public List<AudioClip> preloadedSounds = new List<AudioClip>();

	private Transform _sourceHolder;

	private List<AudioSource> _pooledSources;

	private int curPoolSize;

	private Dictionary<string, AudioClip> _preloadedClips = new Dictionary<string, AudioClip>();

	private Dictionary<string, AudioSource> _trackedSources;

	private Dictionary<AudioSource, string> _trackedSourcesKeys;

	private Dictionary<string, GFSoundGroup> _groupedSources;

	private Dictionary<AudioSource, string> _groupedSourcesKeys;

	private Dictionary<AudioClip, float> _lastPlayTime = new Dictionary<AudioClip, float>();

	private List<AudioSource> _activeSources;

	private List<AudioSource> _pausedSources;

	private Coroutine loopRoutine;

	private static GFAudioManager _instance;

	public static GFAudioManager instance
	{
		get
		{
			if (!_destroyed && !_applicationQuit && _instance == null)
			{
				_instance = new GameObject("GFAudioManager").AddComponent<GFAudioManager>();
			}
			return _instance;
		}
	}

	public event Action SoundGroupMuteChanged
	{
		add
		{
			Action action = SoundGroupMuteChanged__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SoundGroupMuteChanged__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SoundGroupMuteChanged__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SoundGroupMuteChanged__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		_instance = this;
		_sourceHolder = new GameObject("Sources").transform;
		_sourceHolder.parent = base.transform;
		_pooledSources = new List<AudioSource>();
		for (int i = 0; i < numPooledAtStart; i++)
		{
			AudioSource audioSource = new GameObject("AudioSource_" + i).AddComponent<AudioSource>();
			audioSource.transform.parent = _sourceHolder;
			audioSource.gameObject.SetActive(false);
			_pooledSources.Add(audioSource);
			curPoolSize++;
		}
		int count = preloadedSounds.Count;
		for (int j = 0; j < count; j++)
		{
			if (!(preloadedSounds[j] == null))
			{
				_preloadedClips.Add(preloadedSounds[j].name, preloadedSounds[j]);
			}
		}
		_trackedSources = new Dictionary<string, AudioSource>();
		_trackedSourcesKeys = new Dictionary<AudioSource, string>();
		_groupedSources = new Dictionary<string, GFSoundGroup>();
		_groupedSourcesKeys = new Dictionary<AudioSource, string>();
		_activeSources = new List<AudioSource>();
		_pausedSources = new List<AudioSource>();
		AddSoundGroup(MUSIC_GROUP, PlayerPrefs.GetFloat(GetVolumeKey(MUSIC_GROUP), 1f));
		AddSoundGroup(SFX_GROUP, PlayerPrefs.GetFloat(GetVolumeKey(SFX_GROUP), 1f));
		AddSoundGroup(VOICEOVER_GROUP, PlayerPrefs.GetFloat(GetVolumeKey(VOICEOVER_GROUP), 1f));
		MuteGroup(MUSIC_GROUP, PlayerPrefs.GetInt(GetIsMutedKey(MUSIC_GROUP), 0) == 1);
		MuteGroup(SFX_GROUP, PlayerPrefs.GetInt(GetIsMutedKey(SFX_GROUP), 0) == 1);
		MuteGroup(VOICEOVER_GROUP, PlayerPrefs.GetInt(GetIsMutedKey(VOICEOVER_GROUP), 0) == 1);
	}

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnApplicationQuit()
	{
		_applicationQuit = true;
	}

	private void OnDestroy()
	{
		_destroyed = true;
	}

	public void Init()
	{
	}

	public AudioSource PlayClip(string clipName, float volume = 1f, bool loop = false, float delay = 0f, int priority = 128, string group = "", bool ignoreLastPlayTime = false)
	{
		if (_preloadedClips.ContainsKey(clipName))
		{
			return PlayClip(_preloadedClips[clipName], volume, loop, delay, priority, group, ignoreLastPlayTime);
		}
		return null;
	}

	public AudioSource PlayClip(AudioClip clip, float volume = 1f, bool loop = false, float delay = 0f, int priority = 128, string group = "", bool ignoreLastPlayTime = false)
	{
		if (clip == null)
		{
			return null;
		}
		if (!ignoreLastPlayTime && _lastPlayTime.ContainsKey(clip) && Time.unscaledTime + delay - _lastPlayTime[clip] < threshold)
		{
			return null;
		}
		if (_lastPlayTime.ContainsKey(clip))
		{
			_lastPlayTime[clip] = Time.unscaledTime + delay;
		}
		else
		{
			_lastPlayTime.Add(clip, Time.unscaledTime);
		}
		AudioSource audioSource = GetAudioSource();
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.loop = loop;
		audioSource.priority = priority;
		audioSource.PlayDelayed(delay);
		if (!string.IsNullOrEmpty(group))
		{
			GFSoundGroup value = null;
			if (!_groupedSources.TryGetValue(group, out value))
			{
				value = new GFSoundGroup();
				_groupedSources.Add(group, value);
			}
			value.sources.Add(audioSource);
			if (_groupedSourcesKeys.ContainsKey(audioSource))
			{
				UnityEngine.Debug.LogFormat("[GFAudioManager::PlayClip] - For some reason there is already a key for the AudioSource with clip currently named {0}, and for sound group {1}. This shouldn't happen, but for now we will just update the sound group.", audioSource.clip.name, _groupedSourcesKeys[audioSource]);
				_groupedSourcesKeys[audioSource] = group;
			}
			else
			{
				_groupedSourcesKeys.Add(audioSource, group);
			}
			audioSource.mute = value.isMuted;
		}
		if (loopRoutine == null)
		{
			loopRoutine = StartCoroutine(Loop());
		}
		return audioSource;
	}

	public AudioSource PlayClip(string clipName, string id, float volume = 1f, bool loop = false, float delay = 0f, int priority = 128, string group = "", bool ignoreLastPlayTime = false)
	{
		if (string.IsNullOrEmpty(clipName))
		{
			return null;
		}
		if (_trackedSources.ContainsKey(id))
		{
			return null;
		}
		AudioSource audioSource = PlayClip(clipName, volume, loop, delay, priority, group, ignoreLastPlayTime);
		if (audioSource != null)
		{
			_trackedSources.Add(id, audioSource);
			_trackedSourcesKeys.Add(audioSource, id);
		}
		return audioSource;
	}

	public AudioSource PlayClip(AudioClip clip, string id, float volume = 1f, bool loop = false, float delay = 0f, int priority = 128, string group = "", bool ignoreLastPlayTime = false)
	{
		if (clip == null)
		{
			UnityEngine.Debug.LogError("Clip cannot be NULL");
			return null;
		}
		if (_trackedSources.ContainsKey(id))
		{
			UnityEngine.Debug.LogError("ID '" + id + "' already exists for a current sound");
			return null;
		}
		AudioSource audioSource = PlayClip(clip, volume, loop, delay, priority, group, ignoreLastPlayTime);
		if (!string.IsNullOrEmpty(id) && audioSource != null)
		{
			_trackedSources.Add(id, audioSource);
			_trackedSourcesKeys.Add(audioSource, id);
		}
		return audioSource;
	}

	public AudioSource PlayClipFromResources(string clipPath, string group = "", string id = "", float volume = 1f, float delay = 0f, bool loop = false, bool ignoreLastPlayTime = false, bool addToPreloaded = true)
	{
		if (string.IsNullOrEmpty(clipPath))
		{
			return null;
		}
		if (_preloadedClips.ContainsKey(clipPath))
		{
			return PlayClip(_preloadedClips[clipPath], id, volume, loop, delay, 128, group, ignoreLastPlayTime);
		}
		AudioClip audioClip = Resources.Load(clipPath) as AudioClip;
		if (audioClip == null)
		{
			UnityEngine.Debug.LogError("No AudioClip was found at path: " + clipPath);
			return null;
		}
		if (addToPreloaded)
		{
			_preloadedClips.Add(clipPath, audioClip);
		}
		return PlayClip(audioClip, id, volume, loop, delay, 128, group, ignoreLastPlayTime);
	}

	public void StopById(string id)
	{
		if (_trackedSources.ContainsKey(id))
		{
			AudioSource audioSource = _trackedSources[id];
			if (audioSource != null)
			{
				audioSource.Stop();
			}
			ReturnAudioSource(audioSource);
		}
	}

	public void StopByClip(AudioClip clip)
	{
		int num = _activeSources.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (_activeSources[num2].clip == clip)
			{
				_activeSources[num2].Stop();
				ReturnAudioSource(_activeSources[num2]);
			}
		}
	}

	public void StopByAudioSource(AudioSource source)
	{
		int num = _activeSources.IndexOf(source);
		if (num > -1)
		{
			_activeSources[num].Stop();
			ReturnAudioSource(_activeSources[num]);
		}
	}

	public void StopAll()
	{
		int num = _activeSources.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			_activeSources[num2].Stop();
			ReturnAudioSource(_activeSources[num2]);
		}
	}

	public void AddPreloadedClip(AudioClip clip, string clipName)
	{
		if (_preloadedClips.ContainsKey(clipName))
		{
			UnityEngine.Debug.LogError("That name is already in use by another AudioClip object");
		}
		else if (_preloadedClips.ContainsValue(clip))
		{
			UnityEngine.Debug.LogError("That AudioClip is already a part of the preloaded clip collection");
		}
		else
		{
			_preloadedClips.Add(clipName, clip);
		}
	}

	public void RemovePreloadedClip(string clipName)
	{
		if (_preloadedClips.ContainsKey(clipName))
		{
			if (_lastPlayTime.ContainsKey(_preloadedClips[clipName]))
			{
				_lastPlayTime.Remove(_preloadedClips[clipName]);
			}
			_preloadedClips.Remove(clipName);
		}
	}

	public void AddSoundGroup(string group, float initialVolume = 1f)
	{
		if (_groupedSources.ContainsKey(group))
		{
			UnityEngine.Debug.LogError(string.Format("A sound group with the name '{0}' already exists", group));
		}
		else
		{
			_groupedSources.Add(group, new GFSoundGroup(initialVolume));
		}
	}

	public void FadeOut(string id, float time, bool useUnscaledTime = false, bool unloadClip = false)
	{
		if (!_trackedSources.ContainsKey(id))
		{
			UnityEngine.Debug.LogError("There is no AudioSource with that ID");
			return;
		}
		AudioSource source = _trackedSources[id];
		source.DOFade(0f, time).SetEase(Ease.Linear).SetUpdate(useUnscaledTime)
			.OnComplete(() =>
			{
				OnFadeComplete(source, unloadClip);
			});
	}

	public void FadeTo(string id, float volume, float time)
	{
		if (!_trackedSources.ContainsKey(id))
		{
			UnityEngine.Debug.LogWarning("There is no AudioSource with that ID");
			return;
		}
		AudioSource target = _trackedSources[id];
		target.DOFade(volume, time).SetEase(Ease.Linear);
	}

	public void FadeOutGroup(string group, float time)
	{
		if (_groupedSources.ContainsKey(group))
		{
			DOTween.To(() => numPooledAtStart, (int x) =>
			{
				numPooledAtStart = x;
			}, 5, time).OnUpdate(() =>
			{
				SetGroupVolume(group, GetGroupVolume(group));
			}).OnComplete(() =>
			{
				OnFadeGroupComplete(group);
			});
		}
	}

	public void SetGroupVolume(string group, float volume)
	{
		if (_groupedSources.ContainsKey(group))
		{
			_groupedSources[group].volume = volume;
			PlayerPrefs.SetFloat(GetVolumeKey(group), volume);
			List<AudioSource> sources = _groupedSources[group].sources;
			int count = sources.Count;
			for (int i = 0; i < count; i++)
			{
				sources[i].volume = volume;
			}
		}
		else
		{
			UnityEngine.Debug.LogError(string.Format("A group with the name '{0}' does not exist.", group));
		}
	}

	public float GetGroupVolume(string group)
	{
		if (_groupedSources.ContainsKey(group))
		{
			return _groupedSources[group].volume;
		}
		return 1f;
	}

	public void MuteGroup(string group, bool mute = true)
	{
		if (_groupedSources.ContainsKey(group))
		{
			GFSoundGroup gFSoundGroup = _groupedSources[group];
			gFSoundGroup.isMuted = mute;
			PlayerPrefs.SetInt(GetIsMutedKey(group), mute ? 1 : 0);
			PlayerPrefs.Save();
			int count = gFSoundGroup.sources.Count;
			for (int i = 0; i < count; i++)
			{
				gFSoundGroup.sources[i].mute = gFSoundGroup.isMuted;
			}
			if (SoundGroupMuteChanged__BackingField != null)
			{
				SoundGroupMuteChanged__BackingField();
			}
		}
	}

	public bool IsGroupMuted(string group)
	{
		if (_groupedSources.ContainsKey(group))
		{
			return _groupedSources[group].isMuted;
		}
		return false;
	}

	public void Pause(string id)
	{
		if (_trackedSources.ContainsKey(id))
		{
			AudioSource audioSource = _trackedSources[id];
			if (!_pausedSources.Contains(audioSource))
			{
				audioSource.Pause();
				_pausedSources.Add(audioSource);
			}
		}
	}

	public void UnPause(string id)
	{
		if (_trackedSources.ContainsKey(id))
		{
			AudioSource audioSource = _trackedSources[id];
			if (_pausedSources.Contains(audioSource))
			{
				audioSource.UnPause();
				_pausedSources.Remove(audioSource);
			}
		}
	}

	public void PauseGroup(string groupName)
	{
		if (_groupedSources.ContainsKey(groupName))
		{
			GFSoundGroup gFSoundGroup = _groupedSources[groupName];
			int count = gFSoundGroup.sources.Count;
			for (int i = 0; i < count; i++)
			{
				gFSoundGroup.sources[i].Pause();
			}
		}
	}

	public void UnPauseGroup(string groupName)
	{
		if (_groupedSources.ContainsKey(groupName))
		{
			GFSoundGroup gFSoundGroup = _groupedSources[groupName];
			int count = gFSoundGroup.sources.Count;
			for (int i = 0; i < count; i++)
			{
				gFSoundGroup.sources[i].UnPause();
			}
		}
	}

	public AudioSource GetAudioSourceById(string id)
	{
		if (!_trackedSources.ContainsKey(id))
		{
			return null;
		}
		return _trackedSources[id];
	}

	public void UnloadPreloadedClip(string clipPath)
	{
		List<string> list = new List<string>(_preloadedClips.Keys);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i] == clipPath)
			{
				AudioClip audioClip = _preloadedClips[list[i]];
				StopByClip(audioClip);
				if (_lastPlayTime.ContainsKey(audioClip))
				{
					_lastPlayTime.Remove(audioClip);
				}
				Resources.UnloadAsset(audioClip);
				_preloadedClips.Remove(list[i]);
				UnityEngine.Debug.LogFormat("[GFAudioManager::UnloadPreloadedClip] - {0}", "Unloaded clip at path: " + list[i]);
				break;
			}
		}
	}

	public void UnloadPreloadedClip(AudioClip clip)
	{
		List<string> list = new List<string>(_preloadedClips.Keys);
		List<AudioClip> list2 = new List<AudioClip>(_preloadedClips.Values);
		int count = list2.Count;
		for (int i = 0; i < count; i++)
		{
			if (list2[i] == clip)
			{
				StopByClip(clip);
				if (_lastPlayTime.ContainsKey(clip))
				{
					_lastPlayTime.Remove(clip);
				}
				Resources.UnloadAsset(clip);
				_preloadedClips.Remove(list[i]);
				UnityEngine.Debug.LogFormat("[GFAudioManager::UnloadPreloadedClip] - {0}", "Unloaded clip at path: " + list[i]);
			}
		}
	}

	public void UnloadAllPreloadedClips()
	{
		List<string> list = new List<string>(_preloadedClips.Keys);
		AudioClip audioClip = null;
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			audioClip = _preloadedClips[list[i]];
			StopByClip(audioClip);
			if (_lastPlayTime.ContainsKey(audioClip))
			{
				_lastPlayTime.Remove(audioClip);
			}
			Resources.UnloadAsset(audioClip);
			_preloadedClips.Remove(list[i]);
		}
	}

	public bool IsPlaying(string id)
	{
		return _trackedSources.ContainsKey(id);
	}

	public bool IsGroupPlaying(string group)
	{
		if (_groupedSources.ContainsKey(group))
		{
			return _groupedSources[group].isAnyPlaying;
		}
		return false;
	}

	private void ReturnAudioSource(AudioSource source)
	{
		if (!(source == null))
		{
			source.gameObject.SetActive(false);
			source.clip = null;
			source.transform.position = Vector3.zero;
			source.rolloffMode = AudioRolloffMode.Logarithmic;
			if (_trackedSourcesKeys.ContainsKey(source))
			{
				_trackedSources.Remove(_trackedSourcesKeys[source]);
				_trackedSourcesKeys.Remove(source);
			}
			if (_groupedSourcesKeys.ContainsKey(source))
			{
				_groupedSources[_groupedSourcesKeys[source]].sources.Remove(source);
				_groupedSourcesKeys.Remove(source);
			}
			_activeSources.Remove(source);
			_pooledSources.Add(source);
		}
	}

	private void ReturnAudioSource(List<AudioSource> sources)
	{
		int num = sources.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			ReturnAudioSource(sources[num2]);
		}
	}

	private IEnumerator Loop()
	{
		while (true)
		{
			int start = _activeSources.Count - 1;
			for (int num = start; num >= 0; num--)
			{
				if (!_activeSources[num].isPlaying && !_pausedSources.Contains(_activeSources[num]))
				{
					ReturnAudioSource(_activeSources[num]);
				}
			}
			if (_activeSources.Count == 0)
			{
				break;
			}
			yield return new WaitForSeconds(loopDelay);
		}
		loopRoutine = null;
	}

	private AudioSource GetAudioSource()
	{
		AudioSource audioSource = null;
		if (_pooledSources.Count > 0)
		{
			audioSource = _pooledSources[_pooledSources.Count - 1];
			_pooledSources.RemoveAt(_pooledSources.Count - 1);
			audioSource.gameObject.SetActive(true);
		}
		else
		{
			audioSource = new GameObject("AudioSource_" + curPoolSize).AddComponent<AudioSource>();
			audioSource.transform.parent = _sourceHolder;
			curPoolSize++;
		}
		_activeSources.Add(audioSource);
		return audioSource;
	}

	private void OnFadeComplete(AudioSource source, bool unloadClip = false)
	{
		AudioClip clip = source.clip;
		ReturnAudioSource(source);
		if (unloadClip)
		{
			UnloadPreloadedClip(clip);
		}
	}

	private void OnFadeGroupComplete(string group)
	{
		ReturnAudioSource(_groupedSources[group].sources);
	}

	private static string GetVolumeKey(string group)
	{
		return "GFAudioManager_" + group + "_Volume";
	}

	private static string GetIsMutedKey(string group)
	{
		return "GFAudioManager_" + group + "_IsMuted";
	}
}
