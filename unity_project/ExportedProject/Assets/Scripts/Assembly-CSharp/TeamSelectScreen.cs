using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TeamSelectScreen : MonoBehaviour
{
	public tk2dUIItem exitButton;

	public tk2dUIItem goButton;

	public tk2dSprite goButtonBackingSprite;

	public tk2dSprite goButtonGoSprite;

	public tk2dSprite goButtonSprite;

	public Transform goButtonScaleGroup;

	public TextMeshPro choosePartyText;

	public Transform playerBar;

	public Transform enemyBar;

	public PreBattleParty preBattleParty;

	public HandheldConsole handheldConsole;

	public List<EnemyPortrait> enemyPortraits;

	public int minFigsToGo = 1;

	public Vector3 regularResolutionGoLocalPos;

	public Vector3 tallResolutionGoLocalPos;

	[Header("PvP Related")]
	public GameObject readyForBattleMessage;

	public GameObject readyForBattleMessageAnimGroup;

	public TextMeshPro waitingForPlayerText;

	public TextMeshPro otherPlayerReadyText;

	public TextMeshPro countdownTimerText;

	public Vector3 regularResolutionReadyLocalPos;

	public Vector3 tallResolutionReadyLocalPos;

	private Tweener goTween;

	private BoxCollider2D goButtonCollider;

	private Player _player;

	private BattleDefinition _definition;

	public Player player
	{
		get
		{
			return _player;
		}
		set
		{
			Init(value, _definition);
		}
	}

	private void Awake()
	{
		goButtonCollider = goButton.GetComponent<BoxCollider2D>();
		EventMessenger.Instance.AddListener<AccessoryEquippedEvent>(HandleAccessoryEquippedEvent);
	}

	private void OnEnable()
	{
		exitButton.OnClick += HandleOnExitClick;
	}

	private void OnDisable()
	{
		exitButton.OnClick -= HandleOnExitClick;
	}

	private void OnDestroy()
	{
		EventMessenger.Instance.RemoveListener<AccessoryEquippedEvent>(HandleAccessoryEquippedEvent);
		if (_player != null)
		{
			_player.TeamChanged -= HandleTeamChanged;
		}
		_player = null;
	}

	public void Init(Player player, BattleDefinition definition)
	{
		if (player == _player)
		{
			return;
		}
		if (_player != null)
		{
			_player.TeamChanged -= HandleTeamChanged;
		}
		_player = player;
		_definition = definition;
		if (_player != null)
		{
			_player.TeamChanged += HandleTeamChanged;
			HandleTeamChanged();
			preBattleParty.Init(_player, _definition);
		}
		if ((float)Screen.height / (float)Screen.width > 1.34f)
		{
			goButtonScaleGroup.transform.localPosition = tallResolutionGoLocalPos;
			if (goButtonCollider != null)
			{
				goButtonCollider.offset = new Vector2(0f, tallResolutionGoLocalPos.y);
			}
			if (readyForBattleMessageAnimGroup != null)
			{
				readyForBattleMessageAnimGroup.transform.localPosition = tallResolutionReadyLocalPos;
			}
		}
		else
		{
			goButtonScaleGroup.transform.localPosition = regularResolutionGoLocalPos;
			if (goButtonCollider != null)
			{
				goButtonCollider.offset = new Vector2(0f, regularResolutionGoLocalPos.y);
			}
			if (readyForBattleMessageAnimGroup != null)
			{
				readyForBattleMessageAnimGroup.transform.localPosition = regularResolutionReadyLocalPos;
			}
		}
	}

	public void EnableGoSpriteButton()
	{
		goButton.enabled = true;
		goButtonBackingSprite.color = Color.white;
		goButtonGoSprite.color = Color.white;
		if (_player.activeTeam.Count == 3)
		{
			goTween = goButtonScaleGroup.DOScale(1.25f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
		}
		else if (goTween != null)
		{
			goTween.Kill();
			goTween = null;
			goButtonScaleGroup.localScale = Vector3.one;
		}
	}

	public void DisableGoSpriteButton()
	{
		if (goTween != null)
		{
			goTween.Kill();
			goTween = null;
			goButtonScaleGroup.localScale = Vector3.one;
		}
		goButton.enabled = false;
		goButtonBackingSprite.color = new Color(0.5f, 0.5f, 0.5f);
		goButtonGoSprite.color = new Color(0.5f, 0.5f, 0.5f);
	}

	private void HandleAccessoryEquippedEvent(AccessoryEquippedEvent evt)
	{
		HandleTeamChanged();
	}

	private void HandleTeamChanged()
	{
		if (AreBattleConditionsMet() && _player.activeTeam.Count >= minFigsToGo)
		{
			EnableGoSpriteButton();
		}
		else
		{
			DisableGoSpriteButton();
		}
	}

	private bool AreBattleConditionsMet()
	{
		if (_definition.newBattleConditions != null)
		{
			List<Character> activeTeam = _player.activeTeam;
			for (int i = 0; i < _definition.newBattleConditions.Count; i++)
			{
				BattleCondition battleCondition = _definition.newBattleConditions[i];
				int num = 0;
				switch (battleCondition.conditionType)
				{
				case BattleCondition.ConditionType.FigureName:
				{
					for (int n = 0; n < activeTeam.Count; n++)
					{
						if (activeTeam[n].characterDefinition.uid == battleCondition.intValue2)
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				case BattleCondition.ConditionType.FigureTags:
				{
					for (int k = 0; k < activeTeam.Count; k++)
					{
						if (activeTeam[k].characterDefinition.tags.Contains(battleCondition.stringValue1))
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				case BattleCondition.ConditionType.FigureLevel:
				{
					for (int num2 = 0; num2 < activeTeam.Count; num2++)
					{
						if (ComparisonUtil.IsComparisonValid(activeTeam[num2].level, battleCondition.comparison2, battleCondition.intValue2))
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				case BattleCondition.ConditionType.FigureShinyRank:
				{
					for (int m = 0; m < activeTeam.Count; m++)
					{
						if (ComparisonUtil.IsComparisonValid(activeTeam[m].shinyRank, battleCondition.comparison2, battleCondition.intValue2))
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				case BattleCondition.ConditionType.Accessory:
					if (battleCondition.isOrNot1 == IsOrNot.Is)
					{
						if (_definition.accessoryUID != battleCondition.intValue1)
						{
							return false;
						}
					}
					else if (_definition.accessoryUID == battleCondition.intValue1)
					{
						return false;
					}
					break;
				case BattleCondition.ConditionType.TeamSize:
					if (!ComparisonUtil.IsComparisonValid(activeTeam.Count, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				case BattleCondition.ConditionType.FigureClass:
				{
					for (int l = 0; l < activeTeam.Count; l++)
					{
						if (activeTeam[l].characterDefinition.characterClass == (CharacterClass)battleCondition.intValue2)
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				case BattleCondition.ConditionType.FigureRepainted:
				{
					for (int j = 0; j < activeTeam.Count; j++)
					{
						if (activeTeam[j].repaintColorID > 0)
						{
							num++;
						}
					}
					if (!ComparisonUtil.IsComparisonValid(num, battleCondition.comparison1, battleCondition.intValue1))
					{
						return false;
					}
					break;
				}
				}
			}
		}
		return true;
	}

	private void HandleOnExitClick()
	{
		Debug.Log("EXIT");
	}
}
