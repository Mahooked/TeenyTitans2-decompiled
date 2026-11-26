using UnityEngine;

public class ShopFigureModel : MonoBehaviour
{
	public Renderer renderer;

	public Renderer faceRenderer;

	public GameObject figureBase;

	public GameObject figureFace;

	public Renderer baseRenderer;

	private ModChipSelectPortrait _modChipModel;

	private Texture originalTexture;

	private Character _character;

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		if (baseRenderer == null && figureBase != null)
		{
			baseRenderer = figureBase.GetComponent<Renderer>();
		}
		if (faceRenderer == null && figureFace != null)
		{
			faceRenderer = figureFace.GetComponent<Renderer>();
		}
		originalTexture = renderer.sharedMaterial.mainTexture;
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
	}

	public void Init(Character newCharacter)
	{
		if (newCharacter != _character)
		{
			if (_character != null)
			{
				_character.Updated -= HandleCharacterUpdated;
			}
			_character = newCharacter;
			if (_character != null)
			{
				_character.Updated += HandleCharacterUpdated;
			}
			HandleCharacterUpdated();
		}
	}

	private void HandleCharacterUpdated()
	{
		if (_character == null)
		{
			if (_modChipModel != null)
			{
				ObjectPool.PoolObject(_modChipModel.gameObject);
				_modChipModel = null;
			}
		}
		else if (_character.modChip != null)
		{
			if (_modChipModel == null)
			{
				_modChipModel = ObjectPool.GetObjectFromResource<ModChipSelectPortrait>("Prefabs/Shop/ModChipShop");
				_modChipModel.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
				_modChipModel.transform.parent = base.transform;
				_modChipModel.modelContainer.localScale = new Vector3(0.755f, 0.755f, 0.57f);
				_modChipModel.transform.localPosition = new Vector3(0f, 0.05f, 0f);
				_modChipModel.transform.localRotation = Quaternion.Euler(270f, 180f, 0f);
			}
			_modChipModel.Init(_character.modChip);
		}
		else if (_modChipModel != null)
		{
			ObjectPool.PoolObject(_modChipModel.gameObject);
			_modChipModel = null;
		}
	}

	public void UpdateTexture(Texture newTexture)
	{
		if (renderer != null)
		{
			renderer.material.mainTexture = newTexture;
		}
		if (faceRenderer != null)
		{
			faceRenderer.material.mainTexture = newTexture;
		}
	}

	public void UpdateTextureToOriginalTexture()
	{
		UpdateTexture(originalTexture);
	}
}
