using UnityEngine;

public class GFSoundItem : MonoBehaviour
{
	public string soundPath = string.Empty;

	public string soundID = string.Empty;

	public string soundGroup = string.Empty;

	public float volume = 1f;

	public float delay;

	public bool loop;

	private void OnEnable()
	{
		if (!string.IsNullOrEmpty(soundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(soundPath, (!string.IsNullOrEmpty(soundGroup)) ? soundGroup : GFAudioManager.SFX_GROUP, soundID, volume, delay, loop);
		}
	}
}
