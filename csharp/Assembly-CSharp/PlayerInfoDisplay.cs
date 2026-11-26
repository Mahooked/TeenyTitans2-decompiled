using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerInfoDisplay : MonoBehaviour
{
	[Header("References")]
	public tk2dSprite portraitBackingSprite;

	public tk2dSprite portraitSprite;

	public tk2dSprite rankNumberBackingSprite;

	public TextMeshPro rankNumberTextMesh;

	public TextMeshPro nameTextMesh;

	public TextMeshPro skillRatingTextMesh;

	public TextMeshPro onlineTextMesh;

	public tk2dSprite battleBtnSprite;

	public tk2dUIItem battleBtn;

	public TextMeshPro battleBtnTextMesh;

	public Collider2D battleBtnCollider;

	public TextMeshPro inviteSentTextMesh;

	public Material battleMat;

	public Material offlineMat;

	public GameObject skillRatingGrp;

	public Transform parentGrp;

	public tk2dUIItem parentUIItem;

	public tk2dUIItem deleteBtn;

	public tk2dSprite deleteSprite;

	public TextMeshPro deleteTextMesh;

	[Header("Display Dragging Properties")]
	[Tooltip("The local X position soft cap of this info display when dragging it to reveal the delete button. The x represents the Min cap, the y represents the Max cap.")]
	public Vector2 softMinMaxX;

	[Tooltip("The local X position hard cap of this info display when dragging it to reveal the delete button. The x represents the Min cap, the y represents the Max cap.")]
	public Vector2 hardMinMaxX;

	[Tooltip("Represents how much on the X axis the direction of the player's drag needs to be to actually trigger dragging the display.")]
	public float minXDirection = 0.866f;

	[Tooltip("Represents the minimum length a player's drag needs to be to actually trigger dragging of the display")]
	public float minDragMagnitude = 4.5f;

	[Tooltip("How fast this display will return to a resting position")]
	public float correctionSpeed = 10f;

	[Tooltip("How fast this display will tween off screen when deleted")]
	public float deletedExitSpeed = 360f;

	private FriendData playerData;

	private bool isLocal;

	private float dragPower = 1f;

	private Vector3 originalLocalPos;

	private bool originalLocalPosSet;

	private bool parentBtnActive;

	private Vector2 prevTouchPos;

	public bool activelyDragging { get; private set; }

	public event Action<FriendData> BattleClicked;

	public event Action<FriendData> DeleteClicked;

	public event Action<FriendData, PlayerInfoDisplay> DeleteExitFinished;

	public event Action DraggingStarted;

	public event Action DraggingEnded;

	public event Action OnlineStatusChanged;

	private void OnEnable()
	{
		battleBtn.OnClick += HandleBattleBtnClicked;
		parentUIItem.OnDown += HandleParentBtnDown;
		parentUIItem.OnRelease += HandleParentBtnRelease;
		deleteBtn.OnClick += HandleDeleteBtnClicked;
		SwipeManager.Instance.SwipeOccurred += HandleSwipe;
	}

	private void OnDisable()
	{
		if (battleBtn != null)
		{
			battleBtn.OnClick -= HandleBattleBtnClicked;
		}
		if (parentUIItem != null)
		{
			parentUIItem.OnDown -= HandleParentBtnDown;
			parentUIItem.OnRelease -= HandleParentBtnRelease;
		}
		if (deleteBtn != null)
		{
			deleteBtn.OnClick -= HandleDeleteBtnClicked;
		}
		if (SwipeManager.Instance != null)
		{
			SwipeManager.Instance.SwipeOccurred -= HandleSwipe;
		}
		if (parentBtnActive)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= UpdateBtnPosition;
			}
			parentBtnActive = false;
		}
		if (parentGrp != null)
		{
			parentGrp.localPosition = originalLocalPos;
		}
	}

	private void OnDestroy()
	{
		if (playerData != null)
		{
			playerData.Updated -= PlayerData_Updated;
			playerData.StatusUpdated -= PlayerData_StatusUpdated;
			playerData.OnlineStatusUpdated -= PlayerData_OnlineStatusUpdated;
		}
	}

	public void Init(FriendData friendData, bool isBattleLocalResult = false)
	{
		if (playerData != null)
		{
			playerData.Updated -= PlayerData_Updated;
			playerData.StatusUpdated -= PlayerData_StatusUpdated;
			playerData.OnlineStatusUpdated -= PlayerData_OnlineStatusUpdated;
			if (playerData != friendData)
			{
				parentGrp.localPosition = originalLocalPos;
			}
		}
		playerData = friendData;
		isLocal = isBattleLocalResult;
		if (playerData != null)
		{
			playerData.Updated += PlayerData_Updated;
			playerData.StatusUpdated += PlayerData_StatusUpdated;
			playerData.OnlineStatusUpdated += PlayerData_OnlineStatusUpdated;
			if (!originalLocalPosSet)
			{
				originalLocalPos = parentGrp.localPosition;
				originalLocalPosSet = true;
			}
			dragPower = 1f;
			Vector3 b = new Vector3(softMinMaxX.x, parentGrp.localPosition.y, parentGrp.localPosition.y);
			float num = Vector3.Distance(parentGrp.localPosition, b);
			float num2 = Vector3.Distance(parentGrp.localPosition, originalLocalPos);
			SetDeleteButtonActive(num < num2);
		}
		RefreshInfoDisplay();
	}

	private void PlayerData_Updated(FriendData friendData)
	{
		RefreshInfoDisplay();
	}

	private void PlayerData_StatusUpdated()
	{
		RefreshInfoDisplay();
	}

	private void PlayerData_OnlineStatusUpdated()
	{
		RefreshInfoDisplay();
		if (OnlineStatusChanged != null)
		{
			OnlineStatusChanged();
		}
	}

	private void RefreshInfoDisplay()
	{
		if (playerData == null)
		{
			return;
		}
		portraitBackingSprite.color = Color.white;
		portraitBackingSprite.SetSprite(GameManager.instance.legionOfDoomSettings.GetBorderSpriteForRank(playerData.level));
		rankNumberBackingSprite.SetSprite(GameManager.instance.legionOfDoomSettings.GetNumberPlateSpriteForRank(playerData.level));
		nameTextMesh.text = ((!string.IsNullOrEmpty(playerData.name)) ? playerData.name : FriendCodeGenerator.GetFormattedCode(playerData.playerID));
		if (isLocal)
		{
			portraitSprite.gameObject.SetActive(value: true);
			portraitSprite.SetSprite(playerData.portraitSpriteName);
			rankNumberBackingSprite.gameObject.SetActive(value: true);
			rankNumberTextMesh.text = playerData.level.ToString();
			inviteSentTextMesh.gameObject.SetActive(value: false);
			skillRatingGrp.SetActive(value: true);
			skillRatingTextMesh.text = playerData.skillRating.ToString();
			onlineTextMesh.gameObject.SetActive(value: true);
			onlineTextMesh.text = "LOCAL";
			nameTextMesh.color = Color.white;
			battleBtn.enabled = true;
			battleBtnCollider.enabled = true;
			battleBtnSprite.color = Color.white;
			battleBtnTextMesh.fontSizeMin = 5f;
			battleBtnTextMesh.color = Color.white;
			battleBtnTextMesh.fontMaterial = battleMat;
			battleBtnTextMesh.text = Language.Get("BATTLE_NOEXCL", "MENUS,GUI");
			return;
		}
		if (playerData.requestAccepted)
		{
			portraitSprite.gameObject.SetActive(value: true);
			portraitSprite.SetSprite(playerData.portraitSpriteName);
			rankNumberBackingSprite.gameObject.SetActive(value: true);
			rankNumberTextMesh.text = playerData.level.ToString();
			inviteSentTextMesh.gameObject.SetActive(value: false);
			skillRatingGrp.SetActive(value: true);
			skillRatingTextMesh.text = playerData.skillRating.ToString();
		}
		else
		{
			if (string.IsNullOrEmpty(playerData.portraitSpriteName))
			{
				portraitSprite.gameObject.SetActive(value: false);
				rankNumberBackingSprite.gameObject.SetActive(value: false);
				portraitBackingSprite.color = new Color(0f, 0f, 0f, 0.3f);
			}
			else
			{
				portraitSprite.gameObject.SetActive(value: true);
				portraitSprite.SetSprite(playerData.portraitSpriteName);
				rankNumberBackingSprite.gameObject.SetActive(value: true);
				rankNumberTextMesh.text = playerData.level.ToString();
			}
			skillRatingGrp.SetActive(value: false);
			inviteSentTextMesh.gameObject.SetActive(value: true);
		}
		if (playerData.requestAccepted && playerData.IsOnline)
		{
			onlineTextMesh.gameObject.SetActive(value: true);
			nameTextMesh.color = Color.white;
			if (playerData.IsInBattle)
			{
				battleBtn.enabled = false;
				battleBtnCollider.enabled = false;
				battleBtnSprite.color = new Color(1f, 1f, 1f, 0.4f);
				battleBtnTextMesh.fontSizeMin = 8f;
				battleBtnTextMesh.color = new Color(1f, 1f, 1f, 0.4f);
				battleBtnTextMesh.fontMaterial = battleMat;
				battleBtnTextMesh.text = Language.Get("INBATTLE", "MENUS,GUI");
			}
			else
			{
				battleBtn.enabled = true;
				battleBtnCollider.enabled = true;
				battleBtnSprite.color = Color.white;
				battleBtnTextMesh.fontSizeMin = 5f;
				battleBtnTextMesh.color = Color.white;
				battleBtnTextMesh.fontMaterial = battleMat;
				battleBtnTextMesh.text = Language.Get("BATTLE_NOEXCL", "MENUS,GUI");
			}
		}
		else
		{
			onlineTextMesh.gameObject.SetActive(value: false);
			nameTextMesh.color = Color.gray;
			battleBtn.enabled = false;
			battleBtnCollider.enabled = false;
			battleBtnSprite.color = new Color(0f, 0f, 0f, 0.3f);
			battleBtnTextMesh.fontSizeMin = 5f;
			battleBtnTextMesh.color = Color.white;
			battleBtnTextMesh.fontMaterial = offlineMat;
			battleBtnTextMesh.text = Language.Get("OFFLINE", "MENUS,GUI");
		}
	}

	private void SetDeleteButtonActive(bool active)
	{
		deleteBtn.enabled = active;
		Color color = ((!active) ? Color.gray : Color.white);
		deleteSprite.color = color;
		deleteTextMesh.color = color;
	}

	private void HandleParentBtnDown()
	{
		if (!isLocal)
		{
			if (!parentBtnActive)
			{
				tk2dUIManager.Instance.OnInputUpdate += UpdateBtnPosition;
			}
			parentBtnActive = true;
			parentGrp.DOKill();
			prevTouchPos = parentUIItem.Touch.position;
			SetDeleteButtonActive(active: false);
		}
	}

	private void HandleParentBtnRelease()
	{
		if (isLocal)
		{
			return;
		}
		if (parentBtnActive)
		{
			tk2dUIManager.Instance.OnInputUpdate -= UpdateBtnPosition;
		}
		if (activelyDragging)
		{
			activelyDragging = false;
			if (DraggingEnded != null)
			{
				DraggingEnded();
			}
		}
		parentBtnActive = false;
		dragPower = 1f;
		Vector3 vector = new Vector3(softMinMaxX.x, parentGrp.localPosition.y, parentGrp.localPosition.y);
		float num = Vector3.Distance(parentGrp.localPosition, vector);
		float num2 = Vector3.Distance(parentGrp.localPosition, originalLocalPos);
		if (num < num2)
		{
			parentGrp.DOLocalMove(vector, correctionSpeed).SetSpeedBased().SetEase(Ease.OutQuart)
				.OnComplete(() =>
				{
					SetDeleteButtonActive(active: true);
				});
		}
		else
		{
			parentGrp.DOLocalMove(originalLocalPos, correctionSpeed).SetSpeedBased().SetEase(Ease.OutQuart);
			SetDeleteButtonActive(active: false);
		}
	}

	private void UpdateBtnPosition()
	{
		Vector3 localPosition = parentGrp.localPosition;
		Vector2 position = parentUIItem.Touch.position;
		Camera camera = GUICamera.instance.camera;
		Vector3 vector = camera.ScreenToWorldPoint(new Vector3(prevTouchPos.x, prevTouchPos.y, parentGrp.position.z - camera.transform.position.z));
		Vector3 vector2 = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, parentGrp.position.z - camera.transform.position.z));
		vector.z = (vector2.z = parentGrp.position.z);
		if (!activelyDragging)
		{
			Vector3 b = new Vector3(softMinMaxX.x, parentGrp.localPosition.y, parentGrp.localPosition.y);
			float num = Vector3.Distance(parentGrp.localPosition, b);
			float num2 = Vector3.Distance(parentGrp.localPosition, originalLocalPos);
			bool flag = num < num2;
			Vector3 vector3 = position - prevTouchPos;
			float num3 = ((!flag) ? Mathf.Abs(vector3.normalized.x) : vector3.normalized.x);
			if (!(num3 >= minXDirection) || !(Vector3.Distance(vector2, vector) > minDragMagnitude))
			{
				prevTouchPos = position;
				return;
			}
			Debug.Log("Begin Actively Dragging this Display for " + playerData.name);
			activelyDragging = true;
			if (DraggingStarted != null)
			{
				DraggingStarted();
			}
		}
		Vector3 vector4 = vector2 - vector;
		parentGrp.position += vector4 * dragPower;
		Vector3 localPosition2 = parentGrp.localPosition;
		localPosition2.y = localPosition.y;
		localPosition2.z = localPosition.z;
		if (localPosition2.x < softMinMaxX.x && vector4.x < 0f)
		{
			dragPower = 0.33f;
			if (localPosition2.x < hardMinMaxX.x)
			{
				localPosition2.x = hardMinMaxX.x;
			}
		}
		else if (localPosition2.x >= hardMinMaxX.y && vector4.x > 0f)
		{
			localPosition2.x = hardMinMaxX.y;
		}
		else
		{
			dragPower = 1f;
		}
		parentGrp.localPosition = localPosition2;
		prevTouchPos = position;
	}

	private void HandleSwipe(SwipeResult swipeResult)
	{
		if (!isLocal)
		{
			BoxCollider2D component = parentUIItem.GetComponent<BoxCollider2D>();
			Vector3 vector = GUICamera.instance.camera.ScreenToWorldPoint(swipeResult.start);
			vector.z = parentGrp.position.z;
			if (component.OverlapPoint(vector))
			{
				StartCoroutine(DelayedProcessSwipe(swipeResult));
			}
		}
	}

	private IEnumerator DelayedProcessSwipe(SwipeResult swipeResult)
	{
		yield return new WaitForEndOfFrame();
		Vector3 openPos = new Vector3(softMinMaxX.x, parentGrp.localPosition.y, parentGrp.localPosition.y);
		float distToOpenPos = Vector3.Distance(parentGrp.localPosition, openPos);
		float distToClosedPos = Vector3.Distance(parentGrp.localPosition, originalLocalPos);
		if (distToOpenPos < distToClosedPos && swipeResult.direction2D.x >= minXDirection)
		{
			parentGrp.DOKill();
			parentGrp.DOLocalMove(originalLocalPos, correctionSpeed).SetSpeedBased().SetEase(Ease.OutQuart);
			SetDeleteButtonActive(active: false);
		}
		else if (distToOpenPos >= distToClosedPos && swipeResult.direction2D.x <= 0f - minXDirection)
		{
			parentGrp.DOKill();
			parentGrp.DOLocalMove(openPos, correctionSpeed).SetSpeedBased().SetEase(Ease.OutQuart)
				.OnComplete(() =>
				{
					SetDeleteButtonActive(active: true);
				});
		}
	}

	private void HandleBattleBtnClicked()
	{
		if (BattleClicked != null)
		{
			BattleClicked(playerData);
		}
	}

	private void HandleDeleteBtnClicked()
	{
		if (LegionOfDoomController.instance.friendsListMenu.deleteSequenceActive)
		{
			return;
		}
		if (DeleteClicked != null)
		{
			DeleteClicked(playerData);
		}
		parentGrp.DOLocalMove(originalLocalPos + new Vector3(-300f, 0f), deletedExitSpeed).SetSpeedBased().SetEase(Ease.OutQuart)
			.OnComplete(() =>
			{
				if (DeleteExitFinished != null)
				{
					DeleteExitFinished(playerData, this);
				}
			});
		SetDeleteButtonActive(active: false);
		float duration = 0.25f;
		DOTween.ToAlpha(() => deleteSprite.color, (Color x) =>
		{
			deleteSprite.color = x;
		}, 0f, duration).SetEase(Ease.Linear).SetTarget(deleteSprite);
		deleteTextMesh.DOFade(0f, duration).SetEase(Ease.Linear);
	}
}
