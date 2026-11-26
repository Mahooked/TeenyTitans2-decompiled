using System;
using System.Collections.Generic;
using TouchScript.Devices.Display;
using TouchScript.Hit;
using TouchScript.Layers;
using UnityEngine;

namespace TouchScript;

public interface ITouchManager
{
	IDisplayDevice DisplayDevice { get; set; }

	float DPI { get; }

	bool ShouldCreateCameraLayer { get; set; }

	IList<TouchLayer> Layers { get; }

	float DotsPerCentimeter { get; }

	int NumberOfTouches { get; }

	IList<ITouch> ActiveTouches { get; }

	event EventHandler FrameStarted;

	event EventHandler FrameFinished;

	event EventHandler<TouchEventArgs> TouchesBegan;

	event EventHandler<TouchEventArgs> TouchesMoved;

	event EventHandler<TouchEventArgs> TouchesEnded;

	event EventHandler<TouchEventArgs> TouchesCancelled;

	bool AddLayer(TouchLayer layer);

	bool AddLayer(TouchLayer layer, int index);

	bool RemoveLayer(TouchLayer layer);

	void ChangeLayerIndex(int at, int to);

	Transform GetHitTarget(Vector2 position);

	bool GetHitTarget(Vector2 position, out ITouchHit hit);

	bool GetHitTarget(Vector2 position, out ITouchHit hit, out TouchLayer layer);
}
