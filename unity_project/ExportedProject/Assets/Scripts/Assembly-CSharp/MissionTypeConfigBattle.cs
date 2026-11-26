using System;
using UnityEngine;

[Serializable]
public class MissionTypeConfigBattle
{
	public enum TargetType
	{
		DefeatNPC = 0,
		DefeatNPCTag = 1,
		UseAccessory = 2,
		UseTofu = 3,
		CollectBatteries = 4,
		BeVictorious = 5,
		WinPVPBattle = 6
	}

	[SerializeField]
	private TargetType _targetType;

	[SerializeField]
	private int _count;

	[SerializeField]
	private string _npcGuid;

	[SerializeField]
	private string _npcTag;

	public TargetType targetType
	{
		get
		{
			return _targetType;
		}
		set
		{
			_targetType = value;
		}
	}

	public int count
	{
		get
		{
			return _count;
		}
		set
		{
			_count = value;
		}
	}

	public string npcGuid
	{
		get
		{
			return _npcGuid;
		}
		set
		{
			_npcGuid = value;
		}
	}

	public string npcTag
	{
		get
		{
			return _npcTag;
		}
		set
		{
			_npcTag = value;
		}
	}

	public bool IsNPCValidTarget(NPCDefinition npcDef)
	{
		if (_targetType != TargetType.DefeatNPC && _targetType != TargetType.DefeatNPCTag)
		{
			return false;
		}
		if (_targetType == TargetType.DefeatNPC)
		{
			if (!npcDef.guid.Equals(_npcGuid))
			{
				return false;
			}
		}
		else if (!npcDef.tags.Contains(_npcTag))
		{
			return false;
		}
		return true;
	}

	public MissionTypeConfigBattle Copy()
	{
		MissionTypeConfigBattle missionTypeConfigBattle = new MissionTypeConfigBattle();
		missionTypeConfigBattle._targetType = _targetType;
		missionTypeConfigBattle._count = _count;
		missionTypeConfigBattle._npcGuid = _npcGuid;
		missionTypeConfigBattle._npcTag = _npcTag;
		return missionTypeConfigBattle;
	}
}
