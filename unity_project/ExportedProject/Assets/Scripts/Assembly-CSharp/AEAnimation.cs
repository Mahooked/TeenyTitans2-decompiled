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

	public AECompositionNew composition
	{
		get
		{
			return _composition;
		}
	}

	public int lastFrame
	{
		get
		{
			return totalFrames - 1;
		}
	}
}
