using System;
using System.Collections;
using UnityEngine;

public class AccessoryButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite accessorySprite;

	public tk2dSprite bgSprite;

	private IEnumerator inputUpdateCoroutine;

	private bool isLongPress;

	public AccessoryItemDefinition accessoryItemDefinition { get; private set; }

	public bool isEnabled { get; private set; }

	public event Action<AccessoryButton> Clicked;

	public event Action<AccessoryButton> LongPressed;

	private void OnDestroy()
	{
		StopAllCoroutines();
		inputUpdateCoroutine = null;
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnUp -= HandleOnUp;
		Clicked = null;
		LongPressed = null;
	}

	public void Init(int uid)
	{
		accessoryItemDefinition = GameManager.instance.itemDatabase.GetItem(uid) as AccessoryItemDefinition;
		if (accessoryItemDefinition != null)
		{
			tk2dSprite tk2dSprite2 = accessorySprite;
			Color white = Color.white;
			bgSprite.color = white;
			tk2dSprite2.color = white;
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
		tk2dSprite tk2dSprite2 = accessorySprite;
		Color white = Color.white;
		bgSprite.color = white;
		tk2dSprite2.color = white;
		isEnabled = true;
	}

	public void DisableButton()
	{
		uiItem.enabled = false;
		tk2dSprite tk2dSprite2 = accessorySprite;
		Color gray = Color.gray;
		bgSprite.color = gray;
		tk2dSprite2.color = gray;
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
		if (!isLongPress && Clicked != null)
		{
			Clicked(this);
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
		if (LongPressed != null)
		{
			LongPressed(this);
		}
	}
}
