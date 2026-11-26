using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CountdownSoundSet
{
	public int characterUID = -1;

	public string threePath = string.Empty;

	public string twoPath = string.Empty;

	public string onePath = string.Empty;

	public List<string> goPaths;

	public CountdownSoundSet()
	{
		goPaths = new List<string>();
	}

	public string GetNumberPath(int number)
	{
		switch (number)
		{
		case 3:
			return threePath;
		case 2:
			return twoPath;
		case 1:
			return onePath;
		default:
			return string.Empty;
		}
	}

	public string GetRandomGoPath()
	{
		if (goPaths.Count > 0)
		{
			return goPaths[UnityEngine.Random.Range(0, goPaths.Count)];
		}
		return string.Empty;
	}
}
