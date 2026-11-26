using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.Examples;

public class VertexZoom : MonoBehaviour
{
	public float AngleMultiplier = 1f;

	public float SpeedMultiplier = 1f;

	public float CurveScale = 1f;

	private TMP_Text m_TextComponent;

	private bool hasTextChanged;

	private void Awake()
	{
		m_TextComponent = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
	}

	private void OnDisable()
	{
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
	}

	private void Start()
	{
		StartCoroutine(AnimateVertexColors());
	}

	private void ON_TEXT_CHANGED(Object obj)
	{
		if (obj == m_TextComponent)
		{
			hasTextChanged = true;
		}
	}

	private IEnumerator AnimateVertexColors()
	{
		m_TextComponent.ForceMeshUpdate();
		TMP_TextInfo textInfo = m_TextComponent.textInfo;
		TMP_MeshInfo[] cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();
		List<float> modifiedCharScale = new List<float>();
		List<int> scaleSortingOrder = new List<int>();
		hasTextChanged = true;
		while (true)
		{
			if (hasTextChanged)
			{
				cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();
				hasTextChanged = false;
			}
			int characterCount = textInfo.characterCount;
			if (characterCount == 0)
			{
				yield return new WaitForSeconds(0.25f);
				continue;
			}
			modifiedCharScale.Clear();
			scaleSortingOrder.Clear();
			for (int i = 0; i < characterCount; i++)
			{
				TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[i];
				if (tMP_CharacterInfo.isVisible)
				{
					int materialReferenceIndex = textInfo.characterInfo[i].materialReferenceIndex;
					int vertexIndex = textInfo.characterInfo[i].vertexIndex;
					Vector3[] vertices = cachedMeshInfoVertexData[materialReferenceIndex].vertices;
					Vector2 vector = (vertices[vertexIndex] + vertices[vertexIndex + 2]) / 2f;
					Vector3 vector2 = vector;
					Vector3[] vertices2 = textInfo.meshInfo[materialReferenceIndex].vertices;
					ref Vector3 reference = ref vertices2[vertexIndex];
					reference = vertices[vertexIndex] - vector2;
					ref Vector3 reference2 = ref vertices2[vertexIndex + 1];
					reference2 = vertices[vertexIndex + 1] - vector2;
					ref Vector3 reference3 = ref vertices2[vertexIndex + 2];
					reference3 = vertices[vertexIndex + 2] - vector2;
					ref Vector3 reference4 = ref vertices2[vertexIndex + 3];
					reference4 = vertices[vertexIndex + 3] - vector2;
					float num = Random.Range(1f, 1.5f);
					modifiedCharScale.Add(num);
					scaleSortingOrder.Add(modifiedCharScale.Count - 1);
					Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, Vector3.one * num);
					ref Vector3 reference5 = ref vertices2[vertexIndex];
					reference5 = matrix.MultiplyPoint3x4(vertices2[vertexIndex]);
					ref Vector3 reference6 = ref vertices2[vertexIndex + 1];
					reference6 = matrix.MultiplyPoint3x4(vertices2[vertexIndex + 1]);
					ref Vector3 reference7 = ref vertices2[vertexIndex + 2];
					reference7 = matrix.MultiplyPoint3x4(vertices2[vertexIndex + 2]);
					ref Vector3 reference8 = ref vertices2[vertexIndex + 3];
					reference8 = matrix.MultiplyPoint3x4(vertices2[vertexIndex + 3]);
					vertices2[vertexIndex] += vector2;
					vertices2[vertexIndex + 1] += vector2;
					vertices2[vertexIndex + 2] += vector2;
					vertices2[vertexIndex + 3] += vector2;
					Vector2[] uvs = cachedMeshInfoVertexData[materialReferenceIndex].uvs0;
					Vector2[] uvs2 = textInfo.meshInfo[materialReferenceIndex].uvs0;
					ref Vector2 reference9 = ref uvs2[vertexIndex];
					reference9 = uvs[vertexIndex];
					ref Vector2 reference10 = ref uvs2[vertexIndex + 1];
					reference10 = uvs[vertexIndex + 1];
					ref Vector2 reference11 = ref uvs2[vertexIndex + 2];
					reference11 = uvs[vertexIndex + 2];
					ref Vector2 reference12 = ref uvs2[vertexIndex + 3];
					reference12 = uvs[vertexIndex + 3];
					Color32[] colors = cachedMeshInfoVertexData[materialReferenceIndex].colors32;
					Color32[] colors2 = textInfo.meshInfo[materialReferenceIndex].colors32;
					ref Color32 reference13 = ref colors2[vertexIndex];
					reference13 = colors[vertexIndex];
					ref Color32 reference14 = ref colors2[vertexIndex + 1];
					reference14 = colors[vertexIndex + 1];
					ref Color32 reference15 = ref colors2[vertexIndex + 2];
					reference15 = colors[vertexIndex + 2];
					ref Color32 reference16 = ref colors2[vertexIndex + 3];
					reference16 = colors[vertexIndex + 3];
				}
			}
			for (int j = 0; j < textInfo.meshInfo.Length; j++)
			{
				scaleSortingOrder.Sort((int a, int b) => modifiedCharScale[a].CompareTo(modifiedCharScale[b]));
				textInfo.meshInfo[j].SortGeometry(scaleSortingOrder);
				textInfo.meshInfo[j].mesh.vertices = textInfo.meshInfo[j].vertices;
				textInfo.meshInfo[j].mesh.uv = textInfo.meshInfo[j].uvs0;
				textInfo.meshInfo[j].mesh.colors32 = textInfo.meshInfo[j].colors32;
				m_TextComponent.UpdateGeometry(textInfo.meshInfo[j].mesh, j);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
