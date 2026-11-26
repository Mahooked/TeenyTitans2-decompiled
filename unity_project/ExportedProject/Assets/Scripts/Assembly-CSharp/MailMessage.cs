using System.Collections.Generic;
using UnityEngine;

public class MailMessage : ScriptableObject
{
	public enum TriggerType
	{
		Mission = 0,
		FigurePurchaseCount = 1,
		FigureCollectionSize = 2,
		TutorialComplete = 3
	}

	[SerializeField]
	private int _uid = -1;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _title = string.Empty;

	[SerializeField]
	private string _titleKey = string.Empty;

	[SerializeField]
	private string _message = string.Empty;

	[SerializeField]
	private string _messageKey = string.Empty;

	[SerializeField]
	private string _signature = string.Empty;

	[SerializeField]
	private string _signatureKey = string.Empty;

	[SerializeField]
	private TriggerType _triggerType;

	[SerializeField]
	private int _missionUID = -1;

	[SerializeField]
	private int _missionUIDRequired = -1;

	[SerializeField]
	private List<int> _additionalMissionUIDsRequired = new List<int>();

	[SerializeField]
	private int _daysAfter;

	[SerializeField]
	private int _timeOfDay;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private Comparison _comparison1;

	[SerializeField]
	private string _tutorialName;

	public int uid
	{
		get
		{
			return _uid;
		}
	}

	public new string name
	{
		get
		{
			return _name;
		}
	}

	public string title
	{
		get
		{
			return _title;
		}
	}

	public string titleKey
	{
		get
		{
			return _titleKey;
		}
	}

	public string message
	{
		get
		{
			return _message;
		}
	}

	public string messageKey
	{
		get
		{
			return _messageKey;
		}
	}

	public string signature
	{
		get
		{
			return _signature;
		}
	}

	public string signatureKey
	{
		get
		{
			return _signatureKey;
		}
	}

	public TriggerType triggerType
	{
		get
		{
			return _triggerType;
		}
	}

	public int missionUID
	{
		get
		{
			return _missionUID;
		}
	}

	public int missionUIDRequired
	{
		get
		{
			return _missionUIDRequired;
		}
	}

	public List<int> additionalMissionUIDsRequired
	{
		get
		{
			return _additionalMissionUIDsRequired;
		}
	}

	public int daysAfter
	{
		get
		{
			return _daysAfter;
		}
	}

	public int timeOfDay
	{
		get
		{
			return _timeOfDay;
		}
	}

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
	}

	public Comparison comparison1
	{
		get
		{
			return _comparison1;
		}
	}

	public string tutorialName
	{
		get
		{
			return _tutorialName;
		}
	}
}
