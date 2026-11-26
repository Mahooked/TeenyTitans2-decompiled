using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore;

[Serializable]
public struct ControlPoint(Vector3 a, Vector3 b)
{
	public Vector3 a = a;

	public Vector3 b = b;
}
