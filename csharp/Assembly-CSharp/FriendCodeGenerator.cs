using UnityEngine;

public static class FriendCodeGenerator
{
	private static readonly string charSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	private static readonly int dashSpacing = 4;

	public static readonly int codeLength = 12;

	public static string Generate()
	{
		string text = string.Empty;
		for (int i = 0; i < codeLength; i++)
		{
			text += charSet[Random.Range(0, charSet.Length)];
		}
		return text;
	}

	public static string GetFormattedCode(string friendCode)
	{
		string text = string.Empty;
		int length = friendCode.Length;
		for (int i = 0; i < codeLength; i++)
		{
			text += ((i >= length) ? ' ' : friendCode[i]);
			if (i < codeLength - 1 && (i + 1) % dashSpacing == 0)
			{
				text += '-';
			}
		}
		return text;
	}

	public static bool IsValidFriendCode(string friendCode)
	{
		if (string.IsNullOrEmpty(friendCode))
		{
			Debug.Log("Friend Code Invalid | Reason: Is Null or Empty");
			return false;
		}
		if (friendCode.Length != codeLength)
		{
			Debug.Log("Friend Code Invalid | Reason: Incorrect length");
			return false;
		}
		for (int i = 0; i < friendCode.Length; i++)
		{
			if (!charSet.Contains(friendCode[i].ToString()))
			{
				Debug.Log("Friend Code Invalid | Reason: Contains invalid character");
				return false;
			}
		}
		return true;
	}

	public static bool IsValidCharacter(char character)
	{
		return charSet.Contains(character.ToString());
	}
}
