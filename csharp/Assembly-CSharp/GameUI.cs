using System;
using DG.Tweening;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public static GameUI instance;

	public GameObject magnifyingGlass;

	public GameObject titanTowerButton;

	public GameObject wallet;

	public GameObject mySquad;

	public GameObject communicator;

	public GameObject pauseButton;

	public MissionObjectGUI missionObjectGUI;

	public GameObject missionObject;

	public Animator missionObjectAnimator;

	public tk2dSprite missionObjectSprite;

	public Transform missionFlagIcon;

	public Camera missionRevealCamera;

	public Camera guiCamera2;

	public CinemaBars cinemaBarTop;

	public CinemaBars cinemaBarBottom;

	public tk2dSprite blackness;

	public bool showingCinemaBars;

	public tk2dSprite robinCharacterSelectSprite;

	public tk2dSprite beastBoyCharacterSelectSprite;

	public SimpleSpriteButton magnifyingGlassButton { get; private set; }

	public event Action CommunicatorEntered;

	private void Awake()
	{
		instance = this;
		if (missionObjectGUI == null && missionObject != null)
		{
			missionObjectGUI = missionObject.GetComponent<MissionObjectGUI>();
		}
		magnifyingGlassButton = magnifyingGlass.GetComponent<SimpleSpriteButton>();
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public void ShowCinemaBars(float t)
	{
		cinemaBarTop.ShowCinemaBars(t);
		cinemaBarBottom.ShowCinemaBars(t);
		showingCinemaBars = true;
	}

	public void HideCinemaBars(float t)
	{
		cinemaBarTop.HideCinemaBars(t);
		cinemaBarBottom.HideCinemaBars(t);
		showingCinemaBars = false;
	}

	public void FadeInBlackness()
	{
		blackness.gameObject.GetComponent<MeshRenderer>().enabled = true;
		DOTween.ToAlpha(() => blackness.color, (Color x) =>
		{
			blackness.color = x;
		}, 1f, 0.3f).SetEase(Ease.InCubic);
	}

	public void FadeOutBlackness()
	{
		blackness.gameObject.GetComponent<MeshRenderer>().enabled = true;
		DOTween.ToAlpha(() => blackness.color, (Color x) =>
		{
			blackness.color = x;
		}, 0f, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
		{
			blackness.gameObject.GetComponent<MeshRenderer>().enabled = false;
		});
	}

	public void HideAll(bool animate = true)
	{
		magnifyingGlass.SetActive(value: false);
		titanTowerButton.SetActive(value: false);
		wallet.SetActive(value: false);
		HideMySquad();
		Communicator.instance.SetCommunicatorShown(show: false, animate);
	}

	public void HideAllButCommunicator()
	{
		magnifyingGlass.SetActive(value: false);
		titanTowerButton.SetActive(value: false);
		wallet.SetActive(value: false);
		HideMySquad();
		Communicator.instance.SetCommunicatorShown(show: true);
	}

	private void OnCommunicatorEntered()
	{
		if (CommunicatorEntered != null)
		{
			CommunicatorEntered();
		}
	}

	public void HideMySquad()
	{
		if (mySquad != null)
		{
			mySquad.transform.DOKill();
			mySquad.transform.localPosition = new Vector3(mySquad.transform.localPosition.x, -500f, 0f);
		}
	}

	public void ShowMySquad()
	{
		if (mySquad != null)
		{
			mySquad.transform.DOKill();
			mySquad.transform.localPosition = new Vector3(mySquad.transform.localPosition.x, -1f, 0f);
		}
	}

	public void ShowAll()
	{
		magnifyingGlass.SetActive(value: true);
		titanTowerButton.SetActive(value: true);
		wallet.SetActive(value: true);
		ShowMySquad();
		Communicator.instance.SetCommunicatorShown(show: true);
	}

	public void ShowOnlyMapButton()
	{
		magnifyingGlass.SetActive(value: true);
		magnifyingGlass.transform.position = titanTowerButton.transform.position;
		titanTowerButton.SetActive(value: false);
		wallet.SetActive(value: true);
		ShowMySquad();
		Communicator.instance.SetCommunicatorShown(show: true);
	}

	public void HideAllLandmarksButShops()
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null && landmarkIcon.landmark != null && landmarkIcon.landmark.gameObject.GetComponent<CityShop>() == null)
			{
				landmarkIcon.transform.parent.gameObject.SetActive(value: false);
			}
		}
	}

	public void HideAllLandmarksBut(GameObject objectToIgnore)
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null && landmarkIcon.landmark != null)
			{
				if (landmarkIcon.landmark.gameObject != objectToIgnore)
				{
					landmarkIcon.transform.parent.gameObject.SetActive(value: false);
				}
				else
				{
					landmarkIcon.transform.parent.gameObject.SetActive(value: true);
				}
			}
		}
	}

	public void EnableAllLandmarksTunnels()
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null && landmarkIcon.landmark != null && landmarkIcon.landmark.gameObject.GetComponent<Tunnel>() != null)
			{
				landmarkIcon.transform.parent.gameObject.SetActive(value: true);
			}
		}
	}

	public void DisableAllLandmarks()
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null)
			{
				landmarkIcon.boxCollider.enabled = false;
			}
		}
	}

	public void DisableAllLandmarkCollidersExcept(LandmarkIcon objectToIgnore)
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null)
			{
				if (objectToIgnore != landmarkIcon)
				{
					landmarkIcon.boxCollider.enabled = false;
				}
				else
				{
					landmarkIcon.boxCollider.enabled = true;
				}
			}
		}
	}

	public void EnableAllLandmarkColliders()
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (landmarkIcon != null)
			{
				landmarkIcon.boxCollider.enabled = true;
			}
		}
	}

	public void ShowAllLandmarks()
	{
		for (int i = 0; i < CityController.instance.landmarkIcons.Count; i++)
		{
			LandmarkIcon landmarkIcon = CityController.instance.landmarkIcons[i];
			if (!(landmarkIcon != null) || !(landmarkIcon.landmark != null))
			{
				continue;
			}
			if (landmarkIcon.IsManualWayPoint)
			{
				if (landmarkIcon.isSelectedLandmarkWayPoint)
				{
					landmarkIcon.transform.parent.gameObject.SetActive(value: true);
				}
			}
			else
			{
				landmarkIcon.transform.parent.gameObject.SetActive(value: true);
			}
		}
	}
}
