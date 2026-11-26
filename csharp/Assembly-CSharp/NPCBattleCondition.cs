using System;

[Serializable]
public class NPCBattleCondition
{
	public NPCBattleConditionType type;

	public int amount;

	public int uid;

	public string tag = string.Empty;

	public NPCBattleCondition()
	{
	}

	public NPCBattleCondition(NPCBattleConditionType type, int amount, int uid, string tag = "")
	{
		this.type = type;
		this.amount = amount;
		this.uid = uid;
		this.tag = tag;
	}
}
