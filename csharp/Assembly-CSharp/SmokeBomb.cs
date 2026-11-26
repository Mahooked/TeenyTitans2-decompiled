using UnityEngine;

public class SmokeBomb : MonoBehaviour, IBattleFlipper
{
	public ParticleSystem pSystem;

	private void OnEnable()
	{
		pSystem.EnableEmission(enabled: true);
	}

	public void TurnOff()
	{
		pSystem.EnableEmission(enabled: false);
		ObjectPool.PoolObject(gameObject, pSystem.duration);
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
		transform.localScale = Vector3.one;
	}

	public void SetFlipped()
	{
		transform.localScale = new Vector3(1f, 1f, -1f);
	}
}
