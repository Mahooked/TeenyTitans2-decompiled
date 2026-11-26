using System;
using TouchScript.Gestures;
using UnityEngine;

[Serializable]
public class TapJS_Breaker : MonoBehaviour
{
	public float Power;

	private Vector3[] directions;

	public TapJS_Breaker()
	{
		Power = 10f;
		directions = new Vector3[8]
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
	}

	public virtual void OnTap(Gesture sender)
	{
		if (!(this.transform.localScale.x <= 0.05f))
		{
			Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			for (int i = 0; i < 8; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(this.gameObject) as GameObject;
				Transform transform = gameObject.transform;
				transform.parent = this.transform.parent;
				transform.name = "Cube";
				transform.localScale = 0.5f * this.transform.localScale;
				transform.position = this.transform.TransformPoint(directions[i] / 4f);
				transform.GetComponent<Rigidbody>().AddForce(Power * UnityEngine.Random.insideUnitSphere, ForceMode.VelocityChange);
				transform.GetComponent<Renderer>().material.color = color;
			}
			UnityEngine.Object.Destroy(this.gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
