using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistributionTest : MonoBehaviour
{
	[Serializable]
	public class Entry
	{
		public int entry;

		public TextMeshPro textMesh;

		[Range(0f, 100f)]
		public int weight;

		public int y { get; set; }

		public int timesChosen { get; set; }
	}

	public Entry[] entries;

	public bool retryTest;

	private List<GameObject> primitives;

	private void Start()
	{
		primitives = new List<GameObject>();
		Test();
	}

	private void Update()
	{
		if (retryTest)
		{
			retryTest = false;
			for (int i = 0; i < primitives.Count; i++)
			{
				UnityEngine.Object.Destroy(primitives[i]);
			}
			for (int j = 0; j < entries.Length; j++)
			{
				entries[j].y = 0;
			}
			primitives.Clear();
			Test();
		}
	}

	private void Test()
	{
		int num = 0;
		for (int i = 0; i < entries.Length; i++)
		{
			num += entries[i].weight;
		}
		for (int j = 0; j < 10000; j++)
		{
			float num2 = UnityEngine.Random.Range(0, num);
			for (int k = 0; k < entries.Length; k++)
			{
				if (num2 < (float)entries[k].weight)
				{
					GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
					gameObject.transform.position = new Vector3(entries[k].entry, (float)entries[k].y++ * 0.2f, 0f);
					gameObject.transform.localScale = Vector3.one * 0.1f;
					primitives.Add(gameObject);
					entries[k].textMesh.text = (++entries[k].timesChosen).ToString();
					break;
				}
				num2 -= (float)entries[k].weight;
			}
		}
	}
}
