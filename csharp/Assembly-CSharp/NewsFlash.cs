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

	public int uid => _uid;

	public string title => _title;

	public string titleKey => _titleKey;

	public string message => _message;

	public string messageKey => _messageKey;

	public bool hideOnCompleted => _hideOnCompleted;

	public int missionUIDRequired => _missionUIDRequired;

	public int newsFlashUIDRequired => _newsFlashUIDRequired;

	public int daysAfter => _daysAfter;

	public int timeOfDay => _timeOfDay;

	public int durationDays => _durationDays;

	public int durationTime => _durationTime;

	public bool showTimer => _showTimer;

	public bool activateWithMission => _activateWithMission;

	public bool activateOnMissionComplete => _activateOnMissionComplete;

	public NewsFlashType newsFlashType => _newsFlashType;

	public int shopDiscountAmount => _shopDiscountAmount;

	public ShopFigure shopFigure => _shopFigure;

	public CityName cityName => _cityName;

	public int targetShopUID => _targetShopUID;

	public bool repeatable => _repeatable;

	public int dayRepeatInterval => _dayRepeatInterval;
}
