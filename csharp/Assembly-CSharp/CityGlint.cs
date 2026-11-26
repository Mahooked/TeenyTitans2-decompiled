using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(tk2dUIItem))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(TransformLink))]
public class CityGlint : MonoBehaviour
{
	public string guid = string.Empty;

	public int instanceID = -1;

	public GlintType type;

	public bool onlyOnce;

	public bool activatedByMission;

	public int activatedByMissionUID = -1;

	public bool activatedByMail;

	public int activatedByMailUID = -1;

	public int activatedByCharacterUID = -1;

	public TimeOfDay spawnDayNight;

	public float spawnChance;

	public int spawnMinDays;

	private tk2dUIItem uiItem;

	private BoxCollider collider;

	public int coinReward;

	public Conversation conversation;

	public List<CharacterRewardData> figureReward = new List<CharacterRewardData>();

	public int modchipUID = -1;

	public int accessoryUID = -1;

	public PlayerReward repaintReward;

	public int missionUID = -1;

	public int invalidAfterCompletedMissionUID = -1;

	public int activeAfterCompletedMissionUID = -1;

	public GameObject glintObject;

	public string eventName;

	public bool eventNameRequiredToEnable;

	public string eventNameToEnable;

	public int eventNameRequiredMinDays;

	public int amount;

	public Animator animator;

	private bool isDayTime;

	private bool isSetup;

	private int currencyIncrement;

	private int leftover;

	private bool handlingGlint;

	private void Awake()
	{
		uiItem = GetComponent<tk2dUIItem>();
		collider = GetComponent<BoxCollider>();
	}

	private void OnEnable()
	{
		uiItem.OnClick += HandleOnClick;
		SetupGlint();
	}

	private void OnDisable()
	{
		uiItem.OnClick -= HandleOnClick;
		BreakdownGlint();
	}

	private void OnDestroy()
	{
		BreakdownGlint();
	}

	private IEnumerator Start()
	{
		if (Application.isPlaying)
		{
			DisableGlint();
			yield return new WaitForEndOfFrame();
			SetupGlint();
		}
	}

