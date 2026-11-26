using UnityEngine;

public class VideoCopyTexture : MonoBehaviour
{
	public MediaPlayerCtrl m_srcVideo;

	private void Start()
	{
	}

	private void Update()
	{
		if (m_srcVideo != null && (m_srcVideo.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED || m_srcVideo.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING) && m_srcVideo.GetVideoTexture() != null && base.transform.GetComponent<MeshRenderer>().material.mainTexture != m_srcVideo.GetVideoTexture())
		{
			base.transform.GetComponent<MeshRenderer>().material.mainTexture = m_srcVideo.GetVideoTexture();
		}
	}
}
