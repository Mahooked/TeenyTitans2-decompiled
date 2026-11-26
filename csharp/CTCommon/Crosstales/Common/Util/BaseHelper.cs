using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Crosstales.Common.Model.Enum;
using UnityEngine;

namespace Crosstales.Common.Util;

public abstract class BaseHelper
{
	private static readonly Regex lineEndingsRegex = new Regex("\\r\\n|\\r|\\n");

	private static readonly Regex cleanSpacesRegex = new Regex("\\s+");

	private static readonly Regex cleanTagsRegex = new Regex("<.*?>");

	private const string file_prefix = "file://";

	public static bool isInternetAvailable => Application.internetReachability != NetworkReachability.NotReachable;

	public static bool isWindowsPlatform
	{
		get
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer)
			{
				return Application.platform == RuntimePlatform.WindowsEditor;
			}
			return true;
		}
	}

	public static bool isMacOSPlatform
	{
		get
		{
			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				return Application.platform == RuntimePlatform.OSXEditor;
			}
			return true;
		}
	}

	public static bool isLinuxPlatform => Application.platform == RuntimePlatform.LinuxPlayer;

	public static bool isStandalonePlatform
	{
		get
		{
			if (!isWindowsPlatform && !isMacOSPlatform)
			{
				return isLinuxPlatform;
			}
			return true;
		}
	}

	public static bool isAndroidPlatform => Application.platform == RuntimePlatform.Android;

	public static bool isIOSPlatform
	{
		get
		{
			if (Application.platform != RuntimePlatform.IPhonePlayer)
			{
				return Application.platform == RuntimePlatform.tvOS;
			}
			return true;
		}
	}

	public static bool isWSAPlatform
	{
		get
		{
			if (Application.platform != RuntimePlatform.MetroPlayerARM && Application.platform != RuntimePlatform.MetroPlayerX86 && Application.platform != RuntimePlatform.MetroPlayerX64)
			{
				return Application.platform == RuntimePlatform.XboxOne;
			}
			return true;
		}
	}

	public static bool isWebGLPlatform => Application.platform == RuntimePlatform.WebGLPlayer;

	public static bool isWebPlayerPlatform => false;

	public static bool isWebPlatform
	{
		get
		{
			if (!isWebPlayerPlatform)
			{
				return isWebGLPlatform;
			}
			return true;
		}
	}

	public static bool isWindowsBasedPlatform
	{
		get
		{
			if (!isWindowsPlatform)
			{
				return isWSAPlatform;
			}
			return true;
		}
	}

	public static bool isAppleBasedPlatform
	{
		get
		{
			if (!isMacOSPlatform)
			{
				return isIOSPlatform;
			}
			return true;
		}
	}

	public static bool isEditor
	{
		get
		{
			if (Application.platform != RuntimePlatform.OSXEditor)
			{
				return Application.platform == RuntimePlatform.WindowsEditor;
			}
			return true;
		}
	}

	public static bool isEditorMode
	{
		get
		{
			if (isEditor)
			{
				return !Application.isPlaying;
			}
			return false;
		}
	}

	public static Platform CurrentPlatform
	{
		get
		{
			if (isWindowsPlatform)
			{
				return Platform.Windows;
			}
			if (isMacOSPlatform)
			{
				return Platform.OSX;
			}
			if (isLinuxPlatform)
			{
				return Platform.Linux;
			}
			if (isAndroidPlatform)
			{
				return Platform.Android;
			}
			if (isIOSPlatform)
			{
				return Platform.IOS;
			}
			if (isWSAPlatform)
			{
				return Platform.WSA;
			}
			if (isWebPlatform)
			{
				return Platform.Web;
			}
			return Platform.Unsupported;
		}
	}

	public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	public static string ValidatePath(string path, bool addEndDelimiter = true)
	{
		if (!string.IsNullOrEmpty(path))
		{
			string text = path.Trim();
			string text2 = null;
			if (isWindowsPlatform)
			{
				text2 = text.Replace('/', '\\');
				if (addEndDelimiter && !text2.EndsWith("\\"))
				{
					text2 += "\\";
				}
			}
			else
			{
				text2 = text.Replace('\\', '/');
				if (addEndDelimiter && !text2.EndsWith("/"))
				{
					text2 += "/";
				}
			}
			return string.Join("_", text2.Split(Path.GetInvalidPathChars()));
		}
		return path;
	}

	public static string ValidateFile(string path)
	{
		if (!string.IsNullOrEmpty(path))
		{
			string text = ValidatePath(path);
			if (text.EndsWith("\\") || text.EndsWith("/"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			string text2 = text.Substring(isWindowsBasedPlatform ? (text.LastIndexOf("\\") + 1) : (text.LastIndexOf("/") + 1));
			string text3 = string.Join(string.Empty, text2.Split(Path.GetInvalidFileNameChars()));
			return text.Substring(0, text.Length - text2.Length) + text3;
		}
		return path;
	}

	public static string ValidURLFromFilePath(string path)
	{
		if (!string.IsNullOrEmpty(path))
		{
			if (!isValidURL(path))
			{
				return BaseConstants.PREFIX_FILE + ValidateFile(path).Replace(" ", "%20").Replace('\\', '/');
			}
			return ValidateFile(path).Replace(" ", "%20").Replace('\\', '/');
		}
		return path;
	}

	public static string CleanUrl(string url, bool removeProtocol = true, bool removeWWW = true, bool removeSlash = true)
	{
		string text = url.Trim();
		if (!string.IsNullOrEmpty(url))
		{
			if (removeProtocol)
			{
				text = text.Substring(text.IndexOf("//") + 2);
			}
			if (removeWWW)
			{
				text = text.CTReplace("www.", string.Empty);
			}
			if (removeSlash && text.EndsWith("/"))
			{
				text = text.Substring(0, text.Length - 1);
			}
		}
		return text;
	}

	public static string ClearTags(string text)
	{
		return cleanTagsRegex.Replace(text, string.Empty).Trim();
	}

	public static string ClearSpaces(string text)
	{
		return cleanSpacesRegex.Replace(text, " ").Trim();
	}

	public static string ClearLineEndings(string text)
	{
		return lineEndingsRegex.Replace(text, string.Empty).Trim();
	}

	public static List<string> SplitStringToLines(string text, bool ignoreCommentedLines = true, int skipHeaderLines = 0, int skipFooterLines = 0)
	{
		List<string> list = new List<string>(100);
		if (string.IsNullOrEmpty(text))
		{
			Debug.LogWarning("Parameter 'text' is null or empty!" + Environment.NewLine + "=> 'SplitStringToLines()' will return an empty string list.");
		}
		else
		{
			string[] array = lineEndingsRegex.Split(text);
			for (int i = 0; i < array.Length; i++)
			{
				if (i + 1 <= skipHeaderLines || i >= array.Length - skipFooterLines || string.IsNullOrEmpty(array[i]))
				{
					continue;
				}
				if (ignoreCommentedLines)
				{
					if (!array[i].StartsWith("#"))
					{
						list.Add(array[i]);
					}
				}
				else
				{
					list.Add(array[i]);
				}
			}
		}
		return list;
	}

	public static string FormatBytesToHRF(long bytes)
	{
		string[] array = new string[5] { "B", "KB", "MB", "GB", "TB" };
		double num = bytes;
		int num2 = 0;
		while (num >= 1024.0 && num2 < array.Length - 1)
		{
			num2++;
			num /= 1024.0;
		}
		return $"{num:0.##} {array[num2]}";
	}

	public static string FormatSecondsToHourMinSec(double seconds)
	{
		int num = (int)seconds;
		int num2 = num % 60;
		if (seconds >= 86400.0)
		{
			int num3 = num / 86400;
			int num4 = (num -= num3 * 86400) / 3600;
			int num5 = (num - num4 * 3600) / 60;
			return num3 + "d " + num4 + ":" + ((num5 < 10) ? ("0" + num5) : num5.ToString()) + ":" + ((num2 < 10) ? ("0" + num2) : num2.ToString());
		}
		if (seconds >= 3600.0)
		{
			int num6 = num / 3600;
			int num7 = (num - num6 * 3600) / 60;
			return num6 + ":" + ((num7 < 10) ? ("0" + num7) : num7.ToString()) + ":" + ((num2 < 10) ? ("0" + num2) : num2.ToString());
		}
		return num / 60 + ":" + ((num2 < 10) ? ("0" + num2) : num2.ToString());
	}

	public static Color HSVToRGB(float h, float s, float v, float a = 1f)
	{
		if (s == 0f)
		{
			return new Color(v, v, v, a);
		}
		h /= 60f;
		int num = Mathf.FloorToInt(h);
		float num2 = h - (float)num;
		float num3 = v * (1f - s);
		float num4 = v * (1f - s * num2);
		float num5 = v * (1f - s * (1f - num2));
		return num switch
		{
			0 => new Color(v, num5, num3, a), 
			1 => new Color(num4, v, num3, a), 
			2 => new Color(num3, v, num5, a), 
			3 => new Color(num3, num4, v, a), 
			4 => new Color(num5, num3, v, a), 
			_ => new Color(v, num3, num4, a), 
		};
	}

	public static bool isValidURL(string url)
	{
		if (!string.IsNullOrEmpty(url))
		{
			if (!url.StartsWith("file://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith(BaseConstants.PREFIX_HTTP, StringComparison.OrdinalIgnoreCase))
			{
				return url.StartsWith(BaseConstants.PREFIX_HTTPS, StringComparison.OrdinalIgnoreCase);
			}
			return true;
		}
		return false;
	}
}
