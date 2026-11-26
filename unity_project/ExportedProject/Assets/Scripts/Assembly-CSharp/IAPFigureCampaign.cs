using System;
using UnityEngine;

[Serializable]
public class IAPFigureCampaign
{
	public enum CampaignType
	{
		Fixed = 0,
		Daily = 1,
		Weekly = 2,
		Monthly = 3,
		Yearly = 4,
		Custom = 5
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

	public CampaignType campaignType
	{
		get
		{
			return _campaignType;
		}
	}

	public int numDaysPerCycle
	{
		get
		{
			return _numDaysPerCycle;
		}
	}

	public FigureSet[] figureSets
	{
		get
		{
			return _figureSets;
		}
	}

	public int GetNumDaysPerCycle()
	{
		switch (_campaignType)
		{
		case CampaignType.Daily:
			return 1;
		case CampaignType.Weekly:
			return 7;
		case CampaignType.Monthly:
			return 30;
		case CampaignType.Yearly:
			return 365;
		case CampaignType.Custom:
			return _numDaysPerCycle;
		default:
			return 0;
		}
	}
}
