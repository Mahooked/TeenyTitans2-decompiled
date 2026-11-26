using UnityEngine;

public class TitleScreen : MonoBehaviour
{
	public tk2dUIItem settingsButton;

	public tk2dUIItem introButton;

	public tk2dUIItem goButton;

	public tk2dUIItem privacyPolicyButton;

	public Animator goAnimator;

	public Animator titleAnimator;

	private bool startedBattle;

	private bool allowBattleStart;

	private float lastUpdate;

	private bool outroStarted;

	private float lastButtonClickTime;

	private void Start()
	{
		GameObject gameObject = new GameObject("Anchor (LowerLeft)", typeof(tk2dCameraAnchor));
		gameObject.transform.parent = base.transform;
		tk2dCameraAnchor component = gameObject.GetComponent<tk2dCameraAnchor>();
		component.AnchorCamera = GUICamera.instance.camera;
		component.AnchorPoint = tk2dBaseSprite.Anchor.LowerLeft;
		component.ForceUpdateTransform();
		settingsButton.transform.parent = gameObject.transform;
		GameObject gameObject2 = new GameObject("Anchor (LowerRight)", typeof(tk2dCameraAnchor));
		gameObject2.transform.parent = base.transform;
		tk2dCameraAnchor component2 = gameObject2.GetComponent<tk2dCameraAnchor>();
		component2.AnchorCamera = GUICamera.instance.camera;
		component2.AnchorPoint = tk2dBaseSprite.Anchor.LowerRight;
		component2.ForceUpdateTransform();
		introButton.transform.parent = gameObject2.transform;
		GameObject gameObject3 = new GameObject("Anchor (LowerCenter)", typeof(tk2dCameraAnchor));
		gameObject3.transform.parent = goButton.transform.parent;
		tk2dCameraAnchor component3 = gameObject3.GetComponent<tk2dCameraAnchor>();
		component3.AnchorCamera = GUICamera.instance.camera;
		component3.AnchorPoint = tk2dBaseSprite.Anchor.LowerCenter;
		component3.ForceUpdateTransform();
		goButton.transform.parent = gameObject3.transform;
		goButton.OnDown += GoButton_OnDown;
		GFAudioManager.instance.StopAll();
	}

	private void Update()
	{
		if (!startedBattle && !LoadingScreenCover.isLoading)
		{
		}
	}

	private void GoButton_OnDown()
	{
		goAnimator.Play("TitleScreen_Button_downstate", 0, 0f);
	}

	public void PlayOutro()
	{
		goAnimator.Play("TitleScreen_Button_downstate_outro", 0, 0f);
		titleAnimator.Play("TitleScreen_Logo_Outro", 0, 0f);
	}

	public bool IsOutroFinished()
	{
		if (goAnimator.NormalizedTime() < 0.99f || titleAnimator.NormalizedTime() < 0.99f)
		{
			return false;
		}
		return true;
	}

	public void HandleOnConnectClicked()
	{
	}

	public void HandleOnOnlineBattlesClicked()
	{
	}
}
