using DG.Tweening;
using TMPro;
using UnityEngine;

public class CharacterViewStat : MonoBehaviour
{
	public tk2dSprite backingSprite;

	public tk2dClippedSprite fillSprite;

	public tk2dSprite iconSprite;

	public TextMeshPro textMesh;

	public tk2dUIItem uiItem;

	public bool isAnimated = true;

	private float statFillSpriteMin = 0.21f;

	private float statFillSpriteMax = 0.73f;

	private Tweener barTween;

	[SerializeField]
	private CharacterStatType _statType;

	public CharacterStatType statType => _statType;

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
	}

	public void Init(Character character)
	{
		int num = character.GetBaseStatByType(_statType) + character.GetBaseStatOffsetByType(_statType);
		int num2 = character.GetModifiedBaseStatByType(_statType) + character.GetBaseStatOffsetByType(_statType);
		int maxStatByType = character.characterDefinition.GetMaxStatByType(_statType);
		float num3 = statFillSpriteMax - statFillSpriteMin;
		textMesh.text = num2.ToString();
		TextMeshPro textMeshPro = textMesh;
		Color color;
		if (num2 > num)
		{
			color = Color.yellow;
		}
		else
		{
			color = ((num2 >= num) ? Color.white : Color.red);
		}
		textMeshPro.color = color;
		Rect rect = new Rect(0f, 0f, statFillSpriteMin + num3 * Mathf.Clamp01((float)num2 / (float)maxStatByType), 1f);
		if (isAnimated)
		{
			if (barTween != null)
			{
				barTween.Kill();
			}
			barTween = DOTween.To(() => fillSprite.ClipRect, (Rect x) =>
			{
				fillSprite.ClipRect = x;
			}, rect, 0.25f).SetEase(Ease.OutSine).OnComplete(HandleTweenComplete);
		}
		else
		{
			fillSprite.ClipRect = rect;
		}
	}

	public void Init(CharacterDefinition definition)
	{
		int num = definition.GetBaseStatByType(_statType) + definition.GetBaseStatOffsetByType(_statType);
		int maxStatByType = definition.GetMaxStatByType(_statType);
		float num2 = statFillSpriteMax - statFillSpriteMin;
		textMesh.text = num.ToString();
		Rect rect = new Rect(0f, 0f, statFillSpriteMin + num2 * Mathf.Clamp01((float)num / (float)maxStatByType), 1f);
		if (isAnimated)
		{
			if (barTween != null)
			{
				barTween.Kill();
			}
			barTween = DOTween.To(() => fillSprite.ClipRect, (Rect x) =>
			{
				fillSprite.ClipRect = x;
			}, rect, 0.25f).SetEase(Ease.OutSine).OnComplete(HandleTweenComplete);
		}
		else
		{
			fillSprite.ClipRect = rect;
		}
	}

	public void Init(ShopFigure shopFigure)
	{
	}

	private void HandleTweenComplete()
	{
		barTween = null;
	}

	public string GetLocalizedStatType()
	{
		string key = string.Empty;
		switch (_statType)
		{
		case CharacterStatType.HitPoints:
			key = "HEALTH";
			break;
		case CharacterStatType.Attack:
			key = "POWER";
			break;
		case CharacterStatType.Speed:
			key = "SPEED";
			break;
		case CharacterStatType.Luck:
			key = "LUCK";
			break;
		case CharacterStatType.Dodge:
			key = "DODGESTAT";
			break;
		}
		return Language.Get(key, "MENUS,GUI");
	}
}
