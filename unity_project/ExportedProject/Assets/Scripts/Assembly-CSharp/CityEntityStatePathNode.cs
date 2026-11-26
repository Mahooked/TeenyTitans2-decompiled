using System;
using UnityEngine;

[Serializable]
public class CityEntityStatePathNode : CityEntityState
{
	[Serializable]
	public class PathNodeOverrideSettings
	{
		public bool overrideIsActive;

		public bool isActiveOverride;

		public bool overrideForceStop;

		public bool forceStopOverride;

		public bool overrideTeleportToNode;

		public bool teleportToNodeOverride;

		public PathNode teleportNodeOverride;

		public bool overrideLoadScene;

		public bool loadSceneOverride;

		public string loadSceneNameOverride;

		public string startAtNodeNamedOverride;

		public bool startFacingRightOverride;

		public bool overrideSetTrigger;

		public bool setTriggerOverride;

		public string triggerNameOverride;

		public bool overrideAdjustCameraFOV;

		public bool adjustCameraFOVOverride;

		public float targetCameraFOVOverride;

		public bool overrideTriggersConvo;

		public Conversation triggersConvoOverride;

		public bool overrideTriggersMusic;

		public AudioClip triggersMusicOverride;

		public bool overrideTriggersMusicNighttime;

		public AudioClip triggersMusicNighttimeOverride;
	}

	[SerializeField]
	private PathNodeOverrideSettings _overrideSettings;

	public PathNodeOverrideSettings overrideSettings
	{
		get
		{
			return _overrideSettings;
		}
	}

	public CityEntityStatePathNode(string name)
		: base(name)
	{
	}
}
