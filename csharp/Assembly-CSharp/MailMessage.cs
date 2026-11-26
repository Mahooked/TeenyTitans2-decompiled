using System.Collections.Generic;
using UnityEngine;

public class MailMessage : ScriptableObject
{
	public enum TriggerType
	{
		Mission,
		FigurePurchaseCount,
		FigureCollectionSize,
		TutorialComplete
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

	public int uid => _uid;

	public new string name => _name;

	public string title => _title;

	public string titleKey => _titleKey;

	public string message => _message;

	public string messageKey => _messageKey;

	public string signature => _signature;

	public string signatureKey => _signatureKey;

	public TriggerType triggerType => _triggerType;

	public int missionUID => _missionUID;

	public int missionUIDRequired => _missionUIDRequired;

	public List<int> additionalMissionUIDsRequired => _additionalMissionUIDsRequired;

	public int daysAfter => _daysAfter;

	public int timeOfDay => _timeOfDay;

	public int intValue1 => _intValue1;

	public Comparison comparison1 => _comparison1;

	public string tutorialName => _tutorialName;
}
