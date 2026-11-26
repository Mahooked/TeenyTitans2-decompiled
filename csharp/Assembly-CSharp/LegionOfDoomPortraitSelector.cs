using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LegionOfDoomPortraitSelector : MonoBehaviour
{
	[SerializeField]
	private PortraitPageController pageController;

	[SerializeField]
	private TextMeshPro notEnoughChangeText;

	private Coroutine showNoChangeRoutine;

	public event Action PortraitSelected;

	public event Action<LegionOfDoomPortrait> PortraitSelectedForPurchase;

	private void OnEnable()
	{
		notEnoughChangeText.gameObject.SetActive(value: false);
	}

	public void Init()
	{
		pageController.Init(InitPortrait, CleanUpPortrait, GameManager.instance.legionOfDoomSettings.portraitUnlocks);
	}

	private void InitPortrait(LegionOfDoomPortrait portrait, LegionOfDoomPortraitUnlock portraitUnlockData)
	{
		portrait.Init(portraitUnlockData);
		portrait.uiItem.OnClickUIItem -= HandlePortraitClicked;
		portrait.uiItem.OnClickUIItem += HandlePortraitClicked;
	}

	private void CleanUpPortrait(LegionOfDoomPortrait portrait)
	{
		portrait.uiItem.OnClickUIItem -= HandlePortraitClicked;
	}

	private void HandlePortraitClicked(tk2dUIItem uiItem)
	{
		if (pageController.isDragging)
		{
			return;
		}
		LegionOfDoomPortrait component = uiItem.GetComponent<LegionOfDoomPortrait>();
		if (Player.instance.pvpData.IsPortraitUnlocked(component.unlockData.portraitSprite))
		{
			if (Player.instance.pvpData.PortraitSpriteName != component.portraitSprite.CurrentSprite.name)
			{
				Player.instance.pvpData.PortraitSpriteName = component.portraitSprite.CurrentSprite.name;
				Player.instance.Save();
			}
			if (PortraitSelected != null)
			{
				PortraitSelected();
			}
		}
		else
		{
			LegionOfDoomPortraitUnlock.UnlockCondition unlockCondition = component.unlockData.unlockCondition;
			if ((unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.Gold || (unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.LevelAndGold && Player.instance.pvpData.level >= component.unlockData.requiredLevel)) && PortraitSelectedForPurchase != null)
			{
				PortraitSelectedForPurchase(component);
			}
		}
		if (showNoChangeRoutine != null)
		{
			StopCoroutine(showNoChangeRoutine);
		}
		notEnoughChangeText.gameObject.SetActive(value: false);
	}

	public void ShowNotEnoughChangeMessage()
	{
		if (showNoChangeRoutine != null)
		{
			StopCoroutine(showNoChangeRoutine);
		}
		showNoChangeRoutine = StartCoroutine(ShowNoChangeMessage());
	}

	private IEnumerator ShowNoChangeMessage()
	{
		notEnoughChangeText.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		notEnoughChangeText.gameObject.SetActive(value: false);
	}
}
