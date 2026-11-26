using UnityEngine;

public class UI_AddImage : MonoBehaviour
{
	public void AddImage()
	{
		Transform child = transform.GetChild(Random.Range(0, transform.childCount));
		GameObject gameObject = Object.Instantiate(child.gameObject);
		gameObject.transform.SetParent(transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		gameObject.transform.localPosition = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), child.localPosition.z);
	}
}
