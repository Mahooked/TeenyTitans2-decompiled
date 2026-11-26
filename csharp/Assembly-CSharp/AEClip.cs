using System;

[Serializable]
public class AEClip
{
	public int id;

	public AEAnimation animation;

	public string name;

	public bool looping;

	public bool useUniquePivot;

	public float pivotX = 0.5f;

	public float pivotY = 0.5f;

	public float timeScale = 1f;
}
