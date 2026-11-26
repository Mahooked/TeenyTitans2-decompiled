using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro.Examples;

public class TMP_TextSelector_B : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IEventSystemHandler
{
	public RectTransform TextPopup_Prefab_01;

	private RectTransform m_TextPopup_RectTransform;

	private TextMeshProUGUI m_TextPopup_TMPComponent;

	private const string k_LinkText = "You have selected link <#ffff00>";

	private const string k_WordText = "Word Index: <#ffff00>";

	private TextMeshProUGUI m_TextMeshPro;

	private Canvas m_Canvas;

	private Camera m_Camera;

	private bool isHoveringObject;

	private int m_selectedWord = -1;

	private int m_selectedLink = -1;

	private int m_lastIndex = -1;

	private Matrix4x4 m_matrix;

	private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

	private void Awake()
	{
		m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
		m_Canvas = gameObject.GetComponentInParent<Canvas>();
		if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			m_Camera = null;
		}
		else
		{
			m_Camera = m_Canvas.worldCamera;
		}
		m_TextPopup_RectTransform = Object.Instantiate(TextPopup_Prefab_01);
		m_TextPopup_RectTransform.SetParent(m_Canvas.transform, worldPositionStays: false);
		m_TextPopup_TMPComponent = m_TextPopup_RectTransform.GetComponentInChildren<TextMeshProUGUI>();
		m_TextPopup_RectTransform.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
	}

	private void OnDisable()
	{
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
	}

	private void ON_TEXT_CHANGED(Object obj)
	{
		if (obj == m_TextMeshPro)
		{
			m_cachedMeshInfoVertexData = m_TextMeshPro.textInfo.CopyMeshInfoVertexData();
		}
	}

	private void LateUpdate()
	{
		if (isHoveringObject)
		{
			int num = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, Input.mousePosition, m_Camera, visibleOnly: true);
			if (num == -1 || num != m_lastIndex)
			{
				RestoreCachedVertexAttributes(m_lastIndex);
				m_lastIndex = -1;
			}
			if (num != -1 && num != m_lastIndex && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				m_lastIndex = num;
				int materialReferenceIndex = m_TextMeshPro.textInfo.characterInfo[num].materialReferenceIndex;
				int vertexIndex = m_TextMeshPro.textInfo.characterInfo[num].vertexIndex;
				Vector3[] vertices = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].vertices;
				Vector2 vector = (vertices[vertexIndex] + vertices[vertexIndex + 2]) / 2f;
				Vector3 vector2 = vector;
				ref Vector3 reference = ref vertices[vertexIndex];
				reference = vertices[vertexIndex] - vector2;
				ref Vector3 reference2 = ref vertices[vertexIndex + 1];
				reference2 = vertices[vertexIndex + 1] - vector2;
				ref Vector3 reference3 = ref vertices[vertexIndex + 2];
				reference3 = vertices[vertexIndex + 2] - vector2;
				ref Vector3 reference4 = ref vertices[vertexIndex + 3];
				reference4 = vertices[vertexIndex + 3] - vector2;
				float num2 = 1.5f;
				m_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * num2);
				ref Vector3 reference5 = ref vertices[vertexIndex];
				reference5 = m_matrix.MultiplyPoint3x4(vertices[vertexIndex]);
				ref Vector3 reference6 = ref vertices[vertexIndex + 1];
				reference6 = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
				ref Vector3 reference7 = ref vertices[vertexIndex + 2];
				reference7 = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
				ref Vector3 reference8 = ref vertices[vertexIndex + 3];
				reference8 = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);
				ref Vector3 reference9 = ref vertices[vertexIndex];
				reference9 = vertices[vertexIndex] + vector2;
				ref Vector3 reference10 = ref vertices[vertexIndex + 1];
				reference10 = vertices[vertexIndex + 1] + vector2;
				ref Vector3 reference11 = ref vertices[vertexIndex + 2];
				reference11 = vertices[vertexIndex + 2] + vector2;
				ref Vector3 reference12 = ref vertices[vertexIndex + 3];
				reference12 = vertices[vertexIndex + 3] + vector2;
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 192, byte.MaxValue);
				Color32[] colors = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
				colors[vertexIndex] = color;
				colors[vertexIndex + 1] = color;
				colors[vertexIndex + 2] = color;
				colors[vertexIndex + 3] = color;
				TMP_MeshInfo tMP_MeshInfo = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex];
				int dst = vertices.Length - 4;
				tMP_MeshInfo.SwapVertexData(vertexIndex, dst);
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
			}
			int num3 = TMP_TextUtilities.FindIntersectingWord(m_TextMeshPro, Input.mousePosition, m_Camera);
			if (m_TextPopup_RectTransform != null && m_selectedWord != -1 && (num3 == -1 || num3 != m_selectedWord))
			{
				TMP_WordInfo tMP_WordInfo = m_TextMeshPro.textInfo.wordInfo[m_selectedWord];
				for (int i = 0; i < tMP_WordInfo.characterCount; i++)
				{
					int num4 = tMP_WordInfo.firstCharacterIndex + i;
					int materialReferenceIndex2 = m_TextMeshPro.textInfo.characterInfo[num4].materialReferenceIndex;
					int vertexIndex2 = m_TextMeshPro.textInfo.characterInfo[num4].vertexIndex;
					Color32[] colors2 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex2].colors32;
					Color32 color2 = colors2[vertexIndex2].Tint(1.33333f);
					colors2[vertexIndex2] = color2;
					colors2[vertexIndex2 + 1] = color2;
					colors2[vertexIndex2 + 2] = color2;
					colors2[vertexIndex2 + 3] = color2;
				}
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
				m_selectedWord = -1;
			}
			if (num3 != -1 && num3 != m_selectedWord && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				m_selectedWord = num3;
				TMP_WordInfo tMP_WordInfo2 = m_TextMeshPro.textInfo.wordInfo[num3];
				for (int j = 0; j < tMP_WordInfo2.characterCount; j++)
				{
					int num5 = tMP_WordInfo2.firstCharacterIndex + j;
					int materialReferenceIndex3 = m_TextMeshPro.textInfo.characterInfo[num5].materialReferenceIndex;
					int vertexIndex3 = m_TextMeshPro.textInfo.characterInfo[num5].vertexIndex;
					Color32[] colors3 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex3].colors32;
					Color32 color3 = colors3[vertexIndex3].Tint(0.75f);
					colors3[vertexIndex3] = color3;
					colors3[vertexIndex3 + 1] = color3;
					colors3[vertexIndex3 + 2] = color3;
					colors3[vertexIndex3 + 3] = color3;
				}
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
			}
			int num6 = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);
			if ((num6 == -1 && m_selectedLink != -1) || num6 != m_selectedLink)
			{
				m_TextPopup_RectTransform.gameObject.SetActive(value: false);
				m_selectedLink = -1;
			}
			if (num6 != -1 && num6 != m_selectedLink)
			{
				m_selectedLink = num6;
				TMP_LinkInfo tMP_LinkInfo = m_TextMeshPro.textInfo.linkInfo[num6];
				Vector3 worldPoint = Vector3.zero;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(m_TextMeshPro.rectTransform, Input.mousePosition, m_Camera, out worldPoint);
				switch (tMP_LinkInfo.GetLinkID())
				{
				case "id_01":
					m_TextPopup_RectTransform.position = worldPoint;
					m_TextPopup_RectTransform.gameObject.SetActive(value: true);
					m_TextPopup_TMPComponent.text = "You have selected link <#ffff00> ID 01";
					break;
				case "id_02":
					m_TextPopup_RectTransform.position = worldPoint;
					m_TextPopup_RectTransform.gameObject.SetActive(value: true);
					m_TextPopup_TMPComponent.text = "You have selected link <#ffff00> ID 02";
					break;
				}
			}
		}
		else if (m_lastIndex != -1)
		{
			RestoreCachedVertexAttributes(m_lastIndex);
			m_lastIndex = -1;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHoveringObject = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHoveringObject = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	private void RestoreCachedVertexAttributes(int index)
	{
		if (index != -1 && index <= m_TextMeshPro.textInfo.characterCount - 1)
		{
			int materialReferenceIndex = m_TextMeshPro.textInfo.characterInfo[index].materialReferenceIndex;
			int vertexIndex = m_TextMeshPro.textInfo.characterInfo[index].vertexIndex;
			Vector3[] vertices = m_cachedMeshInfoVertexData[materialReferenceIndex].vertices;
			Vector3[] vertices2 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].vertices;
			ref Vector3 reference = ref vertices2[vertexIndex];
			reference = vertices[vertexIndex];
			ref Vector3 reference2 = ref vertices2[vertexIndex + 1];
			reference2 = vertices[vertexIndex + 1];
			ref Vector3 reference3 = ref vertices2[vertexIndex + 2];
			reference3 = vertices[vertexIndex + 2];
			ref Vector3 reference4 = ref vertices2[vertexIndex + 3];
			reference4 = vertices[vertexIndex + 3];
			Color32[] colors = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
			Color32[] colors2 = m_cachedMeshInfoVertexData[materialReferenceIndex].colors32;
			ref Color32 reference5 = ref colors[vertexIndex];
			reference5 = colors2[vertexIndex];
			ref Color32 reference6 = ref colors[vertexIndex + 1];
			reference6 = colors2[vertexIndex + 1];
			ref Color32 reference7 = ref colors[vertexIndex + 2];
			reference7 = colors2[vertexIndex + 2];
			ref Color32 reference8 = ref colors[vertexIndex + 3];
			reference8 = colors2[vertexIndex + 3];
			Vector2[] uvs = m_cachedMeshInfoVertexData[materialReferenceIndex].uvs0;
			Vector2[] uvs2 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].uvs0;
			ref Vector2 reference9 = ref uvs2[vertexIndex];
			reference9 = uvs[vertexIndex];
			ref Vector2 reference10 = ref uvs2[vertexIndex + 1];
			reference10 = uvs[vertexIndex + 1];
			ref Vector2 reference11 = ref uvs2[vertexIndex + 2];
			reference11 = uvs[vertexIndex + 2];
			ref Vector2 reference12 = ref uvs2[vertexIndex + 3];
			reference12 = uvs[vertexIndex + 3];
			Vector2[] uvs3 = m_cachedMeshInfoVertexData[materialReferenceIndex].uvs2;
			Vector2[] uvs4 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].uvs2;
			ref Vector2 reference13 = ref uvs4[vertexIndex];
			reference13 = uvs3[vertexIndex];
			ref Vector2 reference14 = ref uvs4[vertexIndex + 1];
			reference14 = uvs3[vertexIndex + 1];
			ref Vector2 reference15 = ref uvs4[vertexIndex + 2];
			reference15 = uvs3[vertexIndex + 2];
			ref Vector2 reference16 = ref uvs4[vertexIndex + 3];
			reference16 = uvs3[vertexIndex + 3];
			int num = (vertices.Length / 4 - 1) * 4;
			ref Vector3 reference17 = ref vertices2[num];
			reference17 = vertices[num];
			ref Vector3 reference18 = ref vertices2[num + 1];
			reference18 = vertices[num + 1];
			ref Vector3 reference19 = ref vertices2[num + 2];
			reference19 = vertices[num + 2];
			ref Vector3 reference20 = ref vertices2[num + 3];
			reference20 = vertices[num + 3];
			colors2 = m_cachedMeshInfoVertexData[materialReferenceIndex].colors32;
			colors = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
			ref Color32 reference21 = ref colors[num];
			reference21 = colors2[num];
			ref Color32 reference22 = ref colors[num + 1];
			reference22 = colors2[num + 1];
			ref Color32 reference23 = ref colors[num + 2];
			reference23 = colors2[num + 2];
			ref Color32 reference24 = ref colors[num + 3];
			reference24 = colors2[num + 3];
			uvs = m_cachedMeshInfoVertexData[materialReferenceIndex].uvs0;
			uvs2 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].uvs0;
			ref Vector2 reference25 = ref uvs2[num];
			reference25 = uvs[num];
			ref Vector2 reference26 = ref uvs2[num + 1];
			reference26 = uvs[num + 1];
			ref Vector2 reference27 = ref uvs2[num + 2];
			reference27 = uvs[num + 2];
			ref Vector2 reference28 = ref uvs2[num + 3];
			reference28 = uvs[num + 3];
			uvs3 = m_cachedMeshInfoVertexData[materialReferenceIndex].uvs2;
			uvs4 = m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].uvs2;
			ref Vector2 reference29 = ref uvs4[num];
			reference29 = uvs3[num];
			ref Vector2 reference30 = ref uvs4[num + 1];
			reference30 = uvs3[num + 1];
			ref Vector2 reference31 = ref uvs4[num + 2];
			reference31 = uvs3[num + 2];
			ref Vector2 reference32 = ref uvs4[num + 3];
			reference32 = uvs3[num + 3];
			m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
		}
	}
}
