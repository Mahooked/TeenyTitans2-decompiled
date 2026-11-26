using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class URLSchemeTestController : MonoBehaviour
{
	public tk2dUIItem openAppButton_NoParams;

	public tk2dUIItem openAppButton_1Param;

	public tk2dUIItem openAppButton_2Params;

	public tk2dUIItem openAppButton_3Params;

	public TextMeshPro infoText;

	private void OnEnable()
	{
		openAppButton_NoParams.OnClick += OpenAppButton_NoParams_OnClick;
		openAppButton_1Param.OnClick += OpenAppButton_1Param_OnClick;
		openAppButton_2Params.OnClick += OpenAppButton_2Params_OnClick;
		openAppButton_3Params.OnClick += OpenAppButton_3Params_OnClick;
	}

	private void OnDisable()
	{
		openAppButton_NoParams.OnClick -= OpenAppButton_NoParams_OnClick;
		openAppButton_1Param.OnClick -= OpenAppButton_1Param_OnClick;
		openAppButton_2Params.OnClick -= OpenAppButton_2Params_OnClick;
		openAppButton_3Params.OnClick -= OpenAppButton_3Params_OnClick;
	}

	private void Start()
	{
		URLSchemeHandler.Instance.OpenedWithURL += URLSchemeHandler_Instance_OpenedWithURL;
		URLSchemeHandler.Instance.FlushQueuedParameters();
	}

	private void OnDestroy()
	{
		if (URLSchemeHandler.Instance != null)
		{
			URLSchemeHandler.Instance.OpenedWithURL -= URLSchemeHandler_Instance_OpenedWithURL;
		}
	}

	private void URLSchemeHandler_Instance_OpenedWithURL(Dictionary<string, string> obj)
	{
		string text = "Opened from TT1 with parameters:\n\n";
		if (obj.Count > 0)
		{
			foreach (string key in obj.Keys)
			{
				text += string.Format("{0} = {1}\n", key, obj[key]);
			}
		}
		else
		{
			text += "No parameters";
		}
		infoText.text = text;
	}

	private void OpenAppButton_NoParams_OnClick()
	{
		Application.OpenURL("cn43://");
	}

	private void OpenAppButton_1Param_OnClick()
	{
		Application.OpenURL(string.Format("cn43://?param1={0}", WWW.EscapeURL("parameter 1")));
	}

	private void OpenAppButton_2Params_OnClick()
	{
		Application.OpenURL(string.Format("cn43://?param1={0}&param2={1}", WWW.EscapeURL("parameter 1"), WWW.EscapeURL("parameter 2")));
	}

	private void OpenAppButton_3Params_OnClick()
	{
		Application.OpenURL(string.Format("cn43://?param1={0}&param2={1}&param3={2}", WWW.EscapeURL("Parameter 1"), WWW.EscapeURL("Parameter 2"), WWW.EscapeURL("Parameter 3")));
	}
}
