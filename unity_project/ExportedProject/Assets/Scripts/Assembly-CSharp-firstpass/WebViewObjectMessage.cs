using System.Collections;
using UnityEngine;

public class WebViewObjectMessage
{
	public string path;

	public Hashtable args;

	public WebViewObjectMessage(string rawMessage)
	{
		string[] array = rawMessage.Split("?"[0]);
		path = array[0];
		args = new Hashtable();
		if (array.Length > 1)
		{
			string[] array2 = array[1].Split("&"[0]);
			foreach (string text in array2)
			{
				string[] array3 = text.Split("="[0]);
				args[array3[0]] = WWW.UnEscapeURL(array3[1]);
			}
		}
	}
}
