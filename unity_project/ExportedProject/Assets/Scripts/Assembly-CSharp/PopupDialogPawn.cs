using TMPro;
using UnityEngine;

public class PopupDialogPawn : PopupDialog
{
	public TextMeshPro costText;

	private int _currencyReceived;

	public int currencyReceived
	{
		get
		{
			return _currencyReceived;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (Player.instance.characters.Count <= 1)
		{
			headerText.text = Language.Get("HOLDIT", "MENUS,GUI");
			descriptionText.text = Language.Get("PAWNLASTFIG", "MENUS,GUI");
			confirmButton.GetComponentInChildren<tk2dAnimationAdapter>(true).color = new Color(0.5f, 0.5f, 0.5f, 1f);
			confirmButton.OnClick -= base.HandleOnConfirmClick;
		}
		else
		{
			headerText.text = Language.Get("WARNING", "MENUS,GUI");
			confirmButton.GetComponentInChildren<tk2dAnimationAdapter>(true).color = Color.white;
		}
	}

	public void InitForCharacter(Character character)
	{
		int[] shopFigureCosts = character.characterDefinition.shopFigureCosts;
		int num = shopFigureCosts[Mathf.Min(character.level - 1, shopFigureCosts.Length - 1)];
		if (num == 0)
		{
			for (int num2 = shopFigureCosts.Length - 1; num2 >= 0; num2--)
			{
				int num3 = shopFigureCosts[num2];
				if (num3 != 0)
				{
					num = num3;
					break;
				}
			}
		}
		_currencyReceived = Mathf.Min(num / 2, GameManager.instance.gameSettings.maxPawnAmount);
		costText.text = Mathf.Max(_currencyReceived, 1).ToString();
	}
}
