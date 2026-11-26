using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class TempDialogueSystem : MonoBehaviour, IPoolable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CityNPC> BeginBattle__BackingField;

	public tk2dSprite blackFade;

	public Transform backing;

	public TempDialogueCharacter leftCharacter;

	public TempDialogueCharacter rightCharacter;

	public TempDialogueButton chatButton;

	public TempDialogueButton battleButton;

	public TempDialogueButton tradeButton;

	public tk2dUIItem exitButton;

	public Transform bubble;

	private List<tk2dCameraAnchor> cameraAnchors;

	private Vector3 backingHomePosition;

	private Vector3 leftCharacterHomePosition;

	private Vector3 rightCharacterHomePosition;

	private Vector3 chatButtonHomePosition;

	private Vector3 battleButtonHomePosition;

	private Vector3 tradeButtonHomePosition;

	private Vector3 exitButtonHomePosition;

	private CityNPC _cityNPC;

	public event Action<CityNPC> BeginBattle
	{
		add
		{
			Action<CityNPC> action = BeginBattle__BackingField;
			Action<CityNPC> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BeginBattle__BackingField, (Action<CityNPC>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CityNPC> action = BeginBattle__BackingField;
			Action<CityNPC> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref BeginBattle__BackingField, (Action<CityNPC>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		cameraAnchors = new List<tk2dCameraAnchor>();
		GetComponentsInChildren(cameraAnchors);
		backingHomePosition = backing.localPosition;
		leftCharacterHomePosition = leftCharacter.transform.localPosition;
		rightCharacterHomePosition = rightCharacter.transform.localPosition;
		chatButtonHomePosition = chatButton.transform.localPosition;
		battleButtonHomePosition = battleButton.transform.localPosition;
		tradeButtonHomePosition = tradeButton.transform.localPosition;
		exitButtonHomePosition = exitButton.transform.localPosition;
	}

	private void Start()
	{
		chatButton.Disable();
		tradeButton.Disable();
		if (Player.instance.activeParty.Count >= 3)
		{
			battleButton.Enable();
		}
		else
		{
			battleButton.Disable();
		}
	}

	private void OnEnable()
	{
		chatButton.uiItem.OnClick += HandleOnChatClick;
		battleButton.uiItem.OnClick += HandleOnBattleClick;
		tradeButton.uiItem.OnClick += HandleOnTradeClick;
		exitButton.OnClick += HandleOnExitClick;
		StartCoroutine(EntranceSequence());
	}

	private void OnDisable()
	{
		chatButton.uiItem.OnClick -= HandleOnChatClick;
		battleButton.uiItem.OnClick -= HandleOnBattleClick;
		tradeButton.uiItem.OnClick -= HandleOnTradeClick;
		exitButton.OnClick -= HandleOnExitClick;
		backing.localPosition = backingHomePosition;
		leftCharacter.transform.localPosition = leftCharacterHomePosition;
		rightCharacter.transform.localPosition = rightCharacterHomePosition;
		chatButton.transform.localPosition = chatButtonHomePosition;
		battleButton.transform.localPosition = battleButtonHomePosition;
		tradeButton.transform.localPosition = tradeButtonHomePosition;
		exitButton.transform.localPosition = exitButtonHomePosition;
	}

	private IEnumerator EntranceSequence()
	{
		exitButton.enabled = false;
		battleButton.uiItem.enabled = false;
		exitButton.enabled = true;
		battleButton.uiItem.enabled = Player.instance.activeParty.Count >= 3;
		yield return new WaitForEndOfFrame();
	}

	public void Init(Camera camera, CityNPC cityNPC)
	{
		int count = cameraAnchors.Count;
		for (int i = 0; i < count; i++)
		{
			cameraAnchors[i].AnchorCamera = camera;
		}
		_cityNPC = cityNPC;
	}

	private void HandleOnChatClick()
	{
		UnityEngine.Debug.Log("CHAT");
	}

	private void HandleOnBattleClick()
	{
		UnityEngine.Debug.Log("BATTLE");
		if (BeginBattle__BackingField != null)
		{
			BeginBattle__BackingField(_cityNPC);
		}
	}

	private void HandleOnTradeClick()
	{
		UnityEngine.Debug.Log("TRADE");
	}

	private void HandleOnExitClick()
	{
		ObjectPool.PoolObject(base.gameObject);
	}

	public void ResetObject()
	{
	}
}
