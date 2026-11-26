using UnityEngine;

[RequireComponent(typeof(PathNode))]
public class CityEntityPathNode : CityEntity
{
	[SerializeField]
	private CityEntityStatePathNode[] _states;

	[SerializeField]
	private PathNode _pathNode;

	private CityEntityStatePathNode.PathNodeOverrideSettings originalOverrideSettings;

	public override CityEntityState[] states
	{
		get
		{
			return _states;
		}
	}

	protected override void Start()
	{
		originalOverrideSettings = new CityEntityStatePathNode.PathNodeOverrideSettings();
		originalOverrideSettings.isActiveOverride = _pathNode.isActive;
		originalOverrideSettings.forceStopOverride = _pathNode.forceStop;
		originalOverrideSettings.teleportToNodeOverride = _pathNode.teleportToNode;
		originalOverrideSettings.teleportNodeOverride = _pathNode.teleportNode;
		originalOverrideSettings.loadSceneOverride = _pathNode.loadScene;
		originalOverrideSettings.loadSceneNameOverride = _pathNode.loadSceneName;
		originalOverrideSettings.startAtNodeNamedOverride = _pathNode.startAtNodeNamed;
		originalOverrideSettings.startFacingRightOverride = _pathNode.startFacingRight;
		originalOverrideSettings.setTriggerOverride = _pathNode.setTrigger;
		originalOverrideSettings.triggerNameOverride = _pathNode.triggerName;
		originalOverrideSettings.adjustCameraFOVOverride = _pathNode.adjustCameraFOV;
		originalOverrideSettings.targetCameraFOVOverride = _pathNode.targetCameraFOV;
		originalOverrideSettings.triggersConvoOverride = _pathNode.triggersConvo;
		originalOverrideSettings.triggersMusicOverride = _pathNode.triggersMusic;
		originalOverrideSettings.triggersMusicNighttimeOverride = _pathNode.triggersMusicNighttime;
		base.Start();
	}

	protected override void InitState(CityEntityState state)
	{
		CityEntityStatePathNode cityEntityStatePathNode = (CityEntityStatePathNode)state;
		if (cityEntityStatePathNode.overrideSettings.overrideIsActive)
		{
			_pathNode.isActive = cityEntityStatePathNode.overrideSettings.isActiveOverride;
		}
		else
		{
			_pathNode.isActive = originalOverrideSettings.isActiveOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideForceStop)
		{
			_pathNode.forceStop = cityEntityStatePathNode.overrideSettings.forceStopOverride;
		}
		else
		{
			_pathNode.forceStop = originalOverrideSettings.forceStopOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideTeleportToNode)
		{
			_pathNode.teleportToNode = cityEntityStatePathNode.overrideSettings.teleportToNodeOverride;
			if (_pathNode.teleportToNode)
			{
				_pathNode.teleportNode = cityEntityStatePathNode.overrideSettings.teleportNodeOverride;
			}
		}
		else
		{
			_pathNode.teleportToNode = originalOverrideSettings.teleportToNodeOverride;
			_pathNode.teleportNode = originalOverrideSettings.teleportNodeOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideLoadScene)
		{
			_pathNode.loadScene = cityEntityStatePathNode.overrideSettings.loadSceneOverride;
			if (_pathNode.loadScene)
			{
				_pathNode.loadSceneName = cityEntityStatePathNode.overrideSettings.loadSceneNameOverride;
				_pathNode.startAtNodeNamed = cityEntityStatePathNode.overrideSettings.startAtNodeNamedOverride;
				_pathNode.startFacingRight = cityEntityStatePathNode.overrideSettings.startFacingRightOverride;
			}
		}
		else
		{
			_pathNode.loadScene = originalOverrideSettings.loadSceneOverride;
			_pathNode.loadSceneName = originalOverrideSettings.loadSceneNameOverride;
			_pathNode.startAtNodeNamed = originalOverrideSettings.startAtNodeNamedOverride;
			_pathNode.startFacingRight = originalOverrideSettings.startFacingRightOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideSetTrigger)
		{
			_pathNode.setTrigger = cityEntityStatePathNode.overrideSettings.setTriggerOverride;
			_pathNode.triggerName = cityEntityStatePathNode.overrideSettings.triggerNameOverride;
		}
		else
		{
			_pathNode.setTrigger = originalOverrideSettings.setTriggerOverride;
			_pathNode.triggerName = originalOverrideSettings.triggerNameOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideAdjustCameraFOV)
		{
			_pathNode.adjustCameraFOV = cityEntityStatePathNode.overrideSettings.adjustCameraFOVOverride;
			if (_pathNode.adjustCameraFOV)
			{
				_pathNode.targetCameraFOV = cityEntityStatePathNode.overrideSettings.targetCameraFOVOverride;
			}
		}
		else
		{
			_pathNode.adjustCameraFOV = originalOverrideSettings.adjustCameraFOVOverride;
			_pathNode.targetCameraFOV = originalOverrideSettings.targetCameraFOVOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideTriggersConvo)
		{
			_pathNode.triggersConvo = cityEntityStatePathNode.overrideSettings.triggersConvoOverride;
		}
		else
		{
			_pathNode.triggersConvo = originalOverrideSettings.triggersConvoOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideTriggersMusic)
		{
			_pathNode.triggersMusic = cityEntityStatePathNode.overrideSettings.triggersMusicOverride;
		}
		else
		{
			_pathNode.triggersMusic = originalOverrideSettings.triggersMusicOverride;
		}
		if (cityEntityStatePathNode.overrideSettings.overrideTriggersMusicNighttime)
		{
			_pathNode.triggersMusicNighttime = cityEntityStatePathNode.overrideSettings.triggersMusicNighttimeOverride;
		}
		else
		{
			_pathNode.triggersMusicNighttime = originalOverrideSettings.triggersMusicNighttimeOverride;
		}
	}

	protected override void InitNullState()
	{
		_pathNode.isActive = originalOverrideSettings.isActiveOverride;
		_pathNode.forceStop = originalOverrideSettings.forceStopOverride;
		_pathNode.teleportToNode = originalOverrideSettings.teleportToNodeOverride;
		_pathNode.teleportNode = originalOverrideSettings.teleportNodeOverride;
		_pathNode.loadScene = originalOverrideSettings.loadSceneOverride;
		_pathNode.loadSceneName = originalOverrideSettings.loadSceneNameOverride;
		_pathNode.startAtNodeNamed = originalOverrideSettings.startAtNodeNamedOverride;
		_pathNode.startFacingRight = originalOverrideSettings.startFacingRightOverride;
		_pathNode.setTrigger = originalOverrideSettings.setTriggerOverride;
		_pathNode.triggerName = originalOverrideSettings.triggerNameOverride;
		_pathNode.adjustCameraFOV = originalOverrideSettings.adjustCameraFOVOverride;
		_pathNode.targetCameraFOV = originalOverrideSettings.targetCameraFOVOverride;
		_pathNode.triggersConvo = originalOverrideSettings.triggersConvoOverride;
		_pathNode.triggersMusic = originalOverrideSettings.triggersMusicOverride;
		_pathNode.triggersMusicNighttime = originalOverrideSettings.triggersMusicNighttimeOverride;
	}
}
