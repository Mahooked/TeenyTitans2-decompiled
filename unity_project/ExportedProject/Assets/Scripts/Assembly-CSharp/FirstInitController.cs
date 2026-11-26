using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstInitController : MonoBehaviour
{
	public static FirstInitController instance;

	public AndroidPermissions androidPermissions;

	public LocalizationSettings localizationSettings;

	public GenericDialogBox genericDialogBox;

	private bool deserializationDidFail;

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
		Debug.Log("Unity " + Application.unityVersion);
		if (deserializationDidFail || !Player.LoadData())
		{
			genericDialogBox.headerText.text = Language.Get("INCOMPATIBLE_SAVE_HEADER", "MENUS,GUI");
			genericDialogBox.messageText.text = Language.Get("INCOMPATIBLE_SAVE_MESSAGE", "MENUS,GUI");
			genericDialogBox.gameObject.SetActive(true);
			while (true)
			{
				yield return null;
			}
		}
		if (Player.instance.currentDailyMission == null)
		{
			DailyMissionManager.instance.ForceGenerateMission();
		}
		SceneManager.LoadScene("InitTwo");
	}
}
