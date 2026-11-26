using TMPro;
using UnityEngine;

public class CharacterViewModChipSlot : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public TextMeshPro descriptionText;

	public Transform modChipContainer;

	private ModChipSelectPortrait modChipPortrait;

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

	private void Start()
	{
		Refresh();
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
		if (_modChip == null)
		{
			descriptionText.text = Language.Get("NOMODCHIP", "MENUS,GUI");
			if (modChipPortrait != null)
			{
				modChipPortrait.uiItem.OnDown -= HandleOnModChipDown;
				modChipPortrait.uiItem.OnRelease -= HandleOnModChipRelease;
				modChipPortrait.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
				ObjectPool.PoolObject(modChipPortrait.gameObject);
				modChipPortrait = null;
			}
			return;
		}
		descriptionText.text = TTGParseHelper.GetModChipDescription(_modChip);
		if (modChipPortrait == null)
		{
			modChipPortrait = ObjectPool.GetObjectFromResource<ModChipSelectPortrait>("Prefabs/GUI/ModChipGUI");
			modChipPortrait.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
			modChipPortrait.uiItemCollider.enabled = false;
			modChipPortrait.transform.parent = modChipContainer;
			modChipPortrait.transform.localPosition = Vector3.zero;
			modChipPortrait.transform.localScale = Vector3.one;
			modChipPortrait.uiItem.OnDown += HandleOnModChipDown;
			modChipPortrait.uiItem.OnRelease += HandleOnModChipRelease;
		}
		modChipPortrait.Init(_modChip);
	}

	private string GetFormattedModChipDescription(ModChipDefinition modChip)
	{
		int count = modChip.effects.Count;
		string[] array = new string[count];
		int i;
		for (i = 0; i < count; i++)
		{
			ModChipEffect modChipEffect = modChip.effects.Find((ModChipEffect x) => x.effectOrderIntVal == i || x.effectOrderFloatVal == i);
			if (modChipEffect != null)
			{
				string text = string.Empty;
				bool flag = modChipEffect.effectType == ModChipEffectType.CastTimeMultiplier || modChipEffect.effectType == ModChipEffectType.StatMultiplier;
				if (modChipEffect.effectOrderIntVal == i)
				{
					text = (flag ? (modChipEffect.intValue * 100 + "%") : modChipEffect.intValue.ToString());
				}
				else if (modChipEffect.effectOrderFloatVal == i)
				{
					text = (flag ? (modChipEffect.floatValue * 100f + "%") : modChipEffect.floatValue.ToString());
				}
				array[i] = text;
			}
		}
		return string.Format(Language.Get(modChip.itemDescriptionKey, "ABILS,CHIPS,ACHVS,LDBS").Trim(), array);
	}

	private void HandleOnModChipDown()
	{
		Debug.Log("TODO: Show mod chip details");
	}

	private void HandleOnModChipRelease()
	{
		Debug.Log("TODO: Hide mod chip details");
	}
}
