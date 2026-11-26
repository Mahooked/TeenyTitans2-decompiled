using System.Collections.Generic;
using UnityEngine;

public class FlashSaleSettings : ScriptableObject
{
	[SerializeField]
	private float _duration;

	[SerializeField]
	private int _entryFee;

	[SerializeField]
	[Tooltip("The maximum amount of tickets that are going to be placed on the map.")]
	private int _numTicketsOnMap;

	[SerializeField]
	[Tooltip("The maximum amount of boosts that are going to be placed on the map.")]
	private int _numBoostsOnMap;

	[SerializeField]
	[Tooltip("The player's move speed unaffected by anything")]
	private float _moveSpeedBase;

	[SerializeField]
	[Tooltip("The player's move speed while affected by the boost")]
	private float _moveSpeedBoost;

	[SerializeField]
	[Tooltip("The player's move speed while affect by the slow down pick up")]
	private float _moveSpeedSlowDown;

	[SerializeField]
	private float _boostDuration;

	[SerializeField]
	private float _slowDownDuration;

	[SerializeField]
	private bool _allowExtraTimeOnTicketPickup;

	[SerializeField]
	private float _timeAddedOnTicketPickup;

	[SerializeField]
	private float _normalBoostPatrolSpeed;

	[SerializeField]
	private float _negativeBoostPatrolSpeed;

	[SerializeField]
	private bool _useRandomItemSetups;

	[SerializeField]
	private List<FlashSaleTicketsToAddOnMission> _ticketsToAddOnMission = new List<FlashSaleTicketsToAddOnMission>();

	public float duration => _duration;

	public int entryFee => _entryFee;

	public int numTicketsOnMap => _numTicketsOnMap;

	public int numBoostsOnMap => _numBoostsOnMap;

	public float moveSpeedBase => _moveSpeedBase;

	public float moveSpeedBoost => _moveSpeedBoost;

	public float moveSpeedSlowDown => _moveSpeedSlowDown;

	public float boostDuration => _boostDuration;

	public float slowDownDuration => _slowDownDuration;

	public bool allowExtraTimeOnTicketPickup => _allowExtraTimeOnTicketPickup;

	public float timeAddedOnTicketPickup => _timeAddedOnTicketPickup;

	public float normalBoostPatrolSpeed => _normalBoostPatrolSpeed;

	public float negativeBoostPatrolSpeed => _negativeBoostPatrolSpeed;

	public bool useRandomItemSetups => _useRandomItemSetups;

	public List<FlashSaleTicketsToAddOnMission> ticketsToAddOnMission
	{
		get
		{
			return _ticketsToAddOnMission;
		}
		set
		{
			_ticketsToAddOnMission = value;
		}
	}
}
