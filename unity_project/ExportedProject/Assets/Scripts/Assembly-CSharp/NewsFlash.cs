using UnityEngine;

public class NewsFlash : ScriptableObject
{
	[SerializeField]
	private int _uid = -1;

	[SerializeField]
	private string _title = string.Empty;

	[SerializeField]
	private string _titleKey = string.Empty;

	[SerializeField]
	private string _message = string.Empty;

	[SerializeField]
	private string _messageKey = string.Empty;

	[SerializeField]
	private bool _hideOnCompleted;

	[SerializeField]
	private int _missionUIDRequired = -1;

	[SerializeField]
	private int _newsFlashUIDRequired = -1;

	[SerializeField]
	private int _daysAfter;

	[SerializeField]
	private int _timeOfDay;

	[SerializeField]
	private int _durationDays;

	[SerializeField]
	private int _durationTime;

	[SerializeField]
	private bool _showTimer;

	[SerializeField]
	private bool _activateWithMission;

	[SerializeField]
	private bool _activateOnMissionComplete;

	[SerializeField]
	private NewsFlashType _newsFlashType;

	[SerializeField]
	private int _shopDiscountAmount;

	[SerializeField]
	private ShopFigure _shopFigure;

	[SerializeField]
	private CityName _cityName;

	[SerializeField]
	private int _targetShopUID;

	[SerializeField]
	private bool _repeatable;

	[SerializeField]
	private int _dayRepeatInterval;

	public int uid
	{
		get
		{
			return _uid;
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

	public bool hideOnCompleted
	{
		get
		{
			return _hideOnCompleted;
		}
	}

	public int missionUIDRequired
	{
		get
		{
			return _missionUIDRequired;
		}
	}

	public int newsFlashUIDRequired
	{
		get
		{
			return _newsFlashUIDRequired;
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

	public int durationDays
	{
		get
		{
			return _durationDays;
		}
	}

	public int durationTime
	{
		get
		{
			return _durationTime;
		}
	}

	public bool showTimer
	{
		get
		{
			return _showTimer;
		}
	}

	public bool activateWithMission
	{
		get
		{
			return _activateWithMission;
		}
	}

	public bool activateOnMissionComplete
	{
		get
		{
			return _activateOnMissionComplete;
		}
	}

	public NewsFlashType newsFlashType
	{
		get
		{
			return _newsFlashType;
		}
	}

	public int shopDiscountAmount
	{
		get
		{
			return _shopDiscountAmount;
		}
	}

	public ShopFigure shopFigure
	{
		get
		{
			return _shopFigure;
		}
	}

	public CityName cityName
	{
		get
		{
			return _cityName;
		}
	}

	public int targetShopUID
	{
		get
		{
			return _targetShopUID;
		}
	}

	public bool repeatable
	{
		get
		{
			return _repeatable;
		}
	}

	public int dayRepeatInterval
	{
		get
		{
			return _dayRepeatInterval;
		}
	}
}
