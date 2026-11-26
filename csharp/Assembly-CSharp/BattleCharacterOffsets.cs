using System;
using UnityEngine;

[Serializable]
public class BattleCharacterOffsets
{
	[SerializeField]
	private Vector3 _playerCameraOffsetPosition;

	[SerializeField]
	private Vector3 _playerCameraOffsetRotation;

	[SerializeField]
	private Vector3 _playerCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _playerCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _enemyCameraOffsetPosition;

	[SerializeField]
	private Vector3 _enemyCameraOffsetRotation;

	[SerializeField]
	private Vector3 _enemyCameraOffsetPositionZoomedOut;

	[SerializeField]
	private Vector3 _enemyCameraOffsetRotationZoomedOut;

	[SerializeField]
	private Vector3 _topRightPortraitOffset;

	[SerializeField]
	private Vector3 _bottomLeftPortraitOffset;

	[SerializeField]
	private Vector3 _tofuBubbleOffsetPosition;

	public Vector3 playerCameraOffsetPosition => _playerCameraOffsetPosition;

	public Vector3 playerCameraOffsetRotation => _playerCameraOffsetRotation;

	public Vector3 playerCameraOffsetPositionZoomedOut
	{
		get
		{
			return _playerCameraOffsetPositionZoomedOut;
		}
		set
		{
			_playerCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 playerCameraOffsetRotationZoomedOut
	{
		get
		{
			return _playerCameraOffsetRotationZoomedOut;
		}
		set
		{
			_playerCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 enemyCameraOffsetPosition => _enemyCameraOffsetPosition;

	public Vector3 enemyCameraOffsetRotation => _enemyCameraOffsetRotation;

	public Vector3 enemyCameraOffsetPositionZoomedOut
	{
		get
		{
			return _enemyCameraOffsetPositionZoomedOut;
		}
		set
		{
			_enemyCameraOffsetPositionZoomedOut = value;
		}
	}

	public Vector3 enemyCameraOffsetRotationZoomedOut
	{
		get
		{
			return _enemyCameraOffsetRotationZoomedOut;
		}
		set
		{
			_enemyCameraOffsetRotationZoomedOut = value;
		}
	}

	public Vector3 topRightPortraitOffset => _topRightPortraitOffset;

	public Vector3 bottomLeftPortraitOffset => _bottomLeftPortraitOffset;

	public Vector3 tofuBubbleOffsetPosition => _tofuBubbleOffsetPosition;
}
