using System;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryEquipBar : MonoBehaviour
{
	public Action<AccessoryItemDefinition> AccessorySelected;

	public List<AccessorySlot> accessorySlots = new List<AccessorySlot>();

	public tk2dBaseSprite backgroundSprite;

	private Player player;

	public void Init(Player player)
	{
		this.player = player;
		player.EquippedAccessoriesChanged -= HandleEquippedAccessoriesChanged;
		player.EquippedAccessoriesChanged += HandleEquippedAccessoriesChanged;
		for (int i = 0; i < accessorySlots.Count; i++)
		{
			accessorySlots[i].uiItem.OnClickUIItem += HandleAccesorySlotClicked;
		}
		UpdateAccessorySlots();
	}

	private void HandleAccesorySlotClicked(tk2dUIItem accessorySlotUIItem)
	{
		AccessorySlot component = accessorySlotUIItem.GetComponent<AccessorySlot>();
		if (component != null && component.accessoryItemDefinition != null)
		{
			OnAccessorySelected(component.accessoryItemDefinition);
		}
	}

	private void HandleEquippedAccessoriesChanged()
	{
		UpdateAccessorySlots();
	}

	private void UpdateAccessorySlots()
	{
		int num = 0;
		for (int i = 0; i < accessorySlots.Count; i++)
		{
			AccessorySlot accessorySlot = accessorySlots[i];
			if (accessorySlot != null)
			{
				if (player.activeAccessories.Count > num)
				{
					AccessoryItemDefinition itemDef = GameManager.instance.itemDatabase.GetItem(player.activeAccessories[num]) as AccessoryItemDefinition;
					accessorySlot.Init(itemDef);
					num++;
				}
				else
				{
					accessorySlot.Init(null);
				}
			}
		}
	}

	private void OnAccessorySelected(AccessoryItemDefinition accessoryItemDefinition)
	{
		if (AccessorySelected != null)
		{
			AccessorySelected(accessoryItemDefinition);
		}
	}

	private void OnDestroy()
	{
		if (player != null)
		{
			player.EquippedAccessoriesChanged -= HandleEquippedAccessoriesChanged;
		}
		for (int i = 0; i < accessorySlots.Count; i++)
		{
			accessorySlots[i].uiItem.OnClickUIItem -= HandleAccesorySlotClicked;
		}
	}

	public void SetGrayedOut(bool grayedOut)
	{
		Color color = ((!grayedOut) ? Color.white : Color.gray);
		int count = accessorySlots.Count;
		backgroundSprite.color = color;
		for (int i = 0; i < count; i++)
		{
			AccessorySlot accessorySlot = accessorySlots[i];
			accessorySlot.SetGrayedOut(grayedOut);
		}
	}
}
