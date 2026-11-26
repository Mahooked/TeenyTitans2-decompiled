using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AccessoryButton : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AccessoryButton> Clicked__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AccessoryButton> LongPressed__BackingField;

	public tk2dUIItem uiItem;

	public tk2dSprite accessorySprite;

	public tk2dSprite bgSprite;

	private IEnumerator inputUpdateCoroutine;

	private bool isLongPress;

	public AccessoryItemDefinition accessoryItemDefinition { get; private set; }

	public bool isEnabled { get; private set; }

	public event Action<AccessoryButton> Clicked
	{
		add
		{
			Action<AccessoryButton> action = Clicked__BackingField;
			Action<AccessoryButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Clicked__BackingField, (Action<AccessoryButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AccessoryButton> action = Clicked__BackingField;
			Action<AccessoryButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Clicked__BackingField, (Action<AccessoryButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AccessoryButton> LongPressed
	{
		add
		{
			Action<AccessoryButton> action = LongPressed__BackingField;
			Action<AccessoryButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action<AccessoryButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AccessoryButton> action = LongPressed__BackingField;
			Action<AccessoryButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action<AccessoryButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
		inputUpdateCoroutine = null;
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnUp -= HandleOnUp;
		Clicked__BackingField = null;
		LongPressed__BackingField = null;
	}

	public void Init(int uid)
	{
		accessoryItemDefinition = GameManager.instance.itemDatabase.GetItem(uid) as AccessoryItemDefinition;
		if (accessoryItemDefinition != null)
		{
			tk2dSprite obj = accessorySprite;
			Color white = Color.white;
			bgSprite.color = white;
			obj.color = white;
			accessorySprite.SetSprite(accessoryItemDefinition.iconSprite);
		}
		else
		{
			accessorySprite.color = Color.clear;
			bgSprite.color = Color.gray;
		}
		uiItem.enabled = true;
		uiItem.OnDown += HandleOnDown;
		uiItem.OnUp += HandleOnUp;
		isEnabled = true;
	}

	public void EnableButton()
	{
		uiItem.enabled = true;
		tk2dSprite obj = accessorySprite;
		Color white = Color.white;
		bgSprite.color = white;
		obj.color = white;
		isEnabled = true;
	}

	public void DisableButton()
	{
		uiItem.enabled = false;
		tk2dSprite obj = accessorySprite;
		Color gray = Color.gray;
		bgSprite.color = gray;
		obj.color = gray;
		isEnabled = false;
	}

	private void HandleOnDown()
	{
		isLongPress = false;
		inputUpdateCoroutine = InputUpdate();
		StartCoroutine(inputUpdateCoroutine);
	}

	private void HandleOnUp()
	{
		StopCoroutine(inputUpdateCoroutine);
		inputUpdateCoroutine = null;
		if (!isLongPress && Clicked__BackingField != null)
		{
			Clicked__BackingField(this);
		}
		isLongPress = false;
	}

	private IEnumerator InputUpdate()
	{
		float holdTime = 0f;
		while (true)
		{
			holdTime += Time.deltaTime;
			if (holdTime >= GameManager.instance.gameSettings.buttonLongPressTime)
			{
				break;
			}
			yield return null;
		}
		isLongPress = true;
		if (LongPressed__BackingField != null)
		{
			LongPressed__BackingField(this);
		}
	}
}
