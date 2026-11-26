using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class CityDialogBox : MonoBehaviour
{
	public static List<CityDialogBox> activeDialogBoxes = new List<CityDialogBox>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Cancelled__BackingField;

	public GameObject mainPage;

	public Animator animator;

	public TextMeshPro mainMessageText;

	public TextMeshPro confirmBtnMessageText;

	public TextMeshPro confirmBtnMessageTextSecondary;

	public TextMeshPro cancelBtnMessageText;

	public tk2dUIItem confirmBtn;

	public tk2dUIItem cancelBtn;

	public AudioClip sfxOnConfirm;

	public AudioClip sfxOnConfirmDenied;

	public DialogBoxPage[] extraPages;

	private bool confirmCanBeClicked;

	private int currentPage = -1;

	private Action Confirmed;

	private Action FirstPageConfirmed;

	private Action ConfirmedDenied;

	private bool hidingDialogBox;

	public static bool IsDialogBoxActive
	{
		get
		{
			return activeDialogBoxes.Count > 0;
		}
	}

	public event Action Cancelled
	{
		add
		{
			Action action = Cancelled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Cancelled__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Cancelled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Cancelled__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Start()
	{
		confirmBtn.OnClick += HandleConfirmClicked;
		cancelBtn.OnClick += HandleCancelClicked;
		base.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		hidingDialogBox = false;
	}

	public virtual void ShowDialogBox(Action confirmBtnCallback = null, Action firstPageConfirmBtnCallback = null, bool allowedToConfirm = true, Action confirmDeniedCallback = null)
	{
		activeDialogBoxes.Add(this);
		currentPage = -1;
		base.gameObject.SetActive(true);
		mainPage.SetActive(true);
		SwipeManager.Instance.enabled = false;
		CityController.instance.SetMagnifyingGlassActive(false);
		CityController.instance.SetTitanTowerBtnActive(false);
		Communicator.instance.SetCommunicatorShown(false);
		GameUI.instance.HideMySquad();
		confirmCanBeClicked = allowedToConfirm;
		for (int i = 0; i < extraPages.Length; i++)
		{
			extraPages[i].pageObj.SetActive(false);
		}
		Confirmed = confirmBtnCallback;
		FirstPageConfirmed = firstPageConfirmBtnCallback;
		ConfirmedDenied = confirmDeniedCallback;
	}

	public virtual void ShowDialogBox(string mainMessage, Action confirmBtnCallback = null, Action firstPageConfirmBtnCallback = null, bool allowedToConfirm = true, Action confirmDeniedCallback = null)
	{
		if (mainMessageText != null)
		{
			mainMessageText.text = mainMessage;
		}
		ShowDialogBox(confirmBtnCallback, firstPageConfirmBtnCallback, allowedToConfirm, confirmDeniedCallback);
	}

	public void HideDialogBox(bool useConfirmCallback)
	{
		hidingDialogBox = true;
		activeDialogBoxes.Remove(this);
		SwipeManager.Instance.enabled = true;
		CityController.instance.SetMagnifyingGlassActive(true);
		CityController.instance.SetTitanTowerBtnActive(true);
		Communicator.instance.SetCommunicatorShown(true);
		GameUI.instance.ShowMySquad();
		Cancelled__BackingField = null;
		animator.Play("FlashSaleMenu_outro", 0, 0f);
		StartCoroutine(HideBoxFinished(useConfirmCallback));
	}

	private IEnumerator HideBoxFinished(bool useConfirmCallback)
	{
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		base.gameObject.SetActive(false);
	}

	private void HandleConfirmClicked()
	{
		if (!confirmCanBeClicked)
		{
			if (!hidingDialogBox)
			{
				if (ConfirmedDenied != null)
				{
					ConfirmedDenied();
				}
				HideDialogBox(false);
				if (sfxOnConfirmDenied != null)
				{
					GFAudioManager.instance.PlayClip(sfxOnConfirmDenied, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
				}
			}
			return;
		}
		if (sfxOnConfirm != null)
		{
			GFAudioManager.instance.PlayClip(sfxOnConfirm, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		}
		currentPage++;
		if (currentPage == 0 && FirstPageConfirmed != null)
		{
			FirstPageConfirmed();
		}
		int num = extraPages.Length;
		if (num > 0 && currentPage < num)
		{
			mainPage.SetActive(false);
			for (int i = 0; i < num; i++)
			{
				DialogBoxPage dialogBoxPage = extraPages[i];
				dialogBoxPage.pageObj.SetActive(i == currentPage);
				if (i == currentPage)
				{
					dialogBoxPage.continueBtn.OnClick -= HandleConfirmClicked;
					dialogBoxPage.continueBtn.OnClick += HandleConfirmClicked;
				}
			}
		}
		else if (!hidingDialogBox)
		{
			if (Confirmed != null)
			{
				Confirmed();
			}
			HideDialogBox(true);
		}
	}

	private void HandleCancelClicked()
	{
		if (!hidingDialogBox)
		{
			if (Cancelled__BackingField != null)
			{
				Cancelled__BackingField();
			}
			HideDialogBox(false);
		}
	}
}
