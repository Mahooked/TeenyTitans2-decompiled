using UnityEngine;

namespace Photon.Pun;

[AddComponentMenu("Photon Networking/Photon Transform View")]
[RequireComponent(typeof(PhotonView))]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
	private float m_Distance;

	private float m_Angle;

	private PhotonView m_PhotonView;

	private Vector3 m_Direction;

	private Vector3 m_NetworkPosition;

	private Vector3 m_StoredPosition;

	private Quaternion m_NetworkRotation;

	public bool m_SynchronizePosition = true;

	public bool m_SynchronizeRotation = true;

	public bool m_SynchronizeScale;

	public void Awake()
	{
		m_PhotonView = GetComponent<PhotonView>();
		m_StoredPosition = transform.position;
		m_NetworkPosition = Vector3.zero;
		m_NetworkRotation = Quaternion.identity;
	}

	public void Update()
	{
		if (!m_PhotonView.IsMine)
		{
			transform.position = Vector3.MoveTowards(transform.position, m_NetworkPosition, m_Distance * (1f / (float)PhotonNetwork.SerializationRate));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, m_NetworkRotation, m_Angle * (1f / (float)PhotonNetwork.SerializationRate));
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			if (m_SynchronizePosition)
			{
				m_Direction = transform.position - m_StoredPosition;
				m_StoredPosition = transform.position;
				stream.SendNext(transform.position);
				stream.SendNext(m_Direction);
			}
			if (m_SynchronizeRotation)
			{
				stream.SendNext(transform.rotation);
			}
			if (m_SynchronizeScale)
			{
				stream.SendNext(transform.localScale);
			}
			return;
		}
		if (m_SynchronizePosition)
		{
			m_NetworkPosition = (Vector3)stream.ReceiveNext();
			m_Direction = (Vector3)stream.ReceiveNext();
			float num = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
			m_NetworkPosition += m_Direction * num;
			m_Distance = Vector3.Distance(transform.position, m_NetworkPosition);
		}
		if (m_SynchronizeRotation)
		{
			m_NetworkRotation = (Quaternion)stream.ReceiveNext();
			m_Angle = Quaternion.Angle(transform.rotation, m_NetworkRotation);
		}
		if (m_SynchronizeScale)
		{
			transform.localScale = (Vector3)stream.ReceiveNext();
		}
	}
}
