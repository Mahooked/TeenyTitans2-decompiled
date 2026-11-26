using System;
using System.Runtime.Serialization;

[Serializable]
public class MissionCompletedData
{
	public int uid;

	public int dayCompleted;

	[OptionalField]
	public int jumpCityDayCompleted;

	[OptionalField]
	public int gothamDayCompleted;

	[OptionalField]
	public int metropolisDayCompleted;

	public MissionCompletedData Copy()
	{
		MissionCompletedData missionCompletedData = new MissionCompletedData();
		missionCompletedData.uid = uid;
		missionCompletedData.dayCompleted = dayCompleted;
		missionCompletedData.jumpCityDayCompleted = jumpCityDayCompleted;
		missionCompletedData.gothamDayCompleted = gothamDayCompleted;
		missionCompletedData.metropolisDayCompleted = metropolisDayCompleted;
		return missionCompletedData;
	}
}
