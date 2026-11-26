using UnityEngine;

public class AEAnimationBuilderSprite : MonoBehaviour
{
	public Transform spriteTransform;

	private AELayer _layer;

	private Transform _childAnchor;

	public Transform childAnchor
	{
		get
		{
			return _childAnchor;
		}
	}

	public AELayer layer
	{
		get
		{
			return _layer;
		}
	}

	public int layerIndex
	{
		get
		{
			return _layer.index;
		}
	}

	public void Init(AELayer newLayer)
	{
		_layer = newLayer;
	}

	public void AddChild(AEAnimationBuilderSprite newChild)
	{
		if (_childAnchor == null)
		{
			_childAnchor = new GameObject("ChildAnchor").transform;
			_childAnchor.parent = base.transform;
			_childAnchor.localPosition = spriteTransform.localPosition;
			_childAnchor.gameObject.layer = base.gameObject.layer;
		}
		newChild.transform.parent = _childAnchor;
	}

	public void GoToFrameForced(int frameIndex)
	{
		if (frameIndex < _layer.inFrame || frameIndex >= _layer.outFrame)
		{
			return;
		}
		AEFrame frame = _layer.GetFrame(frameIndex);
		if (frame != null)
		{
			base.transform.localPosition = frame.positionUnity;
			Vector3 pivot = frame.pivot;
			spriteTransform.localPosition = new Vector3(0f - pivot.x, pivot.y, layer.index);
			if (_childAnchor != null)
			{
				_childAnchor.localPosition = new Vector3(spriteTransform.localPosition.x, spriteTransform.localPosition.y, 0f);
			}
			base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f - frame.rotation);
		}
	}
}
