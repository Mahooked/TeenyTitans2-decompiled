using System;
using UnityEngine;

[Serializable]
public class MissionTypeConfigMailbox
{
	public enum DeliveryType
	{
		Receive = 0,
		Send = 1
	}

	[SerializeField]
	private DeliveryType _deliveryType;

	[SerializeField]
	private PlayerReward[] _deliveries;

	[SerializeField]
	private LocalizedString _sendHeader;

	[SerializeField]
	private LocalizedString _sendBody;

	public DeliveryType deliveryType
	{
		get
		{
			return _deliveryType;
		}
	}

	public PlayerReward[] deliveries
	{
		get
		{
			return _deliveries;
		}
	}

	public LocalizedString sendHeader
	{
		get
		{
			return _sendHeader;
		}
	}

	public LocalizedString sendBody
	{
		get
		{
			return _sendBody;
		}
	}
}
