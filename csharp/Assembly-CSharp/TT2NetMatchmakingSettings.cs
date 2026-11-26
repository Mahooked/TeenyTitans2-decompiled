using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/Matchmaking Settings")]
public class TT2NetMatchmakingSettings : ScriptableObject
{
	public enum FilterType
	{
		HighestLevelFig,
		SkillRating
	}

	[Serializable]
	public class SearchStep
	{
		public FilterType filterType;

		public int intValue1;

		public RangedInt rangedIntValue1;
	}

	[Serializable]
	public class SearchGroup
	{
		public RangedInt playerRange;

		public SearchStep[] searchSteps;
	}

	[SerializeField]
	private float _searchStepDelay = 2f;

	[SerializeField]
	private float _timeout = 10f;

	[SerializeField]
	private int _minFigureLevel = 5;

	[SerializeField]
	private int _maxLevelDisparity = 8;

	[SerializeField]
	private SearchGroup[] _searchGroups;

	public float searchStepDelay => _searchStepDelay;

	public float timeout => _timeout;

	public int minFigureLevel => _minFigureLevel;

	public int maxLevelDisparity => _maxLevelDisparity;

	public SearchGroup[] searchGroups => _searchGroups;

	public SearchGroup GetSearchGroup(int playerCount)
	{
		SearchGroup searchGroup = null;
		for (int i = 0; i < searchGroups.Length; i++)
		{
			searchGroup = searchGroups[i];
			if (i == searchGroups.Length - 1 || searchGroup.playerRange.IsInRange(playerCount))
			{
				break;
			}
		}
		return searchGroup;
	}
}
