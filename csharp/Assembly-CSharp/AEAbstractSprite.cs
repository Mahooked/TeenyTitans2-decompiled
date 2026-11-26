using UnityEngine;

public abstract class AEAbstractSprite : MonoBehaviour
{
	public int layerIndex;

	public Transform spriteTransform;

	[SerializeField]
	protected AEAnimation _animAsset;

	[SerializeField]
	protected int _pixelsPerMeter;

	private Transform _transform;

	private Renderer _spriteRenderer;

	private bool _isRendererEnabled = true;

	private Material _activeMaterial;

	[SerializeField]
	protected Transform _childAnchor;

	private AELayer _layer;

	public Transform cachedTransform
	{
		get
		{
			if (_transform == null)
			{
				_transform = transform;
			}
			return _transform;
		}
	}

	public Renderer spriteRenderer
	{
		get
		{
			if (_spriteRenderer == null)
			{
				spriteTransform.gameObject.SetActive(value: true);
				_spriteRenderer = spriteTransform.GetComponent<Renderer>();
			}
			return _spriteRenderer;
		}
	}

	public bool isRendererEnabled
	{
		get
		{
			return _isRendererEnabled;
		}
		set
		{
			_isRendererEnabled = value;
		}
	}

	public Material activeMaterial
	{
		get
		{
			if (_activeMaterial == null)
			{
				_activeMaterial = spriteRenderer.sharedMaterial;
			}
			return _activeMaterial;
		}
		set
		{
			SetMaterial(value);
		}
	}

	public Transform childAnchor => _childAnchor;

	public AELayer layer
	{
		get
		{
			if (_layer == null)
			{
				_layer = _animAsset.composition.GetLayer(layerIndex);
			}
			return _layer;
		}
	}

	public virtual void Init(AEAnimation newAnimAsset, AELayer newLayer, int newPixelsPerMeter, bool updateSprite, params object[] args)
	{
		_animAsset = newAnimAsset;
		_layer = newLayer;
		_pixelsPerMeter = newPixelsPerMeter;
		layerIndex = newLayer.index;
		if (updateSprite)
		{
			gameObject.name = _layer.sourceNoExt;
		}
	}

	public void Clear()
	{
		_animAsset = null;
		_layer = null;
		layerIndex = 0;
		gameObject.name = "Pooled";
	}

	public void AddChild(AEAbstractSprite newChild)
	{
		if (_childAnchor == null)
		{
			_childAnchor = new GameObject("ChildAnchor").transform;
			_childAnchor.parent = transform;
			_childAnchor.localPosition = spriteTransform.localPosition;
			_childAnchor.gameObject.layer = gameObject.layer;
		}
		newChild.transform.parent = _childAnchor;
	}

	public void SetMaterial(Material newMaterial)
	{
		if (!layer.hasStaticMaterial)
		{
			_activeMaterial = newMaterial;
			spriteRenderer.material = _activeMaterial;
		}
	}

	public abstract void GoToFrame(int frameIndex);

	public abstract void GoToFrameForced(int frameIndex);

	public abstract void SetColor(Color newColor);

	public abstract void SetSortingLayerName(string newSortingLayerName);

	public abstract void SetSortingOrder(int newSortingOrder);

	public abstract void SetFlipped(bool isFlipped);
}
