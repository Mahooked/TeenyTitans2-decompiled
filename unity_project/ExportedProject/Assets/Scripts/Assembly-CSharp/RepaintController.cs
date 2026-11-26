using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RepaintController : MonoBehaviour
{
	public static RepaintController instance;

	public Camera camera;

	public Transform cameraRepaintPosition;

	public Transform figureRepaintPosition;

	public Transform showOffRepaintPosition;

	public tk2dUIItem buttonExitShop;

	public tk2dUIItem buttonExitRepaint;

	public tk2dUIItem buttonSurprise;

	public tk2dUIItem buttonRepaint;

	public tk2dUIItem buyButton;

	public TextMeshPro buyButtonText;

	public GameObject buttonRepaintNewTag;

	public GameObject repaintButtonSelector;

	public GameObject repaintShowOffScreen;

	public List<GameObject> repaintShowOffTurnOffObjects;

	public GameObject repaintSprayGroup;

	public float sprayDuration = 1f;

	public Vector3 cameraStartPosition;

	public List<RepaintColorButton> repaintButtons;

	private RepaintColorButton prevColorButtonClick;

	private RepaintColorSlot currentRepaintSlot;

	public MySquad mySquad;

	public CharacterInfoBox characterInfoBox;

	public Camera lowerCamera;

	public Camera upperCamera;

	public Camera upperPerspectiveCamera;

	private MySquadSlot selectedSlot;

	private ShopFigureModel dragFigure;

	private ShopFigureModel prevFigure;

	private Character dragCharacter;

	public TextMeshPro buyButtonCostText;

	public TextMeshPro limitedEditionLockMessage;

	public TextMeshPro currentRainbowCurrency;

	public TextMeshPro currentCurrency;

	public float showOffAutoRotateSpeed;

	public float dragRotateSpeed;

	public float rotationDampening;

	public TextMeshPro msgText;

	[Header("Sound Effects")]
	public AudioClip sfxOnDrag;

	public AudioClip sfxOnPlace;

	public AudioClip sfxOnRemove;

	public AudioClip sfxInfoPane;

	public AudioClip sfxSprayPaint;

	public AudioClip sfxChaChing;

	public AudioClip sfxPurchase;

	[Header("Particle Effects")]
	public ParticleSystem topColorSystem;

	public ParticleSystem leftColorSystem;

	public ParticleSystem rightColorSystem;

	public ParticleSystem botColorSystem;

	public ParticleSystem topSprayFrontSystem;

	public ParticleSystem topSprayBackSystem;

	public ParticleSystem leftSprayFrontSystem;

	public ParticleSystem leftSprayBackSystem;

	public ParticleSystem rightSprayFrontSystem;

	public ParticleSystem rightSprayBackSystem;

	public ParticleSystem botSprayFrontSystem;

	public ParticleSystem botSprayBackSystem;

	public tk2dAnimationAdapter topSprayFrontCone;

	public tk2dAnimationAdapter topSprayBackCone;

	public tk2dAnimationAdapter leftSprayFrontCone;

	public tk2dAnimationAdapter leftSprayBackCone;

	public tk2dAnimationAdapter rightSprayFrontCone;

	public tk2dAnimationAdapter rightSprayBackCone;

	public tk2dAnimationAdapter botSprayFrontCone;

	public tk2dAnimationAdapter botSprayBackCone;

	[Header("Debug")]
	public tk2dUIItem debugAddRainbowCoins;

	public tk2dUIItem debugUnlockRepaints;

	private Vector3 defaultFigureRotation = new Vector3(0f, 180f, 0f);

	private bool hasPlacedFigure;

	private Texture currentFigureDefaultTexture;

	private Texture prevFigureDefaultTexture;

	private Dictionary<string, Texture> defaultCharacterTexCache = new Dictionary<string, Texture>();

	private AudioSource sprayAudioSource;

	private Coroutine startRepaintRoutine;

	private Coroutine showOffRepaintRoutine;

	private Vector2 topPixPercent = new Vector2(0.5f, 0.7f);

	private Vector2 leftPixPercent = new Vector2(0.32f, 0.5f);

	private Vector2 rightPixPercent = new Vector2(0.7f, 0.5f);

	private Vector2 botPixPercent = new Vector2(0.5f, 0.32f);

	private Quaternion rotateBy;

	private Vector3 curTouchPosition;

	private Vector3 prevTouchPosition;

	private bool figurePlacedSettled;

	private Coroutine displayMessageRoutine;

	private bool showingOff;

	private bool exitingScene;

	private bool _isDragEnabled = true;

	public bool isDragEnabled
	{
		get
		{
			return _isDragEnabled;
		}
		set
		{
			_isDragEnabled = value;
		}
	}

	private void Awake()
	{
		instance = this;
		Input.multiTouchEnabled = false;
		int count = mySquad.slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = mySquad.slots[i];
			mySquadSlot.uiItem.OnClickUIItem += HandleOnClickUIItem;
			mySquadSlot.uiItem.OnExitUIItem += HandleOnExitUIItem;
		}
		count = repaintButtons.Count;
		for (int j = 0; j < count; j++)
		{
			RepaintColorButton repaintColorButton = repaintButtons[j];
			repaintColorButton.tk2dUIItem.OnClickUIItem += HandleOnClickRepaintUIItem;
		}
		repaintButtonSelector.SetActive(false);
		buyButton.gameObject.SetActive(false);
		repaintSprayGroup.SetActive(false);
		buttonExitRepaint.gameObject.SetActive(false);
		repaintShowOffScreen.SetActive(false);
		limitedEditionLockMessage.renderer.enabled = false;
		buttonRepaint.OnClick += MoveCameraToRepaintPosition;
		buttonSurprise.OnClick += HandleSurpriseRepaintPressed;
		buyButton.OnClick += HandleBuyButtonPressed;
		buttonExitShop.OnClick += ExitRepaintShopPressed;
		buttonExitRepaint.OnClick += BackPressed;
		RefreshRepaintNewTag();
		Player.instance.RainbowCurrencyUpdated += HandleRainbowCurrencyUpdated;
		Player.instance.CurrencyUpdated += HandleCurrencyUpdated;
		Player.instance.FreeRepaintSlotAdded += HandleFreeRepaintSlotUpdated;
		Player.instance.FreeRepaintSlotSpent += HandleFreeRepaintSlotUpdated;
		HandleRainbowCurrencyUpdated(Player.instance.rainbowCurrency);
		HandleCurrencyUpdated(Player.instance.currency);
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void RefreshRepaintNewTag()
	{
		bool flag = false;
		for (int i = 0; i < Player.instance.activeParty.Count; i++)
		{
			CharacterDefinition characterDefinition = Player.instance.activeParty[i].characterDefinition;
			for (int j = 0; j < characterDefinition.repaintSlots.Count; j++)
			{
				if (Player.instance.HasFreeRepaint(characterDefinition.repaintSlots[j].id))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
		}
		buttonRepaintNewTag.SetActive(flag);
	}

	private void OnDestroy()
	{
		int count = mySquad.slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = mySquad.slots[i];
			if (mySquadSlot != null)
			{
				mySquadSlot.uiItem.OnClickUIItem -= HandleOnClickUIItem;
				mySquadSlot.uiItem.OnExitUIItem -= HandleOnExitUIItem;
			}
		}
		mySquad.CharacterSelected -= MySquadOnCharacterSelected;
		count = repaintButtons.Count;
		for (int j = 0; j < count; j++)
		{
			RepaintColorButton repaintColorButton = repaintButtons[j];
			if (repaintColorButton != null && repaintColorButton.tk2dUIItem != null)
			{
				repaintColorButton.tk2dUIItem.OnClickUIItem -= HandleOnClickRepaintUIItem;
			}
		}
		buttonRepaint.OnClick -= MoveCameraToRepaintPosition;
		buttonSurprise.OnClick -= HandleSurpriseRepaintPressed;
		buyButton.OnClick -= HandleBuyButtonPressed;
		buttonExitShop.OnClick -= ExitRepaintShopPressed;
		buttonExitRepaint.OnClick -= BackPressed;
		Player.instance.RainbowCurrencyUpdated -= HandleRainbowCurrencyUpdated;
		Player.instance.CurrencyUpdated -= HandleCurrencyUpdated;
		Player.instance.FreeRepaintSlotAdded -= HandleFreeRepaintSlotUpdated;
		Player.instance.FreeRepaintSlotSpent -= HandleFreeRepaintSlotUpdated;
		if (debugAddRainbowCoins != null)
		{
			debugAddRainbowCoins.OnClick -= HandleAddRainbowCoins;
		}
		if (debugUnlockRepaints != null)
		{
			debugUnlockRepaints.OnClick -= HandleUnlockAllRepaints;
		}
		Input.multiTouchEnabled = true;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
		instance = null;
	}

	private void Start()
	{
		cameraStartPosition = camera.transform.position;
		mySquad.Init(Player.instance, false, true);
		mySquad.animator.Play("MySquad_outro_big", 0, 1f);
		mySquad.CharacterSelected += MySquadOnCharacterSelected;
		int count = mySquad.slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = mySquad.slots[i];
			if (i < Player.instance.activeParty.Count)
			{
				mySquadSlot.Init(Player.instance.activeParty[i]);
				mySquadSlot.portraitSprite.color = Color.white;
				mySquadSlot.xpFillSprite.gameObject.SetActive(false);
				mySquadSlot.xpBackingSprite.gameObject.SetActive(false);
				mySquadSlot.healthText.gameObject.SetActive(false);
				mySquadSlot.levelText.gameObject.SetActive(false);
				mySquadSlot.showHealth = false;
			}
			else
			{
				mySquadSlot.Init(null);
				mySquadSlot.portraitSprite.color = Color.white;
				mySquadSlot.showHealth = false;
			}
		}
		if (debugAddRainbowCoins != null)
		{
			debugAddRainbowCoins.OnClick += HandleAddRainbowCoins;
		}
		if (debugUnlockRepaints != null)
		{
			debugUnlockRepaints.OnClick += HandleUnlockAllRepaints;
		}
	}

	private void MySquadOnCharacterSelected(Character character)
	{
		if (dragFigure != null && character == dragFigure.character)
		{
			return;
		}
		if (dragFigure != null)
		{
			prevFigure = dragFigure;
		}
		dragCharacter = character;
		hasPlacedFigure = (figurePlacedSettled = false);
		dragFigure = ObjectPool.GetObjectFromResource<ShopFigureModel>(character.characterDefinition.shopFigurePath);
		dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Character"));
		dragFigure.transform.localRotation = Quaternion.Euler(defaultFigureRotation);
		dragFigure.transform.localScale = new Vector3(4f, 4f, 4f);
		dragFigure.character = dragCharacter;
		Texture defaultCharacterTexture = GetDefaultCharacterTexture(dragFigure.character.characterDefinition.characterName);
		if (defaultCharacterTexture == null)
		{
			currentFigureDefaultTexture = dragFigure.renderer.material.mainTexture;
			defaultCharacterTexCache.Add(dragFigure.character.characterDefinition.characterName, currentFigureDefaultTexture);
		}
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(dragFigure.character.uid);
		if (repaintColorPreferenceForCharacterUID > 0)
		{
			RepaintColorSlot repaintColorSlotForID = dragFigure.character.characterDefinition.GetRepaintColorSlotForID(repaintColorPreferenceForCharacterUID);
			if (repaintColorSlotForID != null)
			{
				string texturePathScaled = repaintColorSlotForID.texturePathScaled;
				Texture texture = Resources.Load<Texture>(texturePathScaled);
				if (texture != null)
				{
					dragFigure.UpdateTexture(texture);
				}
			}
		}
		buyButton.gameObject.SetActive(false);
		limitedEditionLockMessage.renderer.enabled = false;
		rotateBy = Quaternion.identity;
		if (prevFigure != null)
		{
			prevFigure.UpdateTexture(GetDefaultCharacterTexture(prevFigure.character.characterDefinition.characterName));
			ObjectPool.PoolObject(prevFigure.gameObject);
		}
		GFAudioManager.instance.PlayClip(sfxOnPlace, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		hasPlacedFigure = true;
		currentFigureDefaultTexture = dragFigure.renderer.material.mainTexture;
		dragFigure.transform.position = figureRepaintPosition.transform.position;
		DropComplete();
	}

	private void HandleAddRainbowCoins()
	{
		Player.instance.AddRainbowCurrency(10);
	}

	private void HandleUnlockAllRepaints()
	{
		int count = Player.instance.characters.Count;
		for (int i = 0; i < count; i++)
		{
			Character character = Player.instance.characters[i];
			for (int j = 0; j < character.characterDefinition.repaintSlots.Count; j++)
			{
				Player.instance.AddToRepaintColorsPurchased(character, character.characterDefinition.repaintSlots[j].id);
			}
		}
	}

	private void HandleRainbowCurrencyUpdated(int currentAmount)
	{
		currentRainbowCurrency.text = currentAmount.ToString();
	}

	private void HandleCurrencyUpdated(int currency)
	{
		currentCurrency.text = currency.ToString();
	}

	private void HandleFreeRepaintSlotUpdated(int colorID)
	{
		if (!figurePlacedSettled)
		{
			return;
		}
		for (int i = 1; i < repaintButtons.Count; i++)
		{
			RepaintColorButton repaintColorButton = repaintButtons[i];
			if (repaintColorButton.gameObject.activeSelf)
			{
				bool isHighlighted = repaintColorButton.isHighlighted;
				repaintColorButton.Init(dragFigure.character.characterDefinition.GetRepaintColorSlotForID(repaintColorButton.id), dragFigure.character);
				repaintColorButton.SetHighlightActive(isHighlighted);
			}
		}
		if (buyButton.gameObject.activeSelf)
		{
			if (Player.instance.HasFreeRepaint(currentRepaintSlot.id))
			{
				buyButtonText.text = Language.Get("REPAINT_SHOP_BUTTON_APPLY", "MENUS,GUI");
				buyButtonCostText.text = string.Empty;
			}
			else
			{
				buyButtonText.text = Language.Get("BUY", "MENUS,GUI");
				buyButtonCostText.text = string.Format("{0}<sprite name=\"icon_currency_rainbowCoin\">", currentRepaintSlot.cost);
			}
		}
	}

	private void HandleOnClickUIItem(tk2dUIItem obj)
	{
		if (!hasPlacedFigure && dragFigure == null)
		{
			selectedSlot = obj.GetComponent<MySquadSlot>();
			Vector3 position = lowerCamera.WorldToScreenPoint(selectedSlot.transform.position + new Vector3(0f, 32f));
			position = upperCamera.ScreenToWorldPoint(position);
			position.z = characterInfoBox.transform.position.z;
		}
	}

	private void HandleOnExitUIItem(tk2dUIItem obj)
	{
		MySquadSlot component = obj.GetComponent<MySquadSlot>();
		if (!_isDragEnabled || component.character == null || (hasPlacedFigure && component.character == dragFigure.character))
		{
			return;
		}
		if (dragFigure != null)
		{
			prevFigure = dragFigure;
		}
		dragCharacter = component.character;
		hasPlacedFigure = (figurePlacedSettled = false);
		dragFigure = ObjectPool.GetObjectFromResource<ShopFigureModel>(component.character.characterDefinition.shopFigurePath);
		dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Character"));
		dragFigure.transform.localRotation = Quaternion.Euler(defaultFigureRotation);
		dragFigure.transform.localScale = new Vector3(4f, 4f, 4f);
		dragFigure.character = dragCharacter;
		Texture defaultCharacterTexture = GetDefaultCharacterTexture(dragFigure.character.characterDefinition.characterName);
		if (defaultCharacterTexture == null)
		{
			currentFigureDefaultTexture = dragFigure.renderer.material.mainTexture;
			defaultCharacterTexCache.Add(dragFigure.character.characterDefinition.characterName, currentFigureDefaultTexture);
		}
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(dragFigure.character.uid);
		if (repaintColorPreferenceForCharacterUID > 0)
		{
			RepaintColorSlot repaintColorSlotForID = dragFigure.character.characterDefinition.GetRepaintColorSlotForID(repaintColorPreferenceForCharacterUID);
			if (repaintColorSlotForID != null)
			{
				string texturePathScaled = repaintColorSlotForID.texturePathScaled;
				Texture texture = Resources.Load<Texture>(texturePathScaled);
				if (texture != null)
				{
					dragFigure.UpdateTexture(texture);
				}
			}
		}
		buyButton.gameObject.SetActive(false);
		limitedEditionLockMessage.renderer.enabled = false;
		Update();
		GFAudioManager.instance.PlayClip(sfxOnDrag, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		rotateBy = Quaternion.identity;
	}

	private void UpdateRepaintButtonPurchaseStatus()
	{
		for (int i = 0; i < repaintButtons.Count; i++)
		{
			RepaintColorButton repaintColorButton = repaintButtons[i];
			if (repaintColorButton.gameObject.activeSelf)
			{
				repaintColorButton.SetPurchased(Player.instance.HasPurchasedRepaintColor(dragFigure.character, repaintColorButton.id));
			}
		}
	}

	private void HandleOnClickRepaintUIItem(tk2dUIItem obj)
	{
		if (dragFigure != null && (!hasPlacedFigure || !figurePlacedSettled))
		{
			return;
		}
		buyButton.gameObject.SetActive(false);
		limitedEditionLockMessage.renderer.enabled = false;
		RepaintColorButton repaintColorButton = null;
		for (int i = 0; i < repaintButtons.Count; i++)
		{
			RepaintColorButton repaintColorButton2 = repaintButtons[i];
			bool flag = obj == repaintColorButton2.tk2dUIItem;
			repaintColorButton2.SetHighlightActive(flag);
			if (flag)
			{
				repaintColorButton = repaintColorButton2;
			}
		}
		prevColorButtonClick = obj.GetComponent<RepaintColorButton>();
		currentRepaintSlot = null;
		for (int j = 0; j < dragFigure.character.characterDefinition.repaintSlots.Count; j++)
		{
			RepaintColorSlot repaintColorSlot = dragFigure.character.characterDefinition.repaintSlots[j];
			if (repaintColorSlot.id == prevColorButtonClick.id)
			{
				currentRepaintSlot = repaintColorSlot;
			}
		}
		if (repaintColorButton != null)
		{
			tk2dSpriteDefinition currentSprite = repaintColorButton.colorSampleSprite.CurrentSprite;
			Texture2D texture2D = (Texture2D)currentSprite.material.mainTexture;
			Vector2 b = new Vector2(texture2D.width, texture2D.height);
			Vector2 vector = currentSprite.uvs[0];
			Vector2 vector2 = currentSprite.uvs[3];
			Vector2 a = new Vector2(Mathf.Lerp(vector.x, vector2.x, topPixPercent.x), Mathf.Lerp(vector.y, vector2.y, topPixPercent.y));
			Vector2 a2 = new Vector2(Mathf.Lerp(vector.x, vector2.x, leftPixPercent.x), Mathf.Lerp(vector.y, vector2.y, leftPixPercent.y));
			Vector2 a3 = new Vector2(Mathf.Lerp(vector.x, vector2.x, rightPixPercent.x), Mathf.Lerp(vector.y, vector2.y, rightPixPercent.y));
			Vector2 a4 = new Vector2(Mathf.Lerp(vector.x, vector2.x, botPixPercent.x), Mathf.Lerp(vector.y, vector2.y, botPixPercent.y));
			Vector2 vector3 = Vector2.Scale(a, b);
			Vector2 vector4 = Vector2.Scale(a2, b);
			Vector2 vector5 = Vector2.Scale(a3, b);
			Vector2 vector6 = Vector2.Scale(a4, b);
			Color pixel = texture2D.GetPixel(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
			Color pixel2 = texture2D.GetPixel(Mathf.RoundToInt(vector4.x), Mathf.RoundToInt(vector4.y));
			Color pixel3 = texture2D.GetPixel(Mathf.RoundToInt(vector5.x), Mathf.RoundToInt(vector5.y));
			Color pixel4 = texture2D.GetPixel(Mathf.RoundToInt(vector6.x), Mathf.RoundToInt(vector6.y));
			Vector3 vector7 = default(Vector3);
			Color.RGBToHSV(pixel, out vector7.x, out vector7.y, out vector7.z);
			Vector3 vector8 = default(Vector3);
			Color.RGBToHSV(pixel2, out vector8.x, out vector8.y, out vector8.z);
			Vector3 vector9 = default(Vector3);
			Color.RGBToHSV(pixel3, out vector9.x, out vector9.y, out vector9.z);
			Vector3 vector10 = default(Vector3);
			Color.RGBToHSV(pixel4, out vector10.x, out vector10.y, out vector10.z);
			ParticleSystem.MinMaxGradient startColor = new ParticleSystem.MinMaxGradient(pixel, Color.HSVToRGB(vector7.x, vector7.y * 0.4f, vector7.z));
			ParticleSystem.MinMaxGradient startColor2 = new ParticleSystem.MinMaxGradient(pixel2, Color.HSVToRGB(vector8.x, vector8.y * 0.4f, vector8.z));
			ParticleSystem.MinMaxGradient startColor3 = new ParticleSystem.MinMaxGradient(pixel3, Color.HSVToRGB(vector9.x, vector9.y * 0.4f, vector9.z));
			ParticleSystem.MinMaxGradient startColor4 = new ParticleSystem.MinMaxGradient(pixel4, Color.HSVToRGB(vector10.x, vector10.y * 0.4f, vector10.z));
			ParticleSystem.MainModule main = topColorSystem.main;
			ParticleSystem.MainModule main2 = leftColorSystem.main;
			ParticleSystem.MainModule main3 = rightColorSystem.main;
			ParticleSystem.MainModule main4 = botColorSystem.main;
			ParticleSystem.MainModule main5 = topSprayFrontSystem.main;
			ParticleSystem.MainModule main6 = topSprayBackSystem.main;
			ParticleSystem.MainModule main7 = leftSprayFrontSystem.main;
			ParticleSystem.MainModule main8 = leftSprayBackSystem.main;
			ParticleSystem.MainModule main9 = rightSprayFrontSystem.main;
			ParticleSystem.MainModule main10 = rightSprayBackSystem.main;
			ParticleSystem.MainModule main11 = botSprayFrontSystem.main;
			ParticleSystem.MainModule main12 = botSprayBackSystem.main;
			main.startColor = startColor;
			main2.startColor = startColor2;
			main3.startColor = startColor3;
			main4.startColor = startColor4;
			main5.startColor = pixel;
			main6.startColor = pixel;
			main7.startColor = pixel2;
			main8.startColor = pixel2;
			main9.startColor = pixel3;
			main10.startColor = pixel3;
			main11.startColor = pixel4;
			main12.startColor = pixel4;
			topSprayFrontCone.color = (topSprayBackCone.color = Color.HSVToRGB(vector7.x, vector7.y * 0.8f, vector7.z));
			leftSprayFrontCone.color = (leftSprayBackCone.color = Color.HSVToRGB(vector8.x, vector8.y * 0.8f, vector8.z));
			rightSprayFrontCone.color = (rightSprayBackCone.color = Color.HSVToRGB(vector9.x, vector9.y * 0.8f, vector9.z));
			botSprayFrontCone.color = (botSprayBackCone.color = Color.HSVToRGB(vector10.x, vector10.y * 0.8f, vector10.z));
		}
		if (startRepaintRoutine != null)
		{
			StopCoroutine(startRepaintRoutine);
		}
		startRepaintRoutine = StartCoroutine(StartRepaint());
	}

	private IEnumerator StartRepaint()
	{
		mySquad.EnableSlotButtons(false);
		if (repaintSprayGroup.activeInHierarchy)
		{
			repaintSprayGroup.SetActive(false);
		}
		repaintSprayGroup.SetActive(true);
		GFAudioManager.instance.StopByAudioSource(sprayAudioSource);
		sprayAudioSource = GFAudioManager.instance.PlayClip(sfxSprayPaint, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		Texture chosenCharacterTexture = null;
		if (currentRepaintSlot == null || currentRepaintSlot.id <= 0)
		{
			Texture defaultCharacterTexture = GetDefaultCharacterTexture(dragFigure.character.characterDefinition.characterName);
			if (defaultCharacterTexture == null)
			{
				chosenCharacterTexture = currentFigureDefaultTexture;
				defaultCharacterTexCache.Add(dragFigure.character.characterDefinition.characterName, currentFigureDefaultTexture);
			}
			else
			{
				chosenCharacterTexture = defaultCharacterTexture;
			}
			Player.instance.SetRepaintColorPreference(dragFigure.character, 0);
			for (int i = 1; i < repaintButtons.Count; i++)
			{
				RepaintColorButton repaintColorButton = repaintButtons[i];
				if (repaintColorButton.gameObject.activeSelf)
				{
					bool isHighlighted = repaintColorButton.isHighlighted;
					repaintColorButton.Init(dragFigure.character.characterDefinition.GetRepaintColorSlotForID(repaintColorButton.id), dragFigure.character);
					repaintColorButton.SetHighlightActive(isHighlighted);
				}
			}
		}
		else
		{
			if (Player.instance.HasPurchasedRepaintColor(dragFigure.character, currentRepaintSlot.id))
			{
				Player.instance.SetRepaintColorPreference(dragFigure.character, currentRepaintSlot.id);
				for (int j = 1; j < repaintButtons.Count; j++)
				{
					RepaintColorButton repaintColorButton2 = repaintButtons[j];
					if (repaintColorButton2.gameObject.activeSelf)
					{
						bool isHighlighted2 = repaintColorButton2.isHighlighted;
						repaintColorButton2.Init(dragFigure.character.characterDefinition.GetRepaintColorSlotForID(repaintColorButton2.id), dragFigure.character);
						repaintColorButton2.SetHighlightActive(isHighlighted2);
					}
				}
			}
			ResourceRequest asyncTexture = Resources.LoadAsync<Texture>(currentRepaintSlot.texturePathScaled);
			while (!asyncTexture.isDone)
			{
				yield return null;
			}
			Texture newTexture = (Texture)asyncTexture.asset;
			if (newTexture != null)
			{
				chosenCharacterTexture = newTexture;
			}
		}
		float delay = 0.2f;
		yield return new WaitForSeconds(delay);
		if (chosenCharacterTexture != null)
		{
			dragFigure.UpdateTexture(chosenCharacterTexture);
		}
		yield return new WaitForSeconds(sprayDuration - delay);
		if (!Player.instance.HasPurchasedRepaintColor(dragFigure.character, prevColorButtonClick.id) && prevColorButtonClick.id > 0)
		{
			if (currentRepaintSlot.rare && !Player.instance.HasAcquiredRepaintSlot(currentRepaintSlot.id))
			{
				limitedEditionLockMessage.renderer.enabled = true;
			}
			else
			{
				buyButton.gameObject.SetActive(true);
				if (Player.instance.HasFreeRepaint(currentRepaintSlot.id))
				{
					buyButtonText.text = Language.Get("REPAINT_SHOP_BUTTON_APPLY", "MENUS,GUI");
					buyButtonCostText.text = string.Empty;
				}
				else
				{
					buyButtonText.text = Language.Get("BUY", "MENUS,GUI");
					buyButtonCostText.text = string.Format("{0}<sprite name=\"icon_currency_rainbowCoin\">", currentRepaintSlot.cost);
				}
			}
		}
		repaintSprayGroup.SetActive(false);
		mySquad.EnableSlotButtons();
	}

	private Texture GetDefaultCharacterTexture(string characterKey)
	{
		Texture value;
		defaultCharacterTexCache.TryGetValue(characterKey, out value);
		return value;
	}

	private void HandleBuyButtonPressed()
	{
		if (currentRepaintSlot == null)
		{
			return;
		}
		bool flag = Player.instance.HasFreeRepaint(currentRepaintSlot.id);
		if (flag || Player.instance.rainbowCurrency >= currentRepaintSlot.cost)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
			if (flag)
			{
				Player.instance.SpendFreeRepaintSlot(currentRepaintSlot.id);
			}
			else
			{
				Player.instance.RemoveRainbowCurrency(currentRepaintSlot.cost);
			}
			Player.instance.AddToRepaintColorsPurchased(dragCharacter, currentRepaintSlot.id);
			Player.instance.SetRepaintColorPreference(dragFigure.character, currentRepaintSlot.id);
			buyButton.gameObject.SetActive(false);
			UpdateRepaintButtonPurchaseStatus();
			GFAudioManager.instance.PlayClip(sfxChaChing, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			if (showOffRepaintRoutine != null)
			{
				StopCoroutine(showOffRepaintRoutine);
			}
			showOffRepaintRoutine = StartCoroutine(StartShowOffRepaint());
			AchievementsDatabase.UnlockAchievementId("TT2_HUEDIDIT", 1);
		}
		else if (Player.instance.rainbowCurrency < currentRepaintSlot.cost)
		{
			DisplayMessage(Language.Get("NOTENOUGHCHANGE", "MENUS,GUI"), new Color(1f, 0.16f, 0.16f));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private IEnumerator StartShowOffRepaint()
	{
		showingOff = true;
		repaintButtonSelector.SetActive(false);
		yield return new WaitForEndOfFrame();
		mySquad.animator.Play("MySquad_outro_big");
		GFAudioManager.instance.PlayClip(sfxPurchase, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
		float tweenDuration = 0.3f;
		Bounds meshBounds = dragFigure.renderer.bounds;
		dragFigure.transform.DOMove(showOffRepaintPosition.position + new Vector3(0f, 0f - meshBounds.extents.y), tweenDuration);
		dragFigure.transform.DORotate(defaultFigureRotation, tweenDuration);
		dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Character"));
		yield return new WaitForSeconds(tweenDuration);
		repaintShowOffScreen.SetActive(true);
		for (int i = 0; i < repaintShowOffTurnOffObjects.Count; i++)
		{
			repaintShowOffTurnOffObjects[i].SetActive(false);
		}
		Tween autoRotateTween = dragFigure.transform.DORotate(new Vector3(0f, 540f), showOffAutoRotateSpeed, RotateMode.FastBeyond360).SetSpeedBased().SetLoops(-1)
			.SetEase(Ease.Linear)
			.SetDelay(0.2f);
		yield return new WaitForSeconds(0.3f);
		yield return new WaitForTap();
		mySquad.ShowNow();
		mySquad.animator.Play("MySquad_Intro_big");
		repaintShowOffScreen.SetActive(false);
		for (int j = 0; j < repaintShowOffTurnOffObjects.Count; j++)
		{
			repaintShowOffTurnOffObjects[j].SetActive(true);
		}
		dragFigure.transform.DOMove(figureRepaintPosition.position, tweenDuration).OnComplete(() =>
		{
			dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		});
		autoRotateTween.Kill();
		dragFigure.transform.DORotate(defaultFigureRotation, tweenDuration);
		repaintButtonSelector.SetActive(true);
		yield return new WaitForSeconds(tweenDuration);
		showingOff = false;
	}

	private void MoveCameraToRepaintPosition()
	{
		camera.transform.DOMove(cameraRepaintPosition.transform.position, 0.4f).SetEase(Ease.OutSine);
		buttonSurprise.gameObject.SetActive(false);
		buttonRepaint.gameObject.SetActive(false);
		buttonExitShop.gameObject.SetActive(false);
		buttonExitRepaint.gameObject.SetActive(true);
		mySquad.ShowNow();
		mySquad.animator.Play("MySquad_Intro_big");
		mySquad.EnableSlotButtons();
		if (debugAddRainbowCoins != null)
		{
			debugAddRainbowCoins.gameObject.SetActive(false);
		}
		if (debugUnlockRepaints != null)
		{
			debugUnlockRepaints.gameObject.SetActive(false);
		}
	}

	private void ExitRepaintShopPressed()
	{
		if (!exitingScene)
		{
			exitingScene = true;
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
	}

	private void BackPressed()
	{
		if (startRepaintRoutine != null)
		{
			StopCoroutine(startRepaintRoutine);
		}
		camera.transform.DOMove(cameraStartPosition, 0.4f).SetEase(Ease.OutSine);
		repaintButtonSelector.SetActive(false);
		buyButton.gameObject.SetActive(false);
		limitedEditionLockMessage.renderer.enabled = false;
		mySquad.animator.Play("MySquad_outro_big");
		if (hasPlacedFigure)
		{
			hasPlacedFigure = false;
			dragFigure.UpdateTexture(GetDefaultCharacterTexture(dragFigure.character.characterDefinition.characterName));
			ObjectPool.PoolObject(dragFigure.gameObject);
		}
		if (prevFigure != null)
		{
			prevFigure.UpdateTexture(GetDefaultCharacterTexture(prevFigure.character.characterDefinition.characterName));
			ObjectPool.PoolObject(prevFigure.gameObject);
		}
		dragFigure = null;
		prevFigure = null;
		figurePlacedSettled = false;
		buttonExitShop.gameObject.SetActive(true);
		buttonExitRepaint.gameObject.SetActive(false);
		StartCoroutine(DelayedActivateMainButtons());
	}

	private IEnumerator DelayedActivateMainButtons()
	{
		yield return null;
		while (mySquad.animator.NormalizedTime() < 0.5f)
		{
			yield return null;
		}
		buttonSurprise.gameObject.SetActive(true);
		buttonRepaint.gameObject.SetActive(true);
		RefreshRepaintNewTag();
	}

	private void Update()
	{
		if (showingOff || LODRequestFlag.isInviteScreenActive)
		{
			return;
		}
		if (dragFigure != null && !hasPlacedFigure)
		{
			if (Input.GetMouseButtonUp(0))
			{
				dragFigure.transform.DOMove(figureRepaintPosition.transform.position, 0.3f).SetEase(Ease.OutBounce).OnComplete(DropComplete);
				if (prevFigure != null)
				{
					prevFigure.UpdateTexture(GetDefaultCharacterTexture(prevFigure.character.characterDefinition.characterName));
					ObjectPool.PoolObject(prevFigure.gameObject);
				}
				GFAudioManager.instance.PlayClip(sfxOnPlace, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
				hasPlacedFigure = true;
				currentFigureDefaultTexture = dragFigure.renderer.material.mainTexture;
			}
			else
			{
				Plane plane = new Plane(Vector3.forward, figureRepaintPosition.position);
				Ray ray = upperPerspectiveCamera.ScreenPointToRay(Input.mousePosition);
				float enter;
				plane.Raycast(ray, out enter);
				Vector3 point = ray.GetPoint(enter);
				dragFigure.transform.position = point;
			}
		}
		else if (dragFigure != null && figurePlacedSettled && tk2dUIManager.Instance.PressedUIItem == null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				prevTouchPosition = (curTouchPosition = Input.mousePosition);
			}
			if (Input.GetMouseButton(0))
			{
				curTouchPosition = Input.mousePosition;
				Vector2 vector = prevTouchPosition - curTouchPosition;
				vector.x /= Screen.dpi;
				vector.y /= Screen.dpi;
				rotateBy = Quaternion.AngleAxis(vector.x * dragRotateSpeed, Vector3.up);
				prevTouchPosition = curTouchPosition;
			}
			if (rotateBy != Quaternion.identity)
			{
				dragFigure.transform.rotation *= rotateBy;
				rotateBy = Quaternion.Lerp(rotateBy, Quaternion.identity, Time.deltaTime * rotationDampening);
			}
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log("Repaint Controller, Save...");
			Player.instance.Save();
		}
		if (Input.GetKeyDown(KeyCode.Backslash) && dragFigure != null && figurePlacedSettled && dragFigure.character.characterDefinition.repaintSlots.Count > 0)
		{
			Player.instance.AddFreeRepaintSlot(dragFigure.character.characterDefinition.repaintSlots[Random.Range(0, dragFigure.character.characterDefinition.repaintSlots.Count)].id);
		}
	}

	private void DropComplete()
	{
		figurePlacedSettled = true;
		for (int i = 0; i < repaintButtons.Count; i++)
		{
			repaintButtons[i].gameObject.SetActive(false);
		}
		int num = 0;
		RepaintColorButton repaintColorButton = repaintButtons[num++];
		repaintColorButton.gameObject.SetActive(true);
		repaintColorButton.InitAsNoColorButton();
		dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		for (int j = 0; j < dragFigure.character.characterDefinition.repaintSlots.Count; j++)
		{
			RepaintColorSlot repaintColorSlot = dragFigure.character.characterDefinition.repaintSlots[j];
			if (num < repaintButtons.Count)
			{
				RepaintColorButton repaintColorButton2 = repaintButtons[num++];
				repaintColorButton2.gameObject.SetActive(true);
				repaintColorButton2.Init(repaintColorSlot, dragFigure.character);
			}
		}
		UpdateRepaintButtonPurchaseStatus();
		repaintButtonSelector.SetActive(true);
	}

	public void HandleSurpriseRepaintPressed()
	{
		StartCoroutine(WaitForSurpriseRepaintDialog());
	}

	private IEnumerator WaitForSurpriseRepaintDialog()
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window_SurpriseRepaint", default(Vector3), false);
		dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI2"));
		dialog.transform.SetParent(tk2dCamera.CameraForLayer(LayerMask.NameToLayer("UI2")).transform, true);
		dialog.transform.localPosition = new Vector3(0f, 0f, 200f);
		dialog.descriptionText.text = Language.Get("REPAINT_SHOP_MESSAGE_SURPRISE_REPAINT", "MENUS,GUI");
		dialog.amountText.text = string.Format("{0}<sprite name=\"icon_currency_rainbowCoin\">", GameManager.instance.gameSettings.surpriseRepaintCost);
		dialog.gameObject.SetActive(true);
		dialog.confirmButton.enabled = Player.instance.HasCurrency(CurrencyType.RainbowCoins, GameManager.instance.gameSettings.surpriseRepaintCost);
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed && Player.instance.HasCurrency(CurrencyType.RainbowCoins, GameManager.instance.gameSettings.surpriseRepaintCost))
		{
			PlayerReward randomRepaintReward = PlayerRewardFactory.GetRandomRepaintReward();
			if (randomRepaintReward != null)
			{
				Player.instance.RemoveCurrency(CurrencyType.RainbowCoins, GameManager.instance.gameSettings.surpriseRepaintCost);
				Player.instance.AddFreeRepaintSlot(randomRepaintReward.repaintUID);
				CityRewardReveal.Show(randomRepaintReward);
				RefreshRepaintNewTag();
			}
		}
		ObjectPool.PoolObject(dialog.gameObject);
	}

	public void DisplayMessage(string text, Color textColor)
	{
		if (displayMessageRoutine != null)
		{
			StopCoroutine(displayMessageRoutine);
		}
		displayMessageRoutine = StartCoroutine(DisplayMessageWithText(text, textColor));
	}

	private IEnumerator DisplayMessageWithText(string text, Color textColor)
	{
		msgText.gameObject.SetActive(true);
		msgText.text = text;
		msgText.color = textColor;
		yield return new WaitForSeconds(1f);
		msgText.gameObject.SetActive(false);
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		exitingScene = true;
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
