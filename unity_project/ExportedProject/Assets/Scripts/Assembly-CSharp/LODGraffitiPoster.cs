using System;
using UnityEngine;

public class LODGraffitiPoster : MonoBehaviour
{
	[Serializable]
	public class PosterGroup
	{
		public GameObject cleanPoster;

		public GameObject graffitiPoster;
	}

	public AudioClip spraySFX;

	public PosterGroup[] posters;

	private PosterGroup activePosterGroup;

	private void Start()
	{
		int num = posters.Length;
		for (int i = 0; i < num; i++)
		{
			PosterGroup posterGroup = posters[i];
			if (posterGroup.cleanPoster.activeInHierarchy)
			{
				activePosterGroup = posterGroup;
				break;
			}
		}
	}

	public void EnableGraffiti(bool refresh)
	{
		Debug.Log("Enable Graffiti...");
		activePosterGroup.cleanPoster.SetActive(false);
		activePosterGroup.graffitiPoster.SetActive(true);
		if (!refresh)
		{
			GFAudioManager.instance.PlayClip(spraySFX, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		}
	}
}
