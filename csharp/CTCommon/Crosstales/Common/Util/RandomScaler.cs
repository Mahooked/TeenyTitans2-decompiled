using UnityEngine;

namespace Crosstales.Common.Util;

public class RandomScaler : MonoBehaviour
{
	public Vector3 ScaleMin = new Vector3(0.1f, 0.1f, 0.1f);

	public Vector3 ScaleMax = new Vector3(3f, 3f, 3f);

	public bool Uniform = true;

	public Vector2 ChangeInterval = new Vector2(5f, 15f);

	private Transform tf;

	private Vector3 endScale;

	private float elapsedTime;

	private float changeTime;

	private Vector3 startScale;

	private float lerpTime;

	public void Start()
	{
		tf = transform;
		elapsedTime = (changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y));
		startScale = tf.localScale;
	}

	public void Update()
	{
		elapsedTime += Time.deltaTime;
		if (elapsedTime > changeTime)
		{
			if (Uniform)
			{
				endScale.x = (endScale.y = (endScale.z = Random.Range(ScaleMin.x, Mathf.Abs(ScaleMax.x))));
			}
			else
			{
				endScale.x = Random.Range(ScaleMin.x, Mathf.Abs(ScaleMax.x));
				endScale.y = Random.Range(ScaleMin.y, Mathf.Abs(ScaleMax.y));
				endScale.z = Random.Range(ScaleMin.z, Mathf.Abs(ScaleMax.z));
			}
			changeTime = Random.Range(ChangeInterval.x, ChangeInterval.y);
			lerpTime = (elapsedTime = 0f);
		}
		tf.localScale = Vector3.Lerp(startScale, endScale, lerpTime);
		if (lerpTime < 1f)
		{
			lerpTime += Time.deltaTime / (changeTime - 0.1f);
		}
		else
		{
			startScale = tf.localScale;
		}
	}
}
