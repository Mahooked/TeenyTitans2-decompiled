using UnityEngine;

public class DayNightTintSprite : MonoBehaviour
{
	[SerializeField]
	private tk2dSprite sprite;

	private void OnEnable()
	{
		if (sprite != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.AddSpriteToTint(sprite);
		}
	}

	private void OnDisable()
	{
		if (sprite != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.RemoveSpriteToTint(sprite);
		}
	}

	private void Start()
	{
		if (sprite != null && DayNightCard.instance != null)
		{
			DayNightCard.instance.AddSpriteToTint(sprite);
		}
	}
}
