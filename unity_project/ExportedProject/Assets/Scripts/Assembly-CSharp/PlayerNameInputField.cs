using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
	private static string playerNamePrefKey = "PlayerName";

	private void Start()
	{
		string nickName = string.Empty;
		InputField component = GetComponent<InputField>();
		if (component != null && PlayerPrefs.HasKey(playerNamePrefKey))
		{
			nickName = (component.text = PlayerPrefs.GetString(playerNamePrefKey));
		}
		PhotonNetwork.NickName = nickName;
	}

	public void SetPlayerName(string value)
	{
		PhotonNetwork.NickName = value + " ";
		PlayerPrefs.SetString(playerNamePrefKey, value);
	}
}
