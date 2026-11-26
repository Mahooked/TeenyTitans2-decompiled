using System;
using UnityEngine;

[Serializable]
public class Choice
{
	public string text;

	public string key = string.Empty;

	public CharacterName benefits;

	public int xpReward;

	public int harmonyReward;

	public string nextId;

	public bool isEnd;

	public string leftFaceSprite;

	public string rightFaceSprite;

	public AudioClip clip;

	public Choice Copy()
	{
		Choice choice = new Choice();
		choice.text = text;
		choice.key = key;
		choice.benefits = benefits;
		choice.xpReward = xpReward;
		choice.harmonyReward = harmonyReward;
		choice.nextId = nextId;
		choice.isEnd = isEnd;
		choice.leftFaceSprite = leftFaceSprite;
		choice.rightFaceSprite = rightFaceSprite;
		choice.clip = clip;
		return choice;
	}
}
