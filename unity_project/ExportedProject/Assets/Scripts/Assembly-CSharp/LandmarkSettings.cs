using System;

[Serializable]
public class LandmarkSettings
{
	public bool visible = true;

	public bool snapToEdge = true;

	public bool showTail;

	public bool hideWhenInView;

	public bool hideAtMinScale = true;

	public float scaleFactor = 0.17f;

	public float minScale = 0.4f;

	public float maxScale = 1f;
}
