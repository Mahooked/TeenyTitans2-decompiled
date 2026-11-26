using System;

[Serializable]
public class TurningForgivenessOverrideData
{
	public PathNode pathNode;

	public float turningForgiveness;

	public int chosenNodeIndex;

	public TurningForgivenessOverrideData(PathNode newNode)
	{
		pathNode = newNode;
		turningForgiveness = 0f;
		chosenNodeIndex = 0;
	}
}
