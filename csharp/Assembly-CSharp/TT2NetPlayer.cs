using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Grumpyface;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TT2NetPlayer : MonoBehaviourPunCallbacks
{
	public delegate void ProcessActionConfirmation(TT2NetPlayerAction action);

	public bool battleReady;

	public bool battleLoaded;

	public bool IsPlayingAction { get; private set; }

	public List<TT2NetPlayerAction> QueuedActions { get; private set; }

	public Photon.Realtime.Player Owner => photonView.Owner;

	public string PlayerName => (!(photonView == null)) ? photonView.Owner.NickName : "Player 1";

	public string PortraitSpriteName
	{
		get
		{
			object value = null;
			if (photonView.Owner.CustomProperties.TryGetValue("PortraitSpriteName", out value))
			{
				return (string)value;
			}
			return string.Empty;
		}
	}

	public int Rank
	{
		get
		{
			object value = null;
			if (Owner.CustomProperties.TryGetValue("Rank", out value))
			{
				return (int)value;
			}
			return 1;
		}
	}

	public int SkillRating
	{
		get
		{
			object value = null;
			if (Owner.CustomProperties.TryGetValue("SkillRating", out value))
			{
				return (int)value;
			}
			return 1000;
		}
	}

	public int AccessoryUid
	{
		get
		{
			object value = null;
			if (Owner.CustomProperties.TryGetValue("AccessoryUid", out value))
			{
				return (int)value;
			}
			return -1;
		}
	}

	public List<Character> Figures { get; private set; }

	public List<Character> Team { get; private set; }

	public int ID
	{
		get
		{
			if (photonView == null || photonView.Owner == null)
			{
				return -1;
			}
			return photonView.Owner.ActorNumber;
		}
	}

	public bool IsMine => photonView.IsMine;

	public bool IsMasterClient => photonView.Owner.IsMasterClient;

	public event Action Ready;

	private void Awake()
	{
		Figures = new List<Character>();
		Team = new List<Character>();
		QueuedActions = new List<TT2NetPlayerAction>();
		if (IsMine)
		{
			Debug.LogFormat("SET THIS PLAYER'S, {0}, CUSTOM PROPERTIES", PlayerName);
			Hashtable hashtable = new Hashtable();
			hashtable["PortraitSpriteName"] = Player.instance.pvpData.PortraitSpriteName;
			hashtable["Rank"] = Player.instance.pvpData.level;
			hashtable["SkillRating"] = Player.instance.pvpData.skillRating;
			hashtable["Party"] = GetSerializedPlayerParty(Player.instance);
			PhotonNetwork.SetPlayerCustomProperties(hashtable);
		}
		else
		{
			ProcessCustomProperties(Owner);
		}
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	public override void OnEnable()
	{
		base.OnEnable();
		gameObject.name = Owner.NickName;
		TT2NetManager.Instance.AddNetworkPlayer(this);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		TT2NetManager.Instance.RemoveNetworkPlayer(this);
	}

	private void OnDestroy()
	{
		if (IsMine)
		{
			ResolveQueuedActions();
		}
	}

	public void ResolveQueuedActions()
	{
		while (QueuedActions.Count > 0)
		{
			TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
			tT2NetPlayerAction.isConfirmed = true;
			tT2NetPlayerAction.isWaiting = false;
		}
	}

	private byte[] GetSerializedPlayerParty(Player player)
	{
		List<TT2NetFigureData> list = new List<TT2NetFigureData>();
		for (int i = 0; i < player.activeParty.Count; i++)
		{
			list.Add(new TT2NetFigureData(player.activeParty[i]));
		}
		return SerializationUtility.SerializeToBytes(list);
	}

	public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player target, Hashtable changedProps)
	{
		Debug.Log("Player Properties Update for: " + target.NickName);
		ProcessCustomProperties(target, changedProps);
	}

	private void ProcessCustomProperties(Photon.Realtime.Player player, Hashtable props = null)
	{
		if (player.IsLocal || Owner != player)
		{
			return;
		}
		Debug.Log("[TT2NetPlayer::ProcessCustomProperties] Player: " + player.NickName);
		if (props == null)
		{
			props = player.CustomProperties;
		}
		if (props.ContainsKey("Party"))
		{
			List<TT2NetFigureData> list = (List<TT2NetFigureData>)SerializationUtility.DeserializeFromBytes((byte[])props["Party"]);
			if (list != null)
			{
				Debug.Log("[TT2NetPlayer::ProcessCustomProperties] Log Figure Data");
				Figures.Clear();
				for (int i = 0; i < list.Count; i++)
				{
					Figures.Add(new Character(i, list[i]));
					Debug.Log(Figures[i].characterDefinition.characterName);
				}
			}
		}
		if (!props.ContainsKey("Team"))
		{
			return;
		}
		List<int> list2 = (List<int>)SerializationUtility.DeserializeFromBytes((byte[])props["Team"]);
		if (list2 != null)
		{
			Debug.Log("[TT2NetPlayer::ProcessCustomProperties] Log Team Data");
			Team.Clear();
			for (int j = 0; j < list2.Count; j++)
			{
				Team.Add(Figures[list2[j]]);
				Debug.Log(Team[j].characterDefinition.characterName);
			}
		}
	}

	public void SendPrepareForBattle(int initiatorPlayerId, int arenaUid)
	{
		TT2NetManager.Log("SendPrepareForBattle");
		photonView.RPC("PrepareForBattle", RpcTarget.Others, initiatorPlayerId, arenaUid);
	}

	public void SendAcceptedRequest(int initiatorPlayerID, int otherPlayerID)
	{
		TT2NetManager.Log("SendAcceptedRequest initiatorPlayerID:" + initiatorPlayerID + " otherPlayerID " + otherPlayerID);
		photonView.RPC("AcceptedRequest", RpcTarget.Others, initiatorPlayerID, otherPlayerID);
	}

	public void SendCancelRequest(int initiatorPlayerID, int otherPlayerID)
	{
		photonView.RPC("CancelRequest", RpcTarget.Others, initiatorPlayerID, otherPlayerID);
	}

	public void SendBattleRequest(int initiatorPlayerID, int otherPlayerID)
	{
		Debug.Log("SendBattleRequest initiatorPlayerID" + initiatorPlayerID + " otherPlayerID " + otherPlayerID);
		photonView.RPC("BattleRequested", RpcTarget.All, initiatorPlayerID, otherPlayerID);
	}

	public void SendUpdateBattleReady(bool ready)
	{
		photonView.RPC("UpdateBattleReady", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, ready);
	}

	public void SendUpdateBattleLoaded(bool loaded)
	{
		photonView.RPC("UpdateBattleLoaded", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, loaded);
	}

	public TT2NetPlayerAction SendBattleDecision(int abilityUid, int characterUid, bool isCast, float meterProgress, float finalDamageMultiplier, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.Ability);
		tT2NetPlayerAction.abilityUid = abilityUid;
		tT2NetPlayerAction.characterUid = characterUid;
		tT2NetPlayerAction.isCast = isCast;
		tT2NetPlayerAction.meterProgress = meterProgress;
		tT2NetPlayerAction.finalDamageMultiplier = finalDamageMultiplier;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("BattleDecision", RpcTarget.All, abilityUid, characterUid, isCast, meterProgress, finalDamageMultiplier, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendBattleDecisionConfirm(int abilityUid, int characterUid, bool isCast, float meterProgress, float finalDamageMultiplier, int seed, bool isConfirmed)
	{
		photonView.RPC("BattleDecisionConfirm", RpcTarget.AllViaServer, abilityUid, characterUid, isCast, meterProgress, finalDamageMultiplier, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendCharacterSwap(int characterUid)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.CharacterSwap);
		tT2NetPlayerAction.characterUid = characterUid;
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("CharacterSwap", RpcTarget.All, characterUid);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendCharacterSwapConfirm(int characterUid, bool isConfirmed)
	{
		photonView.RPC("CharacterSwapConfirm", RpcTarget.AllViaServer, characterUid, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public void SendTofuSpawned()
	{
		photonView.RPC("TofuSpawned", RpcTarget.Others);
	}

	public void SendTofuCollected()
	{
		photonView.RPC("TofuCollected", RpcTarget.Others);
	}

	public TT2NetPlayerAction SendTofuUsed(int tofuType, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.Tofu);
		tT2NetPlayerAction.tofuType = tofuType;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("TofuUsed", RpcTarget.All, tofuType, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendTofuUsedConfirm(int tofuType, int seed, bool isConfirmed)
	{
		photonView.RPC("TofuUsedConfirm", RpcTarget.AllViaServer, tofuType, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendStatusEffectTick(int effectUid, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.StatusEffectTick);
		tT2NetPlayerAction.effectUid = effectUid;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("StatusEffectTick", RpcTarget.All, effectUid, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendStatusEffectTickConfirm(int effectUid, int seed, bool isConfirmed)
	{
		photonView.RPC("StatusEffectTickConfirm", RpcTarget.AllViaServer, effectUid, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendStatusEffectExpire(int effectUid, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.StatusEffectExpire);
		tT2NetPlayerAction.effectUid = effectUid;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("StatusEffectExpire", RpcTarget.All, effectUid, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendStatusEffectExpireConfirm(int effectUid, int seed, bool isConfirmed)
	{
		photonView.RPC("StatusEffectExpireConfirm", RpcTarget.AllViaServer, effectUid, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendChannelingTick(int characterUid, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.ChannelingTick);
		tT2NetPlayerAction.characterUid = characterUid;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("ChannelingTick", RpcTarget.All, characterUid, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendChannelingTickConfirm(int characterUid, int seed, bool isConfirmed)
	{
		photonView.RPC("ChannelingTickConfirm", RpcTarget.AllViaServer, characterUid, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendChannelingNextStep(int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.ChannelingNextStep);
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("ChannelingNextStep", RpcTarget.All, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendChannelingNextStepConfirm(int seed, bool isConfirmed)
	{
		photonView.RPC("ChannelingNextStepConfirm", RpcTarget.AllViaServer, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendChannelingExpire(int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.ChannelingExpire);
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("ChannelingExpire", RpcTarget.All, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendChannelingExpireConfirm(int seed, bool isConfirmed)
	{
		photonView.RPC("ChannelingExpireConfirm", RpcTarget.AllViaServer, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendAccessoryUsed(float curChargeHealth, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.AccessoryUsed);
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("AccessoryUsed", RpcTarget.All, curChargeHealth, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendAccessoryUsedConfirm(float curChargeHealth, int seed, bool isConfirmed)
	{
		photonView.RPC("AccessoryUsedConfirm", RpcTarget.AllViaServer, curChargeHealth, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendAccessoryTick(int effectIndex, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.AccessoryTick);
		tT2NetPlayerAction.effectIndex = effectIndex;
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("AccessoryTick", RpcTarget.All, effectIndex, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendAccessoryTickConfirm(int effectIndex, int seed, bool isConfirmed)
	{
		photonView.RPC("AccessoryTickConfirm", RpcTarget.AllViaServer, effectIndex, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendAccessoryExpire(int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.AccessoryExpire);
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("AccessoryExpire", RpcTarget.All, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendAccessoryExpireConfirm(int seed, bool isConfirmed)
	{
		photonView.RPC("AccessoryExpireConfirm", RpcTarget.AllViaServer, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public TT2NetPlayerAction SendModChipTriggerTick(int characterUid, int triggerType, int seed)
	{
		TT2NetPlayerAction tT2NetPlayerAction = new TT2NetPlayerAction(TT2NetPlayerAction.ActionType.ModChipTriggerTick);
		tT2NetPlayerAction.seed = ((seed != -1) ? seed : 0);
		QueuedActions.Add(tT2NetPlayerAction);
		photonView.RPC("ModChipTriggerTick", RpcTarget.All, characterUid, triggerType, seed);
		PhotonNetwork.SendAllOutgoingCommands();
		return tT2NetPlayerAction;
	}

	public void SendModChipTriggerTickConfirm(int characterUid, int triggerType, int seed, bool isConfirmed)
	{
		photonView.RPC("ModChipTriggerTickConfirm", RpcTarget.AllViaServer, characterUid, triggerType, seed, isConfirmed);
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public void SendBattlePickupCollected(int pickupType)
	{
		photonView.RPC("BattlePickupCollected", RpcTarget.Others, pickupType);
	}

	public void SendEmoteSent(EmoteType emoteType)
	{
		photonView.RPC("EmoteSent", RpcTarget.Others, (int)emoteType);
	}

	public void SendPlayingAction(bool isPlaying, RpcTarget target)
	{
		photonView.RPC("PlayingAction", target, isPlaying);
	}

	[PunRPC]
	private void PrepareForBattle(int initiatorID, int arenaUid)
	{
		TT2NetManager.Instance.PrepareForBattle(initiatorID, arenaUid);
	}

	[PunRPC]
	private void BattleRequested(int initiatorID, int otherPlayerID)
	{
		TT2NetManager.Log("Received Battle Requested From Initiator ID: " + initiatorID + " Other Player ID:" + otherPlayerID);
		TT2NetManager.Instance.AddPendingBattleRequest(initiatorID, otherPlayerID);
	}

	[PunRPC]
	private void AcceptedRequest(int initiatorID, int otherPlayerID)
	{
		TT2NetManager.Log("Received Accepted Request Initiator ID: " + initiatorID + " Other Player ID: " + otherPlayerID);
		if (!IsMine)
		{
			TT2NetManager.Instance.AcceptBattleRequest(initiatorID, otherPlayerID);
		}
	}

	[PunRPC]
	private void CancelRequest(int initiatorID, int otherPlayerID)
	{
		TT2NetManager.Log("Received Cancel Request Initiator ID: " + initiatorID + " Other Player ID: " + otherPlayerID);
		TT2NetManager.Instance.CancelPendingBattleRequest(initiatorID, otherPlayerID);
	}

	[PunRPC]
	private void UpdateBattleReady(int senderID, bool isReady)
	{
		TT2NetManager.Log("Received Update Battle Ready Sender ID: " + senderID + " is Ready: " + isReady);
		if (photonView.Owner.ActorNumber == senderID)
		{
			battleReady = isReady;
			if (Ready != null)
			{
				Ready();
			}
			TT2NetManager.Log("Did Update Ready " + isReady + " For ID: " + photonView.Owner.ActorNumber);
		}
	}

	[PunRPC]
	private void UpdateBattleLoaded(int senderID, bool isLoaded)
	{
		TT2NetManager.Log("Received Update Battle Loaded Sender ID: " + senderID + " is Loaded: " + isLoaded);
		if (photonView.Owner.ActorNumber == senderID)
		{
			battleLoaded = isLoaded;
			TT2NetManager.Log("Did Update Loaded " + isLoaded + " For ID: " + photonView.Owner.ActorNumber);
		}
	}

	[PunRPC]
	private void BattleDecision(int abilityUid, int characterUid, bool isCast, float meterProgress, float finalDamageMultiplier, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Battle Decision | Ability UID: {abilityUid}    Character UID: {characterUid}    Is Cast: {isCast}    Meter Progress: {meterProgress}    Final Damage Modifier: {finalDamageMultiplier}    Seed: {seed}    Timestamp: {info.timestamp}    TT2NetPlayer: {name}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		if (flag)
		{
			BattleController.instance.ForceStartWaiting();
		}
		SendBattleDecisionConfirm(abilityUid, characterUid, isCast, meterProgress, finalDamageMultiplier, seed, flag);
	}

	[PunRPC]
	private void BattleDecisionConfirm(int abilityUid, int characterUid, bool isCast, float meterProgress, float finalDamageMultiplier, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Battle Decision Confirm | Ability UID: {abilityUid}    Character UID: {characterUid}    Is Cast: {isCast}    Meter Progress: {meterProgress}    Final Damage Modifier: {finalDamageMultiplier}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.ForceEndWaiting();
				PlayerAI_Network component = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI_Network>();
				component.NextAbilitySeed = seed;
				BattleController.instance.enemyActionMeter.progress = meterProgress;
				if (BattleController.instance.playerPortraits.IsSelectMode)
				{
					BattleController.instance.playerPortraits.ExitSelectMode();
				}
				if (component != null)
				{
					component.MakeDecision(abilityUid, isCast, finalDamageMultiplier);
				}
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void CharacterSwap(int characterUid, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Character Swap Character UID: {characterUid}     Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		if (flag)
		{
			BattleController.instance.ForceStartWaiting();
		}
		SendCharacterSwapConfirm(characterUid, flag);
	}

	[PunRPC]
	private void CharacterSwapConfirm(int characterUid, bool isConfirmed, PhotonMessageInfo info)
	{
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.ForceEndWaiting();
				if (BattleController.instance.playerPortraits.IsSelectMode)
				{
					BattleController.instance.playerPortraits.ExitSelectMode();
				}
				PlayerAI_Network component = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI_Network>();
				if (component != null)
				{
					component.SwapCharacter(characterUid);
				}
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void TofuSpawned()
	{
		if (BattleController.instance != null && BattleController.instance.enemyActionMeter != null)
		{
			TT2NetManager.Log("Tofu Spawned for player: " + PlayerName);
			Vector3 position = BattleController.instance.guiCamera.ViewportToWorldPoint(new Vector3(0.35f, 0.65f));
			BattleObjectManager.instance.SpawnTofuAt(position, isPlayerTofu: false);
		}
		else
		{
			TT2NetManager.Log("Failed Spawning Tofu for player: " + PlayerName);
		}
	}

	[PunRPC]
	private void TofuCollected()
	{
		List<TofuPickup> allObjects = BattleObjectManager.instance.GetAllObjects<TofuPickup>();
		for (int i = 0; i < allObjects.Count; i++)
		{
			if (!allObjects[i].isPlayerTofu)
			{
				allObjects[i].ForceCollect();
				break;
			}
		}
	}

	[PunRPC]
	private void TofuUsed(int tofuType, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Tofu Used: {(TofuType)tofuType}     Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendTofuUsedConfirm(tofuType, seed, flag);
	}

	[PunRPC]
	private void TofuUsedConfirm(int tofuType, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				PlayerAI_Network component = BattleController.instance.enemyActionMeter.GetComponent<PlayerAI_Network>();
				if (component != null)
				{
					component.TofuUsed(tofuType, seed);
				}
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void StatusEffectTick(int effectUid, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Status Effect Tick | Effect UID: {effectUid}    Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendStatusEffectTickConfirm(effectUid, seed, flag);
	}

	[PunRPC]
	private void StatusEffectTickConfirm(int effectUid, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Status Effect Tick Confirm | Effect UID: {effectUid}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				StatusEffect statusEffectByUid = StatusEffectManager.instance.GetStatusEffectByUid(effectUid);
				if (statusEffectByUid != null)
				{
					UnityEngine.Random.State state = UnityEngine.Random.state;
					UnityEngine.Random.InitState(seed);
					statusEffectByUid.Tick();
					UnityEngine.Random.state = state;
				}
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void StatusEffectExpire(int effectUid, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Status Effect Expire | Effect UID: {effectUid}    Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendStatusEffectExpireConfirm(effectUid, seed, flag);
	}

	[PunRPC]
	private void StatusEffectExpireConfirm(int effectUid, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Status Effect Expire Confirm | Effect UID: {effectUid}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				StatusEffect statusEffectByUid = StatusEffectManager.instance.GetStatusEffectByUid(effectUid);
				if (statusEffectByUid != null)
				{
					UnityEngine.Random.State state = UnityEngine.Random.state;
					UnityEngine.Random.InitState(seed);
					statusEffectByUid.Expire();
					UnityEngine.Random.state = state;
				}
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void ChannelingTick(int characterUid, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Channeling Tick: Character UID: {characterUid}    Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendChannelingTickConfirm(characterUid, seed, flag);
	}

	[PunRPC]
	private void ChannelingTickConfirm(int characterUid, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Channeling Tick Confirm | Character UID: {characterUid}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyActionMeter.ChannelingTick(seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void ChannelingNextStep(int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Channeling Next Step: Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendChannelingNextStepConfirm(seed, flag);
	}

	[PunRPC]
	private void ChannelingNextStepConfirm(int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyActionMeter.ChannelingNextStep(seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void ChannelingExpire(int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Channeling Expire: Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendChannelingExpireConfirm(seed, flag);
	}

	[PunRPC]
	private void ChannelingExpireConfirm(int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyActionMeter.ChannelingExpire(seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void AccessoryUsed(float curChargeHealth, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Accessory Used: Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendAccessoryUsedConfirm(curChargeHealth, seed, flag);
	}

	[PunRPC]
	private void AccessoryUsedConfirm(float curChargeHealth, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyAccessoryButton.UpdateChargeHealth(curChargeHealth);
				BattleController.instance.enemyAccessoryButton.ForceActivateAccessory();
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void AccessoryTick(int effectIndex, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Accessory Tick | Effect Index: {effectIndex}    Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendAccessoryTickConfirm(effectIndex, seed, flag);
	}

	[PunRPC]
	private void AccessoryTickConfirm(int effectIndex, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Accessory Tick Confirm | Effect Index: {effectIndex}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyAccessoryButton.accessory.Tick(effectIndex, seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void AccessoryExpire(int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Accessory Expire | Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		if (TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
		{
			flag = false;
		}
		SendAccessoryExpireConfirm(seed, flag);
	}

	[PunRPC]
	private void AccessoryExpireConfirm(int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Accessory Expire Confirm | Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyAccessoryButton.Expire(seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void ModChipTriggerTick(int characterUid, int triggerType, int seed, PhotonMessageInfo info)
	{
		if (info.Sender.IsLocal)
		{
			TT2NetPlayerAction lastItem = QueuedActions.GetLastItem();
			lastItem.timestamp = info.timestamp;
			return;
		}
		TT2NetManager.Log($"Mod Chip Trigger Tick | Character UID: {characterUid}    Trigger Type: {(ModChipEffectTriggerType)triggerType}    Seed: {seed}    Timestamp: {info.timestamp}");
		bool flag = true;
		if (TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count > 0)
		{
			for (int i = 0; i < TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count; i++)
			{
				TT2NetPlayerAction tT2NetPlayerAction = TT2NetManager.Instance.myNetworkPlayer.QueuedActions[i];
				if (!(tT2NetPlayerAction.timestamp <= info.timestamp))
				{
					break;
				}
				switch (tT2NetPlayerAction.type)
				{
				case TT2NetPlayerAction.ActionType.Ability:
					if (!tT2NetPlayerAction.isCast)
					{
						flag = false;
					}
					break;
				case TT2NetPlayerAction.ActionType.CharacterSwap:
					flag = false;
					break;
				default:
					flag = false;
					break;
				case TT2NetPlayerAction.ActionType.Tofu:
				case TT2NetPlayerAction.ActionType.ModChipTriggerTick:
					break;
				}
				if (!flag)
				{
					break;
				}
			}
		}
		SendModChipTriggerTickConfirm(characterUid, triggerType, seed, flag);
	}

	[PunRPC]
	private void ModChipTriggerTickConfirm(int characterUid, int triggerType, int seed, bool isConfirmed, PhotonMessageInfo info)
	{
		TT2NetManager.Log($"Mod Chip Trigger Tick Confirm | Character UID: {characterUid}    Trigger Type: {(ModChipEffectTriggerType)triggerType}    Seed: {seed}    Is Confirmed: {isConfirmed}    Timestamp: {info.timestamp}");
		if (isConfirmed)
		{
			if (info.Sender.IsLocal)
			{
				BattleController.instance.enemyCharacter.character.modChipSlot.DoTriggerTick((ModChipEffectTriggerType)triggerType, seed);
			}
			else
			{
				TT2NetPlayerAction tT2NetPlayerAction = QueuedActions.Shift();
				tT2NetPlayerAction.isConfirmed = true;
				tT2NetPlayerAction.isWaiting = false;
			}
			TT2NetManager.Instance.LastSeed = seed;
		}
		else if (!info.Sender.IsLocal)
		{
			TT2NetPlayerAction tT2NetPlayerAction2 = QueuedActions.Shift();
			tT2NetPlayerAction2.isConfirmed = false;
			tT2NetPlayerAction2.isWaiting = false;
		}
	}

	[PunRPC]
	private void BattlePickupCollected(int type)
	{
		Type o = null;
		switch (type)
		{
		case 2:
			o = typeof(BatteryPickup);
			break;
		case 0:
			o = typeof(FirePickup);
			break;
		case 1:
			o = typeof(HeartPickup);
			break;
		}
		List<BattlePickup> allObjects = BattleObjectManager.instance.GetAllObjects<BattlePickup>();
		for (int i = 0; i < allObjects.Count; i++)
		{
			if (!allObjects[i].isPlayerPickup && allObjects[i].GetType().Equals(o))
			{
				allObjects[i].ForceCollect();
				break;
			}
		}
	}

	[PunRPC]
	private void EmoteSent(int type)
	{
		BattleController.instance.enemyEmoteUI.Emote((EmoteType)type);
	}

	[PunRPC]
	private void PlayingAction(bool isPlaying, PhotonMessageInfo info)
	{
		if (info.Sender == Owner)
		{
			IsPlayingAction = isPlaying;
		}
	}
}
