using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleCharacter))]
public class JessicaCruzExtrasRepaintHelper : MonoBehaviour
{
	[Serializable]
	public class IntColor
	{
		public int intValue;

		public Color color;
	}

	public BattleCharacter battleCharacter;

	public Renderer gunRenderer;

	[Tooltip("The fresnal property of the material changes depending on the repaint the character has.")]
	public List<IntColor> gunFresnalColors;

	private int fresnalColorID;

	private Color originalColor;

	private void Awake()
	{
		battleCharacter.Repainted += HandleCharacterRepainted;
		fresnalColorID = Shader.PropertyToID("_Fresnel_Color");
		originalColor = gunRenderer.sharedMaterial.GetColor(fresnalColorID);
	}

	private void OnDestroy()
	{
		battleCharacter.Repainted -= HandleCharacterRepainted;
		if (gunRenderer != null)
		{
			gunRenderer.sharedMaterial.SetColor(fresnalColorID, originalColor);
		}
	}

	private void HandleCharacterRepainted(RepaintColorSlot repaintColorSlot)
	{
		int num = repaintColorSlot?.id ?? (-1);
		Debug.Log("Handle Jessica Cruz Repainted, id: " + num);
		int count = gunFresnalColors.Count;
		for (int i = 0; i < count; i++)
		{
			IntColor intColor = gunFresnalColors[i];
			if (intColor.intValue == num)
			{
				gunRenderer.sharedMaterial.SetColor(fresnalColorID, intColor.color);
				break;
			}
		}
	}
}
