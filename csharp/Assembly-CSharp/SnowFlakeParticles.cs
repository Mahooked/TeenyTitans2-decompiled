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
		snowFlakePS.EnableEmission(enabled: false);
		snowSparklePS.EnableEmission(enabled: false);
		snowFogPS.EnableEmission(enabled: false);
		yield return new WaitForSeconds(deathDelayTime);
		ObjectPool.PoolObject(gameObject);
	}

	public void ResetObject()
	{
		snowFlakePS.EnableEmission(enabled: true);
		snowSparklePS.EnableEmission(enabled: true);
		snowFogPS.EnableEmission(enabled: true);
		endingParticles = false;
		transform.localScale = Vector3.one;
	}
}
