using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class CityQuestItem : MonoBehaviour
{
	[SerializeField]
	private string _guid;

	[SerializeField]
	private int _itemUID;

	[SerializeField]
	private CityQuestItemModel _itemModel;

	[SerializeField]
	private bool _isUnique;

	[SerializeField]
	private bool _isAlwaysVisible;

	private CityController cityController;

	protected CameraPinchZoomer cameraPinchZoomer;

	private int activeMissionUID = -1;

	public string guid
	{
		get
		{
			return _guid;
		}
	}

	public int itemUID
	{
		get
		{
			return _itemUID;
		}
	}

	public CityQuestItemModel itemModel
	{
		get
		{
			return _itemModel;
		}
	}

	public bool isUnique
	{
		get
		{
			return _isUnique;
		}
	}

	public bool isAlwaysVisible
	{
		get
		{
			return _isAlwaysVisible;
		}
	}

	public bool canCollect { get; private set; }

	private void OnEnable()
	{
		Player.instance.MissionAdded += Player_instance_MissionAdded;
		Player.instance.MissionCompleted += Player_instance_MissionCompleted;
		Player.instance.MissionUpdated += Player_instance_MissionUpdated;
		_itemModel.uiItem.OnClick += HandleOnClick;
	}

	private void OnDisable()
	{
		Player.instance.MissionAdded -= Player_instance_MissionAdded;
		Player.instance.MissionCompleted -= Player_instance_MissionCompleted;
		Player.instance.MissionUpdated -= Player_instance_MissionUpdated;
		if (_itemModel != null)
		{
			_itemModel.uiItem.OnClick -= HandleOnClick;
		}
	}

	private void Start()
	{
		cityController = CityController.instance;
		cameraPinchZoomer = cityController.pinchZoomer;
		Refresh();
	}

	private void Refresh()
	{
		if (_isUnique && Player.instance.HasCollectedGlintGUID(_guid))
		{
			_itemModel.gameObject.SetActive(false);
			canCollect = false;
			return;
		}
		bool flag = false;
		MissionData missionData = null;
		Mission mission = null;
		int count = Player.instance.activeMissions.Count;
		for (int i = 0; i < count; i++)
		{
			missionData = Player.instance.activeMissions[i];
			mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (mission.sideType == MissionSideType.Find && mission.itemToCollectUID == _itemUID)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			activeMissionUID = missionData.uid;
			if (missionData.completed || missionData.collectedGUIDs.Contains(_guid))
			{
				_itemModel.Disable();
				_itemModel.gameObject.SetActive(_isAlwaysVisible || !missionData.collectedGUIDs.Contains(_guid));
				canCollect = false;
			}
			else
			{
				_itemModel.Enable();
				_itemModel.gameObject.SetActive(true);
				canCollect = true;
			}
		}
		else
		{
			_itemModel.Disable();
			_itemModel.gameObject.SetActive(_isAlwaysVisible);
			canCollect = false;
		}
	}

	private void Collect()
	{
		if (canCollect)
		{
			CityController.instance.cityCharacter.StopMove();
			Camera thisCamera = CityController.instance.cameraHolder.GetComponent<CameraPinchZoomer>().thisCamera;
			Vector3 position = thisCamera.WorldToScreenPoint(_itemModel.transform.position);
			Vector3 position2 = GameUI.instance.missionFlagIcon.GetComponentInParent<Camera>().ScreenToWorldPoint(position);
			CityQuestItemModel itemModelCopy = Object.Instantiate(_itemModel);
			if (itemModelCopy.transformLink != null)
			{
				itemModelCopy.transformLink.enabled = false;
			}
			itemModelCopy.transform.SetParent(null, true);
			itemModelCopy.transform.position = position2;
			itemModelCopy.transform.rotation = Quaternion.identity;
			itemModelCopy.transform.localScale = new Vector3(200f, 200f, 200f);
			itemModelCopy.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
			Sequence sequence = DOTween.Sequence().OnComplete(() =>
			{
				Object.Destroy(itemModelCopy.gameObject);
			});
			sequence.Append(itemModelCopy.transform.DOMove(GameUI.instance.missionFlagIcon.position, 0.8f).SetEase(Ease.OutSine));
			sequence.Append(itemModelCopy.transform.DOScale(new Vector3(300f, 300f, 300f), 0.1f));
			sequence.Append(itemModelCopy.transform.DOScale(new Vector3(0f, 0f, 0f), 0.1f).SetDelay(0.1f));
			sequence.Play();
			MissionData missionData = Player.instance.GetMissionData(activeMissionUID);
			missionData.collectedGUIDs.Add(_guid);
			if (_isUnique)
			{
				Player.instance.CollectGlint(_guid, cityController.communicator.GetCurrentDay());
			}
			CommunicatorManager.instance.UpdateMission(missionData);
		}
	}

	private void Player_instance_MissionAdded(int missionUID, bool isDialogueMission)
	{
		Refresh();
	}

	private void Player_instance_MissionCompleted(int missionUID)
	{
		Refresh();
	}

	private void Player_instance_MissionUpdated(int missionUID)
	{
		Refresh();
	}

	private void HandleOnClick()
	{
		if (!(cameraPinchZoomer != null) || !cameraPinchZoomer.isZoomedOut)
		{
			Collect();
		}
	}
}
