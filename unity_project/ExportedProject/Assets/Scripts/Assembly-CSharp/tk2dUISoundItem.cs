using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUISoundItem")]
public class tk2dUISoundItem : tk2dUIBaseItemControl
{
	public AudioClip downButtonSound;

	public AudioClip upButtonSound;

	public AudioClip clickButtonSound;

	public AudioClip releaseButtonSound;

	private void OnEnable()
	{
		if ((bool)uiItem)
		{
			if (downButtonSound != null)
			{
				uiItem.OnDown += PlayDownSound;
			}
			if (upButtonSound != null)
			{
				uiItem.OnUp += PlayUpSound;
			}
			if (clickButtonSound != null)
			{
				uiItem.OnClick += PlayClickSound;
			}
			if (releaseButtonSound != null)
			{
				uiItem.OnRelease += PlayReleaseSound;
			}
		}
	}

	private void OnDisable()
	{
		if ((bool)uiItem)
		{
			if (downButtonSound != null)
			{
				uiItem.OnDown -= PlayDownSound;
			}
			if (upButtonSound != null)
			{
				uiItem.OnUp -= PlayUpSound;
			}
			if (clickButtonSound != null)
			{
				uiItem.OnClick -= PlayClickSound;
			}
			if (releaseButtonSound != null)
			{
				uiItem.OnRelease -= PlayReleaseSound;
			}
		}
	}

	private void PlayDownSound()
	{
		PlaySound(downButtonSound);
	}

	private void PlayUpSound()
	{
		PlaySound(upButtonSound);
	}

	private void PlayClickSound()
	{
		PlaySound(clickButtonSound);
	}

	private void PlayReleaseSound()
	{
		PlaySound(releaseButtonSound);
	}

	private void PlaySound(AudioClip source)
	{
		if (Type.GetType("GFAudioManager") != null && GFAudioManager.instance != null)
		{
			GFAudioManager.instance.PlayClip(source, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		}
		else
		{
			tk2dUIAudioManager.Instance.Play(source);
		}
	}
}
