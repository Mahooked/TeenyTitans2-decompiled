using System;
using UnityEngine;

[Serializable]
public class IAPFigureCampaign
{
	public enum CampaignType
	{
		Fixed,
		Daily,
		Weekly,
		Monthly,
		Yearly,
		Custom
	}

	[Serializable]
	public class Figure
	{
		public int characterUID;

		public string productID;
	}

	[Serializable]
	public class FigureSet
	{
		public Figure[] figures;
	}

	[SerializeField]
	private CampaignType _campaignType;

	[SerializeField]
	private int _numDaysPerCycle;

	[SerializeField]
	private FigureSet[] _figureSets;

	public CampaignType campaignType => _campaignType;

	public int numDaysPerCycle => _numDaysPerCycle;

	public FigureSet[] figureSets => _figureSets;

	public int GetNumDaysPerCycle()
	{
		return _campaignType switch
		{
			CampaignType.Daily => 1, 
			CampaignType.Weekly => 7, 
			CampaignType.Monthly => 30, 
			CampaignType.Yearly => 365, 
			CampaignType.Custom => _numDaysPerCycle, 
			_ => 0, 
		};
	}
}
