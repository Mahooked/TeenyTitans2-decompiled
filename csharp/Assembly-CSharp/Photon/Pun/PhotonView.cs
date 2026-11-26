using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Photon.Pun;

[AddComponentMenu("Photon Networking/Photon View &v")]
public class PhotonView : MonoBehaviour
{
	[NonSerialized]
	private int ownerId;

	[FormerlySerializedAs("group")]
	public byte Group;

	protected internal bool mixedModeIsReliable;

	[NonSerialized]
	public bool OwnershipWasTransfered;

	[FormerlySerializedAs("prefixBackup")]
	public int prefixField = -1;

	internal object[] instantiationDataField;

	protected internal object[] lastOnSerializeDataSent;

	protected internal object[] lastOnSerializeDataReceived;

	[FormerlySerializedAs("synchronization")]
	public ViewSynchronization Synchronization;

	[FormerlySerializedAs("ownershipTransfer")]
	public OwnershipOption OwnershipTransfer;

	public List<Component> ObservedComponents;

	[SerializeField]
	private int viewIdField;

	[FormerlySerializedAs("instantiationId")]
	public int InstantiationId;

	protected internal bool didAwake;

	[SerializeField]
	protected internal bool isRuntimeInstantiated;

	protected internal bool removedFromLocalViewList;

	internal MonoBehaviour[] RpcMonoBehaviours;

	public int Prefix
	{
		get
		{
			if (prefixField == -1 && PhotonNetwork.NetworkingClient != null)
			{
				prefixField = PhotonNetwork.currentLevelPrefix;
			}
			return prefixField;
		}
		set
		{
			prefixField = value;
		}
	}

	public object[] InstantiationData
	{
		get
		{
			if (!didAwake)
			{
				Debug.LogError("PhotonNetwork.FetchInstantiationData() was removed. Can only return this.instantiationDataField.");
			}
			return instantiationDataField;
		}
		set
		{
			instantiationDataField = value;
		}
	}

	public int ViewID
	{
		get
		{
			return viewIdField;
		}
		set
		{
			bool flag = didAwake && viewIdField == 0 && value != 0;
			viewIdField = value;
			ownerId = value / PhotonNetwork.MAX_VIEW_IDS;
			if (flag)
			{
				PhotonNetwork.RegisterPhotonView(this);
			}
		}
	}

	public bool IsSceneView => CreatorActorNr == 0;

	public Photon.Realtime.Player Owner => (PhotonNetwork.CurrentRoom != null) ? PhotonNetwork.CurrentRoom.GetPlayer(OwnerActorNr) : null;

	public int OwnerActorNr
	{
		get
		{
			return (!didAwake) ? (ViewID / PhotonNetwork.MAX_VIEW_IDS) : ownerId;
		}
		protected internal set
		{
			ownerId = value;
		}
	}

	public Photon.Realtime.Player Controller
	{
		get
		{
			if (PhotonNetwork.CurrentRoom == null)
			{
				return PhotonNetwork.LocalPlayer;
			}
			if (!IsOwnerActive)
			{
				return PhotonNetwork.MasterClient;
			}
			return Owner;
		}
	}

	public int ControllerActorNr
	{
		get
		{
			int result;
			if (IsOwnerActive)
			{
				result = OwnerActorNr;
			}
			else
			{
				result = ((PhotonNetwork.MasterClient == null) ? (-1) : PhotonNetwork.MasterClient.ActorNumber);
			}
			return result;
		}
	}

	public bool IsOwnerActive => Owner != null && !Owner.IsInactive;

	public int CreatorActorNr => viewIdField / PhotonNetwork.MAX_VIEW_IDS;

	public bool IsMine => OwnerActorNr == PhotonNetwork.LocalPlayer.ActorNumber || (PhotonNetwork.IsMasterClient && !IsOwnerActive);

	protected internal void Awake()
	{
		if (ViewID != 0)
		{
			ownerId = ViewID / PhotonNetwork.MAX_VIEW_IDS;
			PhotonNetwork.RegisterPhotonView(this);
		}
		didAwake = true;
	}

	protected internal void OnDestroy()
	{
		if (!removedFromLocalViewList)
		{
			bool flag = PhotonNetwork.LocalCleanPhotonView(this);
			bool flag2 = false;
			if (flag && !flag2 && InstantiationId > 0 && !PhotonHandler.AppQuits && PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				Debug.Log("PUN-instantiated '" + gameObject.name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy().");
			}
		}
	}

	public void RequestOwnership()
	{
		PhotonNetwork.RequestOwnership(ViewID, ownerId);
	}

	public void TransferOwnership(Photon.Realtime.Player newOwner)
	{
		TransferOwnership(newOwner.ActorNumber);
	}

	public void TransferOwnership(int newOwnerId)
	{
		PhotonNetwork.TransferOwnership(ViewID, newOwnerId);
		ownerId = newOwnerId;
	}

	public void SerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (ObservedComponents != null && ObservedComponents.Count > 0)
		{
			for (int i = 0; i < ObservedComponents.Count; i++)
			{
				SerializeComponent(ObservedComponents[i], stream, info);
			}
		}
	}

	public void DeserializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (ObservedComponents != null && ObservedComponents.Count > 0)
		{
			for (int i = 0; i < ObservedComponents.Count; i++)
			{
				DeserializeComponent(ObservedComponents[i], stream, info);
			}
		}
	}

	protected internal void DeserializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component is IPunObservable punObservable)
		{
			punObservable.OnPhotonSerializeView(stream, info);
			return;
		}
		Debug.LogError(string.Concat("Observed scripts have to implement IPunObservable. ", component, " does not. It is Type: ", component.GetType()), component.gameObject);
	}

	protected internal void SerializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component is IPunObservable punObservable)
		{
			punObservable.OnPhotonSerializeView(stream, info);
			return;
		}
		Debug.LogError(string.Concat("Observed scripts have to implement IPunObservable. ", component, " does not. It is Type: ", component.GetType()), component.gameObject);
	}

	public void RefreshRpcMonoBehaviourCache()
	{
		RpcMonoBehaviours = GetComponents<MonoBehaviour>();
	}

	public void RPC(string methodName, RpcTarget target, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, encrypt: false, parameters);
	}

	public void RpcSecure(string methodName, RpcTarget target, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
	}

	public void RPC(string methodName, Photon.Realtime.Player targetPlayer, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, encrpyt: false, parameters);
	}

	public void RpcSecure(string methodName, Photon.Realtime.Player targetPlayer, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
	}

	public static PhotonView Get(Component component)
	{
		return component.GetComponent<PhotonView>();
	}

	public static PhotonView Get(GameObject gameObj)
	{
		return gameObj.GetComponent<PhotonView>();
	}

	public static PhotonView Find(int viewID)
	{
		return PhotonNetwork.GetPhotonView(viewID);
	}

	public override string ToString()
	{
		return string.Format("View {0}{3} on {1} {2}", ViewID, (!(gameObject != null)) ? "GO==null" : gameObject.name, (!IsSceneView) ? string.Empty : "(scene)", (Prefix <= 0) ? string.Empty : ("lvl" + Prefix));
	}
}
