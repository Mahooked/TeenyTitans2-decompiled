using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AddFriendMenu : MonoBehaviour
{
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

	public event Action FriendAdded;

	public event Action Canceled;

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
		Debug.Log("Init Add Friend Menu...");
		prevFriendCode = (curFriendCode = string.Empty);
		allowInput = true;
		friendCodeTextMesh.text = FriendCodeGenerator.GetFormattedCode(curFriendCode);
		friendCodeBoxObj.SetActive(value: true);
		friendRequestSentObj.SetActive(value: false);
		cursorSprite.transform.position = defaultCursorPos;
		keyboard = TouchScreenKeyboard.Open(curFriendCode, TouchScreenKeyboardType.Default, autocorrection: false, multiline: false, secure: false, alert: false);
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
				Debug.LogErrorFormat("Entered Friend Code is invalid. Can contain only capital letters and numbers.");
			}
		}
		else if (keyboard.status == TouchScreenKeyboard.Status.Canceled)
		{
			if (Canceled != null)
			{
				Canceled();
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
				Debug.Log("Update Cursor Position...");
			}
			else
			{
				cursorSprite.transform.position = defaultCursorPos;
				Debug.Log("Return Cursor to Original Position...");
			}
		}
		prevFriendCode = curFriendCode;
	}

	private IEnumerator FriendRequestSentSequence()
	{
		string friendCode = curFriendCode;
		if (FriendCodeGenerator.IsValidFriendCode(friendCode) && !Player.instance.pvpData.HasFriend(friendCode) && friendCode != Player.instance.pvpData.playerID)
		{
			friendCodeBoxObj.SetActive(value: false);
			friendRequestSentObj.SetActive(value: true);
			Player.instance.pvpData.AddFriend(friendCode);
			AchievementsDatabase.UnlockAchievementId("TT2_BEABUDDY");
			yield return new WaitForSeconds(2f);
			if (FriendAdded != null)
			{
				FriendAdded();
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
