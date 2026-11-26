using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopHanger
{
	[SerializeField]
	public int number;

	[SerializeField]
	public bool foldOut;

	[SerializeField]
	public List<ShopFigure> figures = new List<ShopFigure>();
}
