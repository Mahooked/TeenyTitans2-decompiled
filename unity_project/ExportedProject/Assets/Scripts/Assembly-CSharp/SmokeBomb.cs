using UnityEngine;

public class SmokeBomb : MonoBehaviour, IBattleFlipper
{
	public ParticleSystem pSystem;

	private void OnEnable()
	{
		pSystem.EnableEmission(true);
	}

	public void TurnOff()
	{
		pSystem.EnableEmission(false);
		ObjectPool.PoolObject(base.gameObject, pSystem.duration);
	}

	public void InitWithCharacter(BattleCharacter character)
	{
		if (character.transform.localScale.z < 0f)
		{
			SetFlipped();
		}
		else
		{
			SetNormal();
		}
	}

	public void SetNormal()
	{
		base.transform.localScale = Vector3.one;
	}

	public void SetFlipped()
	{
		base.transform.localScale = new Vector3(1f, 1f, -1f);
	}
}
