using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(tk2dUIToggleButtonGroup))]
public class PageDots : MonoBehaviour
{
	public tk2dUIToggleButtonGroup toggleGroup;

	public GameObject dotPrefab;

	public float spacing = 25f;

	public int initialPoolCount = 2;

	private List<tk2dUIToggleButton> dotPool = new List<tk2dUIToggleButton>();

	private List<tk2dUIToggleButton> activeDots = new List<tk2dUIToggleButton>();

	private int _numDots;

	public int numDots
	{
		get
		{
			return _numDots;
		}
		set
		{
			if (value != _numDots)
			{
				_numDots = value;
				Rebuild();
			}
		}
	}

	private void Awake()
	{
		if (toggleGroup == null)
		{
			toggleGroup = GetComponent<tk2dUIToggleButtonGroup>();
		}
		tk2dUIToggleButton tk2dUIToggleButton2 = null;
		for (int i = 0; i < initialPoolCount; i++)
		{
			tk2dUIToggleButton2 = Object.Instantiate(dotPrefab).GetComponent<tk2dUIToggleButton>();
			tk2dUIToggleButton2.transform.parent = transform;
			tk2dUIToggleButton2.transform.localPosition = Vector3.zero;
			tk2dUIToggleButton2.transform.localScale = Vector3.one;
			tk2dUIToggleButton2.gameObject.SetLayerRecursively(gameObject.layer);
			tk2dUIToggleButton2.gameObject.SetActive(value: false);
			dotPool.Add(tk2dUIToggleButton2);
		}
	}

	private void Reset()
	{
		toggleGroup = GetComponent<tk2dUIToggleButtonGroup>();
	}

	private void Rebuild()
	{
		float num = spacing * (float)(_numDots - 1);
		float num2 = num * -0.5f;
		tk2dUIToggleButton tk2dUIToggleButton2 = null;
		for (int i = 0; i < _numDots; i++)
		{
			if (i == activeDots.Count)
			{
				activeDots.Add(GetDot());
			}
			tk2dUIToggleButton2 = activeDots[i];
			Vector3 localPosition = tk2dUIToggleButton2.transform.localPosition;
			localPosition.x = num2;
			tk2dUIToggleButton2.transform.localPosition = localPosition;
			num2 += spacing;
		}
		while (activeDots.Count > _numDots)
		{
			ReturnDot(activeDots.Pop());
		}
		toggleGroup.AddNewToggleButtons(activeDots.ToArray());
	}

	private tk2dUIToggleButton GetDot()
	{
		tk2dUIToggleButton tk2dUIToggleButton2 = null;
		if (dotPool.Count == 0)
		{
			tk2dUIToggleButton2 = Object.Instantiate(dotPrefab).GetComponent<tk2dUIToggleButton>();
			tk2dUIToggleButton2.gameObject.SetLayerRecursively(gameObject.layer);
			tk2dUIToggleButton2.transform.parent = transform;
			tk2dUIToggleButton2.transform.localPosition = Vector3.zero;
			tk2dUIToggleButton2.transform.localScale = Vector3.one;
		}
		else
		{
			tk2dUIToggleButton2 = dotPool.Pop();
		}
		tk2dUIToggleButton2.gameObject.SetActive(value: true);
		return tk2dUIToggleButton2;
	}

	private void ReturnDot(tk2dUIToggleButton dot)
	{
		dot.gameObject.SetActive(value: false);
		dotPool.Add(dot);
	}
}
