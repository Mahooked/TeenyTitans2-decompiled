using TMPro;
using UnityEngine;

public class ModChipDescriptionBox : MonoBehaviour
{
	public Animator animator;

	public ModChipSelectPortrait modchipPortrait;

	public tk2dSprite chipColorSprite;

	public TextMeshPro nameText;

	public TextMeshPro descriptionText;

	private ModChipDefinition _modChip;

	public ModChipDefinition modChip
	{
		get
		{
			return _modChip;
		}
		set
		{
			_modChip = value;
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
		if (_modChip == null)
		{
			nameText.text = "n/a";
			descriptionText.text = "n/a";
		}
		else
		{
			nameText.text = Language.Get(_modChip.codeNameKey, "ABILS,CHIPS,ACHVS,LDBS");
			descriptionText.text = TTGParseHelper.GetModChipDescription(_modChip);
		}
		if (modchipPortrait != null)
		{
			modchipPortrait.Init(_modChip);
		}
	}
}
