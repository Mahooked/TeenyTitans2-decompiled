using System;
using UnityEngine;

[Serializable]
public class AEAnimation : ScriptableObject
{
	public float duration;

	public int totalFrames;

	public int imageScale = 4;

	[SerializeField]
	private AECompositionNew _composition;

	public AECompositionNew composition => _composition;

	public int lastFrame => totalFrames - 1;
}
