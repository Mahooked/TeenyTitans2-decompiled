using System.Collections;
using TMPro;
using UnityEngine;

public class CaptionsManager : MonoBehaviour
{
	public TextMeshPro textMesh;

	public float portraitTextMeshWidth = 8.5f;

	public float portraitTextMeshSize = 7f;

	public float portraitYPosition = -6.5f;

	public float landscapeTextMeshWidth = 20f;

	public float landscapeTextMeshSize = 8f;

	public float landscapeYPosition = -8f;

	private ScreenOrientation previousOrientation;

	private Coroutine captionRoutine;

	private Coroutine orientationRoutine;

	public void StartVideoCaption(string fileName)
	{
		VideoCaption videoCaption = Resources.Load("Captions/" + fileName) as VideoCaption;
		StartVideoCaption(videoCaption);
	}

	public void StartVideoCaption(VideoCaption videoCaption)
	{
		textMesh.text = string.Empty;
		if (!(videoCaption == null))
		{
			if (captionRoutine != null)
			{
				StopCoroutine(captionRoutine);
			}
			captionRoutine = StartCoroutine(StartVideoCaptionRoutine(videoCaption));
			if (orientationRoutine != null)
			{
				StopCoroutine(orientationRoutine);
			}
			orientationRoutine = StartCoroutine(UpdateTextOrientation());
		}
	}

	private IEnumerator StartVideoCaptionRoutine(VideoCaption videoCaption)
	{
		yield return new WaitForEndOfFrame();
		float startTime = Time.time;
		int len = videoCaption.captionLines.Count;
		int current = 0;
		while (current < len)
		{
			float currentTime = Time.time - startTime;
			VideoCaptionItem videoCaptionItem = videoCaption.captionLines[current];
			if (currentTime >= videoCaptionItem.captionStartTime && currentTime <= videoCaptionItem.captionEndTime)
			{
				textMesh.text = Language.Get(videoCaption.captionLines[current].captionKey, "INTRO CAPTIONS");
			}
			else if (currentTime > videoCaptionItem.captionEndTime)
			{
				current++;
				textMesh.text = string.Empty;
			}
			yield return new WaitForEndOfFrame();
		}
		StopCoroutine(orientationRoutine);
	}

	private IEnumerator UpdateTextOrientation()
	{
		while (true)
		{
			yield return null;
			if (previousOrientation != Screen.orientation)
			{
				if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
				{
					previousOrientation = Screen.orientation;
					textMesh.rectTransform.sizeDelta = new Vector2(portraitTextMeshWidth, textMesh.rectTransform.sizeDelta.y);
					textMesh.fontSize = portraitTextMeshSize;
					textMesh.fontSizeMax = portraitTextMeshSize;
					textMesh.transform.localPosition = new Vector3(textMesh.transform.localPosition.x, portraitYPosition, textMesh.transform.localPosition.z);
				}
				if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
				{
					previousOrientation = Screen.orientation;
					textMesh.rectTransform.sizeDelta = new Vector2(landscapeTextMeshWidth, textMesh.rectTransform.sizeDelta.y);
					textMesh.fontSize = landscapeTextMeshSize;
					textMesh.fontSizeMax = landscapeTextMeshSize;
					textMesh.transform.localPosition = new Vector3(textMesh.transform.localPosition.x, landscapeYPosition, textMesh.transform.localPosition.z);
				}
			}
		}
	}
}
