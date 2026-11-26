using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSaleSetupManager : MonoBehaviour
{
	public static FlashSaleSetupManager instance;

	private FlashSaleSetup[] allSetups;

	public FlashSaleSetup[] AllSetups => allSetups;

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		allSetups = GetComponentsInChildren<FlashSaleSetup>(includeInactive: true);
		FlashSaleSetup[] array = allSetups;
		foreach (FlashSaleSetup flashSaleSetup in array)
		{
			flashSaleSetup.gameObject.SetActive(value: true);
		}
		yield return new WaitForEndOfFrame();
		gameObject.SetActive(value: false);
	}

	public void CreateRandomSaleSetup(out List<GameObject> boostObjs, out List<GameObject> ticketObjs)
	{
		List<PathNode> reachableNodes = CityController.instance.cityCharacter.GetReachableNodes();
		List<FlashSaleSetup> list = new List<FlashSaleSetup>(allSetups);
		boostObjs = new List<GameObject>();
		ticketObjs = new List<GameObject>();
		for (int num = list.Count - 1; num >= 0; num--)
		{
			FlashSaleSetup flashSaleSetup = list[num];
			bool flag = true;
			FlashSaleSetupObj[] flashSaleObjs = flashSaleSetup.FlashSaleObjs;
			foreach (FlashSaleSetupObj flashSaleSetupObj in flashSaleObjs)
			{
				if (!reachableNodes.Contains(flashSaleSetupObj.spawnNode))
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				list.RemoveAt(num);
			}
		}
		FlashSaleSetup flashSaleSetup2 = list[Random.Range(0, list.Count)];
		FlashSaleSetupObj[] flashSaleObjs2 = flashSaleSetup2.FlashSaleObjs;
		foreach (FlashSaleSetupObj flashSaleSetupObj2 in flashSaleObjs2)
		{
			if (flashSaleSetupObj2 is BoostSetupObj)
			{
				BoostSetupObj boostSetupObj = (BoostSetupObj)flashSaleSetupObj2;
				GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSale_Boost", boostSetupObj.spawnNode.transform.position);
				FlashSaleBoostPickup component = objectFromResource.GetComponent<FlashSaleBoostPickup>();
				component.boostType = (boostSetupObj.isNegativeBoost ? FlashSaleBoostPickup.BoostType.Negative : FlashSaleBoostPickup.BoostType.Normal);
				component.shouldPatrol = boostSetupObj.shouldPatrol;
				component.loopType = boostSetupObj.patrolLoopType;
				if (component.shouldPatrol)
				{
					component.patrolPath = boostSetupObj.patrolPath;
					component.spawnNode = boostSetupObj.spawnNode;
					component.startMovingTowardStartNode = boostSetupObj.startMovingTowardStartNode;
					if (boostSetupObj.overridePatrolSpeed)
					{
						component.patrolSpeed = boostSetupObj.patrolSpeed;
					}
					else
					{
						component.patrolSpeed = ((component.boostType != FlashSaleBoostPickup.BoostType.Normal) ? GameManager.instance.flashSaleSettings.negativeBoostPatrolSpeed : GameManager.instance.flashSaleSettings.normalBoostPatrolSpeed);
					}
				}
				boostObjs.Add(objectFromResource);
			}
			else if (flashSaleSetupObj2 is TicketSetupObj)
			{
				GameObject objectFromResource2 = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/FlashSaleTicket", flashSaleSetupObj2.spawnNode.transform.position);
				ticketObjs.Add(objectFromResource2);
			}
		}
	}
}
