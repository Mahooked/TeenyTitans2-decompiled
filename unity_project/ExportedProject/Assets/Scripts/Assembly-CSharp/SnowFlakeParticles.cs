using System.Collections;
using UnityEngine;

public class SnowFlakeParticles : MonoBehaviour, IPoolable
{
	public ParticleSystem snowFlakePS;

	public ParticleSystem snowSparklePS;

	public ParticleSystem snowFogPS;

	public float emissionDisableDelay;

	public float deathDelayTime = 5f;

	private bool endingParticles;

	private float deathCounter;

	public void EndParticles()
	{
		if (!endingParticles)
		{
			endingParticles = true;
			StartCoroutine(WaitForDeath());
		}
	}

	private IEnumerator WaitForDeath()
	{
		yield return new WaitForSeconds(emissionDisableDelay);
		snowFlakePS.EnableEmission(false);
		snowSparklePS.EnableEmission(false);
		snowFogPS.EnableEmission(false);
		yield return new WaitForSeconds(deathDelayTime);
		ObjectPool.PoolObject(base.gameObject);
	}

	public void ResetObject()
	{
		snowFlakePS.EnableEmission(true);
		snowSparklePS.EnableEmission(true);
		snowFogPS.EnableEmission(true);
		endingParticles = false;
		base.transform.localScale = Vector3.one;
	}
}
