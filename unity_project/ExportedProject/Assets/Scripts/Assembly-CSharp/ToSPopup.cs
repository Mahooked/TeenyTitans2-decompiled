using UnityEngine;

public class ToSPopup : MonoBehaviour
{
	public tk2dUIItem termsOfServiceButton;

	public tk2dUIItem privacyPolicyButton;

	public tk2dUIItem acceptButton;

	private void OnEnable()
	{
		termsOfServiceButton.OnClick += HandleTermsOfServiceClick;
		privacyPolicyButton.OnClick += HandlePrivacyPolicyClick;
		acceptButton.OnClick += HandleAcceptClick;
	}

	private void OnDisable()
	{
		termsOfServiceButton.OnClick -= HandleTermsOfServiceClick;
		privacyPolicyButton.OnClick -= HandlePrivacyPolicyClick;
		acceptButton.OnClick -= HandleAcceptClick;
	}

	private void HandleTermsOfServiceClick()
	{
		SocialSystem.OpenTermsOfUse();
	}

	private void HandlePrivacyPolicyClick()
	{
		SocialSystem.OpenPrivacyPolicy();
	}

	private void HandleAcceptClick()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		PlayerPrefs.SetInt("AcceptedTerms", 1);
		PlayerPrefs.Save();
		base.gameObject.SetActive(false);
	}
}
