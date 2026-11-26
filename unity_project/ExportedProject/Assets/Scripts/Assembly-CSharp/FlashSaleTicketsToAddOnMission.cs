using System;

[Serializable]
public class FlashSaleTicketsToAddOnMission
{
	public int missionUID;

	public int ticketsToAdd;

	public int boostsToAdd;

	public FlashSaleTicketsToAddOnMission(int newMissionUID, int newTicketsToAdd)
	{
		missionUID = newMissionUID;
		ticketsToAdd = newTicketsToAdd;
	}
}
