using UnityEngine;

public class BattleDisplayManager : MonoBehaviour
{
	private BattleController _battleController;

	private static BattleDisplayManager _instance;

	public BattleController battleController
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

	public static BattleDisplayManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("BattleDisplayManager").AddComponent<BattleDisplayManager>();
			}
			return _instance;
		}
	}

	public void ShowHealResult(HealResult healResult, BattleCharacter character)
	{
		if (GameManager.instance.gameSettings.showBattleMessages && healResult.display)
		{
			DamageText displayText = GetDisplayText();
			Camera guiCamera = BattleController.instance.guiCamera;
			Camera cameraForCharacter = GetCameraForCharacter(character);
			Vector3 position = cameraForCharacter.WorldToScreenPoint(character.top.position);
			Vector3 position2 = guiCamera.ScreenToWorldPoint(position) + new Vector3(0f, -60f, 100f);
			displayText.transform.position = position2;
			displayText.Init(healResult);
			displayText.gameObject.SetActive(true);
		}
	}

	public void ShowDamageResult(DamageResult damageResult, BattleCharacter character)
	{
		if (GameManager.instance.gameSettings.showBattleMessages && damageResult.showDamage)
		{
			DamageText displayText = GetDisplayText();
			Camera guiCamera = BattleController.instance.guiCamera;
			Camera cameraForCharacter = GetCameraForCharacter(character);
			Vector3 position = cameraForCharacter.WorldToScreenPoint(character.top.position);
			Vector3 position2 = guiCamera.ScreenToWorldPoint(position) + new Vector3(0f, -60f, 100f);
			if (damageResult.useRandomPositionOffset)
			{
				position2 += new Vector3(Random.Range(-30, 30), Random.Range(-30, 20), 0f);
			}
			displayText.transform.position = position2;
			displayText.Init(damageResult);
			displayText.gameObject.SetActive(true);
		}
	}

	public void ShowDamageResult(DamageResult damageResult, BattleCharacter character, Vector3 worldPosition, string textAnimation = "Default")
	{
		if (GameManager.instance.gameSettings.showBattleMessages)
		{
			DamageText displayText = GetDisplayText();
			Camera guiCamera = BattleController.instance.guiCamera;
			Camera cameraForCharacter = GetCameraForCharacter(character);
			Vector3 position = cameraForCharacter.WorldToScreenPoint(worldPosition);
			Vector3 position2 = guiCamera.ScreenToWorldPoint(position) + new Vector3(0f, -60f, 100f);
			if (damageResult.useRandomPositionOffset)
			{
				position2 += new Vector3(Random.Range(-30, 30), Random.Range(-30, 20), 0f);
			}
			displayText.transform.position = position2;
			displayText.Init(damageResult, textAnimation);
			displayText.gameObject.SetActive(true);
		}
	}

	public void ShowBuffResult(BuffResult buffResult, BattleCharacter character)
	{
		if (GameManager.instance.gameSettings.showBattleMessages && buffResult.display)
		{
			string message = ((buffResult.statType != CharacterStatType.Luck) ? string.Format("{0}{1} {2}", (buffResult.amount <= 0) ? string.Empty : "+", buffResult.amount, Language.Get(buffResult.statTypeKey, "MENUS,GUI")) : Language.Get("LUCKUP", "ABILS,CHIPS,ACHVS,LDBS"));
			ShowMessage(message, character);
		}
	}

	public void ShowMessage(string message, BattleCharacter character, bool autoSize = false)
	{
		if (!GameManager.instance.gameSettings.showBattleMessages)
		{
			return;
		}
		DamageText displayText = GetDisplayText();
		displayText.damageText.enableAutoSizing = autoSize;
		Camera guiCamera = BattleController.instance.guiCamera;
		Camera cameraForCharacter = GetCameraForCharacter(character);
		Vector3 position = cameraForCharacter.WorldToScreenPoint(character.top.position);
		Vector3 position2 = guiCamera.ScreenToWorldPoint(position) + new Vector3(0f, -10f, 100f);
		if (GUICamera.instance != null)
		{
			float num = 148f;
			float num2 = position2.y + num;
			float y = GUICamera.instance.anchorUpperRight.position.y;
			float num3 = y - num2;
			if (num3 < 0f)
			{
				tk2dCameraAnchorOffsetDeviceOverride component = GUICamera.instance.anchorUpperRight.GetComponent<tk2dCameraAnchorOffsetDeviceOverride>();
				position2.y += num3 + ((!(component != null)) ? 0f : component.Offset.y);
			}
		}
		displayText.transform.position = position2;
		displayText.Init(message);
		displayText.gameObject.SetActive(true);
	}

	public Vector3 GetPositionInGUICamera(BattleCharacter character, bool useCenter = false)
	{
		Camera guiCamera = BattleController.instance.guiCamera;
		Camera cameraForCharacter = GetCameraForCharacter(character);
		Vector3 position = cameraForCharacter.WorldToScreenPoint((!useCenter || !(character.center != null)) ? character.top.position : character.center.position);
		return guiCamera.ScreenToWorldPoint(position) + new Vector3(0f, -30f, 100f);
	}

	public Vector3 GetPositionInGUICamera(BattleCharacter character, Transform transObj)
	{
		Camera guiCamera = BattleController.instance.guiCamera;
		Camera cameraForCharacter = GetCameraForCharacter(character);
		Vector3 position = cameraForCharacter.WorldToScreenPoint(transObj.position);
		return guiCamera.ScreenToWorldPoint(position);
	}

	private DamageText GetDisplayText()
	{
		DamageText objectFromResource = ObjectPool.GetObjectFromResource<DamageText>("Prefabs/GUI/Battle/DamageText_ClassBonus", default(Vector3), false);
		objectFromResource.damageText.enableAutoSizing = false;
		objectFromResource.damageText.fontSize = 45f;
		return objectFromResource;
	}

	private Camera GetCameraForCharacter(BattleCharacter character)
	{
		if (battleController.splitCameras.isSplit)
		{
			if (battleController.playerBattleCamera.isFullscreen)
			{
				return battleController.playerBattleCamera.camera;
			}
			if (battleController.enemyBattleCamera.isFullscreen)
			{
				return battleController.enemyBattleCamera.camera;
			}
			if (battleController.playerParty.Contains(character.character))
			{
				return battleController.playerBattleCamera.camera;
			}
			return battleController.enemyBattleCamera.camera;
		}
		return battleController.splitCameras.activeMainCamera;
	}

	private void OnDestroy()
	{
		_instance = null;
		_battleController = null;
	}
}
