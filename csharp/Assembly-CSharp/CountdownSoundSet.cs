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
		return number switch
		{
			3 => threePath, 
			2 => twoPath, 
			1 => onePath, 
			_ => string.Empty, 
		};
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
