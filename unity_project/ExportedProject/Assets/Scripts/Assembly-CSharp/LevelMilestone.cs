using System;

[Serializable]
public class LevelMilestone
{
	public enum MilestoneType
	{
		None = 0,
		PortraitBorderUpgrade = 1,
		NewNPCHandler = 2
	}

	public MilestoneType milestoneType;

	public string portraitBorderSpriteName;

	public int npcHandlerID;
}
