using System.Collections;
using TMPro;
using UnityEngine;

public class CityShopSign : MonoBehaviour
{
	public enum State
	{
		None = 0,
		Closed = 1,
		ClosedUntilDay = 2,
		ClosedUntilNight = 3,
		ComingSoon = 4
	}

	public TextMeshPro closedComingSoonText;

	public TextMeshPro closedUntilText;

	public MeshRenderer sunMesh;

	public MeshRenderer moonMesh;

	public bool standAloneSign;

	public TimeOfDay standAloneTimeOfDay;

	public GameObject signObject;

	private bool standAloneIsSetup;

	private string comingSoonTextKey = string.Empty;

	private bool hasSetup;

	[SerializeField]
	private State _activeState;

	public State activeState
	{
		get
		{
			return _activeState;
		}
		set
		{
			SetState(value);
		}
	}

	private void Awake()
	{
		SetState(_activeState);
	}

	private void OnEnable()
	{
		if (standAloneSign)
		{
			CheckStandAloneShouldOpen();
		}
		if (!hasSetup)
		{
			hasSetup = true;
			if (TitleScreenController.instance != null)
			{
				TitleScreenController.instance.LanguageChanged += HandleLanguageUpdated;
			}
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if (!hasSetup)
		{
			hasSetup = true;
			if (TitleScreenController.instance != null)
			{
				TitleScreenController.instance.LanguageChanged += HandleLanguageUpdated;
			}
		}
	}

	private void OnDisable()
	{
		if (standAloneSign && standAloneIsSetup && CityController.instance != null)
		{
			standAloneIsSetup = false;
			CityController.instance.communicator.StartDayTime -= CheckStandAloneShouldOpen;
			CityController.instance.communicator.StartNightTime -= CheckStandAloneShouldOpen;
		}
		if (hasSetup)
		{
			hasSetup = false;
			if (TitleScreenController.instance != null)
			{
				TitleScreenController.instance.LanguageChanged -= HandleLanguageUpdated;
			}
		}
	}

	private void HandleLanguageUpdated()
	{
		if (!string.IsNullOrEmpty(comingSoonTextKey) && closedComingSoonText.gameObject.activeInHierarchy)
		{
			closedComingSoonText.text = Language.Get(comingSoonTextKey, "MENUS,GUI");
		}
	}

	private void CheckStandAloneShouldOpen()
	{
		StartCoroutine("DelayCheckStandAloneShouldOpen");
	}

	private IEnumerator DelayCheckStandAloneShouldOpen()
	{
		yield return new WaitForEndOfFrame();
		if (standAloneSign && !standAloneIsSetup)
		{
			standAloneIsSetup = true;
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime += CheckStandAloneShouldOpen;
				CityController.instance.communicator.StartNightTime += CheckStandAloneShouldOpen;
			}
		}
		if (!(CityController.instance == null))
		{
			if (standAloneTimeOfDay == TimeOfDay.Day && CityController.instance.communicator.isDayTime)
			{
				signObject.SetActive(false);
			}
			if (standAloneTimeOfDay == TimeOfDay.Night && !CityController.instance.communicator.isDayTime)
			{
				signObject.SetActive(false);
			}
			if (standAloneTimeOfDay == TimeOfDay.Day && !CityController.instance.communicator.isDayTime)
			{
				signObject.SetActive(true);
				ShowClosedUntilDaySign();
			}
			if (standAloneTimeOfDay == TimeOfDay.Night && CityController.instance.communicator.isDayTime)
			{
				signObject.SetActive(true);
				ShowClosedUntilNightSign();
			}
		}
	}

	public void SetState(State state)
	{
		_activeState = state;
		switch (_activeState)
		{
		case State.Closed:
			ShowClosedSign();
			break;
		case State.ClosedUntilDay:
			ShowClosedUntilDaySign();
			break;
		case State.ClosedUntilNight:
			ShowClosedUntilNightSign();
			break;
		case State.ComingSoon:
			ShowComingSoonSign();
			break;
		default:
			ShowNoSign();
			break;
		}
	}

	private void ShowClosedSign()
	{
		signObject.SetActive(true);
		closedComingSoonText.gameObject.SetActive(true);
		closedUntilText.gameObject.SetActive(false);
		comingSoonTextKey = "CLOSED";
		closedComingSoonText.text = Language.Get(comingSoonTextKey, "MENUS,GUI");
		sunMesh.enabled = false;
		moonMesh.enabled = false;
	}

	private void ShowComingSoonSign()
	{
		signObject.SetActive(true);
		closedComingSoonText.gameObject.SetActive(true);
		closedUntilText.gameObject.SetActive(false);
		comingSoonTextKey = "COMING_SOON";
		closedComingSoonText.text = Language.Get(comingSoonTextKey, "MENUS,GUI");
		sunMesh.enabled = false;
		moonMesh.enabled = false;
	}

	private void ShowClosedUntilDaySign()
	{
		signObject.SetActive(true);
		closedComingSoonText.gameObject.SetActive(false);
		closedUntilText.gameObject.SetActive(true);
		sunMesh.enabled = true;
		moonMesh.enabled = false;
	}

	private void ShowClosedUntilNightSign()
	{
		signObject.SetActive(true);
		closedComingSoonText.gameObject.SetActive(false);
		closedUntilText.gameObject.SetActive(true);
		sunMesh.enabled = false;
		moonMesh.enabled = true;
	}

	private void ShowNoSign()
	{
		signObject.SetActive(false);
	}
}
