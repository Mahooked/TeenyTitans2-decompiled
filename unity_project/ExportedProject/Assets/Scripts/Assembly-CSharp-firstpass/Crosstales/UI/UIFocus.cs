using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.UI;

public class UIFocus : MonoBehaviour
{
	public string CanvasName = "Canvas";

	private UIWindowManager manager;

	private Image image;

	public void Start()
	{
		manager = GameObject.Find(CanvasName).GetComponent<UIWindowManager>();
		image = transform.Find("Panel/Header").GetComponent<Image>();
	}

	public void OnPanelEnter()
	{
		manager.ChangeState(gameObject);
		Color color = image.color;
		color.a = 255f;
		image.color = color;
		transform.SetAsLastSibling();
		transform.SetAsFirstSibling();
		transform.SetSiblingIndex(-1);
		transform.GetSiblingIndex();
	}
}
