using System;
using UnityEngine;

public abstract class StatusEffect
{
	protected Accessory casterAccessory;

	public abstract StatusEffectType type { get; }

	public abstract bool isStackable { get; }

	public abstract bool isResettable { get; }

	public abstract bool isZoomedOut { get; }

	public int uid { get; set; }

	public bool isInitialized { get; private set; }

	public bool isRemoved { get; private set; }

	public virtual bool isCleansable => true;

	protected float DeltaTime
	{
		get
		{
			if (BattleController.battleDefinition != null && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				return TT2NetManager.Instance.DeltaTimeSeconds;
			}
			return Time.deltaTime;
		}
	}

	public event Action<StatusEffect> Removed;

	protected StatusEffect(Accessory casterAccessory)
	{
		this.casterAccessory = casterAccessory;
	}

	public void Init()
	{
		if (!isInitialized && !isRemoved)
		{
			if (casterAccessory != null)
			{
				casterAccessory.Destroyed += HandleCasterAccessoryDestroyed;
			}
			isInitialized = true;
			InitEffect();
		}
	}

	public void Update()
	{
		if (isInitialized)
		{
			UpdateEffect();
		}
	}

	public void Reset()
	{
		ResetEffect();
	}

	public void Remove()
	{
		if (!isRemoved)
		{
			if (casterAccessory != null)
			{
				casterAccessory.Destroyed -= HandleCasterAccessoryDestroyed;
			}
			if (isInitialized)
			{
				RemoveEffect();
			}
			CleanUp();
			isRemoved = true;
			OnRemoved();
		}
	}

	protected void HandleCasterAccessoryDestroyed()
	{
		Remove();
	}

	public virtual void Tick()
	{
	}

	public virtual void Expire()
	{
		Remove();
	}

	protected abstract void InitEffect();

	protected abstract void UpdateEffect();

	protected abstract void ResetEffect();

	protected abstract void RemoveEffect();

	protected abstract void CleanUp();

	protected void OnRemoved()
	{
		if (Removed != null)
		{
			Removed(this);
		}
	}
}
