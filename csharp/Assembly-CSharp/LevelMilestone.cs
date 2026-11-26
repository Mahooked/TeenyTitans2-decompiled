using System;

[Serializable]
public class LevelMilestone
{
	public enum MilestoneType
	{
		None,
		PortraitBorderUpgrade,
		NewNPCHandler
	}

	public MilestoneType milestoneType;

	public string portraitBorderSpriteName;

	public int npcHandlerID;
}
