using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MutableAudioSource : MonoBehaviour
{
	public enum SoundType
	{
		Music = 0,
		SoundEffect = 1,
		VoiceOver = 2
	}

	public SoundType soundType;

	private AudioSource sound;

	private void Start()
	{
		sound = GetComponent<AudioSource>();
		UpdateMuteStatus();
		if (GFAudioManager.instance != null)
		{
			GFAudioManager.instance.SoundGroupMuteChanged += UpdateMuteStatus;
		}
	}

	private void OnDestroy()
	{
		if (GFAudioManager.instance != null)
		{
			GFAudioManager.instance.SoundGroupMuteChanged -= UpdateMuteStatus;
		}
	}

	private void UpdateMuteStatus()
	{
		if (soundType == SoundType.Music)
		{
			sound.mute = PlayerPrefs.GetInt("Game_Music_Off") == 1;
		}
		else if (soundType == SoundType.SoundEffect)
		{
			sound.mute = PlayerPrefs.GetInt("Game_Sfx_Off") == 1;
		}
		else if (soundType == SoundType.VoiceOver)
		{
			sound.mute = PlayerPrefs.GetInt("Game_DialogueAudio_Off") == 1;
		}
	}
}
