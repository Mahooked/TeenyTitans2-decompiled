public class BattleEndedEvent : GameEvent
{
	public BattleDefinition battleDefinition { get; private set; }

	public BattleResult battleResult { get; private set; }

	public BattleEndedEvent(BattleDefinition battleDefinition, BattleResult battleResult)
	{
		this.battleDefinition = battleDefinition;
		this.battleResult = battleResult;
	}
}
