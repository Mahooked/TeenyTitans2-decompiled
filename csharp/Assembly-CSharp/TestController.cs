using System.Collections;
using UnityEngine;

public class TestController : MonoBehaviour
{
	public Transform textMesh;

	private bool isSpriteHighlighted;

	private bool isModelHighlighted;

	private IEnumerator Start()
	{
		yield break;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			textMesh.transform.position += new Vector3(1f, 0f, 0f);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			textMesh.transform.position += new Vector3(-1f, 0f, 0f);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			textMesh.transform.position += new Vector3(0f, 1f, 0f);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			textMesh.transform.position += new Vector3(0f, -1f, 0f);
		}
	}
}
