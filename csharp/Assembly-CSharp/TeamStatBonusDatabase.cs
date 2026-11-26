using System.Collections.Generic;
using UnityEngine;

public class TeamStatBonusDatabase : ScriptableObject
{
	public class BonusResult
	{
		public CharacterStatType statType;

		public int bonusAmount;

		public List<Character> characters;

		public bool IsBonus => characters.Count > 1;

		public BonusResult()
		{
			characters = new List<Character>();
		}
	}

	[SerializeField]
	private List<TeamStatBonusDefinition> _items = new List<TeamStatBonusDefinition>();

	[SerializeField]
	private int _nextId = -1;

	public List<TeamStatBonusDefinition> items => _items;

	private int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	private void OnEnable()
	{
		if (_items == null)
		{
			_items = new List<TeamStatBonusDefinition>();
		}
	}

	public TeamStatBonusDefinition GetItem(int uid)
	{
		return _items.Find((TeamStatBonusDefinition obj) => obj.uid == uid);
	}

	public List<TeamStatBonusDefinition> GetItems(List<int> uids)
	{
		List<TeamStatBonusDefinition> list = new List<TeamStatBonusDefinition>();
		int count = uids.Count;
		for (int i = 0; i < count; i++)
		{
			list.Add(_items.Find((TeamStatBonusDefinition obj) => obj.uid == uids[i]));
		}
		return list;
	}

	public BonusResult GetBonusResult(List<Character> team)
	{
		BonusResult bonusResult = new BonusResult();
		if (team != null && team.Count > 1)
		{
			int num = -1;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			for (int i = 0; i < items.Count; i++)
			{
				List<int> list = new List<int>();
				TeamStatBonusDefinition teamStatBonusDefinition = items[i];
				for (int j = 0; j < team.Count; j++)
				{
					Character character = team[j];
					int uid = character.characterDefinition.uid;
					if (teamStatBonusDefinition.characterUIDS.Contains(uid) && !list.Contains(uid))
					{
						list.Add(uid);
					}
				}
				if (list.Count > 1 && num != teamStatBonusDefinition.uid && (list.Count > num2 || (teamStatBonusDefinition.characterUIDS.Count == 2 && list.Count == 2 && !flag) || (num2 == 2 && list.Count == 2 && teamStatBonusDefinition.priority > num3) || (num2 == 3 && list.Count == 3 && teamStatBonusDefinition.priority > num3)))
				{
					if (list.Count == 3)
					{
						flag = true;
					}
					num2 = list.Count;
					num = teamStatBonusDefinition.uid;
					num3 = teamStatBonusDefinition.priority;
				}
			}
			if (num > -1)
			{
				TeamStatBonusDefinition item = GetItem(num);
				for (int k = 0; k < item.characterUIDS.Count; k++)
				{
					int num4 = item.characterUIDS[k];
					for (int l = 0; l < team.Count; l++)
					{
						if (team[l].characterDefinition.uid == num4)
						{
							bonusResult.characters.Add(team[l]);
						}
					}
				}
				bonusResult.statType = item.statType;
				if (num2 == 2)
				{
					bonusResult.bonusAmount = item.statBonus1;
				}
				else if (num2 >= 3)
				{
					bonusResult.bonusAmount = item.statBonus2;
				}
			}
		}
		return bonusResult;
	}
}
