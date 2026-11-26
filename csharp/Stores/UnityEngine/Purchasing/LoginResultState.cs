namespace UnityEngine.Purchasing;

public enum LoginResultState
{
	LoginSucceed,
	UserNotExists,
	PasswordError,
	UserOrPasswordEmpty,
	LoginCallBackIsNull,
	NetworkError,
	NotKnown
}
