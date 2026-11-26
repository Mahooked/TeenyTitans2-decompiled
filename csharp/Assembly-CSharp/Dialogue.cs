using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
	public Character character;

	public AvatarPosition position;

	public string faceSpriteName;

	public string poseSpriteName;

	public string message = string.Empty;

	public AudioClip clip;
}
