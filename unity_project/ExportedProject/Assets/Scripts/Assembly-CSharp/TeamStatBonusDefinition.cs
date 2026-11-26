using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TeamStatBonusDefinition
{
	public List<int> characterUIDS = new List<int>();

	public CharacterStatType statType;

	public int statBonus1 = 2;

	public int statBonus2 = 5;

	public Color fontFaceColor = new Color(1f, 1f, 1f, 1f);

	public Color fontOutlineColor = new Color(0f, 0f, 0f, 1f);

	public int priority;

	public bool foldout = true;

	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _teamName = string.Empty;

	[SerializeField]
	private string _teamNameKey = string.Empty;

	[SerializeField]
	private string _teamBackingSpriteName = string.Empty;

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public string teamName
	{
		get
		{
			return _teamName;
		}
	}

	public string teamNameKey
	{
		get
		{
			return _teamNameKey;
		}
	}

	public string teamBackingSpriteName
	{
		get
		{
			return _teamBackingSpriteName;
		}
	}
}
