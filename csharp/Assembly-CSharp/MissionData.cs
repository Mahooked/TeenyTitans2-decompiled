using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class MissionData
{
	public int uid;

	public int currentAmount;

	public bool active;

	public bool completed;

	public bool collected;

	public bool pinned;

	public bool viewed;

	public bool isDaily;

	public bool debugCollected;

	[SerializeField]
	private string _startTimestamp;

	private DateTime _startDateTime;

	[SerializeField]
	[OptionalField]
	private List<int> _collectedUIDs = new List<int>();

	[SerializeField]
	private List<string> _collectedGUIDs;

	[SerializeField]
	[OptionalField]
	private int[] _configBattleCounts;

	[SerializeField]
	[OptionalField]
	private int[] _counts;

	public string startTimestamp
	{
		get
		{
			if (string.IsNullOrEmpty(_startTimestamp))
			{
				Initialize();
			}
			return string.Empty;
		}
	}

	private DateTime startDateTime => _startDateTime;

	public List<int> collectedUIDs
	{
		get
		{
			if (_collectedUIDs == null)
			{
				_collectedUIDs = new List<int>();
			}
			return _collectedUIDs;
		}
		set
		{
			_collectedUIDs = value;
		}
	}

	public List<string> collectedGUIDs
	{
		get
		{
			if (_collectedGUIDs == null)
			{
				_collectedGUIDs = new List<string>();
			}
			return _collectedGUIDs;
		}
	}

	public int[] configBattleCounts
	{
		get
		{
			if (_configBattleCounts == null)
			{
				_configBattleCounts = new int[0];
			}
			return _configBattleCounts;
		}
		set
		{
			_configBattleCounts = value;
		}
	}

	public int[] counts
	{
		get
		{
			if (_counts == null)
			{
				_counts = new int[0];
			}
			return _counts;
		}
		set
		{
			_counts = value;
		}
	}

	public void Initialize()
	{
		string text = DateTime.Now.ToString("yy-MM-dd");
		_startTimestamp = text;
		_collectedGUIDs = new List<string>();
	}

	public MissionData Copy()
	{
		MissionData missionData = new MissionData();
		missionData.uid = uid;
		missionData.currentAmount = currentAmount;
		missionData.active = active;
		missionData.completed = completed;
		missionData.collected = collected;
		missionData.pinned = pinned;
		missionData.viewed = viewed;
		missionData.isDaily = isDaily;
		missionData.debugCollected = debugCollected;
		return missionData;
	}

	[OnDeserializing]
	private void HandleSerializationVersioning(StreamingContext context)
	{
		_collectedUIDs = new List<int>();
		_configBattleCounts = new int[0];
		_counts = new int[0];
	}
}
