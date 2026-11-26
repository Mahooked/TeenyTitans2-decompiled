using System.Text.RegularExpressions;
using UnityEngine;

namespace TouchScript.Devices.Display;

public class GenericDisplayDevice : DisplayDevice
{
	private static bool? isLaptop;

	internal static bool IsLaptop
	{
		get
		{
			bool? flag = isLaptop;
			if (!flag.HasValue)
			{
				string input = SystemInfo.graphicsDeviceName.ToLower();
				Regex regex = new Regex("^(.*mobile.*|intel hd graphics.*|.*m\\s*(series)?\\s*(opengl engine)?)$", RegexOptions.IgnoreCase);
				if (regex.IsMatch(input))
				{
					isLaptop = true;
				}
				else
				{
					isLaptop = false;
				}
			}
			return isLaptop == true;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Name = Application.platform.ToString();
		if (IsLaptop)
		{
			Name += " (Laptop)";
		}
		dpi = Screen.dpi;
		if (!(dpi < float.Epsilon))
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.OSXWebPlayer:
		case RuntimePlatform.OSXDashboardPlayer:
		case RuntimePlatform.WindowsWebPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.LinuxPlayer:
		{
			int num5 = Mathf.Max(Screen.currentResolution.width, Screen.currentResolution.height);
			int num6 = Mathf.Min(Screen.currentResolution.width, Screen.currentResolution.height);
			if (num5 >= 3840)
			{
				if (num6 <= 2160)
				{
					dpi = 150f;
				}
				else
				{
					dpi = 200f;
				}
			}
			else if (num5 >= 2880 && num6 == 1800)
			{
				dpi = 220f;
			}
			else if (num5 >= 2560)
			{
				if (num6 >= 1600)
				{
					if (IsLaptop)
					{
						dpi = 226f;
					}
					else
					{
						dpi = 101f;
					}
				}
				else if (num6 >= 1440)
				{
					dpi = 109f;
				}
			}
			else if (num5 >= 2048)
			{
				if (num6 <= 1152)
				{
					dpi = 100f;
				}
				else
				{
					dpi = 171f;
				}
			}
			else if (num5 >= 1920)
			{
				if (num6 >= 1440)
				{
					dpi = 110f;
				}
				else if (num6 >= 1200)
				{
					dpi = 90f;
				}
				else if (num6 >= 1080)
				{
					if (IsLaptop)
					{
						dpi = 130f;
					}
					else
					{
						dpi = 92f;
					}
				}
			}
			else if (num5 >= 1680)
			{
				dpi = 129f;
			}
			else if (num5 >= 1600)
			{
				dpi = 140f;
			}
			else if (num5 >= 1440)
			{
				if (num6 >= 1050)
				{
					dpi = 125f;
				}
				else
				{
					dpi = 110f;
				}
			}
			else if (num5 >= 1366)
			{
				dpi = 125f;
			}
			else if (num5 >= 1280)
			{
				dpi = 110f;
			}
			else
			{
				dpi = 96f;
			}
			break;
		}
		case RuntimePlatform.Android:
		{
			int num3 = Mathf.Max(Screen.currentResolution.width, Screen.currentResolution.height);
			int num4 = Mathf.Min(Screen.currentResolution.width, Screen.currentResolution.height);
			if (num3 >= 1280)
			{
				if (num4 >= 800)
				{
					dpi = 285f;
				}
				else
				{
					dpi = 312f;
				}
			}
			else if (num3 >= 1024)
			{
				dpi = 171f;
			}
			else if (num3 >= 960)
			{
				dpi = 256f;
			}
			else if (num3 >= 800)
			{
				dpi = 240f;
			}
			else
			{
				dpi = 160f;
			}
			break;
		}
		case RuntimePlatform.IPhonePlayer:
		{
			int num = Mathf.Max(Screen.currentResolution.width, Screen.currentResolution.height);
			int num2 = Mathf.Min(Screen.currentResolution.width, Screen.currentResolution.height);
			if (num >= 2048)
			{
				dpi = 290f;
			}
			else if (num >= 1136)
			{
				dpi = 326f;
			}
			else if (num >= 1024)
			{
				dpi = 160f;
			}
			else if (num >= 960)
			{
				dpi = 326f;
			}
			else
			{
				dpi = 160f;
			}
			break;
		}
		case RuntimePlatform.MetroPlayerX86:
		case RuntimePlatform.MetroPlayerX64:
		case RuntimePlatform.MetroPlayerARM:
			dpi = 160f;
			break;
		case RuntimePlatform.WP8Player:
			dpi = 160f;
			break;
		case (RuntimePlatform)6:
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
		case RuntimePlatform.NaCl:
		case (RuntimePlatform)14:
		case RuntimePlatform.FlashPlayer:
		case RuntimePlatform.LinuxEditor:
		case RuntimePlatform.WebGLPlayer:
			break;
		}
	}
}
