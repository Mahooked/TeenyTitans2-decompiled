using UnityEngine;

public class BattleSetupController : MonoBehaviour
{
	public Animator player1Animator;

	public Animator player2Animator;

	public Animator player1CameraAnimator;

	public Animator player2CameraAnimator;

	public SplitCameras splitCameras;

	private bool isPlaying;

	private bool isPlayer1 = true;

	public void PlayAnimation(string stateName, bool isPlayer1)
	{
		if (!isPlaying)
		{
			Animator animator;
			Animator animator2;
			if (isPlayer1)
			{
				animator = player1Animator;
				animator2 = player1CameraAnimator;
			}
			else
			{
				animator = player2Animator;
				animator2 = player2CameraAnimator;
			}
			animator.Play(stateName);
			animator2.Play(stateName);
		}
	}

	private void OnSequenceComplete()
	{
		isPlaying = false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !isPlaying)
		{
			PlayAnimation((!(Random.value < 0.5f)) ? "heroic pose" : "birdarang", isPlayer1);
			isPlayer1 = !isPlayer1;
		}
	}
}
