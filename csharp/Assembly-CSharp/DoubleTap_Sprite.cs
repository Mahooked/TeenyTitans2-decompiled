using System;
using TouchScript.Gestures;
using UnityEngine;

public class DoubleTap_Sprite : MonoBehaviour
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
		TapGesture[] components = GetComponents<TapGesture>();
		foreach (TapGesture tapGesture in components)
		{
			tapGesture.Tapped += tappedHandler;
		}
	}

	private void OnDisable()
	{
		TapGesture[] components = GetComponents<TapGesture>();
		foreach (TapGesture tapGesture in components)
		{
			tapGesture.Tapped -= tappedHandler;
		}
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

	private void breakObject()
	{
		for (int i = 0; i < 3; i++)
		{
			Quaternion quaternion = Quaternion.Euler(0f, 0f, 120 * i);
			Rigidbody2D rigidbody2D = createCopy(transform.localPosition + quaternion * Vector3.right * transform.localScale.x * 1.3f);
			rigidbody2D.mass = GetComponent<Rigidbody2D>().mass / 3f;
			rigidbody2D.AddForce(quaternion * Vector2.right * 500f * rigidbody2D.mass);
		}
		UnityEngine.Object.Destroy(gameObject);
	}

	private Rigidbody2D createCopy(Vector3 position)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(base.gameObject);
		gameObject.name = "Logo";
		Transform transform = gameObject.transform;
		transform.parent = base.transform.parent;
		transform.position = position;
		transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360));
		transform.localScale = Vector3.one * base.transform.localScale.x * 0.6f;
		return gameObject.GetComponent<Rigidbody2D>();
	}

	private void tappedHandler(object sender, EventArgs eventArgs)
	{
		TapGesture tapGesture = sender as TapGesture;
		switch (tapGesture.NumberOfTapsRequired)
		{
		case 1:
			changeColor();
			break;
		case 2:
			breakObject();
			break;
		}
	}
}
