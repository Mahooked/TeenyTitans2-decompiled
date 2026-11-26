using System;
using Boo.Lang.Runtime;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

[Serializable]
public class TapJS_Spawner : MonoBehaviour
{
	public Transform CubePrefab;

	public Transform Container;

	public float Scale;

	public TapJS_Spawner()
	{
		Scale = 0.5f;
	}

	public virtual void OnTap(Gesture sender)
	{
		ITouchHit hit = null;
		sender.GetTargetHitResult(out hit);
		ITouchHit3D touchHit3D = hit as ITouchHit3D;
		if (!RuntimeServices.EqualityOperator(touchHit3D, null))
		{
			Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			Transform transform = UnityEngine.Object.Instantiate(CubePrefab) as Transform;
			transform.parent = Container;
			transform.name = "Cube";
			transform.localScale = Vector3.one * Scale * transform.localScale.x;
			transform.position = touchHit3D.Point + touchHit3D.Normal * 0.5f;
			transform.GetComponent<Renderer>().material.color = color;
		}
	}

	public virtual void Main()
	{
	}
}
