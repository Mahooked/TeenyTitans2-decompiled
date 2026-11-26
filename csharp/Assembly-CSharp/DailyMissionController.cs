using System.Collections.Generic;
using UnityEngine;

public class DailyMissionController : MonoBehaviour
{
	public List<DailyMissionPickup> pickupObjects = new List<DailyMissionPickup>();

	public void FindAllDailyMissionPickups()
	{
		Object[] array = Object.FindObjectsOfType(typeof(GameObject));
		Object[] array2 = array;
		foreach (Object obj in array2)
		{
			GameObject gameObject = (GameObject)obj;
			DailyMissionPickup component = gameObject.GetComponent<DailyMissionPickup>();
			if (!(component != null))
			{
				continue;
			}
			if (gameObject.GetComponent<Collider>() != null)
			{
				if (!pickupObjects.Contains(component))
				{
					pickupObjects.Add(component);
				}
			}
			else
			{
				Debug.LogWarning(gameObject.name + " has a DailyMissionPickup with no collider");
			}
		}
	}
}
