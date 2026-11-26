using UnityEngine;

public class RiddleEggRevealReward : MonoBehaviour, IPoolable
{
	public AudioClip revealClip;

	[Range(0f, 1f)]
	public float revealVolume = 1f;

	public PlayerReward reward { get; private set; }

	public virtual void Init(PlayerReward reward)
	{
		this.reward = reward;
	}

	public virtual void ResetObject()
	{
		reward = null;
	}
}
