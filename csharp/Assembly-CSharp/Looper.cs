using UnityEngine;

public class Looper : MonoBehaviour
{
	public float length;

	public float speed;

	public bool useX;

	public bool useUnscaledTime;

	private Transform thisTransform;

	private void Awake()
	{
		thisTransform = transform;
		if (length < 0f)
		{
			length = 0f - length;
		}
	}

	private void Update()
	{
		Vector3 localPosition = thisTransform.localPosition;
		float num = Mathf.Min(0.03f, Time.unscaledDeltaTime);
		if (useX)
		{
			localPosition.x += speed * ((!useUnscaledTime) ? Time.deltaTime : num);
			if (speed < 0f && localPosition.x <= 0f)
			{
				localPosition.x += length;
			}
			else if (speed > 0f && localPosition.x >= length)
			{
				localPosition.x -= length;
			}
		}
		else
		{
			localPosition.y += speed * ((!useUnscaledTime) ? Time.deltaTime : num);
			if (speed < 0f && localPosition.y <= 0f)
			{
				localPosition.y += length;
			}
			else if (speed > 0f && localPosition.y >= length)
			{
				localPosition.y -= length;
			}
		}
		thisTransform.localPosition = localPosition;
	}
}
