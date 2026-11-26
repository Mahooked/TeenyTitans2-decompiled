using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FlashSaleBoostPickup : CityPickup
{
	public enum BoostType
	{
		Normal,
		Negative
	}

	public enum PatrolLoopType
	{
		Loop,
		PingPong
	}

	public BoostType boostType;

	public Animator animator;

	public tk2dBaseSprite sprite;

	private PathNode currentNode;

	private PathNode nextNode;

	private int direction;

	private int nodeIndex;

	private Tweener patrolTween;

	private bool canLoop;

	private bool _shouldPatrol;

	private PathNode _spawnNode;

	private PathNode[] _patrolPath;

	private bool _startMovingTowardStartNode;

	private float _patrolSpeed;

	private PatrolLoopType _loopType;

	public bool shouldPatrol
	{
		get
		{
			return _shouldPatrol;
		}
		set
		{
			_shouldPatrol = value;
		}
	}

	public PathNode spawnNode
	{
		get
		{
			return _spawnNode;
		}
		set
		{
			_spawnNode = value;
		}
	}

	public PathNode[] patrolPath
	{
		get
		{
			return _patrolPath;
		}
		set
		{
			_patrolPath = value;
		}
	}

	public bool startMovingTowardStartNode
	{
		get
		{
			return _startMovingTowardStartNode;
		}
		set
		{
			_startMovingTowardStartNode = value;
		}
	}

	public float patrolSpeed
	{
		get
		{
			return _patrolSpeed;
		}
		set
		{
			_patrolSpeed = value;
		}
	}

	public PatrolLoopType loopType
	{
		get
		{
			return _loopType;
		}
		set
		{
			_loopType = value;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		animator.Play("FlashSale_Boost_Loop", 0, 0f);
		FlashSaleController.instance.FlashSaleStarted += StartPatrol;
		StartCoroutine(CheckTint());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (FlashSaleController.instance != null)
		{
			FlashSaleController.instance.FlashSaleStarted -= StartPatrol;
		}
	}

	protected override void Start()
	{
		GetComponent<TransformLink>().LinkTo = CityController.instance.cameraHolder;
		base.Start();
	}

	private IEnumerator CheckTint()
	{
		yield return new WaitForEndOfFrame();
		if (boostType == BoostType.Normal)
		{
			sprite.SetSprite("gui_flashsale_boost");
		}
		else if (boostType == BoostType.Negative)
		{
			sprite.SetSprite("gui_flashsale_reverseboost");
		}
	}

	protected override void PlayerCollected()
	{
		if (boostType == BoostType.Normal)
		{
			animator.Play("FlashSale_Boost_Select", 0, 0f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_speedboost", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else if (boostType == BoostType.Negative)
		{
			animator.Play("FlashSale_Boost_Select_negative", 0, 0f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_slowdown", GFAudioManager.SFX_GROUP, string.Empty);
		}
		CityController.instance.cityCharacter.CollectedFlashSaleBoost(boostType);
		if (patrolTween != null)
		{
			patrolTween.Kill();
		}
		StartCoroutine(WaitForAnimationEnd());
	}

	private IEnumerator WaitForAnimationEnd()
	{
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		ObjectPool.PoolObject(gameObject);
	}

	private void StartPatrol()
	{
		FlashSaleController.instance.FlashSaleStarted -= StartPatrol;
		if (!shouldPatrol)
		{
			return;
		}
		int num = patrolPath.Length;
		nodeIndex = -1;
		for (int i = 0; i < num; i++)
		{
			if (patrolPath[i] == spawnNode)
			{
				nodeIndex = i;
				break;
			}
		}
		if (nodeIndex == -1)
		{
			Debug.LogError("Spawn Node not on Patrol Path! Stopping patrol.", gameObject);
			return;
		}
		canLoop = patrolPath[0] == patrolPath[num - 1];
		direction = ((!startMovingTowardStartNode) ? 1 : (-1));
		currentNode = patrolPath[nodeIndex];
		if (nodeIndex == 0 && ((canLoop && loopType == PatrolLoopType.PingPong) || !canLoop))
		{
			direction = 1;
		}
		else if (nodeIndex == 0 && canLoop && loopType == PatrolLoopType.Loop)
		{
			nodeIndex = num - 1;
		}
		else if (nodeIndex == num - 1 && ((canLoop && loopType == PatrolLoopType.PingPong) || !canLoop))
		{
			direction = -1;
		}
		else if (nodeIndex == num - 1 && canLoop && loopType == PatrolLoopType.Loop)
		{
			nodeIndex = 0;
		}
		nodeIndex += direction;
		nextNode = patrolPath[nodeIndex];
		patrolTween = transform.DOMove(nextNode.transform.position, patrolSpeed).SetSpeedBased().SetEase(Ease.Linear)
			.OnComplete(ReachedNextNode);
	}

	private void ReachedNextNode()
	{
		int num = patrolPath.Length;
		currentNode = nextNode;
		if (nodeIndex == 0 && ((canLoop && loopType == PatrolLoopType.PingPong) || !canLoop))
		{
			direction = 1;
		}
		else if (nodeIndex == 0 && canLoop && loopType == PatrolLoopType.Loop)
		{
			nodeIndex = num - 1;
		}
		else if (nodeIndex == num - 1 && ((canLoop && loopType == PatrolLoopType.PingPong) || !canLoop))
		{
			direction = -1;
		}
		else if (nodeIndex == num - 1 && canLoop && loopType == PatrolLoopType.Loop)
		{
			nodeIndex = 0;
		}
		nodeIndex += direction;
		nextNode = patrolPath[nodeIndex];
		if (patrolTween != null)
		{
			patrolTween.Kill();
		}
		patrolTween = transform.DOMove(nextNode.transform.position, patrolSpeed).SetSpeedBased().SetEase(Ease.Linear)
			.OnComplete(ReachedNextNode);
	}
}
