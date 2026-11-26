using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AndroidPermissions : MonoBehaviour
{
	private const string WRITE_STORAGE_PERMISSION = "android.permission.WRITE_EXTERNAL_STORAGE";

	public GameObject initialWindow;

	public GameObject tryAgainWindow;

	public GameObject closeWindow;

	public GameObject BGBackground;

	public GameObject BGButton;

	public LanguageCode[] languageCodes;

	public List<GameObject> EN = new List<GameObject>();

	public List<GameObject> FR = new List<GameObject>();

	public List<GameObject> IT = new List<GameObject>();

	public List<GameObject> RU = new List<GameObject>();

	public List<GameObject> TR = new List<GameObject>();

	public List<GameObject> ES = new List<GameObject>();

	public List<GameObject> PT = new List<GameObject>();

	public List<GameObject> KO = new List<GameObject>();

	public List<GameObject> JA = new List<GameObject>();

	public List<GameObject> CURRENT_GROUP;

	private bool waitingForResponse;

	private bool permissionGranted;

	private bool shouldShowReasoning = true;

	private void Awake()
	{
		CURRENT_GROUP = EN;
		initialWindow.SetActive(false);
		tryAgainWindow.SetActive(false);
		closeWindow.SetActive(false);
		BGBackground.SetActive(false);
		BGButton.SetActive(false);
	}

	public IEnumerator StartAndroidPermissionsRoutine()
	{
		Debug.Log("GetSystemLanguageCode: " + Language.GetSystemLanguageCode());
		CURRENT_GROUP = GetGroupForLanguage(Language.GetSystemLanguageCode());
		yield return null;
	}

	private void ShowInitialWindow()
	{
		CURRENT_GROUP[0].SetActive(true);
		BGBackground.SetActive(true);
		BGButton.SetActive(true);
	}

	private void ShowTryAgainWindow()
	{
		CURRENT_GROUP[1].SetActive(true);
		BGBackground.SetActive(true);
		BGButton.SetActive(true);
	}

	private void ShowBreadCrumbWindow()
	{
		CURRENT_GROUP[2].SetActive(true);
		BGBackground.SetActive(true);
		BGButton.SetActive(true);
	}

	private void HideInitialWindow()
	{
		CURRENT_GROUP[0].SetActive(false);
		BGBackground.SetActive(false);
		BGButton.SetActive(false);
	}

	private void HideTryAgainWindow()
	{
		CURRENT_GROUP[1].SetActive(false);
		BGBackground.SetActive(false);
		BGButton.SetActive(false);
	}

	private void HideBreadCrumbWindow()
	{
		CURRENT_GROUP[2].SetActive(false);
		BGBackground.SetActive(false);
		BGButton.SetActive(false);
	}

	private void HandlePermissionGranted(string permission)
	{
		Debug.Log("Handle Permission GRANTED");
		waitingForResponse = false;
		permissionGranted = true;
	}

	private void HandlePermissionsDenied(string permission)
	{
		Debug.Log("Handle Permission DENIED");
		waitingForResponse = false;
		permissionGranted = false;
	}

	private void OnClickedInitialGotIt()
	{
		HideInitialWindow();
	}

	private void OnClickedTryAgain()
	{
		HideTryAgainWindow();
	}

	private void OnClickedClosed()
	{
		Application.Quit();
	}

	public List<GameObject> GetGroupForLanguage(LanguageCode code)
	{
		switch (code)
		{
		case LanguageCode.EN:
			return EN;
		case LanguageCode.FR:
			return FR;
		case LanguageCode.IT:
			return IT;
		case LanguageCode.RU:
			return RU;
		case LanguageCode.TR:
			return TR;
		case LanguageCode.ES:
			return ES;
		case LanguageCode.PT:
			return PT;
		case LanguageCode.KO:
			return KO;
		case LanguageCode.JA:
			return JA;
		default:
			return EN;
		}
	}

	public void GenerateLanguageGroups()
	{
		for (int i = 0; i < languageCodes.Length; i++)
		{
			LanguageCode languageCode = languageCodes[i];
			List<GameObject> groupForLanguage = GetGroupForLanguage(languageCode);
			foreach (GameObject item in groupForLanguage)
			{
				Object.DestroyImmediate(item);
			}
			groupForLanguage.Clear();
			GameObject gameObject = Object.Instantiate(initialWindow);
			gameObject.transform.parent = initialWindow.transform.parent;
			gameObject.transform.localPosition = initialWindow.transform.localPosition;
			gameObject.name = gameObject.name.Replace("(Clone)", "-" + languageCode);
			groupForLanguage.Add(gameObject);
			GameObject gameObject2 = Object.Instantiate(tryAgainWindow);
			gameObject2.transform.parent = tryAgainWindow.transform.parent;
			gameObject2.transform.localPosition = tryAgainWindow.transform.localPosition;
			gameObject2.name = gameObject2.name.Replace("(Clone)", "-" + languageCode);
			groupForLanguage.Add(gameObject2);
			GameObject gameObject3 = Object.Instantiate(closeWindow);
			gameObject3.transform.parent = closeWindow.transform.parent;
			gameObject3.transform.localPosition = closeWindow.transform.localPosition;
			gameObject3.name = gameObject3.name.Replace("(Clone)", "-" + languageCode);
			groupForLanguage.Add(gameObject3);
			LocalizedText[] componentsInChildren = gameObject.GetComponentsInChildren<LocalizedText>(true);
			foreach (LocalizedText localizedText in componentsInChildren)
			{
				localizedText.UpdateBakedTextAndMaterial(languageCode);
			}
			componentsInChildren = gameObject2.GetComponentsInChildren<LocalizedText>(true);
			foreach (LocalizedText localizedText2 in componentsInChildren)
			{
				localizedText2.UpdateBakedTextAndMaterial(languageCode);
			}
			componentsInChildren = gameObject3.GetComponentsInChildren<LocalizedText>(true);
			foreach (LocalizedText localizedText3 in componentsInChildren)
			{
				localizedText3.UpdateBakedTextAndMaterial(languageCode);
			}
		}
	}
}
