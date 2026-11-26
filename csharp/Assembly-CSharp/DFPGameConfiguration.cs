using System;
using System.Collections.Generic;

[Serializable]
public class DFPGameConfiguration
{
	public string GameName;

	public string PropertyName;

	public string SchemeName;

	public string AdUnitId;

	public string Promo;

	public string PackageName;

	public List<int> TimesPlayedThresholds;

	public List<int> TimeSpentThresholds;

	public List<int> MoneySpentThresholds;

	public DFPGameConfiguration()
	{
	}

	public DFPGameConfiguration(DFPGameConfiguration config)
	{
		GameName = config.GameName;
		PropertyName = config.PropertyName;
		SchemeName = config.SchemeName;
		AdUnitId = config.AdUnitId;
		Promo = config.Promo;
		PackageName = config.PackageName;
		TimesPlayedThresholds = config.TimeSpentThresholds;
		TimeSpentThresholds = config.TimeSpentThresholds;
		MoneySpentThresholds = config.MoneySpentThresholds;
	}
}
