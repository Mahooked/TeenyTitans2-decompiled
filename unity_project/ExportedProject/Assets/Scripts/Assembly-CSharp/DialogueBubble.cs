using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
	public Animator animator;

	public tk2dSlicedSprite background;

	public TextMeshPro messageTextMesh;

	public tk2dSprite tailSprite;

	public tk2dUIItem button;

	public tk2dSprite buttonSprite;

	public tk2dSprite buttonWhiteSprite;

	public float printTextDelay = 0.03f;

	public AudioClip printTextSound;

	private bool _isFlipped;

	private float _bubbleOffsetX;

	private Vector3 _buttonPosition;

	public Bounds _textBounds;

	private string _formattedText;

	private tk2dSlicedSprite _curBackground;

	private TextMeshPro _curTextMesh;

	private CharacterName _character;

	public CharacterName character
	{
		get
		{
			return _character;
		}
		set
		{
			_character = value;
			RefreshSkin();
		}
	}

	public bool isFlipped
	{
		get
		{
			return _isFlipped;
		}
		set
		{
			if (value != _isFlipped)
			{
				_isFlipped = value;
				Refresh();
			}
		}
	}

	private void Awake()
	{
		_curBackground = background;
		_curTextMesh = messageTextMesh;
		_bubbleOffsetX = background.transform.localPosition.x;
		if (button != null)
		{
			_buttonPosition = button.transform.localPosition;
		}
	}

	private void OnEnable()
	{
		animator.enabled = true;
	}

	private void Update()
	{
		if (messageTextMesh != null)
		{
			_textBounds = messageTextMesh.bounds;
		}
	}

	public void SetText(string newText)
	{
		messageTextMesh.text = newText;
		messageTextMesh.ForceMeshUpdate();
		_textBounds = messageTextMesh.bounds;
		if (_curTextMesh == null)
		{
			_curTextMesh = messageTextMesh;
		}
		_formattedText = newText;
		Vector3 size = _textBounds.size;
		messageTextMesh.maxVisibleCharacters = 0;
		if (_curBackground == null)
		{
			_curBackground = background;
		}
		float num = 30f;
		if (Language.CurrentLanguage() == LanguageCode.JA)
		{
			TMP_CharacterInfo[] characterInfo = messageTextMesh.textInfo.characterInfo;
			float num2 = 0f;
			int num3 = 0;
			for (int i = 0; i < messageTextMesh.textInfo.characterCount; i++)
			{
				TMP_CharacterInfo tMP_CharacterInfo = messageTextMesh.textInfo.characterInfo[i];
				Vector3 vector = messageTextMesh.transform.TransformPoint(tMP_CharacterInfo.topLeft);
				Vector3 vector2 = messageTextMesh.transform.TransformPoint(tMP_CharacterInfo.topRight);
				float num4 = Mathf.Abs(vector.x - vector2.x);
				num2 += num4;
				if (num2 > _textBounds.size.x)
				{
					num3++;
					num2 = 0f;
				}
			}
			num = 30 + 20 * num3;
		}
		Vector2 dimensions = _curBackground.dimensions;
		dimensions.x = Mathf.Max(120f, size.x + 30f);
		dimensions.y = size.y + num;
		_curBackground.dimensions = dimensions;
		_curTextMesh.transform.localPosition = new Vector3(background.transform.localPosition.x, background.transform.localPosition.y + background.dimensions.y / 2f, -2f);
		messageTextMesh.rectTransform.sizeDelta = new Vector2(dimensions.x, dimensions.y);
		Refresh();
	}

	public IEnumerator Print()
	{
		char[] chars = _formattedText.ToCharArray();
		int numChars = chars.Length;
		int index = 0;
		messageTextMesh.text = _formattedText;
		messageTextMesh.maxVisibleCharacters = 0;
		GFAudioManager.instance.StopById("printText");
		GFAudioManager.instance.PlayClip(printTextSound, "printText", 1f, true, 0f, 128, GFAudioManager.SFX_GROUP);
		while (index < numChars)
		{
			float delay = printTextDelay;
			if (Input.GetMouseButton(0))
			{
				messageTextMesh.text = _formattedText;
				index = numChars;
				messageTextMesh.maxVisibleCharacters = index;
				delay = 0f;
				AudioSource audioSourceById = GFAudioManager.instance.GetAudioSourceById("printText");
				if (audioSourceById != null)
				{
					audioSourceById.volume = 0f;
				}
			}
			else
			{
				index++;
				messageTextMesh.maxVisibleCharacters = index;
				if (index == numChars)
				{
					AudioSource audioSourceById2 = GFAudioManager.instance.GetAudioSourceById("printText");
					if (audioSourceById2 != null)
					{
						audioSourceById2.volume = 0f;
					}
				}
			}
			yield return new WaitForSeconds(delay);
		}
		AudioSource printAudioSource = GFAudioManager.instance.GetAudioSourceById("printText");
		if (printAudioSource != null)
		{
			GFAudioManager.instance.StopById("printText");
		}
	}

	private void Refresh()
	{
		if (_isFlipped)
		{
			_curBackground.FlipX = true;
			Vector3 localPosition = background.transform.localPosition;
			localPosition.x = 0f - _bubbleOffsetX;
			_curBackground.transform.localPosition = localPosition;
			localPosition = _buttonPosition;
			localPosition.x = _curBackground.transform.localPosition.x + _curBackground.dimensions.x - 5f;
			localPosition.y = _curBackground.transform.localPosition.y + _curBackground.dimensions.y - 6f;
			if (button != null)
			{
				button.transform.localPosition = localPosition;
			}
			return;
		}
		_curBackground.FlipX = false;
		Vector3 localPosition2 = _curBackground.transform.localPosition;
		localPosition2.x = 0f - _bubbleOffsetX;
		_curBackground.transform.localPosition = localPosition2;
		localPosition2 = _buttonPosition;
		localPosition2.x = _curBackground.transform.localPosition.x - 5f;
		localPosition2.y = _curBackground.transform.localPosition.y + _curBackground.dimensions.y - 6f;
		if (button != null)
		{
			button.transform.localPosition = localPosition2;
		}
		if (_curTextMesh == null)
		{
			_curTextMesh = messageTextMesh;
		}
	}

	private void RefreshSkin()
	{
		background.gameObject.SetActive(true);
		messageTextMesh.gameObject.SetActive(true);
		_curBackground = background;
		NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(_character);
		Material fontMaterial = messageTextMesh.fontMaterial;
		fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, nPCDefByType.dialogueFaceColor);
		fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, nPCDefByType.dialogueOutlineColor);
		messageTextMesh.fontMaterial = fontMaterial;
		_curTextMesh = messageTextMesh;
		_curBackground.SetSprite(nPCDefByType.conversationBubbleSprite);
		tailSprite.SetSprite(nPCDefByType.conversationBubbleSprite.Replace("TTG_gui_dialoguebox_", "TTG_gui_dialoguebox_tail_"));
	}

	public IEnumerator StartMoveUp()
	{
		yield return new WaitForEndOfFrame();
		animator.enabled = true;
		animator.Play("Dialogue_P1_Bubble_Up", 0, 0f);
	}
}
