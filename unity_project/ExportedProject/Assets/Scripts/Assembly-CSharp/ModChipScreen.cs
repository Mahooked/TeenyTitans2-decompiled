using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ModChipScreen : MonoBehaviour
{
	private static Dictionary<CharacterClass, string> classRestrictionKeys = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"RESTRICT_BEAST"
		},
		{
			CharacterClass.Cute,
			"RESTRICT_CUTE"
		},
		{
			CharacterClass.DarkArts,
			"RESTRICT_DARK_ARTS"
		},
		{
			CharacterClass.MartialArts,
			"RESTRICT_MARTIAL_ARTS"
		},
		{
			CharacterClass.None,
			"RESTRICT_NONE"
		},
		{
			CharacterClass.Super,
			"RESTRICT_SUPER"
		},
		{
			CharacterClass.Tech,
			"RESTRICT_TECH"
		}
	};

	private static Dictionary<AbilityType, string> abilityRestrictionKeys = new Dictionary<AbilityType, string> { 
	{
		AbilityType.Heal,
		"RESTRICT_HEALING"
	} };

	public TextMeshPro headerText;

	public PageControllerModChip modChipSelector;

	public ModChipDescriptionBox leftDescriptionBox;

	public ModChipDescriptionBox rightDescriptionBox;

	public Collider2D dropTarget;

	public tk2dUIItem equippedModChipButton;

	public Transform leftDescriptionBoxAnchor;

	public Transform rightDescriptionBoxAnchor;

	public ModelViewer modelViewer;

	public ModelViewerCamera modelViewerCamera;

	public float shakeDuration = 0.5f;

	public Vector3 shakeStrength = new Vector3(0.075f, 0.075f, 0f);

	public int shakeVibrato = 20;

	public float shakeRandomness = 90f;

	public tk2dCameraAnchor lowerLeftAnchor;

	public tk2dCameraAnchor lowerCenterAnchor;

	public tk2dCameraAnchor lowerRightAnchor;

	private ModChipSelectPortrait selectedPortrait;

	private Player _player;

	private Character _character;

	public Player player
	{
		get
		{
			return _player;
		}
		set
		{
			Init(value);
		}
	}

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			Init(_player, value);
		}
	}

	private void Awake()
	{
		leftDescriptionBox.gameObject.SetActive(false);
		rightDescriptionBox.gameObject.SetActive(false);
		modChipSelector.Init(InitPortrait);
	}

	private void Start()
	{
		modChipSelector.ForceUpdate();
		HandleInventoryChanged();
	}

	private void OnEnable()
	{
		modChipSelector.PageDragStarted += HandlePageDragStarted;
		modChipSelector.DragStarted += HandleDragStarted;
		modChipSelector.DragFinished += HandleDragFinished;
		modChipSelector.PortraitSelected += HandlePortraitSelected;
		equippedModChipButton.OnClick += HandleEquippedModChipClick;
		tk2dCamera tk2dCamera2 = tk2dCamera.CameraForLayer(leftDescriptionBoxAnchor.gameObject.layer);
		tk2dCamera tk2dCamera3 = tk2dCamera.CameraForLayer(leftDescriptionBox.gameObject.layer);
		tk2dCamera tk2dCamera4 = tk2dCamera.CameraForLayer(rightDescriptionBoxAnchor.gameObject.layer);
		tk2dCamera tk2dCamera5 = tk2dCamera.CameraForLayer(rightDescriptionBox.gameObject.layer);
		Camera camera = ((!(tk2dCamera2 != null)) ? null : tk2dCamera2.ScreenCamera);
		Camera camera2 = ((!(tk2dCamera3 != null)) ? null : tk2dCamera3.ScreenCamera);
		Camera camera3 = ((!(tk2dCamera4 != null)) ? null : tk2dCamera4.ScreenCamera);
		Camera camera4 = ((!(tk2dCamera5 != null)) ? null : tk2dCamera5.ScreenCamera);
		if (camera == null)
		{
			Debug.Log("Left Description Box Anchor camera not found. Attempting to find it another way. Left Description Box Anchor layer: " + LayerMask.LayerToName(leftDescriptionBoxAnchor.gameObject.layer));
			camera = CameraUtility.GetCameraRenderingObject(leftDescriptionBoxAnchor.gameObject);
		}
		if (camera2 == null)
		{
			Debug.Log("Left Description Box camera not found. Attempting to find it another way. Left Description Box layer: " + LayerMask.LayerToName(leftDescriptionBox.gameObject.layer));
			camera2 = CameraUtility.GetCameraRenderingObject(leftDescriptionBox.gameObject);
		}
		if (camera3 == null)
		{
			Debug.Log("Right Description Box Anchor camera not found. Attempting to find it another way. Right Description Box Anchor layer: " + LayerMask.LayerToName(rightDescriptionBoxAnchor.gameObject.layer));
			camera3 = CameraUtility.GetCameraRenderingObject(rightDescriptionBoxAnchor.gameObject);
		}
		if (camera4 == null)
		{
			Debug.Log("Right Description Box camera not found. Attempting to find it another way. Right Description Box layer: " + LayerMask.LayerToName(rightDescriptionBox.gameObject.layer));
			camera4 = CameraUtility.GetCameraRenderingObject(rightDescriptionBox.gameObject);
		}
		Vector3 position = camera.WorldToScreenPoint(leftDescriptionBoxAnchor.position);
		position = camera2.ScreenToWorldPoint(position);
		position.x += 26f;
		position.y += -15f;
		position.z = leftDescriptionBox.transform.position.z;
		leftDescriptionBox.transform.position = position;
		position = camera3.WorldToScreenPoint(rightDescriptionBoxAnchor.position);
		position = camera4.ScreenToWorldPoint(position);
		position.x += -130f;
		position.y += 126f;
		position.z = rightDescriptionBox.transform.position.z;
		rightDescriptionBox.transform.position = position;
		if (_character != null && _character.modChip != null)
		{
			rightDescriptionBox.Init(_character.modChip);
			rightDescriptionBox.gameObject.SetActive(true);
		}
	}

	private void OnDisable()
	{
		modChipSelector.PageDragStarted -= HandlePageDragStarted;
		modChipSelector.DragStarted -= HandleDragStarted;
		modChipSelector.DragFinished -= HandleDragFinished;
		modChipSelector.PortraitSelected -= HandlePortraitSelected;
		equippedModChipButton.OnClick -= HandleEquippedModChipClick;
		selectedPortrait = null;
		leftDescriptionBox.gameObject.SetActive(false);
		rightDescriptionBox.gameObject.SetActive(false);
		CoreGameTutorialManager.RemoveTutorialHands();
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.InventoryChanged -= HandleInventoryChanged;
		}
	}

	public void Init(Player player, Character character = null)
	{
		if (player == _player && character == _character)
		{
			return;
		}
		if (player != _player)
		{
			if (_player != null)
			{
				_player.InventoryChanged -= HandleInventoryChanged;
			}
			_player = player;
			if (_player != null)
			{
				_player.InventoryChanged += HandleInventoryChanged;
			}
			HandleInventoryChanged();
		}
		if (character != _character)
		{
			_character = character;
		}
	}

	private void InitPortrait(ModChipSelectPortrait portrait, ModChipDefinition modChip)
	{
		portrait.Init(modChip);
	}

	private bool IsModChipValid(ModChipDefinition modChip)
	{
		if (modChip.classRestrictions.Count > 0 && !modChip.classRestrictions.Contains(_character.characterDefinition.characterClass))
		{
			return false;
		}
		AbilityData abilityData = null;
		if (modChip.abilityTypeRestriction != AbilityType.None)
		{
			abilityData = _character.abilities.Find((AbilityData obj) => obj.type == modChip.abilityTypeRestriction);
			if (abilityData == null)
			{
				return false;
			}
		}
		if (modChip.castingRestriction)
		{
			abilityData = _character.abilities.Find((AbilityData obj) => obj.castTime > 0f);
			if (abilityData == null)
			{
				return false;
			}
		}
		return true;
	}

	private void HandlePageDragStarted()
	{
		CoreGameTutorialManager.RemoveTutorialHands();
		if (selectedPortrait != null)
		{
			selectedPortrait.animator.Play("Idle");
			selectedPortrait = null;
			leftDescriptionBox.gameObject.SetActive(false);
		}
	}

	private void HandleDragStarted(ModChipSelectPortrait portrait)
	{
		HandlePageDragStarted();
	}

	private void HandlePortraitSelected(ModChipSelectPortrait portrait)
	{
		if (selectedPortrait != null)
		{
			selectedPortrait.animator.Play("Idle");
			leftDescriptionBox.gameObject.SetActive(false);
		}
		if (portrait == selectedPortrait)
		{
			selectedPortrait = null;
			CoreGameTutorialManager.RemoveTutorialHands();
			return;
		}
		selectedPortrait = portrait;
		selectedPortrait.animator.Play("Selected");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_info", GFAudioManager.SFX_GROUP, string.Empty);
		leftDescriptionBox.Init(portrait.modChip);
		leftDescriptionBox.gameObject.SetActive(true);
		if (!Player.instance.HasTriggered("firstModChipEquipped") && CoreGameTutorialManager.instance != null)
		{
			CoreGameTutorialManager.ShowDrag(selectedPortrait.transform, dropTarget.transform);
		}
	}

	private void HandleEquippedModChipClick()
	{
	}

	private void HandleDragFinished(ModChipSelectPortrait portrait)
	{
		Ray ray = modelViewerCamera.camera.ScreenPointToRay(modChipSelector.upperCamera.WorldToScreenPoint(portrait.transform.position));
		float enter;
		new Plane(Vector3.back, Vector3.zero).Raycast(ray, out enter);
		Vector2 point = ray.GetPoint(enter);
		Collider2D[] array = Physics2D.OverlapPointAll(point, 1 << LayerMask.NameToLayer("Default"));
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			if (array[i] == dropTarget)
			{
				StartCoroutine(EquipModChip(portrait.modChip));
				break;
			}
		}
	}

	private IEnumerator ShowInvalidModChipWarning(ModChipDefinition modChip)
	{
		string message = string.Empty;
		AbilityData ability = null;
		if (modChip.classRestrictions.Count > 0 && !modChip.classRestrictions.Contains(_character.characterDefinition.characterClass))
		{
			message = Language.Get(classRestrictionKeys[modChip.classRestrictions[0]], "MENUS,GUI");
		}
		else if (modChip.abilityTypeRestriction != AbilityType.None)
		{
			ability = _character.abilities.Find((AbilityData obj) => obj.type == modChip.abilityTypeRestriction);
			if (ability == null)
			{
				message = Language.Get(abilityRestrictionKeys[modChip.abilityTypeRestriction], "MENUS,GUI");
			}
		}
		else if (modChip.castingRestriction)
		{
			ability = _character.abilities.Find((AbilityData obj) => obj.castTime > 0f);
			if (ability == null)
			{
				message = Language.Get("RESTRICT_CHARGE_UP", "MENUS,GUI");
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default(Vector3), false);
		dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
		dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI4"));
		dialog.transform.localPosition = new Vector3(0f, 0f, -300f);
		dialog.descriptionText.text = message;
		dialog.gameObject.SetActive(true);
		yield return new WaitForInactive(dialog.gameObject);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning_exit", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private IEnumerator EquipModChip(ModChipDefinition modChip)
	{
		bool doEquip = true;
		if (!IsModChipValid(modChip))
		{
			yield return StartCoroutine(ShowInvalidModChipWarning(modChip));
			doEquip = false;
		}
		else if (_character.modChip != null)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
			PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default(Vector3), false);
			dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI4"));
			dialog.transform.localPosition = new Vector3(0f, 0f, -300f);
			dialog.descriptionText.text = Language.Get("MODCHIPDESTROYED", "MENUS,GUI");
			dialog.gameObject.SetActive(true);
			yield return new WaitForInactive(dialog.gameObject);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning_exit", GFAudioManager.SFX_GROUP, string.Empty);
			doEquip = dialog.isConfirmed;
		}
		if (!doEquip)
		{
			yield break;
		}
		_player.RemoveItem(modChip);
		_character.EquipModChip(modChip);
		modelViewer.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_install", GFAudioManager.SFX_GROUP, string.Empty);
		Transform transform = ObjectPool.GetObjectFromResource("Prefabs/FX/ModChip_techlines_fx", default(Vector3), false).transform;
		transform.parent = modelViewer.transform.GetChild(0);
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3(0f, -0.01f, 0f);
		transform.gameObject.SetActive(true);
		Vector3 position = modChipSelector.upperCamera.ScreenToWorldPoint(modelViewerCamera.camera.WorldToScreenPoint(modelViewer.transform.GetChild(0).position));
		position.z = 0f;
		Vector3 zero = Vector3.zero;
		if (Random.value < 0.5f)
		{
			if (Random.value < 0.5f)
			{
				position.x += -27f;
				zero.y = 5f;
			}
			else
			{
				position.x += 27f;
				zero.y = 355f;
			}
			position.y += Random.Range(-27, 27);
		}
		else
		{
			position.x += Random.Range(-27, 27);
			if (Random.value < 0.5f)
			{
				position.y += -27f;
				zero.x = 355f;
			}
			else
			{
				position.y += 27f;
				zero.x = 5f;
			}
		}
		Transform transform2 = ObjectPool.GetObjectFromResource("Prefabs/FX/ModChip_Spark", default(Vector3), false).transform;
		transform2.position = position;
		transform2.localRotation = Quaternion.Euler(zero);
		transform2.gameObject.SetActive(true);
		rightDescriptionBox.Init(modChip);
		rightDescriptionBox.gameObject.SetActive(true);
		rightDescriptionBox.animator.Play("Intro", 0, 0f);
		if (!Player.instance.HasTriggered("firstModChipEquipped"))
		{
			Player.instance.SetTrigger("firstModChipEquipped");
		}
	}

	private void HandleInventoryChanged()
	{
		if (_player != null)
		{
			modChipSelector.SetList(_player.inventory.FindAll((InventoryItem obj) => obj.item is ModChipDefinition).ConvertAll((InventoryItem input) => (ModChipDefinition)input.item));
		}
	}
}
