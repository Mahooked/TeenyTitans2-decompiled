using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AbilityCustomizationGUI : MonoBehaviour
{
	private static Dictionary<CharacterClass, int> classIconSpriteIndices = new Dictionary<CharacterClass, int>
	{
		{
			CharacterClass.Beast,
			14
		},
		{
			CharacterClass.Cute,
			13
		},
		{
			CharacterClass.DarkArts,
			10
		},
		{
			CharacterClass.MartialArts,
			11
		},
		{
			CharacterClass.Super,
			15
		},
		{
			CharacterClass.Tech,
			16
		},
		{
			CharacterClass.None,
			12
		}
	};

	public AbilityCustomizationAbilityButton[] abilityButtons;

	public AbilityCustomizationDropSlot[] dropSlots;

	public tk2dUIItem uiItem;

	public SimpleTextButton buyButton;

	public SimpleTextButton factoryResetButton;

	public tk2dUIItem closeButton;

	public tk2dSprite closeButtonSprite;

	public tk2dUIItem tintButton;

	public TextMeshPro nameText;

	public float startDragDistance = 0.5f;

	public float swipeDistance = 0.5f;

	public float portraitDragDelay = 0.5f;

	[Tooltip("Number of seconds to wait before shifting abilities when dragging an ability over a drop slot that is already occupied")]
	public float swapAbilityDelay = 0.5f;

	public float swapAbilityTweenDuration = 0.15f;

	public bool dragEnabled = true;

	public bool closeOnTintClick;

	public Camera camera;

	private bool isDown;

	private Vector3 downPosition;

	private Vector3 curPosition;

	private Vector3 prevPosition;

	private AbilityCustomizationAbilityButton downAbility;

	private float abilityDragCounter;

	private bool isDraggingAbility;

	private Vector3 abilityDragOffset;

	private AbilityCustomizationAbilityButton originalDragAbility;

	private AbilityCustomizationAbilityButton dragAbility;

	private AbilityCustomizationDropSlot curDropSlot;

	private float curDropSlotTimer;

	private AbilityInfoBox infoBox;

	private Character _character;

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			SetCharacter(value);
		}
	}

	public event Action<AbilityCustomizationAbilityButton> DragStarted;

	private void Awake()
	{
		AbilityCustomizationAbilityButton abilityCustomizationAbilityButton = null;
		for (int i = 0; i < abilityButtons.Length; i++)
		{
			abilityCustomizationAbilityButton = abilityButtons[i];
			abilityCustomizationAbilityButton.uiItem.OnDownUIItem += HandleOnDownPortrait;
		}
		buyButton.textMesh.text = $"{GameManager.instance.gameSettings.abilityCustomizationCost}<sprite name=\"icon_currency_coin\" tint=1>";
		buyButton.SetEnabled(enabled: false);
	}

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
		uiItem.OnRelease += HandleOnRelease;
		tintButton.OnClick += HandleTintClick;
		factoryResetButton.uiItem.OnClick += HandleFactoryResetClick;
		buyButton.uiItem.OnClick += HandleBuyClick;
		closeButton.OnClick += Close;
		if (TitanTowerController.instance != null)
		{
			TitanTowerController.instance.currencyGUI.backing.gameObject.SetActive(value: false);
			TitanTowerController.instance.currencyGUI.Show();
		}
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnRelease -= HandleOnRelease;
		tintButton.OnClick -= HandleTintClick;
		factoryResetButton.uiItem.OnClick -= HandleFactoryResetClick;
		buyButton.uiItem.OnClick -= HandleBuyClick;
		closeButton.OnClick -= Close;
		if (TitanTowerController.instance != null)
		{
			TitanTowerController.instance.currencyGUI.backing.gameObject.SetActive(value: true);
			TitanTowerController.instance.currencyGUI.Hide();
		}
		buyButton.isEnabled = false;
	}

	private void Update()
	{
		if (infoBox != null && Input.GetMouseButtonUp(0))
		{
			Collider2D collider2D = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(Input.mousePosition));
			if (collider2D != null)
			{
				tk2dUIItem component = collider2D.GetComponent<tk2dUIItem>();
				if (component == null || Array.Find(abilityButtons, (AbilityCustomizationAbilityButton obj) => obj.abilityData == infoBox.abilityData).uiItem != component)
				{
					HideInfoBox();
				}
			}
			else
			{
				HideInfoBox();
			}
		}
		if (isDown)
		{
			curPosition = Input.mousePosition;
			Vector2 vector = new Vector2(curPosition.x - downPosition.x, curPosition.y - downPosition.y);
			if (downAbility != null)
			{
				abilityDragCounter += Time.unscaledDeltaTime;
				if (dragEnabled && (abilityDragCounter >= portraitDragDelay || vector.magnitude / Screen.dpi >= startDragDistance))
				{
					downAbility.uiItem.Release(ignoreParent: true);
					HideInfoBox();
					isDraggingAbility = true;
					isDown = false;
					AbilityCustomizationDropSlot abilityCustomizationDropSlot = null;
					for (int num = 0; num < dropSlots.Length; num++)
					{
						if (dropSlots[num].button == downAbility)
						{
							abilityCustomizationDropSlot = dropSlots[num];
							break;
						}
					}
					if (abilityCustomizationDropSlot != null)
					{
						abilityCustomizationDropSlot.SetButton(null);
						abilityCustomizationDropSlot.isHighlighted = false;
						curDropSlot = abilityCustomizationDropSlot;
						curDropSlot.transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutSine);
						curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0.15f);
						for (int num2 = 0; num2 < abilityButtons.Length; num2++)
						{
							if (abilityButtons[num2].abilityData == downAbility.abilityData)
							{
								originalDragAbility = abilityButtons[num2];
								break;
							}
						}
						dragAbility = downAbility;
						dragAbility.transform.SetParent(null, worldPositionStays: true);
						dragAbility.transform.position += new Vector3(0f, 0f, -10f);
						dragAbility.uiItem.transform.localScale = Vector3.one;
						Vector3 vector2 = camera.ScreenToWorldPoint(curPosition);
						abilityDragOffset = new Vector3(dragAbility.transform.position.x - vector2.x, dragAbility.transform.position.y - vector2.y);
						CheckDropSlots();
					}
					else
					{
						originalDragAbility = downAbility;
						dragAbility = UnityEngine.Object.Instantiate(downAbility);
						dragAbility.originalPosition = downAbility.originalPosition;
						dragAbility.position = downAbility.position;
						dragAbility.Init(downAbility.character, downAbility.abilityData);
						dragAbility.transform.position = downAbility.transform.position + new Vector3(0f, 0f, -10f);
						dragAbility.transform.localScale = Vector3.one;
						dragAbility.uiItem.transform.localScale = Vector3.one;
						Vector3 vector3 = camera.ScreenToWorldPoint(curPosition);
						abilityDragOffset = new Vector3(dragAbility.transform.position.x - vector3.x, dragAbility.transform.position.y - vector3.y);
						originalDragAbility.isEnabled = false;
					}
					if (DragStarted != null)
					{
						DragStarted(dragAbility);
					}
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_generalbuttons", GFAudioManager.SFX_GROUP, string.Empty);
				}
			}
			prevPosition = curPosition;
		}
		else
		{
			if (!isDraggingAbility)
			{
				return;
			}
			curPosition = Input.mousePosition;
			Vector3 vector4 = camera.ScreenToWorldPoint(curPosition);
			Vector3 position = dragAbility.transform.position;
			position.x = vector4.x + abilityDragOffset.x;
			position.y = vector4.y + abilityDragOffset.y;
			dragAbility.transform.position = position;
			prevPosition = curPosition;
			Collider2D[] array = Physics2D.OverlapPointAll(dragAbility.transform.position, LayerMask.GetMask("UI4"));
			int num3 = array.Length;
			AbilityCustomizationDropSlot abilityCustomizationDropSlot2 = null;
			for (int num4 = 0; num4 < num3; num4++)
			{
				abilityCustomizationDropSlot2 = array[num4].GetComponent<AbilityCustomizationDropSlot>();
				if (!(abilityCustomizationDropSlot2 != null))
				{
					continue;
				}
				if (abilityCustomizationDropSlot2 != curDropSlot)
				{
					if (curDropSlot != null)
					{
						curDropSlot.transform.DOScale(1f, 0.1f).SetEase(Ease.OutSine);
						curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0f);
					}
					curDropSlot = abilityCustomizationDropSlot2;
					curDropSlot.transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutSine);
					if (curDropSlot.button == null)
					{
						curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0.15f);
					}
					curDropSlotTimer = 0f;
				}
				break;
			}
			if (abilityCustomizationDropSlot2 == null && curDropSlot != null)
			{
				curDropSlot.transform.DOScale(1f, 0.1f).SetEase(Ease.OutSine);
				curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0f);
				curDropSlot = null;
			}
			else
			{
				if (!(curDropSlot != null))
				{
					return;
				}
				if (curDropSlot.button != null && curDropSlotTimer > swapAbilityDelay)
				{
					bool flag = dragAbility.transform.position.x > curDropSlot.transform.position.x;
					int num5 = -1;
					int num6 = -1;
					for (int num7 = 0; num7 < dropSlots.Length; num7++)
					{
						if (dropSlots[num7] == curDropSlot)
						{
							num5 = (num6 = num7);
							break;
						}
					}
					AbilityCustomizationDropSlot abilityCustomizationDropSlot3 = null;
					if (flag)
					{
						while (num6 > 0)
						{
							num6--;
							if (dropSlots[num6].button == null)
							{
								abilityCustomizationDropSlot3 = dropSlots[num6];
								break;
							}
						}
						if (abilityCustomizationDropSlot3 != null)
						{
							AbilityCustomizationAbilityButton abilityCustomizationAbilityButton = null;
							for (; num6 < num5; num6++)
							{
								abilityCustomizationAbilityButton = dropSlots[num6 + 1].button;
								dropSlots[num6 + 1].SetButton(null);
								dropSlots[num6 + 1].isHighlighted = false;
								abilityCustomizationAbilityButton.position = _character.characterDefinition.orderedSlots[num6].position;
								abilityCustomizationAbilityButton.ForceUpdate();
								AbilityCustomizationDropSlot slot = dropSlots[num6];
								slot.SetButton(abilityCustomizationAbilityButton);
								abilityCustomizationAbilityButton.transform.DOLocalMove(slot.transform.localPosition + new Vector3(0f, 1f, -2f), swapAbilityTweenDuration).SetEase(Ease.OutSine).OnComplete(() =>
								{
									OnAbilityReachedSlot(slot);
								});
							}
							curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0.15f);
						}
					}
					else
					{
						while (num6 < dropSlots.Length - 1)
						{
							num6++;
							if (dropSlots[num6].button == null)
							{
								abilityCustomizationDropSlot3 = dropSlots[num6];
								break;
							}
						}
						if (abilityCustomizationDropSlot3 != null)
						{
							AbilityCustomizationAbilityButton abilityCustomizationAbilityButton2 = null;
							while (num6 > num5)
							{
								abilityCustomizationAbilityButton2 = dropSlots[num6 - 1].button;
								dropSlots[num6 - 1].SetButton(null);
								dropSlots[num6 - 1].isHighlighted = false;
								abilityCustomizationAbilityButton2.position = _character.characterDefinition.orderedSlots[num6].position;
								abilityCustomizationAbilityButton2.ForceUpdate();
								AbilityCustomizationDropSlot slot2 = dropSlots[num6];
								slot2.SetButton(abilityCustomizationAbilityButton2);
								abilityCustomizationAbilityButton2.transform.DOLocalMove(slot2.transform.localPosition + new Vector3(0f, 1f, -2f), swapAbilityTweenDuration).SetEase(Ease.OutSine).OnComplete(() =>
								{
									OnAbilityReachedSlot(slot2);
								});
								num6--;
							}
							curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0.15f);
						}
					}
				}
				curDropSlotTimer += Time.deltaTime;
			}
		}
	}

	private void HandleOnDown()
	{
		isDown = true;
		downPosition = (curPosition = (prevPosition = Input.mousePosition));
	}

	protected virtual void HandleOnRelease()
	{
		isDown = false;
		if (isDraggingAbility)
		{
			OnDragFinished(dragAbility, originalDragAbility);
			isDraggingAbility = false;
			downAbility = null;
			UnityEngine.Object.Destroy(dragAbility.gameObject);
			dragAbility = null;
			originalDragAbility = null;
			CheckDropSlots();
		}
		else
		{
			if (!(downAbility != null) || !(Vector3.Distance(downPosition, Input.mousePosition) / Screen.dpi < 0.2f))
			{
				return;
			}
			AbilityCustomizationDropSlot abilityCustomizationDropSlot = null;
			for (int i = 0; i < dropSlots.Length; i++)
			{
				if (dropSlots[i].button == downAbility)
				{
					abilityCustomizationDropSlot = dropSlots[i];
					break;
				}
			}
			if (abilityCustomizationDropSlot != null)
			{
				abilityCustomizationDropSlot.SetButton(null);
				abilityCustomizationDropSlot.isHighlighted = false;
				for (int j = 0; j < abilityButtons.Length; j++)
				{
					if (abilityButtons[j].abilityData == downAbility.abilityData)
					{
						abilityButtons[j].SetEnabled(enabled: true);
						break;
					}
				}
				UnityEngine.Object.Destroy(downAbility.gameObject);
				CheckDropSlots();
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_unequipchar", GFAudioManager.SFX_GROUP, string.Empty);
			}
			else
			{
				ShowInfoBox(downAbility);
			}
			downAbility = null;
		}
	}

	private void HandleOnDownPortrait(tk2dUIItem obj)
	{
		downAbility = obj.GetComponentInParent<AbilityCustomizationAbilityButton>();
		abilityDragCounter = 0f;
	}

	private void HandleTintClick()
	{
		if (closeOnTintClick)
		{
			Close();
		}
	}

	private void HandleFactoryResetClick()
	{
		AbilityCustomizationAbilityButton abilityCustomizationAbilityButton = null;
		for (int i = 0; i < dropSlots.Length; i++)
		{
			if (!(dropSlots[i].button != null))
			{
				continue;
			}
			abilityCustomizationAbilityButton = dropSlots[i].button;
			dropSlots[i].SetButton(null);
			dropSlots[i].isHighlighted = false;
			for (int j = 0; j < abilityButtons.Length; j++)
			{
				if (abilityButtons[j].abilityData == abilityCustomizationAbilityButton.abilityData)
				{
					abilityButtons[j].SetEnabled(enabled: true);
					break;
				}
			}
			UnityEngine.Object.Destroy(abilityCustomizationAbilityButton.gameObject);
		}
		abilityCustomizationAbilityButton = null;
		for (int k = 0; k < _character.characterDefinition.orderedSlots.Count; k++)
		{
			abilityCustomizationAbilityButton = null;
			for (int l = 0; l < abilityButtons.Length; l++)
			{
				if (abilityButtons[l].abilityData != null && abilityButtons[l].abilityData.uid == _character.characterDefinition.orderedSlots[k].abilityId)
				{
					abilityCustomizationAbilityButton = abilityButtons[l];
					break;
				}
			}
			if (abilityCustomizationAbilityButton != null)
			{
				AbilityCustomizationAbilityButton abilityCustomizationAbilityButton2 = UnityEngine.Object.Instantiate(abilityCustomizationAbilityButton);
				abilityCustomizationAbilityButton2.overrideNumberColors = true;
				abilityCustomizationAbilityButton2.originalPosition = abilityCustomizationAbilityButton.position;
				abilityCustomizationAbilityButton2.position = _character.characterDefinition.orderedSlots[k].position;
				abilityCustomizationAbilityButton2.Init(abilityCustomizationAbilityButton.character, abilityCustomizationAbilityButton.abilityData);
				abilityCustomizationAbilityButton2.transform.SetParent(dropSlots[k].transform.parent, worldPositionStays: true);
				abilityCustomizationAbilityButton2.transform.position = dropSlots[k].transform.position + new Vector3(0f, 1f, -2f);
				abilityCustomizationAbilityButton2.transform.localRotation = Quaternion.identity;
				abilityCustomizationAbilityButton2.transform.localScale = Vector3.one;
				abilityCustomizationAbilityButton2.uiItem.transform.localScale = Vector3.one;
				abilityCustomizationAbilityButton2.uiItem.OnDownUIItem += HandleOnDownPortrait;
				dropSlots[k].SetButton(abilityCustomizationAbilityButton2);
				dropSlots[k].isHighlighted = true;
				abilityCustomizationAbilityButton.isEnabled = false;
			}
		}
		CheckDropSlots();
	}

	private void HandleBuyClick()
	{
		if (Player.instance.currency < GameManager.instance.gameSettings.abilityCustomizationCost)
		{
			return;
		}
		Player.instance.RemoveCurrency(GameManager.instance.gameSettings.abilityCustomizationCost);
		List<AbilityData> list = new List<AbilityData>();
		for (int i = 0; i < dropSlots.Length; i++)
		{
			if (dropSlots[i].button != null)
			{
				list.Add(dropSlots[i].button.abilityData);
			}
		}
		_character.SetNewAbilityOrder(list);
		for (int j = 0; j < dropSlots.Length; j++)
		{
			if (dropSlots[j].button != null)
			{
				UnityEngine.Object.Destroy(dropSlots[j].button.gameObject);
				dropSlots[j].SetButton(null);
				dropSlots[j].isHighlighted = false;
			}
		}
		for (int k = 0; k < abilityButtons.Length; k++)
		{
			abilityButtons[k].isEnabled = true;
		}
		CheckDropSlots();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_install", GFAudioManager.SFX_GROUP, string.Empty);
		AchievementsDatabase.UnlockAchievementId("TT2_TEENYTINKERING", 1);
	}

	public void SetCharacter(Character character)
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
		_character = character;
		if (_character != null)
		{
			_character.Updated += HandleCharacterUpdated;
			HandleCharacterUpdated();
			return;
		}
		for (int i = 0; i < abilityButtons.Length; i++)
		{
			abilityButtons[i].Init(null, null);
		}
	}

	private void HandleCharacterUpdated()
	{
		nameText.text = string.Format("{0}<sprite index={1}>", Language.Get(_character.characterDefinition.characterNameKey, "NPCS,FIGS"), classIconSpriteIndices[_character.characterDefinition.characterClass]);
		int num = 0;
		for (int i = 0; i < abilityButtons.Length; i++)
		{
			AbilityCustomizationAbilityButton obj = abilityButtons[i];
			int position = _character.characterDefinition.orderedSlots[i].position;
			abilityButtons[i].originalPosition = position;
			obj.position = position;
			if (_character.abilities.Contains(GameManager.instance.abilityDatabase.GetAbility(_character.characterDefinition.orderedSlots[i].abilityId)))
			{
				abilityButtons[i].Init(_character, _character.abilities[num]);
				num++;
				dropSlots[i].isLocked = false;
			}
			else
			{
				abilityButtons[i].Init(_character, null);
				dropSlots[i].isLocked = true;
			}
		}
	}

	private void OnAbilityReachedSlot(AbilityCustomizationDropSlot slot)
	{
		slot.isHighlighted = true;
	}

	public void Close()
	{
		gameObject.SetActive(value: false);
		SetCharacter(null);
		for (int i = 0; i < dropSlots.Length; i++)
		{
			if (dropSlots[i].button != null)
			{
				UnityEngine.Object.Destroy(dropSlots[i].button.gameObject);
				dropSlots[i].SetButton(null);
				dropSlots[i].isHighlighted = false;
			}
		}
		for (int j = 0; j < abilityButtons.Length; j++)
		{
			abilityButtons[j].isEnabled = true;
		}
	}

	private void OnDragFinished(AbilityCustomizationAbilityButton dragButton, AbilityCustomizationAbilityButton originalButton)
	{
		if (curDropSlot != null)
		{
			curDropSlot.transform.DOKill(complete: true);
			curDropSlot.transform.localScale = Vector3.one;
			if (curDropSlot.button != null)
			{
				AbilityCustomizationAbilityButton button = curDropSlot.button;
				curDropSlot.SetButton(null);
				curDropSlot.isHighlighted = false;
				for (int i = 0; i < abilityButtons.Length; i++)
				{
					if (object.ReferenceEquals(abilityButtons[i].abilityData, button.abilityData))
					{
						abilityButtons[i].isEnabled = true;
						break;
					}
				}
				UnityEngine.Object.Destroy(button.gameObject);
			}
			if (!(curDropSlot.button == null))
			{
				return;
			}
			int position = 0;
			for (int j = 0; j < dropSlots.Length; j++)
			{
				if (dropSlots[j] == curDropSlot)
				{
					position = _character.characterDefinition.orderedSlots[j].position;
					break;
				}
			}
			AbilityCustomizationAbilityButton abilityCustomizationAbilityButton = UnityEngine.Object.Instantiate(dragButton);
			abilityCustomizationAbilityButton.overrideNumberColors = true;
			abilityCustomizationAbilityButton.originalPosition = originalButton.position;
			abilityCustomizationAbilityButton.position = position;
			abilityCustomizationAbilityButton.Init(dragButton.character, dragButton.abilityData);
			abilityCustomizationAbilityButton.transform.SetParent(curDropSlot.transform.parent, worldPositionStays: true);
			abilityCustomizationAbilityButton.transform.localPosition = curDropSlot.transform.localPosition + new Vector3(0f, 1f, -2f);
			abilityCustomizationAbilityButton.transform.localRotation = Quaternion.identity;
			abilityCustomizationAbilityButton.transform.localScale = Vector3.one;
			abilityCustomizationAbilityButton.uiItem.transform.localScale = Vector3.one;
			abilityCustomizationAbilityButton.uiItem.OnDownUIItem += HandleOnDownPortrait;
			curDropSlot.SetButton(abilityCustomizationAbilityButton);
			curDropSlot.isHighlighted = true;
			curDropSlot.tintAdapter.tintColor = new Color(1f, 1f, 1f, 0f);
			CheckDropSlots();
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_opencharinfopage", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			originalButton.isEnabled = true;
		}
	}

	private void HandleOnClickCustomButton(tk2dUIItem uiItem)
	{
		uiItem.OnClickUIItem -= HandleOnClickCustomButton;
		AbilityCustomizationAbilityButton componentInParent = uiItem.GetComponentInParent<AbilityCustomizationAbilityButton>();
		AbilityCustomizationDropSlot abilityCustomizationDropSlot = null;
		for (int i = 0; i < dropSlots.Length; i++)
		{
			if (object.ReferenceEquals(dropSlots[i].button, componentInParent))
			{
				abilityCustomizationDropSlot = dropSlots[i];
				break;
			}
		}
		if (!(abilityCustomizationDropSlot != null))
		{
			return;
		}
		abilityCustomizationDropSlot.SetButton(null);
		abilityCustomizationDropSlot.isHighlighted = false;
		for (int j = 0; j < abilityButtons.Length; j++)
		{
			if (abilityButtons[j].abilityData == componentInParent.abilityData)
			{
				abilityButtons[j].SetEnabled(enabled: true);
				break;
			}
		}
		UnityEngine.Object.Destroy(componentInParent.gameObject);
		CheckDropSlots();
	}

	private void ShowInfoBox(AbilityCustomizationAbilityButton button)
	{
		if (infoBox == null)
		{
			infoBox = ObjectPool.GetObjectFromResource<AbilityInfoBox>("Prefabs/GUI/AbilityCustomization/AbilityInfoBox_AbilityCustomization");
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		if (infoBox.abilityData == button.abilityData || button.abilityData == null)
		{
			HideInfoBox();
			return;
		}
		infoBox.Init(button.abilityData, _character);
		infoBox.transform.SetParent(button.transform, worldPositionStays: true);
		infoBox.transform.localPosition = Vector3.zero;
		infoBox.transform.localRotation = Quaternion.identity;
		infoBox.transform.localScale = Vector3.one;
		infoBox.transform.SetParent(null, worldPositionStays: true);
	}

	public void HideInfoBox()
	{
		if (infoBox != null)
		{
			infoBox.Init(null, null);
			ObjectPool.PoolObject(infoBox.gameObject);
			infoBox = null;
		}
	}

	private void CheckDropSlots()
	{
		bool flag = true;
		int num = 0;
		for (int i = 0; i < dropSlots.Length; i++)
		{
			if (dropSlots[i].button == null)
			{
				if (!dropSlots[i].isLocked)
				{
					flag = false;
					break;
				}
				num++;
			}
			else if (dropSlots[i].button.abilityData == abilityButtons[i].abilityData)
			{
				num++;
			}
		}
		if (flag && num < abilityButtons.Length && Player.instance.currency >= GameManager.instance.gameSettings.abilityCustomizationCost)
		{
			buyButton.isEnabled = true;
		}
		else
		{
			buyButton.isEnabled = false;
		}
	}

	public void SetCloseButtonInteractive(bool interactive)
	{
		closeButton.enabled = interactive;
		closeButtonSprite.color = ((!interactive) ? Color.gray : Color.white);
	}

	public void SetFactoryResetInteractive(bool interactive)
	{
		factoryResetButton.isEnabled = interactive;
	}
}
