using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopControllerSetupType
{
	public string rackPrefab;

	public float rackSpacing = 4.8f;

	public float rackStartingZ = 1f;

	public float figureBoxHorzSpacing = 2.4f;

	public float figureBoxVertSpacing = 2.4f;

	public float figureBoxVertTop = 3.87f;

	public float figureBoxZStart = -0.6f;

	public float figureBoxZOffset = -0.95f;

	public bool figuresHanging = true;

	public bool figuresFloating;

	public int maxPositions = 6;

	public Transform[] customPositions;

	public BoxCollider[] customColliders;

	public List<int> ignoreHangingForPositions;
}
