using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tk2dSpriteRoulette : MonoBehaviour
{
	public tk2dBaseSprite sprite;

	public List<string> spriteNames;

	public float delay = 0.08f;

	private IEnumerator spinEnumerator;

	private int index;

	private int prevIndex;

	private List<int> indexPool;

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
		}
		indexPool = new List<int>();
	}

	private void Reset()
	{
		sprite = GetComponent<tk2dBaseSprite>();
	}

	public void StartRoulette()
	{
		if (spinEnumerator == null && !(sprite == null))
		{
			spinEnumerator = Spin();
			StartCoroutine(spinEnumerator);
		}
	}

	public void StopRoulette()
	{
		if (spinEnumerator != null)
		{
			StopCoroutine(spinEnumerator);
			spinEnumerator = null;
		}
	}

	private IEnumerator Spin()
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			if (spriteNames.Count == 0)
			{
				yield return null;
			}
			index++;
			if (index >= spriteNames.Count)
			{
				index = 0;
			}
			sprite.SetSprite(spriteNames[index]);
			prevIndex = index;
		}
	}
}
