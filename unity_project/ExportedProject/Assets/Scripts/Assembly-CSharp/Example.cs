using UnityEngine;

[ExecuteInEditMode]
public class Example : MonoBehaviour
{
	[Multiline(16)]
	public string Text;

	private GUIStyle style;

	private Color color = new Color(0f, 0f, 0f, 0.9f);

	private void OnGUI()
	{
		if (style == null)
		{
			style = new GUIStyle(GUI.skin.label);
			style.normal.background = new Texture2D(1, 1);
			style.normal.background.SetPixel(0, 0, Color.black);
			style.padding = new RectOffset(5, 5, 5, 5);
		}
		Color backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = color;
		Vector2 vector = GUI.skin.label.CalcSize(new GUIContent(Text));
		GUILayout.BeginArea(new Rect(50f, (float)Screen.height - vector.y - 50f, vector.x + 20f, vector.y + 20f));
		GUILayout.Label(Text, style);
		GUILayout.EndArea();
		GUI.backgroundColor = backgroundColor;
	}
}
