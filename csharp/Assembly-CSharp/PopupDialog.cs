using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupDialog : MonoBehaviour, IPoolable
{
	public Animator animator;

	public TextMeshPro headerText;

	public TextMeshPro descriptionText;

	public tk2dUIItem confirmButton;

	public tk2dUIItem cancelButton;

	public tk2dUIItem tintButton;

	public bool closeOnOutsideClick;

	public TextMeshPro amountText;

	private bool _isConfirmed;

	public bool isConfirmed => _isConfirmed;

	public event Action Confirmed;

	public event Action Canceled;

	protected virtual void OnEnable()
	{
		confirmButton.OnClick += HandleOnConfirmClick;
		cancelButton.OnClickUIItem += HandleOnCancelClick;
		if (tintButton != null)
		{
			tintButton.OnClickUIItem += HandleOnCancelClick;
		}
		_isConfirmed = false;
		confirmButton.enabled = true;
		cancelButton.enabled = true;
		if (tintButton != null)
		{
			tintButton.enabled = true;
		}
	}

	private void OnDisable()
	{
		confirmButton.OnClick -= HandleOnConfirmClick;
		cancelButton.OnClickUIItem -= HandleOnCancelClick;
		if (tintButton != null)
		{
			tintButton.OnClickUIItem -= HandleOnCancelClick;
		}
		Confirmed = null;
		Canceled = null;
	}

	private void OnConfirmed()
	{
		if (Confirmed != null)
		{
			Confirmed();
		}
	}

	private void OnCanceled()
	{
		if (Canceled != null)
		{
			Canceled();
		}
	}

	protected void HandleOnConfirmClick()
	{
		_isConfirmed = true;
		StartCoroutine(Close());
	}

	private void HandleOnCancelClick(tk2dUIItem uiItem)
	{
		if (uiItem == cancelButton || closeOnOutsideClick)
		{
			StartCoroutine(Close());
		}
	}

	public void UpdateWithAmount(int coinAmount)
	{
		if (amountText != null)
		{
			amountText.text = coinAmount.ToString();
		}
	}

	public void UpdateWithAmount(CurrencyType currencyType, int amount)
	{
		if (amountText != null)
		{
			string arg = "icon_currency_" + ((currencyType != CurrencyType.Coins) ? "rainbowCoin" : "coin");
			amountText.text = $"<sprite name=\"{arg}\">{amount}";
		}
	}

	public static void UpdateColorRecursivly(GameObject go, Color color)
	{
		if (go.GetComponent<tk2dBaseSprite>() != null)
		{
			go.GetComponent<tk2dBaseSprite>().color = color;
		}
		if (go.GetComponent<TextMeshPro>() != null)
		{
			go.GetComponent<TextMeshPro>().color = color;
		}
		int childCount = go.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			UpdateColorRecursivly(go.transform.GetChild(i).gameObject, color);
		}
	}

	public void DisableConfirmButton()
	{
		confirmButton.GetComponent<BoxCollider2D>().enabled = false;
		UpdateColorRecursivly(confirmButton.gameObject, new Color(0.5f, 0.5f, 0.5f));
	}

	public void EnableConfirmButton()
	{
		confirmButton.GetComponent<BoxCollider2D>().enabled = true;
		UpdateColorRecursivly(confirmButton.gameObject, new Color(1f, 1f, 1f));
	}

	public void LayoutOnlyConfirmButton()
	{
		cancelButton.gameObject.SetActive(value: false);
		confirmButton.transform.localPosition = new Vector3(0f, confirmButton.transform.localPosition.y, confirmButton.transform.localPosition.z);
	}

	private IEnumerator Close()
	{
		confirmButton.enabled = false;
		cancelButton.enabled = false;
		if (tintButton != null)
		{
			tintButton.enabled = false;
		}
		animator.Play("Outro");
		yield return null;
		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		if (_isConfirmed)
		{
			OnConfirmed();
		}
		else
		{
			OnCanceled();
		}
		gameObject.SetActive(value: false);
	}

	public void ResetObject()
	{
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
	}
}
