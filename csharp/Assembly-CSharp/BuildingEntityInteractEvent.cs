public class BuildingEntityInteractEvent : GameEvent
{
	public CityEntityBuilding buildingEntity { get; private set; }

	public BuildingEntityInteractEvent(CityEntityBuilding buildingEntity)
	{
		this.buildingEntity = buildingEntity;
	}
}
