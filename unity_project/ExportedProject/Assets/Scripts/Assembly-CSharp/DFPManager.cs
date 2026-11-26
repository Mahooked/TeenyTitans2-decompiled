using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DFPManager : MonoBehaviour
{
	public string GameName;

	public DFPConfiguration Config;

	private string ConfigURL = "https://www.cartoonnetwork.com/papi/v2/config/dfputility";

	private int TargetVersion;

	private static DFPManager instance;

	private int attempts = 3;

	public static DFPManager Instance
	{
		get
		{
			return instance;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Start()
	{
		instance = this;
		SetPhoneOrTablet();
		if (!DFP.Instance.Initialized)
		{
			StartCoroutine(GetConfig());
		}
		DFP.Instance.debugString = "prod";
	}

	private IEnumerator GetConfig()
	{
		WWW www = new WWW(ConfigURL);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.LogError(www.error);
			attempts--;
			if (attempts > 0)
			{
				Debug.Log("Retrying... ");
				yield return new WaitForSeconds(0.5f);
				StartCoroutine(GetConfig());
			}
			else
			{
				Debug.Log("Maximum retries hit.  Cannot retrieve config file.");
			}
		}
		else
		{
			Debug.Log(www.text);
			Initialize(www.text);
		}
	}

	private void Initialize(string json)
	{
		if (string.IsNullOrEmpty(json))
		{
			Debug.LogError("Web config for DFP is empty!");
			return;
		}
		try
		{
			JToken value = JsonConvert.DeserializeObject<JObject>(json).GetValue("data");
			Config = JsonConvert.DeserializeObject<DFPConfiguration>(value.ToString());
			if (Config == null || Config.GetGameConfigByName(GameName) == null)
			{
				Debug.LogError("Game Name field on the DFPManager gameobject is null or incorrect! Cannot initialize!");
				return;
			}
			DFP.Instance.Initialize(Config.GetGameConfigByName(GameName));
			Debug.Log("DFP initialized!");
			DFP.Instance.RequestBanner();
		}
		catch (Exception ex)
		{
			Debug.LogError("Deserialization of DFPConfiguration failed!: " + ex.Message);
		}
	}

	public static float DeviceDiagonalSizeInInches()
	{
		float f = (float)Screen.width / Screen.dpi;
		float f2 = (float)Screen.height / Screen.dpi;
		float num = Mathf.Sqrt(Mathf.Pow(f, 2f) + Mathf.Pow(f2, 2f));
		Debug.Log("Getting device inches: " + num);
		return num;
	}

	public void SetPhoneOrTablet()
	{
		if (DeviceDiagonalSizeInInches() > 6.5f)
		{
			Debug.Log("Tablet");
			DFP.Instance.isPhone = false;
		}
		else
		{
			Debug.Log("Phone");
			DFP.Instance.isPhone = true;
		}
	}
}
