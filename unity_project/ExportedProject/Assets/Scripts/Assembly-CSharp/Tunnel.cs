using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
	public Animator animator;

	public PathNode entranceNode;

	public tk2dUIItem uiItem;

	public Landmark landmark;

	public bool startUnlocked;

	public string regularLandmarkIcon;

	public string zoomedOutLandmarkIcon;

	public int missionUIDToStartUnlocked = -1;

	private tk2dUIItem landmarkUIItem;

	private bool originTunnel;

	private GameObject warpText;

	private static bool warpTutorialShown;

	private TutorialPopUp tutorialPopUp;

	[SerializeField]
	private string guid;

	[SerializeField]
	private int instanceID = -1;

	private bool unlocked;

	private static bool inTunnelViewMode;

	public string GUID
	{
		get
		{
			return guid;
		}
		set
		{
			guid = value;
		}
	}

	public int InstanceID
	{
		get
		{
			return instanceID;
		}
		set
		{
			instanceID = value;
		}
	}

	public bool Unlocked
	{
		get
		{
			return unlocked;
		}
	}

	public static bool InTunnelViewMode
	{
		get
		{
			return inTunnelViewMode;
		}
	}

	private void OnEnable()
	{
		uiItem.OnClick += HandleTunnelClicked;
		if (landmarkUIItem != null)
		{
			landmarkUIItem.OnClick += HandleTunnelLandmarkClicked;
		}
	}

	private void OnDisable()
	{
		uiItem.OnClick -= HandleTunnelClicked;
		if (landmarkUIItem != null)
		{
			landmarkUIItem.OnClick -= HandleTunnelLandmarkClicked;
		}
	}

	private void OnDestroy()
	{
		if (CityController.instance != null)
		{
			CityController.instance.pinchZoomer.ZoomedOut -= OnZoomedOut;
			CityController.instance.pinchZoomer.ZoomedIn -= OnZoomedIn;
		}
		inTunnelViewMode = false;
	}

	private IEnumerator Start()
	{
		originTunnel = false;
		unlocked = Player.instance.HasUnlockedTunnel(GUID);
		if (startUnlocked && Player.instance.GetMissionCompletedData(missionUIDToStartUnlocked) != null)
		{
			unlocked = true;
			Player.instance.AddUnlockedTunnel(GUID);
		}
		if (unlocked)
		{
			animator.Play("open idle");
		}
		if (entranceNode == null)
		{
			List<PathNode> list = new List<PathNode>(Object.FindObjectsOfType<PathNode>());
			PathNode pathNode = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < list.Count; i++)
			{
				PathNode pathNode2 = list[i];
				float num2 = Vector3.Distance(pathNode2.transform.position, base.transform.position);
				if (num2 < num)
				{
					num = num2;
					pathNode = pathNode2;
				}
			}
			if (pathNode != null)
			{
				entranceNode = pathNode;
			}
		}
		yield return null;
		landmark.icon.sprite.SortingOrder = 1;
		landmark.icon.tailSprite.SortingOrder = 1;
		landmarkUIItem = landmark.icon.uiItem;
		landmarkUIItem.OnClick += HandleTunnelLandmarkClicked;
		warpText = ObjectPool.GetObjectFromResource("Prefabs/GUI/WarpText");
		warpText.transform.parent = landmark.icon.transform;
		warpText.transform.localPosition = new Vector3(-0.2f, -18.8f);
		warpText.transform.localScale = Vector3.one;
		warpText.SetActive(false);
		if (CityController.instance != null)
		{
			CityController.instance.pinchZoomer.ZoomedOut += OnZoomedOut;
			CityController.instance.pinchZoomer.ZoomedIn += OnZoomedIn;
		}
	}

	public void ForceUnlock()
	{
		if (!unlocked)
		{
			animator.Play("open idle");
		}
		if (!Player.instance.HasUnlockedTunnel(GUID))
		{
			Player.instance.AddUnlockedTunnel(GUID);
			Player.instance.Save();
		}
		unlocked = true;
	}

	private void HandleTunnelClicked()
	{
		if (!FlashSaleController.instance.FlashSaleActive && !FlashSaleController.instance.IntroSequenceActive && !CityController.instance.pinchZoomer.isZoomedOut && CityController.instance.cityCharacter.AllowInput)
		{
			if (CityController.instance.cityCharacter.CurrentNode == entranceNode)
			{
				InteractWithTunnel();
				return;
			}
			CityController.instance.cityCharacter.MoveToInteractiveNode(entranceNode, InteractWithTunnel);
			CityController.instance.pinchZoomer.ZoomIn();
		}
	}

	private void HandleTunnelLandmarkClicked()
	{
		if (CityController.instance.pinchZoomer.isZoomedOut && unlocked && CityController.instance.zoomType == ZoomType.Tunnel)
		{
			if (!originTunnel)
			{
				StartCoroutine("MoveCharacterToTunnel");
			}
			else
			{
				StartCoroutine(DelayedZoomIn());
			}
		}
	}

	private void InteractWithTunnel()
	{
		if (unlocked)
		{
			EnterTunnel();
		}
		else
		{
			StartCoroutine(UnlockAndEnterTunnel());
		}
	}

	private IEnumerator MoveCharacterToTunnel()
	{
		StartCoroutine(DelayedZoomIn());
		CityController.instance.pinchZoomer.ZoomedInMoveFinished += OnZoomInFinished;
		CityController.instance.cityCharacter.SetPosition(entranceNode);
		CityController.instance.cameraHolder.transform.position = entranceNode.transform.position;
		CityController.instance.UpdateCityLocation();
		CityController.instance.PlayCurrentLocationMusic(true);
		animator.Play("slide open", 0, 0.9f);
		yield return null;
	}

	private IEnumerator DelayedZoomIn()
	{
		yield return new WaitForEndOfFrame();
		CityController.instance.pinchZoomer.ZoomIn();
	}

	private void EnterTunnel(bool playOpenSFX = true)
	{
		unlocked = true;
		if (!Player.instance.HasUnlockedTunnel(GUID))
		{
			Player.instance.AddUnlockedTunnel(GUID);
			Player.instance.Save();
		}
		if (Player.instance.GetUnlockedTunnelCount() < 2)
		{
			if (tutorialPopUp == null)
			{
				tutorialPopUp = Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_PopUp") as GameObject).GetComponent<TutorialPopUp>();
			}
			if (!tutorialPopUp.IsActive)
			{
				tutorialPopUp.transform.localScale = Vector3.one;
				tutorialPopUp.SetUpPopUp(Language.Get("UNLOCKANOTHERMANHOLE", "MENUS,GUI"), TutorialPopUp.TailType.BottomRight, base.transform, CityController.instance.pinchZoomer.thisCamera, true, 3f);
			}
		}
		else
		{
			StartCoroutine(EnterTunnelSequence(playOpenSFX));
		}
	}

	private IEnumerator EnterTunnelSequence(bool playOpenSFX = true)
	{
		animator.Play("slide open");
		CityController.instance.cityCharacter.StopMove();
		if (playOpenSFX)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_manhole_opennormal", GFAudioManager.SFX_GROUP, string.Empty);
		}
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		originTunnel = true;
		inTunnelViewMode = true;
		CityController.instance.zoomType = ZoomType.Tunnel;
		CityController.instance.pinchZoomer.ZoomOut();
		CityController.instance.pinchZoomer.ZoomedOutMoveFinished += OnZoomOutFinished;
		CityController.instance.cityCharacter.StopMove();
	}

	private IEnumerator UnlockAndEnterTunnel()
	{
		CityController.instance.cityCharacter.StopMove();
		animator.Play("open");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_manhole_open", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		EnterTunnel();
	}

	private void OnZoomOutFinished()
	{
		CityController.instance.pinchZoomer.ZoomedOutMoveFinished -= OnZoomOutFinished;
		if (!warpTutorialShown)
		{
			if (tutorialPopUp == null)
			{
				tutorialPopUp = Object.Instantiate(Resources.Load("Prefabs/GUI/Tutorial/Tutorial_PopUp") as GameObject).GetComponent<TutorialPopUp>();
			}
			if (!tutorialPopUp.IsActive)
			{
				tutorialPopUp.transform.localScale = Vector3.one * 0.8f;
				tutorialPopUp.SetUpPopUp(Language.Get("ANYMANHOLE", "MENUS,GUI"), TutorialPopUp.TailType.BottomRight, base.transform, CityController.instance.pinchZoomer.thisCamera, true, 2f, new Vector3(-0.3f, 0f, 0.45f));
			}
		}
	}

	private void OnZoomInFinished()
	{
		CityController.instance.pinchZoomer.ZoomedInMoveFinished -= OnZoomInFinished;
		animator.Play("slide close");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_manhole_opennormal", GFAudioManager.SFX_GROUP, string.Empty);
		CityController.instance.zoomType = ZoomType.Normal;
	}

	private void OnZoomedOut()
	{
		if (landmark != null)
		{
			if (unlocked && inTunnelViewMode)
			{
				landmark.Show(zoomedOutLandmarkIcon);
				warpText.gameObject.SetActive(true);
				uiItem.enabled = true;
			}
			else
			{
				landmark.Hide();
				warpText.gameObject.SetActive(false);
				uiItem.enabled = false;
			}
		}
	}

	private void OnZoomedIn()
	{
		if (!CityController.instance.exitingCityScene)
		{
			if (originTunnel)
			{
				animator.Play("slide close");
			}
			if (CityController.instance.zoomType == ZoomType.Tunnel)
			{
				CityController.instance.zoomType = ZoomType.Normal;
			}
			warpText.gameObject.SetActive(false);
			landmark.Show(regularLandmarkIcon);
			originTunnel = false;
			uiItem.enabled = true;
			inTunnelViewMode = false;
		}
	}
}
