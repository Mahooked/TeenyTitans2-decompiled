using System.Collections.Generic;
using UnityEngine;

public class CityLocationMusicDatabase : ScriptableObject
{
	public List<CityLocationMusic> cityMusic;

	public string defaultMusicClipName;

	public string resourcePath;

	public string GetCityLocationMusicName(CityLocation location, bool useDayMusic = true)
	{
		CityLocationMusic cityLocationMusic = cityMusic.Find((CityLocationMusic x) => x.location == location);
		if (cityLocationMusic != null)
		{
			return (!useDayMusic) ? cityLocationMusic.nightMusicClipName : cityLocationMusic.dayMusicClipName;
		}
		return defaultMusicClipName;
	}

	public AudioClip GetCityLocationMusic(CityLocation location, bool useDayMusic = true)
	{
		CityLocationMusic cityLocationMusic = cityMusic.Find((CityLocationMusic x) => x.location == location);
		if (cityLocationMusic != null)
		{
			return Resources.Load<AudioClip>(resourcePath + ((!useDayMusic) ? cityLocationMusic.nightMusicClipName : cityLocationMusic.dayMusicClipName));
		}
		return Resources.Load<AudioClip>(resourcePath + defaultMusicClipName);
	}
}
