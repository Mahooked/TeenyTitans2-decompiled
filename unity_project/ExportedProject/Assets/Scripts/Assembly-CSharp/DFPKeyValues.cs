using System;
using System.Collections.Generic;
using UnityEngine;

public class DFPKeyValues
{
	public List<int> timesPlayedThresholds;

	public List<int> timeSpentThresholds;

	public List<int> moneySpentThresholds;

	private string timespent = "timespent";

	private string timesplayed = "timesplayed";

	private string moneyspent = "moneyspent";

	private int timespentOld;

	private DateTime startTime;

	private int timesplayedOld;

	private int moneyspentOld;

	private int moneyspentCurrent;

	private static DFPKeyValues instance;

	public static DFPKeyValues Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new DFPKeyValues();
			}
			return instance;
		}
	}

	public void LoadValues(DFPGameConfiguration gameConfig)
	{
		if (gameConfig == null)
		{
			Debug.LogWarning("DFPGameConfiguration is null!");
			return;
		}
		timesPlayedThresholds = gameConfig.TimesPlayedThresholds;
		timeSpentThresholds = gameConfig.TimeSpentThresholds;
		moneySpentThresholds = gameConfig.MoneySpentThresholds;
		startTime = DateTime.Now;
		loadTimeSpent();
		loadTimesPlayed();
		loadMoneySpent();
		moneyspentCurrent = 0;
	}

	private void OnApplicationQuit()
	{
		saveTimeSpent();
		saveTimesPlayed();
		saveMoneySpent();
	}

	public string getTimeSpent()
	{
		int num = 0;
		foreach (int timeSpentThreshold in timeSpentThresholds)
		{
			if (timespentOld <= timeSpentThreshold && timeSpentThresholds[0] == timeSpentThreshold)
			{
				return num.ToString();
			}
			if (num == timeSpentThresholds.Count - 1)
			{
				return num.ToString();
			}
			if (timespentOld >= timeSpentThresholds[num] && timespentOld < timeSpentThresholds[num + 1])
			{
				return num.ToString();
			}
			num++;
		}
		return timespentOld.ToString();
	}

	public string getTimesPlayed()
	{
		int num = 0;
		foreach (int timesPlayedThreshold in timesPlayedThresholds)
		{
			if (timesplayedOld <= timesPlayedThreshold && timesPlayedThresholds[0] == timesPlayedThreshold)
			{
				return num.ToString();
			}
			if (num == timesPlayedThresholds.Count - 1)
			{
				return num.ToString();
			}
			if (timesplayedOld >= timesPlayedThresholds[num] && timesplayedOld < timesPlayedThresholds[num + 1])
			{
				return num.ToString();
			}
			num++;
		}
		return timesplayedOld.ToString();
	}

	public string getMoneySpent()
	{
		int num = 0;
		foreach (int moneySpentThreshold in moneySpentThresholds)
		{
			if (moneyspentOld <= moneySpentThreshold && moneySpentThresholds[0] == moneySpentThreshold)
			{
				return num.ToString();
			}
			if (num == moneySpentThresholds.Count - 1)
			{
				return num.ToString();
			}
			if (moneyspentOld >= moneySpentThresholds[num] && moneyspentOld < moneySpentThresholds[num + 1])
			{
				return num.ToString();
			}
			num++;
		}
		return moneyspentOld.ToString();
	}

	public void loadTimeSpent()
	{
		if (PlayerPrefs.HasKey(timespent))
		{
			timespentOld = PlayerPrefs.GetInt(timespent);
			Debug.Log("loaded " + timespentOld + " minutes timespent");
		}
		else
		{
			timespentOld = 0;
		}
	}

	public void saveTimeSpent()
	{
		int num = (int)Math.Round(DateTime.Now.Subtract(startTime).TotalMinutes);
		Debug.Log("saved " + (timespentOld + num) + " minutes timespent");
		PlayerPrefs.SetInt(timespent, timespentOld + num);
	}

	public void loadTimesPlayed()
	{
		if (PlayerPrefs.HasKey(timesplayed))
		{
			timesplayedOld = PlayerPrefs.GetInt(timesplayed);
			Debug.Log("loaded " + timesplayedOld + " timesplayed");
		}
		else
		{
			timesplayedOld = 0;
		}
	}

	public void saveTimesPlayed()
	{
		Debug.Log("saved " + (timesplayedOld + 1) + " timesplayed");
		PlayerPrefs.SetInt(timesplayed, timesplayedOld + 1);
	}

	public void loadMoneySpent()
	{
		if (PlayerPrefs.HasKey(moneyspent))
		{
			moneyspentOld = PlayerPrefs.GetInt(moneyspent);
			Debug.Log("loaded " + moneyspentOld + " money spent");
		}
		else
		{
			moneyspentOld = 0;
		}
	}

	public void saveMoneySpent()
	{
		Debug.Log("saved " + (moneyspentOld + moneyspentCurrent) + " money spent");
		PlayerPrefs.SetInt(moneyspent, moneyspentOld + moneyspentCurrent);
	}
}
