using System.Collections;
using Photon.Pun;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
	public string loadSceneName;

	public string startAtNodeNamed;

	public bool startFacingRight;

	public PathNode moveToNode;

	public TournamentDefinition tournamentDef;

	private bool didExit;

	public void LoadScene()
	{
		if (!CityController.instance.exitingCityScene)
		{
			CityController.instance.cityCharacter.StopMove();
			SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.gameObject.transform.position.ToString(), save: false);
			SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, save: false);
			CityController.instance.communicator.SaveDayAndTime();
			Player.instance.IncreaseCurrentTime(20);
			CityController.instance.ExitCityScene();
			if (!string.IsNullOrEmpty(startAtNodeNamed))
			{
				CityController.startFromNodeNamed = startAtNodeNamed;
			}
			CityController.startFacingRight = startFacingRight;
			LoadingScreenCover.LoadScene(loadSceneName);
		}
	}

	public void MoveToPathNode()
	{
		if (moveToNode != null)
		{
			StartCoroutine(StartTransitionRoutine());
		}
	}

	public void LoadTournament()
	{
		if (tournamentDef != null && !didExit)
		{
			didExit = true;
			TournamentController.initDefinition = tournamentDef;
			TournamentController.isDebugTournament = false;
			LoadingScreenCover.LoadScene("Tournament");
		}
	}

	private IEnumerator StartTransitionRoutine()
	{
		yield return new WaitForEndOfFrame();
		GameUI.instance.FadeInBlackness();
		CityController.instance.cityCharacter.StopMove();
		CityController.instance.cityCharacter.PlayIdleAnimation();
		CityController.instance.pinchZoomer.cameraFollow.approach = false;
		yield return new WaitForSeconds(0.8f);
		CityController.instance.cityCharacter.transform.position = moveToNode.transform.position;
		StartCoroutine(WaitToEnableCameraApproach());
		CityController.instance.cityCharacter.UpdateCurrentNode(moveToNode);
		GameUI.instance.FadeOutBlackness();
	}

	private IEnumerator WaitToEnableCameraApproach()
	{
		yield return new WaitForEndOfFrame();
		CityController.instance.pinchZoomer.cameraFollow.approach = true;
	}
}
