using System.Collections.Generic;
using UnityEngine;

public class AccessorySelect : MonoBehaviour
{
	public AccessoryPageController accessoryPageController;

	public float timeEnabled;

	private bool accessoryPageControllerInitialized;

	private Player _player;

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

	private void OnEnable()
	{
		accessoryPageController.DragFinished += HandleDragFinished;
	}

	public void OnDisable()
	{
		accessoryPageController.DragFinished -= HandleDragFinished;
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.EquippedAccessoriesChanged -= HandleAccessoriesChanged;
		}
		_player = null;
	}

	public void Init(Player newPlayer)
	{
		if (newPlayer == _player)
		{
			return;
		}
		if (_player != null)
		{
			_player.EquippedAccessoriesChanged -= HandleAccessoriesChanged;
		}
		_player = newPlayer;
		if (_player != null)
		{
			accessoryPageController.Init(InitAccessoryPortrait);
			List<AccessoryItemDefinition> list = _player.accessories.ConvertAll((int x) => GameManager.instance.itemDatabase.GetItem(x) as AccessoryItemDefinition);
			list.RemoveAll((AccessoryItemDefinition x) => !x.showInTower);
			accessoryPageController.SetList(list);
			_player.EquippedAccessoriesChanged += HandleAccessoriesChanged;
			HandleAccessoriesChanged();
		}
	}

	private void InitAccessoryPortrait(AccessorySelectPortrait portrait, AccessoryItemDefinition accessoryItemDefinition)
	{
		portrait.Init(accessoryItemDefinition);
		portrait.mode = (_player.IsAccessoryEquipped(accessoryItemDefinition.uid) ? CharacterSelectPortraitMode.Equipped : CharacterSelectPortraitMode.Normal);
		if (Time.unscaledTime - timeEnabled > 0.25f)
		{
			portrait.animator.Play("Intro", 0, 1f);
		}
	}

	private void HandleDragFinished(AccessorySelectPortrait portrait)
	{
		Collider2D[] array = Physics2D.OverlapPointAll(accessoryPageController.upperCamera.ScreenToWorldPoint(accessoryPageController.lowerCamera.WorldToScreenPoint(portrait.transform.position)), 1 << LayerMask.NameToLayer("UI"));
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			if (array[i].GetComponent<AccessoryEquipBar>() != null && _player.activeAccessories.Count < 6)
			{
				_player.AddToEquippedAccessories(portrait.accessoryItemDefinition.uid);
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_equipcharacter", GFAudioManager.SFX_GROUP, string.Empty);
				break;
			}
		}
	}

	private void HandleAccessoriesChanged()
	{
		accessoryPageController.ForceUpdate();
	}
}
