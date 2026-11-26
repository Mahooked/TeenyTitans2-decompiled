using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLinkController : MonoBehaviour
{
	private static GameLinkController _instance;

	public static GameLinkController instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("!GameLinkController").AddComponent<GameLinkController>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(gameObject);
	}

	private void OnEnable()
	{
		URLSchemeHandler.Instance.OpenedWithURL += URLSchemeHandler_Instance_OpenedWithURL;
	}

	private void OnDisable()
	{
		URLSchemeHandler.Instance.OpenedWithURL -= URLSchemeHandler_Instance_OpenedWithURL;
	}

	private void URLSchemeHandler_Instance_OpenedWithURL(Dictionary<string, string> parameters)
	{
		foreach (string key in parameters.Keys)
		{
			if (key != null && key == "unlockfig")
			{
				UnlockFig(parameters[key]);
			}
		}
	}

	private void UnlockFig(string figName)
	{
		int num = -1;
		switch (figName)
		{
		case "mr_chibi":
			num = 81;
			break;
		case "hooded_hood":
			num = 80;
			break;
		}
		if (num > -1)
		{
			StartCoroutine(UnlockFigSequence(num));
		}
	}

	private IEnumerator UnlockFigSequence(int figureUID)
	{
		CharacterDefinition charDef = GameManager.instance.characterDatabase.GetCharacter(figureUID);
		if (charDef == null)
		{
			Debug.LogErrorFormat("[GameLinkController] - A figure with UID {0} does not exist in the current character database.", figureUID);
			yield break;
		}
		string triggerName = "secret_figure_unlocked_" + figureUID;
		if (Player.instance.HasAcquiredCharacter(charDef) || Player.instance.HasTriggered(triggerName))
		{
			while (!CanShowPopup())
			{
				yield return new WaitForSeconds(0.5f);
			}
			GameLinkPopupDialog.Show(Language.Get("GAMELINK_POPUP_ALREADYCLAIMED_HEADER", "MENUS,GUI"), Language.Get("GAMELINK_POPUP_ALREADYCLAIMED_MESSAGE", "MENUS,GUI"), Language.Get("GAMELINK_POPUP_ALREADYCLAIMED_BUTTON", "MENUS,GUI"), string.Empty);
			yield break;
		}
		if (!IsPlayerAvailableInCity())
		{
			while (!CanShowPopup())
			{
				yield return new WaitForSeconds(0.5f);
			}
			GameLinkPopupDialog.Show(Language.Get("GAMELINK_POPUP_UNLOCKED_HEADER", "MENUS,GUI"), Language.Get("GAMELINK_POPUP_UNLOCKED_MESSAGE", "MENUS,GUI"), Language.Get("GAMELINK_POPUP_UNLOCKED_BUTTON", "MENUS,GUI"), string.Empty);
			yield return new WaitWhile(() => GameLinkPopupDialog.Instance != null);
			do
			{
				yield return new WaitForSeconds(0.5f);
			}
			while (!IsPlayerAvailableInCity());
		}
		CityReward.instance.ShowFigureReward(new List<CharacterRewardData>
		{
			new CharacterRewardData(figureUID)
		});
		Player.instance.SetTrigger(triggerName);
	}

	private bool CanShowPopup()
	{
		switch (SceneManager.GetActiveScene().name)
		{
		case "Transition":
		case "Init":
		case "InitTwo":
			return false;
		default:
			return true;
		}
	}

	private bool IsPlayerAvailableInCity()
	{
		if (CityController.instance == null)
		{
			return false;
		}
		if (TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive)
		{
			return false;
		}
		if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (CityReward.instance != null && CityReward.instance.showingRewards)
		{
			return false;
		}
		if (PauseManager.instance.isPaused)
		{
			return false;
		}
		if (CoreGameTutorial.instance != null && CoreGameTutorial.instance.isActive)
		{
			return false;
		}
		if (SaveUtility.GetInt(string.Concat(CoreGameTutorial.Step.CityTutorialViewMapStart, "_Complete"), 0) == 0)
		{
			return false;
		}
		return true;
	}
}
