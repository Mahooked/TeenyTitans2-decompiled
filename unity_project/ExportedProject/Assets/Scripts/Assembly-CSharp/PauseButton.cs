using UnityEngine;

public class PauseButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite sprite;

	private void Awake()
	{
		if (uiItem == null)
		{
			GetComponent<tk2dUIItem>();
		}
	}

	private void Reset()
	{
		uiItem = GetComponent<tk2dUIItem>();
	}

	private void Start()
	{
		PauseManager.instance.Paused += HandlePaused;
		PauseManager.instance.IsEnabledChanged += HandlePauseManagerIsEnabledChanged;
	}

	private void OnDestroy()
	{
		PauseManager.instance.Paused -= HandlePaused;
		PauseManager.instance.IsEnabledChanged -= HandlePauseManagerIsEnabledChanged;
	}

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
	}

	private void HandleOnDown()
	{
		PauseManager.instance.Pause();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_pausebutton", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void HandlePaused(bool pauseStatus)
	{
		base.gameObject.SetActive(!pauseStatus);
	}

	private void HandlePauseManagerIsEnabledChanged(bool isEnabled)
	{
		sprite.color = ((!isEnabled) ? Color.gray : Color.white);
	}
}
