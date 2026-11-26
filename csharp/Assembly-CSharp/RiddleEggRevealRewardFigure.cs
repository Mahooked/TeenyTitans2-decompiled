using UnityEngine;

public class RiddleEggRevealRewardFigure : RiddleEggRevealReward
{
	public Transform figureContainer;

	private SimpleFigureBox figureBox;

	public override void Init(PlayerReward reward)
	{
		base.Init(reward);
		CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(reward.figure.characterUID);
		figureBox = ObjectPool.GetObjectFromResource<SimpleFigureBox>("Prefabs/GUI/SimpleFigureBox");
		figureBox.Init(character, reward.figure.characterLvl);
		figureBox.transform.SetParent(figureContainer);
		figureBox.transform.localPosition = Vector3.zero;
		figureBox.transform.localRotation = Quaternion.identity;
		figureBox.transform.localScale = Vector3.one;
	}

	public override void ResetObject()
	{
		base.ResetObject();
		if (figureBox != null)
		{
			ObjectPool.PoolObject(figureBox.gameObject);
			figureBox = null;
		}
	}
}
