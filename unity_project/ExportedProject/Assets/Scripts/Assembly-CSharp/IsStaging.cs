using UnityEngine;

public class IsStaging : MonoBehaviour
{
	public bool staging;

	public void toggleStaging(bool toggle)
	{
		staging = !staging;
	}
}
