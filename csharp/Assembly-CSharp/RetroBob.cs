using UnityEngine;

public class RetroBob : MonoBehaviour
{
	public float startY;

	public float endY;

	public float interval = 0.5f;

	private float counter;

	private bool atEndY;

	private void OnEnable()
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.y = startY;
		transform.localPosition = localPosition;
		atEndY = false;
		counter = interval;
	}

	private void Update()
	{
		if (counter > 0f)
		{
			counter -= Time.deltaTime;
			if (counter <= 0f)
			{
				Vector3 localPosition = transform.localPosition;
				localPosition.y = ((!atEndY) ? endY : startY);
				transform.localPosition = localPosition;
				atEndY = !atEndY;
				counter += interval;
			}
		}
	}
}
