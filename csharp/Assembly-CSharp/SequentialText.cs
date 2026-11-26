using UnityEngine;

public abstract class SequentialText : MonoBehaviour
{
	public float delay = 0.5f;

	public string[] strings;

	private int index;

	private float timer;

	private void Awake()
	{
		index = 0;
		timer = delay;
		UpdateText(strings[index]);
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			timer += delay;
			index++;
			if (index == strings.Length)
			{
				index = 0;
			}
			UpdateText(strings[index]);
		}
	}

	protected abstract void UpdateText(string text);
}
