using System;
using System.Collections.Generic;

[Serializable]
public class DFPConfiguration
{
	public static readonly int UNLIMITED_APPS = -1;

	public int Version;

	public List<DFPGameConfiguration> GameConfigs;

	public int MaxInstalledApps = UNLIMITED_APPS;

	public DFPGameConfiguration GetGameConfigByName(string name)
	{
		for (int i = 0; i < GameConfigs.Count; i++)
		{
			if (string.Equals(name, GameConfigs[i].GameName))
			{
				return GameConfigs[i];
			}
		}
		return null;
	}

	public List<string> GetGameSchemes()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < GameConfigs.Count; i++)
		{
			list.Add(GameConfigs[i].SchemeName);
		}
		return list;
	}

	public List<string> GetPackages()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < GameConfigs.Count; i++)
		{
			list.Add(GameConfigs[i].PackageName);
		}
		return list;
	}

	public List<string> GetSchemeNamesFromAndroidPackages(List<string> packageList)
	{
		List<string> list = new List<string>();
		foreach (string package in packageList)
		{
			for (int i = 0; i < GameConfigs.Count; i++)
			{
				if (package == GameConfigs[i].PackageName)
				{
					list.Add(GameConfigs[i].SchemeName);
				}
			}
		}
		return list;
	}
}
