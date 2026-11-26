using UnityEngine;

public class DefeatScreen : MonoBehaviour
{
	public tk2dSprite headerSprite;

	public tk2dUIItem homeButton;

	public tk2dUIItem retryButton;

	private void OnEnable()
	{
		homeButton.OnClick += HandleOnHomeClick;
		retryButton.OnClick += HandleOnRetryClick;
	}

	private void OnDisable()
	{
		homeButton.OnClick -= HandleOnHomeClick;
		retryButton.OnClick -= HandleOnRetryClick;
	}

	private void HandleOnHomeClick()
	{
		Communicator.afterBattleConvoName = string.Empty;
		LoadingScreenCover.LoadScene(CityController.previousCityScene);
	}

	private void HandleOnRetryClick()
	{
		if (TT2NetMatch.IsPlayer2Available())
		{
			Communicator.afterBattleConvoName = string.Empty;
			LoadingScreenCover.LoadScene(GameManager.instance.gameSettings.citySceneName);
		}
		else
		{
			LoadingScreenCover.ReloadActiveScene();
		}
	}
}
