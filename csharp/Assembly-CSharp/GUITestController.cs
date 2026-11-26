using System.Collections;
using UnityEngine;

public class GUITestController : MonoBehaviour
{
	public CityName cityName;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1f);
		DailyMissionGUI dailyMissionGUI = ObjectPool.GetObjectFromResource<DailyMissionGUI>("Prefabs/GUI/DailyMissionGUI");
		dailyMissionGUI.transform.SetParent(GUICamera.instance.anchorMiddleCenter);
		dailyMissionGUI.transform.localPosition = Vector3.zero;
	}
}
