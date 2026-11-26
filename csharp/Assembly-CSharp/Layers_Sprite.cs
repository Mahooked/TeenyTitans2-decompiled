using System;
using TouchScript.Gestures;
using UnityEngine;

public class Layers_Sprite : MonoBehaviour
{
	private static Color[] COLORS = new Color[6]
	{
		Color.yellow,
		Color.red,
		Color.magenta,
		Color.green,
		Color.cyan,
		Color.blue
	};

	private void OnEnable()
	{
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}

	private void OnDisable()
	{
		GetComponent<TapGesture>().Tapped -= tappedHandler;
	}

	private void changeColor()
	{
		Color color = COLORS[UnityEngine.Random.Range(0, COLORS.Length)];
		while (color == GetComponent<Renderer>().material.color)
		{
			color = COLORS[UnityEngine.Random.Range(0, COLORS.Length)];
		}
		GetComponent<Renderer>().material.color = color;
	}

	private void tappedHandler(object sender, EventArgs eventArgs)
	{
		changeColor();
	}
}
