using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActionMeterDepleter : StatusEffect
{
	private ActionMeter actionMeter;

	private float amount;

	private float tickRate;

	private int maxTicks;

	private float tickTimer;

	private int ticks;

	private GameObject electricEffect;

	private Color barTintColor = new Color(0.52f, 0.92f, 1f);

	private bool instant;

	private IEnumerator waitForTickConfirmation;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public ActionMeterDepleter(ActionMeter actionMeter, float amount, float tickRate, int numTicks, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.actionMeter = actionMeter;
		this.amount = amount;
		this.tickRate = tickRate;
		maxTicks = numTicks;
	}

	public override void Tick()
	{
		float num = ((!(actionMeter.progress - amount < 0f)) ? amount : (amount + (actionMeter.progress - amount)));
		actionMeter.progress -= num;
		tk2dSlicedSpriteHelper barChunk = UnityEngine.Object.Instantiate(actionMeter.fillSprite);
		barChunk.transform.parent = actionMeter.fillSprite.transform.parent;
		barChunk.transform.localPosition = actionMeter.fillSprite.transform.localPosition + new Vector3(actionMeter.fillSprite.width, 0f, 0f);
		barChunk.transform.localRotation = actionMeter.fillSprite.transform.localRotation;
		barChunk.transform.localScale = actionMeter.fillSprite.transform.localScale;
		barChunk.width = actionMeter.maxWidth * num;
		barChunk.slicedSprite.SortingOrder = 1;
		DOTween.ToAlpha(() => barChunk.slicedSprite.color, (Color x) =>
		{
			barChunk.slicedSprite.color = x;
		}, 0f, 1f / 30f).SetDelay(0.2f).SetLoops(13, LoopType.Yoyo);
		Rigidbody2D rigidbody2D = (Rigidbody2D)barChunk.gameObject.AddComponent(typeof(Rigidbody2D));
		rigidbody2D.mass = 5f;
		rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
		rigidbody2D.gravityScale = 80f;
		float f = (float)UnityEngine.Random.Range(70, 110) * ((float)Math.PI / 180f);
		Vector2 vector = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
		Debug.DrawRay(barChunk.transform.position, vector * 100f, Color.magenta, 3f);
		rigidbody2D.AddForce(vector * 1000f, ForceMode2D.Impulse);
		SelfDestruct selfDestruct = (SelfDestruct)barChunk.gameObject.AddComponent(typeof(SelfDestruct));
		selfDestruct.destructType = SelfDestructType.Destroy;
		selfDestruct.SetTime(0.5f);
		ticks++;
		if (ticks >= maxTicks)
		{
			Expire();
		}
	}

	private IEnumerator WaitForTickConfirmation(float prevTickTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendStatusEffectTick(uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForTickConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			tickTimer = prevTickTimer;
			yield break;
		}
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		UnityEngine.Random.InitState(playerAction.seed);
		Tick();
		UnityEngine.Random.state = prevRandomState;
	}

	protected override void InitEffect()
	{
		tickTimer = 0f;
		ticks = 0;
		if (maxTicks > 0)
		{
			instant = false;
			electricEffect = ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/ElectricityBarEffect");
			electricEffect.transform.parent = actionMeter.transform;
			electricEffect.transform.localPosition = Vector3.zero;
			electricEffect.transform.localScale = Vector3.one;
			if (actionMeter == BattleController.instance.enemyActionMeter)
			{
				electricEffect.transform.localPosition = new Vector3(0.8f, 0f);
			}
		}
		else
		{
			instant = true;
		}
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting && !instant)
		{
			return;
		}
		float prevTickTimer = tickTimer;
		tickTimer -= DeltaTime;
		if (!(tickTimer <= 0f))
		{
			return;
		}
		tickTimer += tickRate;
		if (!instant && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (TT2NetManager.Instance.myNetworkPlayer.IsMasterClient && waitForTickConfirmation == null)
			{
				waitForTickConfirmation = WaitForTickConfirmation(prevTickTimer);
				BattleController.instance.StartCoroutine(waitForTickConfirmation);
			}
		}
		else
		{
			Tick();
		}
	}

	protected override void ResetEffect()
	{
		ticks = 0;
	}

	protected override void RemoveEffect()
	{
		if (waitForTickConfirmation != null)
		{
			BattleController.instance.StopCoroutine(waitForTickConfirmation);
			waitForTickConfirmation = null;
		}
		actionMeter = null;
		ObjectPool.PoolObject(electricEffect);
		electricEffect = null;
	}

	protected override void CleanUp()
	{
	}
}