	private void SetupGlint()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (!isSetup && CityController.instance != null)
		{
			isSetup = true;
			GetComponent<TransformLink>().LinkTo = CityController.instance.cameraHolder;
			CityController.instance.communicator.StartDayTime += HandleStartDayTime;
			CityController.instance.communicator.StartNightTime += HandleStartNightTime;
			Player.instance.MissionAdded += HandleMissionAdded;
			Player.instance.MissionCompleted += HandleMissionCompleted;
			if (activatedByMail)
			{
				Player.instance.MailViewed += HandleMailViewed;
			}
			if (FlashSaleController.instance != null)
			{
				FlashSaleController.instance.FlashSaleStarted += HandleFlashSaleStarted;
			}
			isDayTime = CityController.instance.communicator.isDayTime;
		}
		if (CityController.instance != null)
		{
			CheckShouldEnable();
		}
	}

	private void BreakdownGlint()
	{
		if (!Application.isPlaying || !isSetup)
		{
			return;
		}
		isSetup = false;
		if (CityController.instance != null)
		{
			CityController.instance.communicator.StartDayTime -= HandleStartDayTime;
			CityController.instance.communicator.StartNightTime -= HandleStartNightTime;
		}
		if (Player.instance != null)
		{
			Player.instance.MissionAdded -= HandleMissionAdded;
			Player.instance.MissionCompleted -= HandleMissionCompleted;
			if (activatedByMail)
			{
				Player.instance.MailViewed -= HandleMailViewed;
			}
		}
		if (FlashSaleController.instance != null)
		{
			FlashSaleController.instance.FlashSaleStarted -= HandleFlashSaleStarted;
		}
	}

	private void DisableGlint(bool lateDisable = false)
	{
		if (!lateDisable)
		{
			if (collider != null)
			{
				collider.enabled = false;
			}
			if (glintObject != null)
			{
				glintObject.SetActive(value: false);
			}
		}
		else
		{
			StartCoroutine(DelayedDisableGlint());
		}
	}

	private IEnumerator DelayedDisableGlint()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		DisableGlint();
	}

	private void EnableGlint()
	{
		if (collider != null)
		{
			collider.enabled = true;
		}
		if (glintObject != null)
		{
			glintObject.SetActive(value: true);
		}
	}

	private void CollectGlint()
	{
		DisableGlint(lateDisable: true);
		Player.instance.CollectGlint(guid, CityController.instance.communicator.GetCurrentDay());
	}

	private void HandleMailViewed(int mailUID)
	{
		CheckShouldEnable();
	}

	private void HandleStartDayTime()
	{
		isDayTime = true;
		CheckShouldEnable();
	}

	private void HandleStartNightTime()
	{
		isDayTime = false;
		CheckShouldEnable();
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		CheckShouldEnable();
	}

	private void HandleMissionCompleted(int missionUID)
	{
		CheckShouldEnable();
	}

	private void HandleFlashSaleStarted()
	{
		CheckShouldEnable();
	}

	private void CheckShouldEnable()
	{
		if (FlashSaleController.instance != null && FlashSaleController.instance.FlashSaleActive)
		{
			DisableGlint();
			return;
		}
		if (type == GlintType.ModChip && modchipUID < 0)
		{
			DisableGlint();
			return;
		}
		if (!onlyOnce && Player.instance.HasCollectedGlintGUID(guid))
		{
			int currentDay = Player.instance.currentDay;
			int collectedGlintGUIDDay = Player.instance.GetCollectedGlintGUIDDay(guid);
			if (currentDay != collectedGlintGUIDDay)
			{
				Player.instance.ClearCollectedGlintGUID(guid);
			}
		}
		if (Player.instance.HasCollectedGlintGUID(guid))
		{
			DisableGlint();
			return;
		}
		if (CityController.instance.communicator.GetCurrentDay() < spawnMinDays)
		{
			DisableGlint();
			return;
		}
		if (activatedByMission)
		{
			if (activatedByMissionUID > -1)
			{
				MissionData missionData = Player.instance.GetMissionData(activatedByMissionUID);
				if (missionData == null || (missionData != null && missionData.collected))
				{
					DisableGlint();
					return;
				}
			}
			if (activeAfterCompletedMissionUID > -1)
			{
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(activeAfterCompletedMissionUID);
				if (missionCompletedData == null)
				{
					DisableGlint();
					return;
				}
			}
			if (invalidAfterCompletedMissionUID > -1)
			{
				MissionCompletedData missionCompletedData2 = Player.instance.GetMissionCompletedData(invalidAfterCompletedMissionUID);
				if (missionCompletedData2 != null)
				{
					DisableGlint();
					return;
				}
			}
		}
		if (activatedByMail)
		{
			MailData mailData = Player.instance.GetMailData(activatedByMailUID);
			if (mailData == null || (mailData != null && !mailData.viewed))
			{
				DisableGlint();
				return;
			}
		}
		if (type == GlintType.RequiresCharacter && activatedByCharacterUID > -1 && !Player.instance.HasCharacter(activatedByCharacterUID))
		{
			DisableGlint();
			return;
		}
		if (spawnDayNight != TimeOfDay.Both)
		{
			if (spawnDayNight == TimeOfDay.Day && !isDayTime)
			{
				DisableGlint();
				return;
			}
			if (spawnDayNight == TimeOfDay.Night && isDayTime)
			{
				DisableGlint();
				return;
			}
		}
		if (type == GlintType.CommunicatorEvent)
		{
			spawnChance = 1f;
		}
		if (eventNameRequiredToEnable)
		{
			int num = SaveUtility.GetInt(eventNameToEnable, -1);
			if (num > -1)
			{
				int currentDay2 = Player.instance.currentDay;
				if (currentDay2 >= num + eventNameRequiredMinDays)
				{
					EnableGlint();
				}
				else
				{
					DisableGlint();
				}
			}
			else
			{
				DisableGlint();
			}
		}
		else if (spawnChance >= 1f)
		{
			EnableGlint();
		}
		else if (Random.value < spawnChance)
		{
			EnableGlint();
		}
	}

	private void HandleOnClick()
	{
		if (!CityController.instance.pinchZoomer.isZoomedOut && !CityReward.instance.showingRewards && (CityController.instance.cityCharacter.AllowInput || !(CoreGameTutorial.instance != null) || CoreGameTutorial.instance.isActive))
		{
			switch (type)
			{
			case GlintType.Coin:
				HandleCoin();
				break;
			case GlintType.Conversation:
				HandleConversation();
				break;
			case GlintType.Figure:
				HandleFigureReward();
				break;
			case GlintType.ModChip:
				HandleModChip();
				break;
			case GlintType.MissionComplete:
				HandleMissionComplete();
				break;
			case GlintType.CommunicatorEvent:
				HandleCommunicatorEvent();
				break;
			case GlintType.MailBoxScam:
				HandleMailBoxScam();
				break;
			case GlintType.RequiresCharacter:
				HandleRequiresCharacter();
				break;
			case GlintType.Accessory:
				HandleAccessory();
				break;
			case GlintType.Repaint:
				HandleRepaint();
				break;
			}
			if (animator != null)
			{
				animator.Play("City_dumpster_tap");
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
	}

	private void HandleCoin()
	{
		CollectGlint();
		StartCoroutine("StartCoinPickupRoutine");
	}

	private IEnumerator StartCoinPickupRoutine()
	{
		WalletGUI walletGUI = CityController.instance.walletGUI;
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_results_coinsadd", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.1f);
		GameObject coinParticle = ObjectPool.GetObjectFromResource("Prefabs/GUI/VictoryCoin_Particle System");
		ParticleSystem coinParticleSystem = coinParticle.GetComponent<ParticleSystem>();
		coinParticleSystem.maxParticles = coinReward;
		Vector3 screenPosition = GUICamera.instance.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(transform.position));
		screenPosition.z = 0f;
		coinParticle.transform.position = screenPosition;
		int count = coinReward;
		currencyIncrement = Mathf.Max(1, Mathf.FloorToInt(count / coinParticleSystem.maxParticles));
		leftover = ((count > coinParticleSystem.maxParticles) ? (count % coinParticleSystem.maxParticles) : 0);
		walletGUI.particleAttractor.AttractParticleSystem(coinParticleSystem);
		walletGUI.particleAttractor.ParticleDestroyed += HandleOnParticleDestroyed;
		while (walletGUI.particleAttractor.numParticleSystems > 0)
		{
			yield return null;
		}
		walletGUI.particleAttractor.ParticleDestroyed -= HandleOnParticleDestroyed;
		CityController.instance.walletGUI.particleAttractor.ParticleDestroyed -= HandleOnParticleDestroyed;
	}

	private void HandleOnParticleDestroyed()
	{
		int num = currencyIncrement;
		if (leftover > 0)
		{
			num++;
			leftover--;
		}
		CityController.instance.walletGUI.AddToWallet(num);
	}

	private void HandleConversation()
	{
		CollectGlint();
		if (conversation != null)
		{
			CityController.instance.HandleShowDialogue(conversation);
		}
	}

	private void HandleFigureReward()
	{
		CollectGlint();
		if (figureReward.Count > 0)
		{
			CityReward.instance.ShowFigureReward(figureReward);
		}
	}

	private void HandleModChip()
	{
		CollectGlint();
		if (modchipUID > -1)
		{
			CityReward.instance.ShowModChipReward(modchipUID);
		}
	}

	private void HandleAccessory()
	{
		CollectGlint();
		if (accessoryUID > -1)
		{
			CityReward.instance.ShowAccessoryReward(accessoryUID);
		}
	}

	private void HandleRepaint()
	{
		CollectGlint();
		if (repaintReward != null && (repaintReward.isRepaintRandom || (repaintReward.repaintUID > -1 && repaintReward.characterUID > -1)))
		{
			PlayerReward playerReward = ((!repaintReward.isRepaintRandom) ? repaintReward : PlayerRewardFactory.GetRandomRepaintReward(repaintReward.isRepaintRare, 0.15f));
			CityReward.instance.ShowRepaintReward(playerReward.characterUID, playerReward.repaintUID);
		}
	}

	private void HandleMissionComplete()
	{
		CollectGlint();
		if (missionUID > -1)
		{
			Player.instance.CompleteMission(missionUID);
		}
	}

	private void HandleCommunicatorEvent()
	{
		CollectGlint();
		if (!string.IsNullOrEmpty(eventName))
		{
			CommunicatorManager.instance.FireAnimationEvent(eventName);
		}
	}

	private void HandleMailBoxScam()
	{
		if (!handlingGlint)
		{
			StartCoroutine("StartMailBoxScam");
		}
	}

	private void HandleRequiresCharacter()
	{
		if (!handlingGlint)
		{
			StartCoroutine("StartRequiresCharacter");
		}
	}

	private IEnumerator StartMailBoxScam()
	{
		handlingGlint = true;
		CityController.instance.cityCharacter.AllowInput = false;
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window_Coins", default, isActive: false);
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.headerText.text = Language.Get("WARNING", "MENUS,GUI");
		dialog.descriptionText.text = Language.Get("SCAMPOPUP", "MENUS,GUI");
		dialog.gameObject.SetActive(value: true);
		dialog.UpdateWithAmount(CurrencyType.Coins, amount);
		if (Player.instance.currency < amount)
		{
			dialog.DisableConfirmButton();
		}
		else
		{
			dialog.EnableConfirmButton();
		}
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			Player.instance.RemoveCurrency(amount);
			CollectGlint();
			if (missionUID > -1)
			{
				Player.instance.CompleteMission(missionUID);
			}
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
		CityController.instance.cityCharacter.AllowInput = true;
		handlingGlint = false;
	}

	private IEnumerator StartRequiresCharacter()
	{
		handlingGlint = true;
		CityController.instance.cityCharacter.AllowInput = false;
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_MissionFigureWindow2D", default, isActive: false);
		dialog.transform.parent = transform;
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.headerText.text = Language.Get("HOLDIT", "MENUS,GUI");
		dialog.descriptionText.text = Language.Get(GameManager.instance.communicatorDatabase.GetMission(missionUID).descriptionKey, "MISSIONS");
		dialog.gameObject.SetActive(value: true);
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			Player.instance.RemoveLowestLevelCharacterWithUID(activatedByCharacterUID);
			CollectGlint();
			if (missionUID > -1)
			{
				Player.instance.CompleteMission(missionUID);
			}
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
		CityController.instance.cityCharacter.AllowInput = true;
		handlingGlint = false;
	}
}
