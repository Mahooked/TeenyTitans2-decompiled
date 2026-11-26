using UnityEngine;

[DisallowMultipleComponent]
public class RendererHelper : MonoBehaviour
{
	public string sortingLayerName = "Default";

	public int sortingLayerID;

	public int sortingOrder;

	[SerializeField]
	private Renderer _renderer;

	private Material sharedMaterial;

	private void Awake()
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<Renderer>();
		}
		sharedMaterial = _renderer.sharedMaterial;
	}

	public void RevertToSharedMaterial()
	{
		_renderer.material = sharedMaterial;
	}

	private void Start()
	{
		_renderer.sortingLayerName = sortingLayerName;
		_renderer.sortingOrder = sortingOrder;
	}

	private void Reset()
	{
		_renderer = GetComponent<Renderer>();
	}
}
