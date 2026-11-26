using System.Collections;
using TMPro;
using UnityEngine;

public class FlashSaleTicketPickup : CityPickup
{
	public Landmark landmark;

	public Animator animator;

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnEnable()
	{
		if (landmark.icon != null)
		{
			landmark.icon.transform.parent.gameObject.SetActive(value: true);
		}
		base.OnEnable();
	}

	private new void OnDisable()
	{
		if (Application.isPlaying && landmark.icon != null && landmark.icon.transform.parent.gameObject.activeInHierarchy)
		{
			landmark.icon.transform.parent.gameObject.SetActive(value: false);
		}
	}

	protected override void PlayerCollected()
	{
		CityController.instance.walletGUI.AddToWalletTickets(1);
		if (GameManager.instance.flashSaleSettings.allowExtraTimeOnTicketPickup)
		{
			GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FlashSale/BonusTime", transform.position);
			objectFromResource.GetComponentInChildren<TextMeshPro>().text = "+" + GameManager.instance.flashSaleSettings.timeAddedOnTicketPickup + " " + Language.Get("SEC", "MENUS,GUI");
			FlashSaleController.instance.AddTime();
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_flashsale_ticketpickup", GFAudioManager.SFX_GROUP, string.Empty);
		if (landmark.icon != null)
		{
			if (CityController.instance != null)
			{
				CityController.instance.landmarkIcons.Remove(landmark.icon);
			}
			Object.Destroy(landmark.icon.transform.parent.gameObject);
		}
		StartCoroutine(PlayOutroAnimation());
	}

	private IEnumerator PlayOutroAnimation()
	{
		animator.Play("FlashSale_Ticket_Select", 0, 0f);
		yield return new WaitForEndOfFrame();
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return null;
		}
		Camera uiCamera = GUICamera.instance.GetComponent<Camera>();
		Camera mainCamera = CityController.instance.pinchZoomer.thisCamera;
		transform.position += (mainCamera.transform.position - transform.position).normalized * 1.5f;
		Vector3 offset = new Vector3(0f, -60f);
		Vector3 walletScreenPoint = uiCamera.WorldToScreenPoint(CityController.instance.walletGUI.transform.position + offset);
		Vector3 targetPosition = GetWorldPositionOnPlane(walletScreenPoint, 1.5f, mainCamera);
		float distToTravel = 7f * Time.deltaTime;
		Vector3 translation = (targetPosition - transform.position).normalized * distToTravel;
		while (distToTravel < Vector3.Distance(transform.position, targetPosition))
		{
			transform.position += translation;
			walletScreenPoint = uiCamera.WorldToScreenPoint(CityController.instance.walletGUI.transform.position + offset);
			targetPosition = GetWorldPositionOnPlane(walletScreenPoint, 1.5f, mainCamera);
			translation = (targetPosition - transform.position).normalized * distToTravel;
			yield return null;
		}
		transform.position = targetPosition;
		ObjectPool.PoolObject(gameObject);
	}

	public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y, Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(screenPosition);
		new Plane(Vector3.up, new Vector3(0f, y, 0f)).Raycast(ray, out var enter);
		return ray.GetPoint(enter);
	}
}
