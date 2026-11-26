using System.Collections.Generic;
using UnityEngine;

public class ApplyNativeAd : MonoBehaviour
{
	public string AdUnitId;

	public List<string> TemplateIds;

	public void Start()
	{
		if (transform.parent != null)
		{
			DFP.Instance.LoadCustomNativeTemplateAd(transform.parent.gameObject, AdUnitId, TemplateIds);
			Debug.Log("native ad");
		}
	}
}
