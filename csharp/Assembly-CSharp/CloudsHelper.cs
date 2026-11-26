using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CloudsHelper : MonoBehaviour
{
	public CityLocation district;

	public GameObject cloudGroup;

	public GameObject tournamentBuilding;

	public Transform districtCenter;

	public List<CloudsAnimationAdapter> cloudsAnimationAdapaters;

	public string cloudOutroName;

	private bool animatingCloudOutro;

	private bool unlockedDistrict;

	private void Awake()
	{
		cloudGroup.SetActive(value: true);
		cloudsAnimationAdapaters = new List<CloudsAnimationAdapter>();
		tk2dAnimationAdapter[] componentsInChildren = cloudGroup.transform.GetComponentsInChildren<tk2dAnimationAdapter>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			cloudsAnimationAdapaters.Add(new CloudsAnimationAdapter(componentsInChildren[i], null));
		}
	}

	private void Start()
	{
		CameraPinchZoomer pinchZoomer = CityController.instance.pinchZoomer;
		pinchZoomer.ZoomedIn += HandleZoomedIn;
		pinchZoomer.ZoomedOut += HandleZoomedOut;
		cloudGroup.SetActive(value: false);
	}

	private void OnDestroy()
	{
		if (CityController.instance != null && CityController.instance.pinchZoomer != null)
		{
			CameraPinchZoomer pinchZoomer = CityController.instance.pinchZoomer;
			pinchZoomer.ZoomedIn -= HandleZoomedIn;
			pinchZoomer.ZoomedOut -= HandleZoomedOut;
		}
	}

	private IEnumerator StartCloudAnimationOutro()
	{
		animatingCloudOutro = true;
		CityController.instance.cityCharacter.AllowInput = false;
		CityController.instance.PreventManualWayPoint(prevent: true);
		yield return new WaitForSeconds(0.5f);
		if (DialogueTree.instance != null)
		{
			while (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		Communicator communicator = CityController.instance.communicator;
		while (communicator.MissionScrollerActive())
		{
			yield return new WaitForEndOfFrame();
		}
		if (CityController.instance.pinchZoomer.isZoomedOut)
		{
			CityController.instance.magnifyingGlassObj.GetComponent<tk2dUIItem>().SimulateClick();
		}
		while (CityController.instance.pinchZoomer.isZoomedOut)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		GameUI.instance.ShowCinemaBars(0.8f);
		yield return new WaitForSeconds(0.8f);
		CityController.instance.communicator.pauseTime = true;
		CityController.instance.magnifyingGlassObj.GetComponent<tk2dUIItem>().SimulateClick();
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = false;
		CityController.instance.pinchZoomer.allowPinchZoom = false;
		CityController.instance.pinchZoomer.allowPan = false;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = false;
		GameUI.instance.HideAll();
		GameUI.instance.HideAllLandmarksBut(null);
		GameUI.instance.communicator.SetActive(value: false);
		yield return new WaitForSeconds(0.6f);
		cloudGroup.SetActive(value: true);
		CityController.instance.cameraHolder.transform.DOMove(districtCenter.position, 2f);
		for (int i = 0; i < cloudsAnimationAdapaters.Count; i++)
		{
			CloudsAnimationAdapter cloud = cloudsAnimationAdapaters[i];
			cloud.animationAdapter.enabled = false;
			if (cloud.animationAdapter.sprite != null)
			{
				if (cloud.tweener != null)
				{
					cloud.tweener.Kill();
				}
				cloud.tweener = DOTween.ToAlpha(() => cloud.animationAdapter.sprite.color, (Color x) =>
				{
					cloud.animationAdapter.sprite.color = x;
				}, 1f, 0.1f);
			}
		}
		yield return new WaitForSeconds(2f);
		for (int num = 0; num < cloudsAnimationAdapaters.Count; num++)
		{
			cloudsAnimationAdapaters[num].animationAdapter.enabled = true;
		}
		Animator[] animators = cloudGroup.transform.GetComponentsInChildren<Animator>(includeInactive: true);
		for (int num2 = 0; num2 < animators.Length; num2++)
		{
			animators[num2].Play(cloudOutroName);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_cloudclear", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(1f);
		if (tournamentBuilding != null && tournamentBuilding.GetComponent<Landmark>() != null && tournamentBuilding.GetComponent<Landmark>().icon != null)
		{
			tournamentBuilding.GetComponent<Landmark>().icon.tailSprite.color = new Color(1f, 1f, 1f, 1f);
			tournamentBuilding.GetComponent<Landmark>().icon.sprite.color = new Color(1f, 1f, 1f, 1f);
			tournamentBuilding.GetComponent<Landmark>().icon.sprite.gameObject.SetActive(value: true);
			tournamentBuilding.GetComponent<Landmark>().icon.gameObject.SetActive(value: true);
			tournamentBuilding.GetComponent<Landmark>().icon.transform.localScale = new Vector3(0f, 0f, 1f);
			tournamentBuilding.GetComponent<Landmark>().icon.transform.parent.gameObject.SetActive(value: true);
		}
		yield return new WaitForSeconds(1.5f);
		CityController.instance.cameraHolder.transform.DOMove(CityController.instance.cityCharacter.transform.position, 1f);
		yield return new WaitForSeconds(0.8f);
		cloudGroup.SetActive(value: false);
		animatingCloudOutro = false;
		CityController.instance.pinchZoomer.allowPinchZoom = true;
		CityController.instance.pinchZoomer.allowPan = true;
		CityController.instance.pinchZoomer.allowDoubleClickZoomIn = true;
		CityController.instance.communicator.pauseTime = false;
		CityController.instance.magnifyingGlassObj.GetComponent<BoxCollider2D>().enabled = true;
		CityController.instance.magnifyingGlassObj.GetComponent<tk2dUIItem>().SimulateClick();
		yield return new WaitForSeconds(0.6f);
		if (district == CityLocation.Beach)
		{
			yield return new WaitForSeconds(0.1f);
			yield return CityController.instance.PlayDialogue("CONVO_HOOD_AFTER2NDTOURNY1");
		}
		CityController.instance.cityCharacter.AllowInput = true;
		CityController.instance.PreventManualWayPoint(prevent: false);
		GameUI.instance.ShowAllLandmarks();
		GameUI.instance.ShowAll();
		GameUI.instance.communicator.SetActive(value: true);
	}

	private void HandleZoomedIn()
	{
		for (int i = 0; i < cloudsAnimationAdapaters.Count; i++)
		{
			CloudsAnimationAdapter cloud = cloudsAnimationAdapaters[i];
			cloud.animationAdapter.enabled = false;
			if (!(cloud.animationAdapter.sprite != null))
			{
				continue;
			}
			if (cloud.tweener != null)
			{
				cloud.tweener.Kill();
			}
			if (i == 0)
			{
				cloud.tweener = DOTween.ToAlpha(() => cloud.animationAdapter.sprite.color, (Color x) =>
				{
					cloud.animationAdapter.sprite.color = x;
				}, 0f, 0.3f).OnComplete(() =>
				{
					cloudGroup.SetActive(value: false);
				});
			}
			else
			{
				cloud.tweener = DOTween.ToAlpha(() => cloud.animationAdapter.sprite.color, (Color x) =>
				{
					cloud.animationAdapter.sprite.color = x;
				}, 0f, 0.3f);
			}
		}
	}

	private void HandleZoomedOut()
	{
		if (animatingCloudOutro)
		{
			return;
		}
		if (!unlockedDistrict)
		{
			cloudGroup.SetActive(value: true);
		}
		for (int i = 0; i < cloudsAnimationAdapaters.Count; i++)
		{
			CloudsAnimationAdapter cloud = cloudsAnimationAdapaters[i];
			cloud.animationAdapter.enabled = false;
			if (cloud.animationAdapter.sprite != null)
			{
				if (cloud.tweener != null)
				{
					cloud.tweener.Kill();
				}
				cloud.tweener = DOTween.ToAlpha(() => cloud.animationAdapter.sprite.color, (Color x) =>
				{
					cloud.animationAdapter.sprite.color = x;
				}, 1f, 0.3f);
			}
		}
	}
}
