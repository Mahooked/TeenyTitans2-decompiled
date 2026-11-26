using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccessoriesView : MonoBehaviour
{
	public TextMeshPro nameText;

	public TextMeshPro descriptionText;

	public TextMeshPro chargeText;

	public tk2dUIItem leftNavButton;

	public tk2dUIItem rightNavButton;

	public tk2dUIItem equipButton;

	public tk2dSprite equipArrowSprite;

	public TextMeshPro equipText;

	public ModelViewer modelViewer;

	public ModelViewerCamera modelViewerCamera;

	private GameObject accessoryModel;

	private Vector3 originalModelViewerOffset;

	private Vector3 originalModelViewerRotationOffset;

	private AudioSource showFigSource;

	private bool playedshowFigSFX;

	private Texture currentFigureDefaultTexture;

	private Dictionary<string, Texture> characterTextureCache = new Dictionary<string, Texture>();

	private AccessoryItemDefinition _accessoryItemDef;

	private Player _player;

	private List<AccessoryItemDefinition> _accessoryList;

	public AccessoryItemDefinition accessoryItemDef
	{
		get
		{
			return _accessoryItemDef;
		}
		set
		{
			Init(value, _accessoryList, _player);
		}
	}

	public Player player
	{
		get
		{
			return _player;
		}
		set
		{
			Init(_accessoryItemDef, _accessoryList, value);
		}
	}

	public void OnEnable()
	{
		leftNavButton.OnClickUIItem += HandleOnNavClick;
		rightNavButton.OnClickUIItem += HandleOnNavClick;
		equipButton.OnClick += HandleOnEquipClick;
		modelViewerCamera.enabled = true;
		originalModelViewerOffset = modelViewerCamera.targetOffset;
		originalModelViewerRotationOffset = modelViewerCamera.targetRotationOffset;
	}

	private void OnDisable()
	{
		leftNavButton.OnClickUIItem -= HandleOnNavClick;
		rightNavButton.OnClickUIItem -= HandleOnNavClick;
		equipButton.OnClick -= HandleOnEquipClick;
		RemoveFigure();
		modelViewerCamera.targetOffset = originalModelViewerOffset;
		modelViewerCamera.targetRotationOffset = originalModelViewerRotationOffset;
	}

	public void Init(AccessoryItemDefinition accessoryItemDef, List<AccessoryItemDefinition> accessoryList, Player player)
	{
		_accessoryItemDef = accessoryItemDef;
		if (_accessoryItemDef != null)
		{
			HandleUpdated();
			RemoveFigure();
			ShowFigure();
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_opencharinfopage", GFAudioManager.SFX_GROUP, string.Empty);
		}
		_accessoryList = accessoryList;
		if (player != _player)
		{
			if (_player != null)
			{
				_player.EquippedAccessoriesChanged -= HandleEquippedAccessoriesChanged;
			}
			_player = player;
			if (_player != null)
			{
				_player.EquippedAccessoriesChanged += HandleEquippedAccessoriesChanged;
				HandleEquippedAccessoriesChanged();
			}
		}
	}

	private void ShowFigure()
	{
		if (accessoryModel != null)
		{
			return;
		}
		accessoryModel = ObjectPool.GetObjectFromResource(_accessoryItemDef.modelPath);
		if (accessoryModel != null)
		{
			modelViewer.transform.localRotation = Quaternion.identity;
			accessoryModel.transform.parent = modelViewer.transform;
			float num;
			float num2;
			if (Screen.height > Screen.width)
			{
				num = Screen.height;
				num2 = Screen.width;
			}
			else
			{
				num = Screen.width;
				num2 = Screen.height;
			}
			Vector3 vector = ((!(num / num2 < 1.45f)) ? _accessoryItemDef.modelViewerOffset : _accessoryItemDef.modelViewerOffsetIpad);
			accessoryModel.transform.localPosition = new Vector3(0f, -0.86f, 0f) + vector;
			accessoryModel.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			modelViewerCamera.targetOffset = _accessoryItemDef.modelViewerCameraOffset;
			modelViewerCamera.targetRotationOffset = _accessoryItemDef.modelViewerCameraRotationOffset;
			StartCoroutine(PlayAccessoryAnimation());
		}
	}

	private IEnumerator PlayAccessoryAnimation()
	{
		Animator modelAnimator = accessoryModel.GetComponentInChildren<Animator>();
		if (modelAnimator != null)
		{
			modelAnimator.Play(_accessoryItemDef.modelViewerAnimation, 0, _accessoryItemDef.modelViewerAnimationTime);
			modelAnimator.speed = 0f;
		}
		yield return null;
	}

	private void RemoveFigure()
	{
		if (accessoryModel != null)
		{
			ObjectPool.PoolObject(accessoryModel);
			accessoryModel = null;
		}
	}

	private Texture GetCharacterTexture(string characterKey)
	{
		Texture value = null;
		characterTextureCache.TryGetValue(characterKey, out value);
		return value;
	}

	private void HandleUpdated()
	{
		nameText.text = Language.Get(_accessoryItemDef.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
		descriptionText.text = TTGParseHelper.GetAccessoryDescription(_accessoryItemDef);
		HandleEquippedAccessoriesChanged();
	}

	private void HandleEquippedAccessoriesChanged()
	{
		if (_player != null && !player.IsAccessoryEquipped(_accessoryItemDef.uid))
		{
			equipArrowSprite.scale = new Vector3(0.9f, 0.9f, 1f);
			equipText.text = Language.Get("EQUIP", "MENUS,GUI");
		}
		else
		{
			equipArrowSprite.scale = new Vector3(0.9f, -0.9f, 1f);
			equipText.text = Language.Get("UNEQUIP", "MENUS,GUI");
		}
	}

	private void HandleOnEquipClick()
	{
		StartCoroutine(DoEquip());
	}

	private IEnumerator DoEquip()
	{
		if (!_player.IsAccessoryEquipped(_accessoryItemDef.uid) && _player.activeAccessories.Count >= 6)
		{
			PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window", default, isActive: false);
			dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI3"));
			dialog.transform.parent = GUICamera.instance.anchorMiddleCenter;
			dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
			dialog.descriptionText.text = Language.Get("TOWER_ACCESSORY_BAR_FULL", "MENUS,GUI");
			dialog.gameObject.SetActive(value: true);
			yield return new WaitForInactive(dialog.gameObject);
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else if (_player.IsAccessoryEquipped(_accessoryItemDef.uid))
		{
			_player.RemoveFromEquippedAccessories(_accessoryItemDef.uid);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_unequipchar", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			_player.AddToEquippedAccessories(_accessoryItemDef.uid);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_equipcharacter", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void HandleOnNavClick(tk2dUIItem button)
	{
		int num = _accessoryList.IndexOf(_accessoryItemDef);
		if (button == leftNavButton)
		{
			num--;
			if (num < 0)
			{
				num = _accessoryList.Count - 1;
			}
		}
		else
		{
			num++;
			if (num >= _accessoryList.Count)
			{
				num = 0;
			}
		}
		accessoryItemDef = _accessoryList[num];
		TitanTowerController.instance.ShowPage("accessoriesView", isBack: true);
	}
}
