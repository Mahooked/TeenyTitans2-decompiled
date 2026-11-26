using TMPro;

public class SequentialTextTMPro : SequentialText
{
	public TextMeshPro textMesh;

	protected override void UpdateText(string text)
	{
		textMesh.text = text;
	}
}
