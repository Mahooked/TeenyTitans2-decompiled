public class BoostSetupObj : FlashSaleSetupObj
{
	public bool isNegativeBoost;

	public bool shouldPatrol;

	public PathNode[] patrolPath;

	public bool startMovingTowardStartNode;

	public bool overridePatrolSpeed;

	public float patrolSpeed;

	public FlashSaleBoostPickup.PatrolLoopType patrolLoopType;
}
