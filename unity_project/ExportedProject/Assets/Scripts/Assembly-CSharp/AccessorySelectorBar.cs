using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AccessorySelectorBar : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action BarOpened__BackingField;

	public GameObject accessoryOffsetGroup;

	public GameObject accessoryShelfGroup;

	public Transform offScreenPoint;

	public tk2dBaseSprite[] backgroundSprites;

	public tk2dSprite accessorySelectedSprite;

	public GameObject[] goButtonGroup;

	public AudioClip pressButtonAudio;

	public AudioClip cancelButtonAudio;

	public AccessoryButton[] accessoryButtons;

	public AccessoryInfoBox accessoryInfoBox;

	public tk2dUIItem barOpenBtn;

	public tk2dUIItem clickOutBox;

	public GameObject notifier;

	public TextMeshPro tournyRuleText;

	public Vector3 regularResolutionLocalPos;

	public Vector3 tallResolutionLocalPos;

	public Tutorial secondPrebattleTutorial;

	private bool isOpen;

	private bool inTournament;

	private bool accessoriesAvailable;

	private Sequence notifierShakeSequence;

	private AccessoryItemDefinition previousAccessoryItemDefinition;

	private bool _allowNonEquipExit = true;

	public bool allowNonEquipExit
	{
		get
		{
			return _allowNonEquipExit;
		}
		set
		{
			_allowNonEquipExit = value;
		}
	}

	public bool isEnabled { get; set; }

	public event Action BarOpened
	{
		add
		{
			Action action = BarOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BarOpened__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = BarOpened__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BarOpened__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private IEnumerator Start()
	{
		if (Player.instance.accessories.Count == 0 || !secondPrebattleTutorial.IsComplete())
		{
			base.gameObject.SetActive(false);
			yield break;
		}
		barOpenBtn.OnClick += HandleAccessoryShelfOpenPressed;
		accessoryInfoBox.gameObject.SetActive(false);
		PreBattleController.Instance.preBattleParty.CharacterSelected += HandleCharacterSelected;
		PreBattleController.Instance.preBattleParty.DragStarted += HandleCharacterDragStarted;
		accessoryOffsetGroup.transform.localPosition = ((!((float)Screen.height / (float)Screen.width > 1.34f)) ? regularResolutionLocalPos : tallResolutionLocalPos);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < accessoryButtons.Length; i++)
		{
			AccessoryButton accessoryButton = accessoryButtons[i];
			if (Player.instance.activeAccessories.Count > i)
			{
				accessoryButton.Init(Player.instance.activeAccessories[i]);
			}
			else
			{
				accessoryButton.Init(-1);
			}
			accessoryButton.Clicked += HandleAccessoryPressed;
			accessoryButton.LongPressed += HandleAccessoryLongPressed;
		}
		if (PreBattleController.battleDefinition.type == BattleType.Tournament && TournamentTracker.isTournamentActive)
		{
			for (int j = 0; j < TournamentTracker.usedAccessories.Count; j++)
			{
				for (int k = 0; k < accessoryButtons.Length; k++)
				{
					AccessoryButton accessoryButton2 = accessoryButtons[k];
					if (accessoryButton2.accessoryItemDefinition.uid == TournamentTracker.usedAccessories[j])
					{
						accessoryButton2.DisableButton();
						break;
					}
				}
			}
			inTournament = true;
		}
		else
		{
			inTournament = false;
		}
		Vector3 hidePosition = new Vector3(GUICamera.instance.anchorLowerLeft.position.x - offScreenPoint.localPosition.x, accessoryShelfGroup.transform.position.y, accessoryShelfGroup.transform.position.z);
		accessoryShelfGroup.transform.position = hidePosition;
		accessoriesAvailable = false;
		for (int l = 0; l < accessoryButtons.Length; l++)
		{
			if (accessoryButtons[l].isEnabled && accessoryButtons[l].accessoryItemDefinition != null)
			{
				accessoriesAvailable = true;
				break;
			}
		}
		if (accessoriesAvailable)
		{
			notifier.gameObject.SetActive(true);
			notifierShakeSequence = DOTween.Sequence();
			notifierShakeSequence.Append(notifier.transform.DOShakeRotation(0.25f, new Vector3(0f, 0f, 67f), 50, 45f));
			notifierShakeSequence.AppendInterval(0.75f);
			notifierShakeSequence.SetLoops(-1, LoopType.Restart);
		}
		else
		{
			notifier.gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < accessoryButtons.Length; i++)
		{
			AccessoryButton accessoryButton = accessoryButtons[i];
			accessoryButton.Clicked -= HandleAccessoryPressed;
			accessoryButton.LongPressed -= HandleAccessoryLongPressed;
		}
		if (PreBattleController.Instance != null)
		{
			PreBattleController.Instance.preBattleParty.CharacterSelected += HandleCharacterSelected;
			PreBattleController.Instance.preBattleParty.DragStarted += HandleCharacterDragStarted;
		}
	}

	public void Enable()
	{
		for (int i = 0; i < accessoryButtons.Length; i++)
		{
			accessoryButtons[i].EnableButton();
		}
		for (int j = 0; j < backgroundSprites.Length; j++)
		{
			backgroundSprites[j].color = Color.white;
		}
		barOpenBtn.GetComponent<tk2dSprite>().color = Color.white;
		barOpenBtn.enabled = true;
		if (accessoriesAvailable)
		{
			notifier.gameObject.SetActive(true);
			notifierShakeSequence = DOTween.Sequence();
			notifierShakeSequence.Append(notifier.transform.DOShakeRotation(0.25f, new Vector3(0f, 0f, 67f), 50, 45f));
			notifierShakeSequence.AppendInterval(0.75f);
			notifierShakeSequence.SetLoops(-1, LoopType.Restart);
		}
		isEnabled = true;
	}

	public void Disable()
	{
		for (int i = 0; i < accessoryButtons.Length; i++)
		{
			accessoryButtons[i].DisableButton();
		}
		for (int j = 0; j < backgroundSprites.Length; j++)
		{
			backgroundSprites[j].color = Color.grey;
		}
		barOpenBtn.GetComponent<tk2dSprite>().color = Color.grey;
		barOpenBtn.enabled = false;
		notifier.SetActive(false);
		isEnabled = false;
	}

	public void MoveOut()
	{
		Vector3 endValue = new Vector3(GUICamera.instance.anchorLowerLeft.position.x - 600f, accessoryShelfGroup.transform.position.y, accessoryShelfGroup.transform.position.z);
		accessoryShelfGroup.transform.DOMove(endValue, 0.33f);
	}

	private void MoveToHidePosition()
	{
		if (isOpen)
		{
			isOpen = false;
			accessoryShelfGroup.transform.DOKill();
			Vector3 endValue = new Vector3(GUICamera.instance.anchorLowerLeft.position.x - offScreenPoint.localPosition.x, accessoryShelfGroup.transform.position.y, accessoryShelfGroup.transform.position.z);
			accessoryShelfGroup.transform.DOMove(endValue, 0.33f).OnComplete(HideComplete);
			GFAudioManager.instance.PlayClip(cancelButtonAudio, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			accessoryInfoBox.gameObject.SetActive(false);
			PreBattleController.Instance.handheldConsole.SetAccessoryRuleTextActive(false);
		}
	}

	private void HideComplete()
	{
		for (int i = 0; i < goButtonGroup.Length; i++)
		{
			goButtonGroup[i].SetActive(true);
		}
	}

	private void ClearAccessory()
	{
		if (_allowNonEquipExit)
		{
			accessorySelectedSprite.gameObject.SetActive(false);
			PreBattleController.battleDefinition.accessoryUID = -1;
			MoveToHidePosition();
		}
	}

	public void HandleAccessoryShelfOpenPressed()
	{
		if (!isOpen)
		{
			isOpen = true;
			accessoryShelfGroup.transform.DOKill();
			accessoryShelfGroup.transform.DOLocalMove(new Vector3(-31.5f, 0f), 0.33f).SetEase(Ease.OutBack, 0.85f).OnComplete(() =>
			{
				if (TournamentTracker.activeTournament != null)
				{
					PreBattleController.Instance.handheldConsole.SetAccessoryRuleTextActive(true);
				}
				if (BarOpened__BackingField != null)
				{
					BarOpened__BackingField();
				}
			});
			for (int num = 0; num < goButtonGroup.Length; num++)
			{
				goButtonGroup[num].SetActive(false);
			}
			GFAudioManager.instance.PlayClip(pressButtonAudio, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			if (notifierShakeSequence != null)
			{
				notifierShakeSequence.Kill(true);
			}
			notifier.gameObject.SetActive(false);
		}
		else if (_allowNonEquipExit)
		{
			MoveToHidePosition();
		}
	}

	private void HandleAccessoryPressed(AccessoryButton accessoryButton)
	{
		MoveToHidePosition();
		accessoryInfoBox.gameObject.SetActive(false);
		if (accessoryButton != null && previousAccessoryItemDefinition != accessoryButton.accessoryItemDefinition && accessoryButton.accessoryItemDefinition != null)
		{
			accessorySelectedSprite.gameObject.SetActive(true);
			accessorySelectedSprite.SetSprite(accessoryButton.accessoryItemDefinition.iconSprite);
			previousAccessoryItemDefinition = accessoryButton.accessoryItemDefinition;
			PreBattleController.battleDefinition.accessoryUID = previousAccessoryItemDefinition.uid;
			EventMessenger.Instance.Raise(new AccessoryEquippedEvent(PreBattleController.battleDefinition.accessoryUID));
		}
		else if (_allowNonEquipExit)
		{
			accessorySelectedSprite.gameObject.SetActive(false);
			previousAccessoryItemDefinition = null;
			PreBattleController.battleDefinition.accessoryUID = -1;
			EventMessenger.Instance.Raise(new AccessoryEquippedEvent(-1));
		}
	}

	private void HandleAccessoryLongPressed(AccessoryButton accessoryButton)
	{
		UnityEngine.Debug.Log("Handle Accessory Long Pressed...");
		if (!(accessoryButton.accessoryItemDefinition == null))
		{
			accessoryInfoBox.gameObject.SetActive(true);
			accessoryInfoBox.transform.position = accessoryButton.transform.position;
			accessoryInfoBox.transform.localPosition += new Vector3(0f, 0f, -200f);
			accessoryInfoBox.Init(accessoryButton.accessoryItemDefinition);
			clickOutBox.OnClick += HandleClickOutBoxClicked;
		}
	}

	private void HandleClickOutBoxClicked()
	{
		UnityEngine.Debug.Log("Handle Click Out Box Clicked");
		clickOutBox.OnClick -= HandleClickOutBoxClicked;
		accessoryInfoBox.gameObject.SetActive(false);
	}

	private void HandleCharacterSelected(Character character)
	{
		if (_allowNonEquipExit)
		{
			MoveToHidePosition();
		}
	}

	private void HandleCharacterDragStarted()
	{
		if (_allowNonEquipExit)
		{
			MoveToHidePosition();
		}
	}
}
