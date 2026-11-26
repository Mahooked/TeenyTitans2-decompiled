using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetColor : MonoBehaviour
{
	public List<Color> Colors;

	public void SetColor(int id)
	{
		GetComponent<Image>().color = Colors[id];
	}
}
