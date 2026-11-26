using System;
using TouchScript.Gestures;
using UnityEngine;

public class Tap_Breaker : MonoBehaviour
{
	public float Power = 10f;

	private Vector3[] directions = new Vector3[8]
	{
		new Vector3(1f, -1f, 1f),
		new Vector3(-1f, -1f, 1f),
		new Vector3(-1f, -1f, -1f),
		new Vector3(1f, -1f, -1f),
		new Vector3(1f, 1f, 1f),
		new Vector3(-1f, 1f, 1f),
		new Vector3(-1f, 1f, -1f),
		new Vector3(1f, 1f, -1f)
	};

	private void OnEnable()
	{
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}

	private void OnDisable()
	{
		GetComponent<TapGesture>().Tapped -= tappedHandler;
	}

	private void tappedHandler(object sender, EventArgs e)
	{
		if (base.transform.localScale.x > 0.05f)
		{
			Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			for (int i = 0; i < 8; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(base.gameObject);
				Transform transform = gameObject.transform;
				transform.parent = base.transform.parent;
				transform.name = "Cube";
				transform.localScale = 0.5f * base.transform.localScale;
				transform.position = base.transform.TransformPoint(directions[i] / 4f);
				transform.GetComponent<Rigidbody>().AddForce(Power * UnityEngine.Random.insideUnitSphere, ForceMode.VelocityChange);
				transform.GetComponent<Renderer>().material.color = color;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
