using System.Collections;
using TMPro;
using UnityEngine;

public class RiddleEggRevealRewardCurrency : RiddleEggRevealReward, IPoolable
{
	public TextMeshPro amountText;

	private void OnEnable()
	{
		StartCoroutine(WaitToShowText());
	}

	private IEnumerator WaitToShowText()
	{
		yield return new WaitForSeconds(0.5f);
		amountText.gameObject.SetActive(value: true);
	}

	public override void Init(PlayerReward reward)
	{
		base.Init(reward);
		amountText.text = reward.amount.ToString();
	}

	public new void ResetObject()
	{
		amountText.gameObject.SetActive(value: false);
	}
}
