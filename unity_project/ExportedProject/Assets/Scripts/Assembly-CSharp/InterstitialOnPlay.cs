using UnityEngine;

public class InterstitialOnPlay : MonoBehaviour
{
	private void Start()
	{
		DFPKeyValues.Instance.LoadValues(DFP.Instance.DFPGameConfig);
		DFP.Instance.RequestInterstitial(DFPKeyValues.Instance);
	}
}
