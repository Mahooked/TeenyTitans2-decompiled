using System.Collections;
using UnityEngine;

public class CityHotel : MonoBehaviour
{
	private bool isResting;

	public void EnterHotel()
	{
		if (!isResting)
		{
			string mainMessage = Language.Get("SKIPTO_NIGHT", "MENUS,GUI");
			if (!CityController.instance.communicator.isDayTime)
			{
				mainMessage = Language.Get("SKIPTO_DAY", "MENUS,GUI");
			}
			CityController.instance.cityCharacter.StopMove();
			CityController.instance.hotelDialogBox.ShowDialogBox(mainMessage, StartRest, null, Player.instance.currency >= GameManager.instance.gameSettings.hotelCost, StartRestDenied);
		}
	}

	private void StartRest()
	{
		StartCoroutine(StartRestDelayed());
	}

	private IEnumerator StartRestDelayed()
	{
		yield return new WaitForSeconds(0.5f);
		int time = 0;
		isResting = true;
		time = ((!CityController.instance.communicator.isDayTime) ? ((int)CityController.instance.communicator.fullCycleRealSeconds + 1) : ((int)(CityController.instance.communicator.fullCycleRealSeconds / 2f) + 1));
		CityController.instance.communicator.SetCurrentTime(time);
		CityController.instance.walletGUI.AddToWallet(-GameManager.instance.gameSettings.hotelCost);
		StartCoroutine("EndRest");
	}

	private IEnumerator EndRest()
	{
		yield return new WaitForSeconds(3f);
		isResting = false;
	}

	private void StartRestDenied()
	{
		CityController.instance.DisplayNotEnoughLooseChange(0.5f);
	}
}
