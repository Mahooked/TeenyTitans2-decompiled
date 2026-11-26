using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
	[Serializable]
	private class AttractedParticleSystem
	{
		[SerializeField]
		private ParticleSystem _pSystem;

		public ParticleSystem.Particle[] particles;

		public List<ParticleSystem.Particle> tempParticles;

		[HideInInspector]
		public float speed;

		[HideInInspector]
		public float velocity;

		[HideInInspector]
		public float moveDelay;

		public ParticleSystem pSystem
		{
			get
			{
				return _pSystem;
			}
			set
			{
				if (!(value == _pSystem))
				{
					_pSystem = value;
					if (_pSystem != null)
					{
						particles = new ParticleSystem.Particle[_pSystem.maxParticles];
					}
				}
			}
		}

		public AttractedParticleSystem(ParticleSystem targetPSystem, float attractDelay)
		{
			pSystem = targetPSystem;
			tempParticles = new List<ParticleSystem.Particle>();
			particles = new ParticleSystem.Particle[targetPSystem.maxParticles];
			moveDelay = attractDelay + 0.01f;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is AttractedParticleSystem))
			{
				return false;
			}
			AttractedParticleSystem attractedParticleSystem = (AttractedParticleSystem)obj;
			if (pSystem == attractedParticleSystem.pSystem)
			{
				return true;
			}
			return false;
		}
	}

	[SerializeField]
	private List<AttractedParticleSystem> pSystems = new List<AttractedParticleSystem>();

	public float startSpeed = 10f;

	public float acceleration = 1.1f;

	public float delay;

	private Transform thisTransform;

	private float lifetime;

	private bool isAttracting;

	private Vector3 targetPosition;

	private bool lifetimeCounterStarted;

	public int numParticleSystems => pSystems.Count;

	public event Action ParticleDestroyed;

	public event Action AllParticlesDestroyed;

	public event Action<ParticleSystem> ParticleDestroyedParticleSystem;

	public event Action<ParticleAttractor> AllParticlesDestroyedAttractor;

	private void Start()
	{
		thisTransform = transform;
		for (int i = 0; i < pSystems.Count; i++)
		{
			if (pSystems[i].pSystem != null)
			{
				pSystems[i].tempParticles = new List<ParticleSystem.Particle>();
				pSystems[i].particles = new ParticleSystem.Particle[pSystems[i].pSystem.maxParticles];
				pSystems[i].moveDelay = delay + 0.01f;
			}
		}
	}

	public void AttractParticleSystem(ParticleSystem targetPSys)
	{
		AttractedParticleSystem item = new AttractedParticleSystem(targetPSys, delay);
		if (!pSystems.Contains(item))
		{
			pSystems.Add(item);
		}
	}

	private void LateUpdate()
	{
		if (isAttracting)
		{
			lifetime += Time.deltaTime;
		}
		bool flag = true;
		for (int i = 0; i < pSystems.Count; i++)
		{
			AttractedParticleSystem attractedParticleSystem = pSystems[i];
			if (!(attractedParticleSystem.pSystem != null) || attractedParticleSystem.pSystem.particleCount == 0)
			{
				continue;
			}
			flag = false;
			if (attractedParticleSystem.moveDelay > 0f)
			{
				attractedParticleSystem.moveDelay -= Time.deltaTime;
				if (attractedParticleSystem.moveDelay <= 0f)
				{
					attractedParticleSystem.velocity = 0f;
					attractedParticleSystem.speed = startSpeed;
					StartLifetimeCounter();
					isAttracting = true;
				}
				continue;
			}
			attractedParticleSystem.pSystem.GetParticles(attractedParticleSystem.particles);
			attractedParticleSystem.tempParticles.Clear();
			attractedParticleSystem.velocity += attractedParticleSystem.speed;
			attractedParticleSystem.speed *= acceleration;
			if (attractedParticleSystem.pSystem.simulationSpace == ParticleSystemSimulationSpace.Local)
			{
				targetPosition = attractedParticleSystem.pSystem.transform.InverseTransformPoint(thisTransform.position);
			}
			else
			{
				targetPosition = thisTransform.position;
			}
			int particleCount = attractedParticleSystem.pSystem.particleCount;
			for (int j = 0; j < particleCount; j++)
			{
				float num = Vector3.SqrMagnitude(targetPosition - attractedParticleSystem.particles[j].position);
				float t = Mathf.Min(1f, (num - (num - attractedParticleSystem.velocity)) / num);
				attractedParticleSystem.particles[j].position = Vector3.Lerp(attractedParticleSystem.particles[j].position, targetPosition, t);
				if (!attractedParticleSystem.particles[j].position.Equals(targetPosition))
				{
					attractedParticleSystem.tempParticles.Add(attractedParticleSystem.particles[j]);
					continue;
				}
				if (ParticleDestroyed != null)
				{
					ParticleDestroyed();
				}
				if (ParticleDestroyedParticleSystem != null)
				{
					ParticleDestroyedParticleSystem(attractedParticleSystem.pSystem);
				}
			}
			attractedParticleSystem.pSystem.SetParticles(attractedParticleSystem.tempParticles.ToArray(), attractedParticleSystem.tempParticles.Count);
		}
		if (flag && isAttracting)
		{
			isAttracting = false;
			OnAllParticlesDestroyed();
		}
	}

	protected void OnAllParticlesDestroyed()
	{
		if (AllParticlesDestroyed != null)
		{
			AllParticlesDestroyed();
		}
		if (AllParticlesDestroyedAttractor != null)
		{
			AllParticlesDestroyedAttractor(this);
		}
		AllParticlesDestroyed = null;
		AllParticlesDestroyedAttractor = null;
		lifetimeCounterStarted = false;
		pSystems.Clear();
	}

	protected void StartLifetimeCounter()
	{
		if (!lifetimeCounterStarted)
		{
			lifetimeCounterStarted = true;
			lifetime = 0f;
		}
	}
}
