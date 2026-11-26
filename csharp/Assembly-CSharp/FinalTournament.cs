using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FinalTournament : MonoBehaviour
{
	public static FinalTournament instance;

	public GameObject finalTournamentGroup;

	public Animator animator;

	public Transform cameraTarget;

	public PathNode entranceNode;

	private float originalZoomFactor = 9f;

	private bool showingFinalTournament;

	private CityController _cityController;

	private bool targetOffsetDirty;

	private GameObject mrChibiNPC;

	private CityController cityController
	{
		get
		{
			if (_cityController == null && CityController.instance != null)
			{
				_cityController = CityController.instance;
			}
			return _cityController;
		}
	}

	private void Awake()
	{
		instance = this;
		if (Player.instance.GetMissionCompletedData(36) != null)
		{
			SaveUtility.SetInt("FinalTournamnentUnlocked", 1, save: false);
		}
		if (SaveUtility.GetInt("FinalTournamnentUnlocked") == 1)
		{
			ShowFinalTournament();
		}
	}

	private void OnDestroy()
	{
		if (CommunicatorManager.instance != null)
		{
			CommunicatorManager.instance.AnimationEvent -= HandleAnimationEvent;
		}
		instance = null;
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		CommunicatorManager.instance.AnimationEvent += HandleAnimationEvent;
		if (SaveUtility.GetInt("GameHasRevealedHoodedHood") == 1)
		{
			SpawnMrChibiFinal(Vector3.zero);
		}
	}

	private void HandleAnimationEvent(string eventName)
	{
		if (eventName.Equals("RevealHoodedHood"))
		{
			StartCoroutine("StartHoodedHoodReveal");
		}
	}

	private IEnumerator DelayHoodedHoodFlip(AEControllerTk2d hoodedHoodController)
	{
		yield return new WaitForSeconds(0.9f);
		hoodedHoodController.isFlipped = !hoodedHoodController.isFlipped;
	}

	private IEnumerator StartHoodedHoodReveal()
	{
		SaveUtility.SetInt("GameHasRevealedHoodedHood", 1);
		CityController.instance.cityCharacter.AllowInput = false;
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		yield return new WaitForSeconds(0.1f);
		CityController.instance.EndHoodedHoodAudio();
		if (CityController.instance.hoodedHoodCity != null)
		{
			AEControllerTk2d componentInChildren = CityController.instance.hoodedHoodCity.GetComponentInChildren<AEControllerTk2d>();
			if (componentInChildren != null)
			{
				componentInChildren.Play("transition", clearQueue: true);
			}
		}
		yield return new WaitForSeconds(1f);
		Vector3 previousRotation = Vector3.zero;
		CityNPC hoodedHood = null;
		if (CityController.instance.hoodedHoodCity != null)
		{
			hoodedHood = CityController.instance.hoodedHoodCity.GetComponent<CityNPC>();
			previousRotation = hoodedHood.gameObject.transform.localEulerAngles;
			hoodedHood.transform.DOMove(hoodedHood.transform.position + new Vector3(0f, 0f, -0.2f), 0.3f);
		}
		CityNPC mrChibiNpc = SpawnMrChibiFinal(previousRotation);
		if (mrChibiNpc != null)
		{
			yield return new WaitForEndOfFrame();
			if (mrChibiNpc.missionIndicator != null)
			{
				mrChibiNpc.missionIndicator.SetActive(value: false);
			}
			yield return new WaitForEndOfFrame();
			if (mrChibiNpc.missionIndicator != null)
			{
				mrChibiNpc.missionIndicator.SetActive(value: false);
			}
			mrChibiNpc.AnimController.gameObject.SetActive(value: true);
			mrChibiNpc.AnimController.PlayFromFrame("transition", 0, clearQueue: true);
			mrChibiNpc.AnimController.AddToQueue("animation_0");
		}
		yield return new WaitForSeconds(0.6f);
		if (hoodedHood != null)
		{
			previousRotation = hoodedHood.gameObject.transform.localEulerAngles;
			hoodedHood.RemoveAnimationControllerFromDayNight();
			Object.Destroy(CityController.instance.hoodedHoodCity);
		}
		Player.instance.Save();
		yield return CityController.instance.PlayDialogue("CONVO_SLADE_AFTERTOURNEY2");
		GameUI.instance.HideCinemaBars(0.8f);
		if (mrChibiNpc != null && mrChibiNpc.missionIndicator != null)
		{
			mrChibiNpc.missionIndicator.SetActive(value: true);
		}
		yield return new WaitForSeconds(2f);
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.ShowTitleScreen();
		}
		CityController.instance.cityCharacter.AllowInput = true;
		yield return null;
	}

	private CityNPC SpawnMrChibiFinal(Vector3 rotation)
	{
		if (mrChibiNPC == null)
		{
			NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.MrChibi);
			Vector3 position = CityController.instance.hoodedHoodEncounters.finalTournament.Hoodedhood.transform.position;
			for (int i = 0; i < nPCDefByType.spawnSetups.Count; i++)
			{
				NPCSpawnPointSetup nPCSpawnPointSetup = nPCDefByType.spawnSetups[i];
				if (nPCSpawnPointSetup.name.Equals("Mr. Chibi After Game End"))
				{
					SpawnPointNPCSetup spawnPointNPCSetup = new SpawnPointNPCSetup();
					spawnPointNPCSetup.npc = nPCDefByType;
					spawnPointNPCSetup.position = position;
					spawnPointNPCSetup.npcSpawnPointSetup = nPCSpawnPointSetup;
					mrChibiNPC = new GameObject(nPCDefByType.name);
					mrChibiNPC.transform.position = position;
					if (!rotation.Equals(Vector3.zero))
					{
						mrChibiNPC.transform.localEulerAngles = rotation;
					}
					CityNPC cityNPC = mrChibiNPC.AddComponent<CityNPC>();
					cityNPC.character = nPCDefByType.characterType;
					cityNPC.npcData.Add(nPCSpawnPointSetup.npcData);
					return cityNPC;
				}
			}
		}
		return null;
	}

	private void Update()
	{
		if (!showingFinalTournament)
		{
			return;
		}
		if (cityController.cityCharacter.CurrentNode != null && (cityController.cityCharacter.CurrentNode == entranceNode || Vector3.Distance(cityController.cityCharacter.CurrentNode.gameObject.transform.position, entranceNode.gameObject.transform.position) < 0.5f))
		{
			if (!targetOffsetDirty)
			{
				targetOffsetDirty = true;
				cityController.pinchZoomer.targetOffset = new Vector3(1f, 0f, -6f);
				cityController.pinchZoomer.zoomFactor = 4f;
			}
		}
		else if (targetOffsetDirty)
		{
			targetOffsetDirty = false;
			cityController.pinchZoomer.targetOffset = Vector3.zero;
			cityController.pinchZoomer.zoomFactor = originalZoomFactor;
		}
	}

	public void FireFinalTournamnentIntro()
	{
		LoadFinalTournamentGroup();
		SaveUtility.SetInt("FinalTournamnentUnlocked", 1);
		finalTournamentGroup.SetActive(value: true);
		animator.Play("finalTourny_intro", 0, 0f);
	}

	private void ShowFinalTournament()
	{
		LoadFinalTournamentGroup();
		finalTournamentGroup.SetActive(value: true);
		animator.Play("finalTourny_idle");
	}

	private void LoadFinalTournamentGroup()
	{
		if (finalTournamentGroup == null)
		{
			finalTournamentGroup = Object.Instantiate(Resources.Load("Prefabs/City/finalTournyShip") as GameObject);
			Vector3 localPosition = finalTournamentGroup.transform.localPosition;
			finalTournamentGroup.transform.parent = transform;
			finalTournamentGroup.transform.localPosition = localPosition;
			finalTournamentGroup.transform.localEulerAngles = Vector3.zero;
			animator = finalTournamentGroup.GetComponentInChildren<Animator>(includeInactive: true);
			showingFinalTournament = true;
		}
	}
}
