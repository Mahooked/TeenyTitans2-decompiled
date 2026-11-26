using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/AI Config")]
public class AIConfig : ScriptableObject
{
	[SerializeField]
	[Tooltip("The maximum number of times an ability can be used consecutively")]
	private int _consecutiveAbilityUseLimit = 3;

	[SerializeField]
	[Tooltip("Number of seconds the AI will wait before attempting to make a decision")]
	private float _decisionInterval = 0.5f;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("The chance that no decision will be made at every decision interval")]
	private float _brainFartProbability = 0.9f;

	[SerializeField]
	[Tooltip("Number of seconds after using an ability before the AI will start attempting to make decisions again")]
	private RangedFloat _abilityDelay = new RangedFloat(2f, 2f);

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("The percentage of health the character must reach before worrying about using Heal type abilities")]
	private float _healthThreshold = 0.3f;

	[SerializeField]
	[Tooltip("Number of abilities that must be executed before swapping out becomes an option during the decision phase")]
	private RangedInt _minAbilitiesBeforeSwap = new RangedInt(2, 4);

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("The chance that a swap will occur during the decision phase once the required minimum ability uses has been reached")]
	private float _swapProbability = 0.03f;

	public int consecutiveAbilityUseLimit
	{
		get
		{
			return _consecutiveAbilityUseLimit;
		}
	}

	public float decisionInterval
	{
		get
		{
			return _decisionInterval;
		}
	}

	public float brainFartProbability
	{
		get
		{
			return _brainFartProbability;
		}
	}

	public RangedFloat abilityDelay
	{
		get
		{
			return _abilityDelay;
		}
	}

	public float healthThreshold
	{
		get
		{
			return _healthThreshold;
		}
	}

	public RangedInt minAbilitiesBeforeSwap
	{
		get
		{
			return _minAbilitiesBeforeSwap;
		}
	}

	public float swapProbability
	{
		get
		{
			return _swapProbability;
		}
	}
}
