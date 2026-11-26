using System;

[Serializable]
public class LegionOfDoomTriggeredConvo
{
	public Conversation convo;

	public int requiredPvpRank;

	public int id;

	public LegionOfDoomTriggeredConvo(int id)
	{
		this.id = id;
	}
}
