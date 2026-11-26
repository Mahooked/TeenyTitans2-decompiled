using System;
using UnityEngine;

[Serializable]
public class MissionTypeConfigMailbox
{
	public enum DeliveryType
	{
		Receive,
		Send
	}

	[SerializeField]
	private DeliveryType _deliveryType;

	[SerializeField]
	private PlayerReward[] _deliveries;

	[SerializeField]
	private LocalizedString _sendHeader;

	[SerializeField]
	private LocalizedString _sendBody;

	public DeliveryType deliveryType => _deliveryType;

	public PlayerReward[] deliveries => _deliveries;

	public LocalizedString sendHeader => _sendHeader;

	public LocalizedString sendBody => _sendBody;
}
