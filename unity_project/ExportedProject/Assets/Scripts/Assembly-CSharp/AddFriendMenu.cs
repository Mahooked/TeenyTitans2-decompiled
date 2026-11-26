using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AddFriendMenu : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action FriendAdded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Canceled__BackingField;

	public TextMeshPro friendCodeTextMesh;

	public tk2dSprite cursorSprite;

	public GameObject friendCodeBoxObj;

	public GameObject friendRequestSentObj;

	private string curFriendCode;

	private string prevFriendCode;

	private Sequence cursorTween;

	private TouchScreenKeyboard keyboard;

	private Vector3 defaultCursorPos;

	private bool allowInput;

	public event Action FriendAdded
	{
		add
		{
			Action action = FriendAdded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendAdded__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = FriendAdded__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref FriendAdded__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Canceled
	{
		add
		{
			Action action = Canceled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Canceled__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Canceled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Canceled__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		defaultCursorPos = cursorSprite.transform.position;
	}

	private void OnDisable()
	{
		keyboard.active = false;
	}

	public void Init()
	{
		UnityEngine.Debug.Log("Init Add Friend Menu...");
		prevFriendCode = (curFriendCode = string.Empty);
		allowInput = true;
		friendCodeTextMesh.text = FriendCodeGenerator.GetFormattedCode(curFriendCode);
		friendCodeBoxObj.SetActive(true);
		friendRequestSentObj.SetActive(false);
		cursorSprite.transform.position = defaultCursorPos;
		keyboard = TouchScreenKeyboard.Open(curFriendCode, TouchScreenKeyboardType.Default, false, false, false, false);
	}

	private void Update()
	{
		if (!allowInput)
		{
			return;
		}
		string text = keyboard.text;
		curFriendCode = string.Empty;
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			if (FriendCodeGenerator.IsValidCharacter(c))
			{
				curFriendCode += c;
			}
			else if (IsLowerCaseLetter(c))
			{
				curFriendCode += c.ToString().ToUpper()[0];
			}
		}
		keyboard.text = curFriendCode;
		friendCodeTextMesh.text = FriendCodeGenerator.GetFormattedCode(curFriendCode);
		if (keyboard.status == TouchScreenKeyboard.Status.Done)
		{
			if (FriendCodeGenerator.IsValidFriendCode(curFriendCode))
			{
				StartCoroutine(FriendRequestSentSequence());
				allowInput = false;
			}
			else
			{
				UnityEngine.Debug.LogErrorFormat("Entered Friend Code is invalid. Can contain only capital letters and numbers.");
			}
		}
		else if (keyboard.status == TouchScreenKeyboard.Status.Canceled)
		{
			if (Canceled__BackingField != null)
			{
				Canceled__BackingField();
			}
			return;
		}
		if (prevFriendCode != curFriendCode)
		{
			if (!string.IsNullOrEmpty(curFriendCode))
			{
				friendCodeTextMesh.ForceMeshUpdate();
				int num = curFriendCode.Length - 1;
				TMP_TextInfo textInfo = friendCodeTextMesh.textInfo;
				TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[Mathf.Min(13, num + Mathf.FloorToInt((num + 1) / 4))];
				float x = friendCodeTextMesh.transform.TransformPoint(tMP_CharacterInfo.xAdvance, 0f, 0f).x;
				cursorSprite.transform.position = new Vector3(x + cursorSprite.GetBounds().size.x / 2f, cursorSprite.transform.position.y, cursorSprite.transform.position.z);
				UnityEngine.Debug.Log("Update Cursor Position...");
			}
			else
			{
				cursorSprite.transform.position = defaultCursorPos;
				UnityEngine.Debug.Log("Return Cursor to Original Position...");
			}
		}
		prevFriendCode = curFriendCode;
	}

	private IEnumerator FriendRequestSentSequence()
	{
		string friendCode = curFriendCode;
		if (FriendCodeGenerator.IsValidFriendCode(friendCode) && !Player.instance.pvpData.HasFriend(friendCode) && friendCode != Player.instance.pvpData.playerID)
		{
			friendCodeBoxObj.SetActive(false);
			friendRequestSentObj.SetActive(true);
			Player.instance.pvpData.AddFriend(friendCode);
			AchievementsDatabase.UnlockAchievementId("TT2_BEABUDDY");
			yield return new WaitForSeconds(2f);
			if (FriendAdded__BackingField != null)
			{
				FriendAdded__BackingField();
			}
		}
		else
		{
			Init();
		}
	}

	private bool IsCharDigit(char c)
	{
		return c >= '0' && c <= '9';
	}

	private bool IsLowerCaseLetter(char c)
	{
		return c >= 'a' && c <= 'z';
	}
}
