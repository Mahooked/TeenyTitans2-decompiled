using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/Daily Mission Config")]
public class DailyMissionConfig : ScriptableObject
{
	[SerializeField]
	private bool _totallyRandom;

	[SerializeField]
	private int _debugDaysOffset;

	[SerializeField]
	private List<string> _jumpCityNPCs = new List<string>();

	[SerializeField]
	private List<string> _gothamNPCs = new List<string>();

	[SerializeField]
	private List<string> _metropolisNPCs = new List<string>();

	[SerializeField]
	private List<string> _validFigTags = new List<string>();

	[SerializeField]
	private List<string> _validNPCTags = new List<string>();

	[SerializeField]
	private List<string> _validLocationTags = new List<string>();

	[SerializeField]
	private List<int> _excludeCharacterUids;

	[SerializeField]
	private List<int> _excludeCharacterUidsShopping;

	public List<DailyMissionSetup> dailyMissionTypeSetup = new List<DailyMissionSetup>();

	public bool totallyRandom => _totallyRandom;

	public int debugDaysOffset => _debugDaysOffset;

	public List<string> jumpCityNPCs => _jumpCityNPCs;

	public List<string> gothamNPCs => _gothamNPCs;

	public List<string> metropolisNPCs => _metropolisNPCs;

	public List<string> validFigTags => _validFigTags;

	public List<string> validNPCTags => _validNPCTags;

	public List<string> validLocationTags => _validLocationTags;

	public List<int> excludeCharacterUids => _excludeCharacterUids;

	public List<int> excludeCharacterShopUidsShopping => _excludeCharacterUidsShopping;

	public DailyMissionSetup GetDailySetupForDifficulty(DailyMissionType dailyMissionType, int difficulty)
	{
		if (dailyMissionType == DailyMissionType.Shopping)
		{
			difficulty = Mathf.Min(difficulty, 2);
		}
		Debug.Log(string.Concat("Get Daily Setup for Difficulty: ", dailyMissionType, ", diff: ", difficulty));
		for (int i = 0; i < dailyMissionTypeSetup.Count; i++)
		{
			DailyMissionSetup dailyMissionSetup = dailyMissionTypeSetup[i];
			if (dailyMissionSetup.type == dailyMissionType && dailyMissionSetup.difficultyLevel == difficulty)
			{
				return dailyMissionSetup;
			}
		}
		return null;
	}

	public DailyMissionSetup GetRandomSetupByType(DailyMissionType type)
	{
		List<DailyMissionSetup> list = new List<DailyMissionSetup>();
		for (int i = 0; i < dailyMissionTypeSetup.Count; i++)
		{
			DailyMissionSetup dailyMissionSetup = dailyMissionTypeSetup[i];
			if (dailyMissionSetup.type == type)
			{
				list.Add(dailyMissionSetup);
			}
		}
		if (list.Count > 0)
		{
			return list[Random.Range(0, list.Count)];
		}
		return null;
	}

	public List<string> GetValidNPCsForCity(CityName cityName)
	{
		return cityName switch
		{
			CityName.JumpCity => jumpCityNPCs, 
			CityName.Gotham => gothamNPCs, 
			CityName.Metropolis => metropolisNPCs, 
			_ => new List<string>(), 
		};
	}

	public List<string> GetAllValidNPCs()
	{
		List<string> list = new List<string>();
		list.AddRange(jumpCityNPCs);
		list.AddRange(gothamNPCs);
		list.AddRange(metropolisNPCs);
		return list;
	}
}
