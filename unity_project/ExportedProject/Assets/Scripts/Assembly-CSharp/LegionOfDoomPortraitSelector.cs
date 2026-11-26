using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class LegionOfDoomPortraitSelector : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action PortraitSelected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<LegionOfDoomPortrait> PortraitSelectedForPurchase__BackingField;

	[SerializeField]
	private PortraitPageController pageController;

	[SerializeField]
	private TextMeshPro notEnoughChangeText;

	private Coroutine showNoChangeRoutine;

	public event Action PortraitSelected
	{
		add
		{
			Action action = PortraitSelected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSelected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = PortraitSelected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSelected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<LegionOfDoomPortrait> PortraitSelectedForPurchase
	{
		add
		{
			Action<LegionOfDoomPortrait> action = PortraitSelectedForPurchase__BackingField;
			Action<LegionOfDoomPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSelectedForPurchase__BackingField, (Action<LegionOfDoomPortrait>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<LegionOfDoomPortrait> action = PortraitSelectedForPurchase__BackingField;
			Action<LegionOfDoomPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PortraitSelectedForPurchase__BackingField, (Action<LegionOfDoomPortrait>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void OnEnable()
	{
		notEnoughChangeText.gameObject.SetActive(false);
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
			if (PortraitSelected__BackingField != null)
			{
				PortraitSelected__BackingField();
			}
		}
		else
		{
			LegionOfDoomPortraitUnlock.UnlockCondition unlockCondition = component.unlockData.unlockCondition;
			if ((unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.Gold || (unlockCondition == LegionOfDoomPortraitUnlock.UnlockCondition.LevelAndGold && Player.instance.pvpData.level >= component.unlockData.requiredLevel)) && PortraitSelectedForPurchase__BackingField != null)
			{
				PortraitSelectedForPurchase__BackingField(component);
			}
		}
		if (showNoChangeRoutine != null)
		{
			StopCoroutine(showNoChangeRoutine);
		}
		notEnoughChangeText.gameObject.SetActive(false);
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
		notEnoughChangeText.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		notEnoughChangeText.gameObject.SetActive(false);
	}
}
