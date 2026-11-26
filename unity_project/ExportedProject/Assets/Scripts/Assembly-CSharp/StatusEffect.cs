using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public abstract class StatusEffect
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<StatusEffect> Removed__BackingField;

	protected Accessory casterAccessory;

	public abstract StatusEffectType type { get; }

	public abstract bool isStackable { get; }

	public abstract bool isResettable { get; }

	public abstract bool isZoomedOut { get; }

	public int uid { get; set; }

	public bool isInitialized { get; private set; }

	public bool isRemoved { get; private set; }

	public virtual bool isCleansable
	{
		get
		{
			return true;
		}
	}

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

	public event Action<StatusEffect> Removed
	{
		add
		{
			Action<StatusEffect> action = Removed__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Removed__BackingField, (Action<StatusEffect>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<StatusEffect> action = Removed__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Removed__BackingField, (Action<StatusEffect>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
		if (Removed__BackingField != null)
		{
			Removed__BackingField(this);
		}
	}
}
