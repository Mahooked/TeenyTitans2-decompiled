using System;
using System.Collections;
using UnityEngine;

namespace Uniject;

internal interface IUtil
{
	RuntimePlatform platform { get; }

	bool isEditor { get; }

	string persistentDataPath { get; }

	DateTime currentTime { get; }

	string deviceModel { get; }

	string deviceName { get; }

	DeviceType deviceType { get; }

	string operatingSystem { get; }

	T[] GetAnyComponentsOfType<T>() where T : class;

	object InitiateCoroutine(IEnumerator start);

	object GetWaitForSeconds(int seconds);

	void InitiateCoroutine(IEnumerator start, int delayInSeconds);

	void RunOnMainThread(Action runnable);

	void AddPauseListener(Action<bool> runnable);
}
