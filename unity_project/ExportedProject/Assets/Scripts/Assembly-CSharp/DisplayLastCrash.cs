using TMPro;
using UnityEngine;

public class DisplayLastCrash : MonoBehaviour
{
	public TextMeshPro textTimeMesh;

	public TextMeshPro textDescrMesh;

	private void UpdatedTextWithLastCrash()
	{
		string text = "***************** /n";
		for (int i = 0; i < CrashReport.reports.Length; i++)
		{
			text = text + CrashReport.reports[i].time.ToLongDateString() + "/n";
			text = text + CrashReport.reports[i].text + "/n";
			text += "******************* /n";
		}
		if (CrashReport.reports.Length > 0)
		{
			SocialSystem.DisplayTOUPP("CRASHES", text, "cancel", "ok", "close");
			return;
		}
		textTimeMesh.text = string.Empty;
		textDescrMesh.text = "No crashes";
	}
}
