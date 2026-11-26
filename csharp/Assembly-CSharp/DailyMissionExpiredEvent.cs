public class DailyMissionExpiredEvent : GameEvent
{
	public DailyMissionData dailyMissionData { get; private set; }

	public DailyMissionExpiredEvent(DailyMissionData dailyMissionData)
	{
		this.dailyMissionData = dailyMissionData;
	}
}
