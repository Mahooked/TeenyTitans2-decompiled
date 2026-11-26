using System;
using UnityEngine;

[Serializable]
public class CityLocationBounds
{
	public CityLocation cityLocaton;

	public Color boundsColor = Color.white;

	public Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 5f);

	public float boundsDiagonalLength => Vector3.Distance(bounds.min, bounds.max);
}
