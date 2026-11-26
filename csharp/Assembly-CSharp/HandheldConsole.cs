using System.Collections.Generic;
using UnityEngine;

public class HandheldConsole : MonoBehaviour
{
	private Dictionary<CityCharacterType, string> characterHandhelds = new Dictionary<CityCharacterType, string>
	{
		{
			CityCharacterType.BeastBoy,
			"HandheldModel_BeastBoy"
		},
		{
			CityCharacterType.Cyborg,
			"HandheldModel_Cyborg"
		},
		{
			CityCharacterType.Raven,
			"HandheldModel_Raven"
		},
		{
			CityCharacterType.Robin,
			"HandheldModel_Robin_New"
		},
		{
			CityCharacterType.Starfire,
			"HandheldModel_Starfire"
		}
	};

	public PreBattleArrow leftArrow;

	public PreBattleArrow centerArrow;

	public PreBattleArrow rightArrow;

	public Renderer leftEffect;

	public Renderer centerEffect;

	public Renderer rightEffect;

	private PreBattleArrow[] _arrows;

	public HandheldConsoleModel model { get; set; }

	public PreBattleArrow[] arrows => _arrows;

	private void Awake()
	{
		leftEffect.enabled = false;
		centerEffect.enabled = false;
		rightEffect.enabled = false;
		_arrows = new PreBattleArrow[3] { centerArrow, leftArrow, rightArrow };
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/PreBattle/" + characterHandhelds[CityCharacter.characterType]);
		objectFromResource.transform.parent = transform;
		objectFromResource.transform.localPosition = Vector3.zero;
		objectFromResource.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		objectFromResource.transform.localScale = Vector3.one;
		objectFromResource.SetActive(value: false);
		model = objectFromResource.GetComponent<HandheldConsoleModel>();
		model.teamBonusManager.leftArrow = leftArrow;
		model.teamBonusManager.centerArrow = centerArrow;
		model.teamBonusManager.rightArrow = rightArrow;
		leftArrow.figurePosition = model.leftFigurePosition;
		centerArrow.figurePosition = model.centerFigurePosition;
		rightArrow.figurePosition = model.rightFigurePosition;
		SetAccessoryRuleTextActive(active: false);
	}

	public void ShowArrows()
	{
		if (leftArrow.character == null)
		{
			leftArrow.sprite.gameObject.SetActive(value: true);
		}
		if (centerArrow.character == null)
		{
			centerArrow.sprite.gameObject.SetActive(value: true);
		}
		if (rightArrow.character == null)
		{
			rightArrow.sprite.gameObject.SetActive(value: true);
		}
	}

	public void HideArrows()
	{
		leftArrow.sprite.gameObject.SetActive(value: false);
		centerArrow.sprite.gameObject.SetActive(value: false);
		rightArrow.sprite.gameObject.SetActive(value: false);
	}

	public void SetAccessoryRuleTextActive(bool active)
	{
		model.accessoryRuleText.gameObject.SetActive(active);
	}
}
