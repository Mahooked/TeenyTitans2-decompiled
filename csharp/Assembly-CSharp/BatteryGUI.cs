using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BatteryGUI : MonoBehaviour
{
	public BoxCollider2D collider;

	public TextMeshPro totalText;

	public tk2dSprite buttonSprite;

	public GameObject batteryDragObj;

	public Transform batteryTargetPos;

	public Color activatedColor = Color.white;

	public Color deactivatedColor = new Color(131f / 255f, 131f / 255f, 131f / 255f, 1f);

	public Color deactivatedFaceColor = new Color(71f / 255f, 76f / 255f, 82f / 255f, 1f);

	public Color deactivatedOutlineColor = new Color(55f / 255f, 37f / 255f, 37f / 255f, 1f);

	public List<AbilityDataBatteryEffectType> abilityBatteryEffects = new List<AbilityDataBatteryEffectType>();

	private int total;

	private Color startingFaceColor;

	private Color startingOutlineColor;

	private Dictionary<string, int> characterAbilityBatteryCount = new Dictionary<string, int>();

	private BonusDropSettings bonusDropSettings;

	private bool touchingDown;

	private Vector3 touchDownPosition = Vector3.zero;

	private bool showing;

	public bool stopTimeInBatteryMode { get; set; }

	public bool allowItemPickups => !isUsingBattery || (isUsingBattery && !stopTimeInBatteryMode);

	public bool isUsingBattery { get; private set; }

	public event Action BatteryModeEntered;

	public event Action BatteryModeExited;

	public event Action BatteryDragModeEntered;

	public event Action BatteryDragModeExited;

	private void Awake()
	{
		total = Player.instance.batteries;
		bonusDropSettings = GameManager.instance.gameSettings.bonusDropSettings;
		SetTotalText(total.ToString());
		transform.localPosition = new Vector3(350f, transform.localPosition.y, transform.localPosition.z);
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		BattleController.instance.playerActionMeter.Paused += HandleActionMeterPaused;
		BattleController.instance.playerActionMeter.Resumed += HandleActionMeterResumed;
		Player.instance.BatteriesAdded += HandleBatteriesAdded;
		Player.instance.BatteriesRemoved += HandleBatteriesRemoved;
		startingFaceColor = totalText.faceColor;
		startingOutlineColor = totalText.fontSharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
	}

	private void OnDestroy()
	{
		if (BattleController.instance != null && BattleController.instance.playerActionMeter != null)
		{
			BattleController.instance.playerActionMeter.Paused -= HandleActionMeterPaused;
			BattleController.instance.playerActionMeter.Resumed -= HandleActionMeterResumed;
		}
		Player.instance.BatteriesAdded -= HandleBatteriesAdded;
		Player.instance.BatteriesRemoved -= HandleBatteriesRemoved;
	}

	private void Update()
	{
		if (touchingDown)
		{
			Vector3 vector = GUICamera.instance.camera.ScreenToWorldPoint(Input.mousePosition);
			float num = Vector3.Distance(touchDownPosition, vector);
			if (num > 15f)
			{
				batteryDragObj.SetActive(value: true);
			}
			if (batteryDragObj.activeSelf)
			{
				batteryDragObj.transform.position = vector;
			}
		}
	}

	public void Show()
	{
		if (!showing)
		{
			showing = true;
			transform.DOKill();
			transform.DOLocalMoveX(-45f, 0.33f).SetEase(Ease.OutBack);
		}
	}

	private void Hide()
	{
		if (showing)
		{
			showing = false;
			transform.DOKill();
			transform.DOLocalMoveX(350f, 0.33f).SetEase(Ease.InBack);
		}
	}

	private void HandleActionMeterPaused()
	{
		if (!isUsingBattery)
		{
			if (collider != null)
			{
				collider.enabled = false;
			}
			buttonSprite.color = deactivatedColor;
			totalText.faceColor = deactivatedFaceColor;
			totalText.outlineColor = deactivatedOutlineColor;
		}
	}

	private void HandleActionMeterResumed()
	{
		if (collider != null)
		{
			collider.enabled = true;
		}
		buttonSprite.color = activatedColor;
		totalText.faceColor = startingFaceColor;
		totalText.outlineColor = startingOutlineColor;
	}

	private void HandleBatteriesAdded(int batteries)
	{
		if (batteries > 0)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void HandleBatteriesRemoved(int batteries)
	{
		total = Player.instance.batteries;
		SetTotalText(total.ToString());
		if (total <= 0)
		{
			Hide();
		}
	}

	public void AddToBatteries(int amount = 1)
	{
		Show();
		AddToTotal(amount);
	}

	private void AddToTotal(int amount = 1)
	{
		total = Player.instance.batteries + amount;
		SetTotalText(total.ToString());
		PlayBumpAnimation();
		if (amount >= 0)
		{
			Player.instance.AddBattery();
		}
		else if (amount < 0)
		{
			Player.instance.RemoveBatteries(-amount);
		}
	}

	private void PlayBumpAnimation()
	{
	}

	public void BumpAmount(int amount)
	{
		total += amount;
		SetTotalText(total.ToString());
	}

	private void SetTotalText(string newText)
	{
		totalText.text = newText;
	}

	public void UseBatteryAccessory(AccessoryBattleButton accessoryBattleButton)
	{
		Player.instance.RemoveBatteries(1);
		StopUsingBatteryMode();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_placeonaccessory", GFAudioManager.SFX_GROUP, string.Empty);
	}

	public void UseBatteryHeal()
	{
		ActionMeter playerActionMeter = BattleController.instance.playerActionMeter;
		if (BattleController.instance.isBattleRunning && playerActionMeter.character != null && playerActionMeter.character.character != null && !playerActionMeter.character.character.isDead)
		{
			int amount = Mathf.Max(bonusDropSettings.heartHealMin, Mathf.CeilToInt((float)playerActionMeter.character.character.maxHitPoints * bonusDropSettings.heartHealPercent));
			playerActionMeter.character.character.DoHeal(new HealResult(null, playerActionMeter.character.character, amount));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_use_heal", GFAudioManager.SFX_GROUP, string.Empty);
			Player.instance.RemoveBatteries(1);
		}
		StopUsingBatteryMode();
	}

	public void UseBatteryOnAbilityButton(AbilityButton button)
	{
		if (!BattleController.instance.isBattleRunning)
		{
		}
	}

	private void AddBatteryToCharacterAbility(ActionMeter targetMeter, AbilityButton button)
	{
		string key = targetMeter.character.character.characterDefinition.characterName + button.abilityData.uid;
		if (characterAbilityBatteryCount.ContainsKey(key))
		{
			int num = characterAbilityBatteryCount[key];
			num++;
			characterAbilityBatteryCount[key] = num;
			StatusEffectManager.instance.GetAbilityBatteryBuffOnButton(targetMeter, button.abilityData)?.UpdateBuffWithBatteryLevel();
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_placeonability", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			characterAbilityBatteryCount.Add(key, 1);
			StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new AbilityBatteryBuff(targetMeter.character.character, targetMeter, button));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_placeonability", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public int GetBatteryCountForAbility(Character character, AbilityData abilityData)
	{
		int result = 0;
		string key = character.characterDefinition.characterName + abilityData.uid;
		if (characterAbilityBatteryCount.ContainsKey(key))
		{
			result = Mathf.Clamp(characterAbilityBatteryCount[key], 0, 1);
		}
		return result;
	}

	public void RemoveAllBatteriesForCharacterAbility(Character character, AbilityButton button)
	{
		string key = character.characterDefinition.characterName + button.abilityData.uid;
		characterAbilityBatteryCount.Remove(key);
	}

	public void StopUsingBatteryMode()
	{
		if (isUsingBattery)
		{
			Debug.Log("<color=red>STOP</color> using battery mode");
			isUsingBattery = false;
			if (stopTimeInBatteryMode)
			{
				Time.timeScale = 1f;
				BattleController.instance.playerActionMeter.Resume();
			}
			if (BatteryModeExited != null)
			{
				BatteryModeExited();
			}
		}
	}

	private void HandleOnDown()
	{
		touchDownPosition = GUICamera.instance.ScreenToWorldPoint(Input.mousePosition);
		touchingDown = true;
	}

	private void HandleOnUp()
	{
	}

	private void HandleOnRelease()
	{
		touchingDown = false;
		if (batteryDragObj.activeSelf)
		{
			batteryDragObj.SetActive(value: false);
			batteryDragObj.transform.parent = batteryTargetPos;
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 5f;
			Vector2 point = GUICamera.instance.ScreenToWorldPoint(mousePosition);
			bool flag = BattleController.instance.playerAccessoryButton.touchCollider.enabled;
			BattleController.instance.playerAccessoryButton.touchCollider.enabled = true;
			Collider2D[] array = Physics2D.OverlapPointAll(point);
			if (array.Length > 0)
			{
				Collider2D[] array2 = array;
				foreach (Collider2D collider2D in array2)
				{
					PortraitSwitcher componentInParent = collider2D.gameObject.GetComponentInParent<PortraitSwitcher>();
					if (componentInParent != null && componentInParent.selectedCharacter == BattleController.instance.playerCharacter.character)
					{
						UseBatteryHeal();
					}
					AccessoryBattleButton component = collider2D.gameObject.GetComponent<AccessoryBattleButton>();
					if (component != null)
					{
						UseBatteryAccessory(component);
					}
				}
			}
			if (isUsingBattery)
			{
				StopUsingBatteryMode();
			}
			return;
		}
		batteryDragObj.SetActive(value: false);
		batteryDragObj.transform.parent = batteryTargetPos;
		if (!isUsingBattery && Player.instance.batteries > 0)
		{
			Debug.Log("<color=green>START</color> using battery mode");
			isUsingBattery = true;
			if (stopTimeInBatteryMode)
			{
				BattleController.instance.playerActionMeter.Pause();
				Time.timeScale = 0f;
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_batte_abilityavailable", GFAudioManager.SFX_GROUP, string.Empty);
			if (BatteryModeEntered != null)
			{
				BatteryModeEntered();
			}
		}
		else
		{
			StopUsingBatteryMode();
		}
	}
}
