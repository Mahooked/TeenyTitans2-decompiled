using UnityEngine;

public class ModChipSelectPortrait : MonoBehaviour, IPoolable
{
	public Animator animator;

	public tk2dUIItem uiItem;

	public Collider2D uiItemCollider;

	public Renderer bodyRenderer;

	public Renderer faceRenderer;

	public Transform modelContainer;

	private ModChipDefinition _modChip;

	public ModChipDefinition modChip
	{
		get
		{
			return _modChip;
		}
		set
		{
			Init(value);
		}
	}

	public void Init(ModChipDefinition modChip)
	{
		if (!(modChip == _modChip))
		{
			_modChip = modChip;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (!(_modChip == null))
		{
			Material material = bodyRenderer.material;
			float y;
			if (_modChip.quality != ModChipQuality.Common)
			{
				y = ((_modChip.quality != ModChipQuality.Legendary) ? 0.66f : 0.33f);
			}
			else
			{
				y = 0f;
			}
			material.SetTextureOffset("_MainTex", new Vector2(0f, y));
			faceRenderer.material.SetTextureOffset("_MainTex", new Vector2((float)_modChip.iconColumn / 6f, 1f - (float)_modChip.iconRow / 6f));
		}
	}

	public void ResetObject()
	{
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		uiItemCollider.enabled = true;
	}
}
