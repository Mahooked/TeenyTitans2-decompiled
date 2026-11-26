using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

public class Tap_Spawner : MonoBehaviour
{
	public Transform CubePrefab;

	public Transform Container;

	public float Scale = 0.5f;

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
		TapGesture tapGesture = sender as TapGesture;
		tapGesture.GetTargetHitResult(out var hit);
		if (hit is ITouchHit3D touchHit3D)
		{
			Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			Transform transform = UnityEngine.Object.Instantiate(CubePrefab);
			transform.parent = Container;
			transform.name = "Cube";
			transform.localScale = Vector3.one * Scale * transform.localScale.x;
			transform.position = touchHit3D.Point + touchHit3D.Normal * 0.5f;
			transform.GetComponent<Renderer>().material.color = color;
		}
	}
}
