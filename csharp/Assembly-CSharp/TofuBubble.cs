using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TofuBubble : MonoBehaviour, IPoolable
{
	private static string[] tofuSpriteNames = new string[10] { "gui_tofu_heal", "gui_tofu_spicy", "gui_tofu_speedy", "gui_tofu_sleepy", "gui_tofu_cleanse", "gui_tofu_battery", "gui_tofu_dance", "gui_tofu_shield", "gui_tofu_freeze", "gui_tofu_waffle" };

	public static Dictionary<TofuType, int> tofuDropWeightOffsets = new Dictionary<TofuType, int>
	{
		{
			TofuType.Battery,
			0
		},
		{
			TofuType.Bubble,
			0
		},
		{
			TofuType.Cleanse,
			0
		},
		{
			TofuType.Dance,
			0
		},
		{
			TofuType.Freeze,
			0
		},
		{
			TofuType.Sleepy,
			0
		},
		{
			TofuType.Speedy,
			0
		},
		{
			TofuType.Spicy,
			0
		},
		{
			TofuType.Tasty,
			0
		},
		{
			TofuType.Waffle,
			0
		}
	};

	public tk2dSprite tofuSprite;

	public Transform followTrans;

	public Vector3 followOffset;

	private Animator animator;

	private Character currentCharacter;

	private BattleCamera otherCamera;

	private Vector3 scale = new Vector3(0.008f, 0.008f, 0.008f);

	private float timeout = 3f;

	private bool spinning;

	private bool used;

	private bool paused;

	private int currentIndex;

	private int previousIndex;

	private Transform cameraTrans;

	private AudioSource audioSource;

	private bool forceTofu;

	private TofuType forceTofuType;

	private BattleController _battleController;

	public bool Spinning => spinning;

	public bool Paused => paused;

	private BattleController battleController
	{
		get
		{
			if (_battleController == null)
			{
				_battleController = BattleController.instance;
			}
			return _battleController;
		}
	}

	public event Action Minimized;

	public event Action Maximized;

	public event Action TofuUsed;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		battleController.CharacterSwapOutStart += HandleCharacterSwapOutStart;
		battleController.CharacterSwappedIn += HandleCharacterSwappedIn;
		battleController.BattleEnded += HandleBattleEnded;
		if (otherCamera == null)
		{
			otherCamera = battleController.enemyBattleCamera;
		}
		otherCamera.OffsetInStart += HandleOffsetInStart;
		otherCamera.OffsetOutComplete += HandleOffsetOutComplete;
	}

	private void OnDisable()
	{
		StopCoroutine("StartRoulette");
		if (audioSource != null && audioSource.clip != null && audioSource.clip.name == "sfx_battle_tofu_rouletteloop" && audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource = null;
		if (battleController != null)
		{
			battleController.CharacterSwapOutStart -= HandleCharacterSwapOutStart;
			battleController.CharacterSwappedIn -= HandleCharacterSwappedIn;
			battleController.BattleEnded -= HandleBattleEnded;
		}
		if (otherCamera != null)
		{
			otherCamera.OffsetInStart -= HandleOffsetInStart;
			otherCamera.OffsetOutComplete -= HandleOffsetOutComplete;
		}
		if (currentCharacter != null)
		{
			currentCharacter.Transformed -= HandleTransformed;
			currentCharacter.DeathCharacter -= HandleDeathCharacter;
			currentCharacter = null;
		}
	}

	private void OnDestroy()
	{
		if (currentCharacter != null)
		{
			currentCharacter.Transformed -= HandleTransformed;
			currentCharacter.DeathCharacter -= HandleDeathCharacter;
			currentCharacter = null;
		}
	}

	public void Init(BattleCharacter battleCharacter)
	{
		currentCharacter = battleCharacter.character;
		currentCharacter.Transformed += HandleTransformed;
		currentCharacter.DeathCharacter += HandleDeathCharacter;
		followTrans = battleCharacter.tofuAnchor;
		followOffset = currentCharacter.tofuBubbleOffsetPosition;
		transform.position = followTrans.position + followOffset;
		if (battleController.waiting || battleController.playerActionMeter.isCasting || battleController.playerActionMeter.isChanneling)
		{
			paused = true;
			animator.Play("TofuBubble_mini_loop");
			OnMinimized();
		}
		StartCoroutine("StartRoulette");
	}

	public void ForceTofuType(TofuType type)
	{
		forceTofu = true;
		forceTofuType = type;
	}

	private void HandleOffsetInStart()
	{
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("CameraMask1"));
	}

	private void HandleOffsetOutComplete()
	{
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("Buildings"));
	}

	private IEnumerator StartRoulette()
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_bubbleappear", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0f, loop: false, ignoreLastPlayTime: true);
		transform.localScale = Vector3.zero;
		transform.DOScale(scale.y, 0.1f).OnComplete(() =>
		{
			transform.DOPunchScale(scale, 0.2f);
		});
		spinning = true;
		float nextIconWaitTime = 0.08f;
		currentIndex = 0;
		previousIndex = 0;
		audioSource = GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_rouletteloop", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0f, loop: false, ignoreLastPlayTime: true);
		List<TofuSettings.TofuDropWeight> tofuDropWeights = new List<TofuSettings.TofuDropWeight>();
		int settingsLen = GameManager.instance.gameSettings.tofuSettings.tofuDropWeights.Count;
		for (int num = 0; num < settingsLen; num++)
		{
			tofuDropWeights.Add(new TofuSettings.TofuDropWeight(GameManager.instance.gameSettings.tofuSettings.tofuDropWeights[num]));
		}
		int count = tofuDropWeights.Count;
		int weightSum = 0;
		bool allowBatteryTofu = BattleController.instance.playerAccessoryButton.isInitialized;
		foreach (KeyValuePair<TofuType, int> offset in tofuDropWeightOffsets)
		{
			TofuSettings.TofuDropWeight tofuDropWeight = tofuDropWeights.Find((TofuSettings.TofuDropWeight dropWeight) => dropWeight.tofuType == offset.Key);
			if (tofuDropWeight != null)
			{
				tofuDropWeight.weight += offset.Value;
			}
		}
		for (int num2 = 0; num2 < count; num2++)
		{
			if (tofuDropWeights[num2].tofuType != TofuType.Battery || allowBatteryTofu)
			{
				weightSum += tofuDropWeights[num2].weight;
			}
		}
		while (spinning)
		{
			if (paused)
			{
				if (audioSource != null)
				{
					audioSource.volume = 0.2f;
				}
				yield return new WaitForEndOfFrame();
			}
			else if (audioSource != null)
			{
				audioSource.volume = 1f;
			}
			previousIndex = currentIndex;
			tofuSprite.SetSprite(tofuSpriteNames[currentIndex]);
			if (!paused)
			{
				animator.Play("TofuBubble_random_bump");
			}
			UnityEngine.Random.State prevState = UnityEngine.Random.state;
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.InitState(Environment.TickCount);
			}
			while (previousIndex == currentIndex)
			{
				int num3 = UnityEngine.Random.Range(0, weightSum);
				for (int num4 = 0; num4 < count; num4++)
				{
					TofuSettings.TofuDropWeight tofuDropWeight2 = tofuDropWeights[num4];
					if (tofuDropWeight2.tofuType != TofuType.Battery || allowBatteryTofu)
					{
						if (num3 < tofuDropWeight2.weight)
						{
							currentIndex = (int)tofuDropWeight2.tofuType;
							break;
						}
						num3 -= tofuDropWeight2.weight;
					}
				}
			}
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				UnityEngine.Random.state = prevState;
			}
			timeout -= nextIconWaitTime;
			if (timeout <= 0f)
			{
				Pressed();
			}
			yield return new WaitForSeconds(nextIconWaitTime);
		}
	}

	private void Hide()
	{
		if (!paused)
		{
			paused = true;
			animator.Play("TofuBubble_mini_intro");
			OnMinimized();
		}
	}

	private void Show()
	{
		if (paused)
		{
			animator.Play("TofuBubble_mini_outro");
			paused = false;
			OnMaximized();
		}
	}

	private void Update()
	{
		if (followTrans != null)
		{
			transform.position = Vector3.Lerp(transform.position, followTrans.position + followOffset, Time.deltaTime * 12f);
		}
		transform.LookAt(battleController.playerBattleCamera.camera.transform);
		if (used)
		{
			return;
		}
		if (battleController.waiting || battleController.playerActionMeter.isCasting || battleController.playerActionMeter.isChanneling)
		{
			if (BattleController.battleDefinition.playerAIType != PlayerAIType.Network || TT2NetManager.Instance.myNetworkPlayer.QueuedActions.Count == 0)
			{
				Hide();
			}
			return;
		}
		Show();
		if (Input.GetMouseButtonDown(0) && !battleController.playerPortraits.IsSelectMode && BattleController.instance.batteryGUI.allowItemPickups)
		{
			Ray ray = battleController.playerBattleCamera.camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hitInfo) && hitInfo.collider.transform == transform)
			{
				Pressed();
			}
		}
	}

	private void Pressed()
	{
		if (spinning)
		{
			StopCoroutine("StartRoulette");
			if (audioSource != null)
			{
				audioSource.Stop();
				audioSource = null;
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_roulettestop", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0f, loop: false, ignoreLastPlayTime: true);
			if (forceTofu)
			{
				currentIndex = (int)forceTofuType;
				forceTofu = false;
			}
			else
			{
				List<TofuSettings.TofuDropWeight> list = new List<TofuSettings.TofuDropWeight>();
				int count = GameManager.instance.gameSettings.tofuSettings.tofuDropWeights.Count;
				for (int i = 0; i < count; i++)
				{
					list.Add(new TofuSettings.TofuDropWeight(GameManager.instance.gameSettings.tofuSettings.tofuDropWeights[i]));
				}
				int count2 = list.Count;
				int num = 0;
				bool isInitialized = BattleController.instance.playerAccessoryButton.isInitialized;
				foreach (KeyValuePair<TofuType, int> offset in tofuDropWeightOffsets)
				{
					TofuSettings.TofuDropWeight tofuDropWeight = list.Find((TofuSettings.TofuDropWeight dropWeight) => dropWeight.tofuType == offset.Key);
					if (tofuDropWeight != null)
					{
						tofuDropWeight.weight += offset.Value;
					}
				}
				for (int num2 = 0; num2 < count2; num2++)
				{
					if (list[num2].tofuType != TofuType.Battery || isInitialized)
					{
						num += list[num2].weight;
					}
				}
				UnityEngine.Random.State state = UnityEngine.Random.state;
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					UnityEngine.Random.InitState(Environment.TickCount);
				}
				int num3 = UnityEngine.Random.Range(0, num);
				int num4 = 0;
				for (int num5 = 0; num5 < count2; num5++)
				{
					TofuSettings.TofuDropWeight tofuDropWeight2 = list[num5];
					if ((tofuDropWeight2.tofuType != TofuType.Battery || isInitialized) && tofuDropWeight2.weight != 0)
					{
						num4 += tofuDropWeight2.weight;
						if (num3 < num4)
						{
							currentIndex = (int)tofuDropWeight2.tofuType;
							break;
						}
					}
				}
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					UnityEngine.Random.state = state;
				}
			}
			tofuSprite.SetSprite(tofuSpriteNames[currentIndex]);
			if (!paused)
			{
				animator.Play("TofuBubble_random_bump");
			}
			spinning = false;
		}
		else if (!spinning && !used && !paused)
		{
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				StartCoroutine(WaitForConfirmation());
				return;
			}
			used = true;
			animator.Play("TofuBubble_select");
			UseTofu();
		}
	}

	private IEnumerator WaitForConfirmation()
	{
		BattleController.instance.ForceStartWaiting(useDisabledVisuals: false);
		TT2NetPlayerAction playerAction = TT2NetManager.SendTofuUsed(currentIndex, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		BattleController.instance.ForceEndWaiting();
		if (playerAction.isConfirmed)
		{
			used = true;
			animator.Play("TofuBubble_select");
			UseTofu(playerAction.seed);
		}
	}

	private void UseTofu(int seed = -1)
	{
		if (currentCharacter != null)
		{
			if (currentCharacter.characterDefinition.sounds.useTofuSounds.soundPaths.Count > 0 && UnityEngine.Random.value < currentCharacter.characterDefinition.sounds.useTofuSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(currentCharacter.characterDefinition.sounds.useTofuSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			TofuEffect tofuEffect = new GameObject("TofuEffect - " + battleController.playerCharacter.characterDefinition.characterName).AddComponent<TofuEffect>();
			tofuEffect.StartEffectForTarget((TofuType)currentIndex, battleController.playerCharacter, this, seed);
			CommunicatorManager.instance.UpdateMissionWithTargetMessage("TofuUsed");
			BattleObjectManager.instance.RemoveObject(gameObject);
			ObjectPool.PoolObject(gameObject, 1f);
			battleController.tofuUsed = true;
			OnTofuUsed(currentCharacter, (TofuType)currentIndex);
		}
	}

	private void HandleCharacterSwapOutStart(Character character)
	{
		if (character == currentCharacter)
		{
			if (currentCharacter != null)
			{
				currentCharacter.Transformed -= HandleTransformed;
				currentCharacter.DeathCharacter -= HandleDeathCharacter;
			}
			currentCharacter = null;
			followTrans = null;
		}
	}

	private void HandleCharacterSwappedIn(Character character)
	{
		if (character == battleController.playerCharacter.character)
		{
			if (currentCharacter != null)
			{
				currentCharacter.Transformed -= HandleTransformed;
				currentCharacter.DeathCharacter -= HandleDeathCharacter;
			}
			currentCharacter = character;
			currentCharacter.Transformed += HandleTransformed;
			currentCharacter.DeathCharacter += HandleDeathCharacter;
			followTrans = battleController.playerCharacter.tofuAnchor;
			followOffset = currentCharacter.tofuBubbleOffsetPosition;
		}
	}

	private void HandleTransformed()
	{
		currentCharacter.Transformed -= HandleTransformed;
		currentCharacter.DeathCharacter -= HandleDeathCharacter;
		currentCharacter = null;
		followTrans = null;
		battleController.WaitingEnded += HandleWaitingEndedPostTransform;
	}

	private void HandleDeathCharacter(Character character)
	{
		character.Transformed -= HandleTransformed;
		character.DeathCharacter -= HandleDeathCharacter;
	}

	private void HandleWaitingEndedPostTransform()
	{
		battleController.WaitingEnded -= HandleWaitingEndedPostTransform;
		currentCharacter = battleController.playerCharacter.character;
		currentCharacter.Transformed += HandleTransformed;
		currentCharacter.DeathCharacter += HandleDeathCharacter;
		followTrans = battleController.playerCharacter.tofuAnchor;
		followOffset = currentCharacter.tofuBubbleOffsetPosition;
	}

	private void HandleBattleEnded()
	{
		if (currentCharacter != null)
		{
			currentCharacter.Transformed -= HandleTransformed;
			currentCharacter.DeathCharacter -= HandleDeathCharacter;
			currentCharacter = null;
		}
		BattleObjectManager.instance.RemoveObject(gameObject);
		ObjectPool.PoolObject(gameObject);
	}

	protected void OnMinimized()
	{
		if (Minimized != null)
		{
			Minimized();
		}
	}

	protected void OnMaximized()
	{
		if (Maximized != null)
		{
			Maximized();
		}
	}

	protected void OnTofuUsed(Character character, TofuType tofuType)
	{
		if (TofuUsed != null)
		{
			TofuUsed();
		}
		EventMessenger.Instance.Raise(new TofuUsedEvent(character, tofuType));
	}

	public void ResetObject()
	{
		timeout = 3f;
		spinning = false;
		used = false;
		paused = false;
		forceTofu = false;
		gameObject.SetLayerRecursively(LayerMask.NameToLayer("Buildings"));
	}
}
