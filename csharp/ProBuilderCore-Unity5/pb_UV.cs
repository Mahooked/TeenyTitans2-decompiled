using System;
using UnityEngine;

[Serializable]
public class pb_UV
{
	[Obsolete("See pb_UV.Anchor")]
	public enum Justify
	{
		Right,
		Left,
		Top,
		Center,
		Bottom,
		None
	}

	public enum Anchor
	{
		UpperLeft,
		UpperCenter,
		UpperRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		LowerLeft,
		LowerCenter,
		LowerRight,
		None
	}

	public enum Fill
	{
		Fit,
		Tile,
		Stretch
	}

	public bool useWorldSpace;

	public bool flipU;

	public bool flipV;

	public bool swapUV;

	public Fill fill;

	public Vector2 scale;

	public Vector2 offset;

	public float rotation;

	[Obsolete("Please use pb_UV.anchor.")]
	public Justify justify;

	public Vector2 localPivot;

	[Obsolete("localPivot and localSize are no longer stored.")]
	public Vector2 localSize;

	public Anchor anchor;

	public pb_UV()
	{
		useWorldSpace = false;
		flipU = false;
		flipV = false;
		swapUV = false;
		fill = Fill.Tile;
		scale = new Vector2(1f, 1f);
		offset = new Vector2(0f, 0f);
		rotation = 0f;
		anchor = Anchor.None;
	}

	public pb_UV(pb_UV uvs)
	{
		useWorldSpace = uvs.useWorldSpace;
		flipU = uvs.flipU;
		flipV = uvs.flipV;
		swapUV = uvs.swapUV;
		fill = uvs.fill;
		scale = uvs.scale;
		offset = uvs.offset;
		rotation = uvs.rotation;
		anchor = uvs.anchor;
	}

	public void Reset()
	{
		useWorldSpace = false;
		flipU = false;
		flipV = false;
		swapUV = false;
		fill = Fill.Tile;
		scale = new Vector2(1f, 1f);
		offset = new Vector2(0f, 0f);
		rotation = 0f;
		anchor = Anchor.LowerLeft;
	}

	public override string ToString()
	{
		return string.Concat("Use World Space: ", useWorldSpace, "\nFlip U: ", flipU, "\nFlip V: ", flipV, "\nSwap UV: ", swapUV, "\nFill Mode: ", fill, "\nAnchor: ", anchor, "\nScale: ", scale, "\nOffset: ", offset, "\nRotation: ", rotation, "\nPivot: ", localPivot, "\n");
	}
}
