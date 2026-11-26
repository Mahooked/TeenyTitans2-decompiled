using UnityEngine;

[RequireComponent(typeof(tk2dUIItem))]
public class ToggleSound : MonoBehaviour
{
	public enum SoundType
	{
		Music,
		SoundEffect,
		VoiceOver
	}

	public SoundType soundTypeToMute;

	public tk2dBaseSprite iconSprite;

	private tk2dUIItem uiItem;

	private void Start()
	{
		uiItem = GetComponentInChildren<tk2dUIItem>();
		uiItem.OnClick += ToggleMute;
	}

	private void OnEnable()
	{
		bool flag = PlayerPrefs.GetInt(GetPropertyName()) == 1;
		UpdateIconSprite();
		tk2dBaseSprite[] componentsInChildren = GetComponentsInChildren<tk2dBaseSprite>();
		foreach (tk2dBaseSprite tk2dBaseSprite2 in componentsInChildren)
		{
			tk2dBaseSprite2.color = ((!flag) ? Color.white : new Color(0.5f, 0.5f, 0.5f));
		}
	}

	private void OnDestroy()
	{
		if (uiItem != null)
		{
			uiItem.OnClick -= ToggleMute;
		}
	}

	private void ToggleMute()
	{
		if (!(GFAudioManager.instance == null))
		{
			string propertyName = GetPropertyName();
			int num = PlayerPrefs.GetInt(propertyName);
			bool flag = num == 0;
			PlayerPrefs.SetInt(propertyName, flag ? 1 : 0);
			tk2dBaseSprite[] componentsInChildren = GetComponentsInChildren<tk2dBaseSprite>();
			foreach (tk2dBaseSprite tk2dBaseSprite2 in componentsInChildren)
			{
				tk2dBaseSprite2.color = ((!flag) ? Color.white : new Color(0.5f, 0.5f, 0.5f));
			}
			GFAudioManager.instance.MuteGroup(GetSoundGroupName(), flag);
		}
	}

	private string GetPropertyName()
	{
		if (soundTypeToMute == SoundType.Music)
		{
			return "Game_Music_Off";
		}
		if (soundTypeToMute == SoundType.SoundEffect)
		{
			return "Game_Sfx_Off";
		}
		if (soundTypeToMute == SoundType.VoiceOver)
		{
			return "Game_DialogueAudio_Off";
		}
		return string.Empty;
	}

	private string GetSoundGroupName()
	{
		if (soundTypeToMute == SoundType.Music)
		{
			return GFAudioManager.MUSIC_GROUP;
		}
		if (soundTypeToMute == SoundType.SoundEffect)
		{
			return GFAudioManager.SFX_GROUP;
		}
		if (soundTypeToMute == SoundType.VoiceOver)
		{
			return GFAudioManager.VOICEOVER_GROUP;
		}
		return string.Empty;
	}

	private void UpdateIconSprite()
	{
		if (soundTypeToMute == SoundType.Music)
		{
			iconSprite.SetSprite("mainmenu_muteMusic");
		}
		else if (soundTypeToMute == SoundType.SoundEffect)
		{
			iconSprite.SetSprite("mainmenu_muteSound");
		}
		else if (soundTypeToMute == SoundType.VoiceOver)
		{
			iconSprite.SetSprite("mainmenu_muteVO");
		}
	}
}
