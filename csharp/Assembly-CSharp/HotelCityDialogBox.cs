using System;
using TMPro;
using UnityEngine;

public class HotelCityDialogBox : CityDialogBox
{
	public TMP_FontAsset ENFont;

	public TextMeshPro headerTextMesh;

	public tk2dBaseSprite backingSprite;

	public LocalizedText localizedHeader;

	public LocalizedText localizedDescription;

	public Material headerDayMat;

	public Material headerNightMat;

	public Material descriptionDayMat;

	public Material descriptionNightMat;

	public override void ShowDialogBox(Action confirmBtnCallback, Action firstPageConfirmBtnCallback, bool allowedToConfirm, Action confirmDeniedCallback)
	{
		base.ShowDialogBox(confirmBtnCallback, firstPageConfirmBtnCallback, allowedToConfirm, confirmDeniedCallback);
		UpdateDialogBox();
	}

	public override void ShowDialogBox(string mainMessage, Action confirmBtnCallback, Action firstPageConfirmBtnCallback, bool allowedToConfirm, Action confirmDeniedCallback)
	{
		base.ShowDialogBox(mainMessage, confirmBtnCallback, firstPageConfirmBtnCallback, allowedToConfirm, confirmDeniedCallback);
		UpdateDialogBox();
	}

	private void UpdateDialogBox()
	{
		bool isDayTime = CityController.instance.communicator.isDayTime;
		headerTextMesh.fontSharedMaterial = ((!isDayTime) ? headerNightMat : headerDayMat);
		mainMessageText.fontSharedMaterial = ((!isDayTime) ? descriptionNightMat : descriptionDayMat);
		backingSprite.SetSprite((!isDayTime) ? "menu_hotel_night" : "menu_hotel_day");
		confirmBtnMessageText.text = GameManager.instance.gameSettings.hotelCost.ToString();
		if (Language.CurrentLanguage() != LanguageCode.EN)
		{
			localizedHeader.ForceInitFontProperties();
			localizedHeader.UpdateFont();
			localizedDescription.ForceInitFontProperties();
			localizedDescription.UpdateFont();
		}
		else
		{
			headerTextMesh.font = ENFont;
			mainMessageText.font = ENFont;
		}
	}
}
