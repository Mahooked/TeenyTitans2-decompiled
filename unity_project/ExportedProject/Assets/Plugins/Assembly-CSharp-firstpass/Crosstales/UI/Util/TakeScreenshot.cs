using System;
using UnityEngine;

namespace Crosstales.UI.Util
{
	public class TakeScreenshot : MonoBehaviour
	{
		public string Prefix = "CT_Screenshot";

		public int Scale = 1;

		public KeyCode KeyCode = KeyCode.F8;

		private Texture2D texture;

		public void Start()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.transform.root.gameObject);
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode))
			{
				string text = Prefix + DateTime.Now.ToString("_d-MM-yyyy-HH-mm-ss-f") + ".png";
				ScreenCapture.CaptureScreenshot(text, Scale);
				Debug.Log("Screenshot saved: " + text);
			}
		}
	}
}
