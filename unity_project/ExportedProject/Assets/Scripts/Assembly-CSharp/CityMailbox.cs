using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMailbox : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private tk2dUIItem _uiItem;

	[SerializeField]
	private Collider _collider;

	[SerializeField]
	private GameObject _glint;

	[SerializeField]
	[Tooltip("Conversation to play upon tapping when there is no mail")]
	private Conversation _emptyConvo;

	[SerializeField]
	private Conversation _gotMailConvo;

	private void Awake()
	{
		Refresh();
	}

	private void OnEnable()
	{
		Player.instance.MissionAdded += HandleOnMissionAdded;
		_uiItem.OnClick += HandleOnTap;
	}

	private void OnDisable()
	{
		Player.instance.MissionAdded -= HandleOnMissionAdded;
		_uiItem.OnClick -= HandleOnTap;
	}

	private void Refresh()
	{
		if (Player.instance.HasActiveMissionOfSideType(MissionSideType.Mailbox))
		{
			_glint.SetActive(true);
		}
		else
		{
			_glint.SetActive(false);
		}
	}

	public void Interact()
	{
		if (Player.instance.HasActiveMissionOfSideType(MissionSideType.Mailbox))
		{
			StartCoroutine(ProcessActiveMissions());
		}
		else
		{
			EventMessenger.Instance.Raise(new StartConversationEvent(_emptyConvo));
		}
	}

	private IEnumerator ProcessActiveMissions()
	{
		List<MissionData> activeMailboxMissions = Player.instance.GetMissionDatasByType(MissionSideType.Mailbox);
		while (activeMailboxMissions.Count > 0)
		{
			yield return StartCoroutine(ProcessMission(activeMailboxMissions.Pop()));
		}
		Refresh();
	}

	private IEnumerator ProcessMission(MissionData missionData)
	{
		Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
		if (mission == null || missionData.completed)
		{
			yield break;
		}
		if (mission.mailboxConfig.deliveryType == MissionTypeConfigMailbox.DeliveryType.Receive)
		{
			yield return StartCoroutine(CityController.instance.PlayConvo(_gotMailConvo));
			CommunicatorManager.instance.CompleteMission(mission.uid, false);
			yield return StartCoroutine(CommunicatorManager.instance.CollectReward(mission.uid));
			yield break;
		}
		CityController.instance.cityCharacter.AllowInput = false;
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window_Coins", default(Vector3), false);
		dialog.transform.SetParent(GUICamera.instance.anchorMiddleCenter, true);
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.headerText.text = ((!string.IsNullOrEmpty(mission.mailboxConfig.sendHeader.key)) ? Language.Get(mission.mailboxConfig.sendHeader.key, "MENUS,GUI") : mission.mailboxConfig.sendHeader.text);
		dialog.descriptionText.text = ((!string.IsNullOrEmpty(mission.mailboxConfig.sendBody.key)) ? Language.Get(mission.mailboxConfig.sendBody.key, "MENUS,GUI") : mission.mailboxConfig.sendBody.text);
		dialog.gameObject.SetActive(true);
		dialog.amountText.text = string.Format("<sprite name=\"icon_currency_{0}\" tint=1>{1}", (mission.mailboxConfig.deliveries[0].currencyType != CurrencyType.Coins) ? "rainbowCoin" : "coin", mission.mailboxConfig.deliveries[0].amount);
		bool hasDeliveries = true;
		for (int i = 0; i < mission.mailboxConfig.deliveries.Length; i++)
		{
			switch (mission.mailboxConfig.deliveries[i].type)
			{
			case PlayerRewardType.Currency:
				if (!Player.instance.HasCurrency(mission.mailboxConfig.deliveries[i].currencyType, mission.mailboxConfig.deliveries[i].amount))
				{
					hasDeliveries = false;
				}
				break;
			case PlayerRewardType.Item:
				if (Player.instance.GetItemCount(mission.mailboxConfig.deliveries[i].itemUID) < mission.mailboxConfig.deliveries[i].amount)
				{
					hasDeliveries = false;
				}
				break;
			}
			if (!hasDeliveries)
			{
				break;
			}
		}
		if (hasDeliveries)
		{
			dialog.EnableConfirmButton();
		}
		else
		{
			dialog.DisableConfirmButton();
		}
		yield return new WaitWhile(() => dialog.gameObject.activeSelf);
		if (dialog.isConfirmed)
		{
			for (int num = 0; num < mission.mailboxConfig.deliveries.Length; num++)
			{
				switch (mission.mailboxConfig.deliveries[num].type)
				{
				case PlayerRewardType.Currency:
					Player.instance.RemoveCurrency(mission.mailboxConfig.deliveries[num].currencyType, mission.mailboxConfig.deliveries[num].amount);
					break;
				case PlayerRewardType.Item:
					Player.instance.RemoveItem(mission.mailboxConfig.deliveries[num].itemUID, mission.mailboxConfig.deliveries[num].amount);
					break;
				}
			}
			Player.instance.CompleteMission(mission.uid, mission.autoCollect);
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
		CityController.instance.cityCharacter.AllowInput = true;
	}

	private void HandleOnMissionAdded(int missionUid, bool isDialogueMission)
	{
		Refresh();
	}

	private void HandleOnTap()
	{
		_animator.Play("ShopBuilding_AnimTap", 0, 0f);
	}
}
