using UnityEngine;

public class PreBattleArrow : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite sprite;

	public Transform figurePosition;

	public Renderer effect;

	public ShopFigureModel figureModel { get; set; }

	public Character character { get; set; }
}
