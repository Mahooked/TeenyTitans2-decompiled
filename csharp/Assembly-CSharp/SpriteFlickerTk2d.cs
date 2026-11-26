using UnityEngine;

public class SpriteFlickerTk2d : MonoBehaviour
{
	public tk2dBaseSprite sprite;

	public float delay = 0.1f;

	private float delayCounter;

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
		}
		delayCounter = delay;
	}

	private void Reset()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
		}
	}

	private void Update()
	{
		delayCounter -= Time.deltaTime;
		if (delayCounter <= 0f)
		{
			Vector3 scale = sprite.scale;
			scale.y = 0f - scale.y;
			sprite.scale = scale;
			delayCounter += delay;
		}
	}
}
