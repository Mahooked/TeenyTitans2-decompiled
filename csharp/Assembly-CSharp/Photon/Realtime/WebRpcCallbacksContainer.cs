using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime;

internal class WebRpcCallbacksContainer : IWebRpcCallback
{
	internal event Action<OperationResponse> OnWebRpcResponseActions;

	public void AddCallbackTarget(IWebRpcCallback target)
	{
		OnWebRpcResponseActions += target.OnWebRpcResponse;
	}

	public void RemoveCallbackTarget(IWebRpcCallback target)
	{
		OnWebRpcResponseActions -= target.OnWebRpcResponse;
	}

	public void OnWebRpcResponse(OperationResponse response)
	{
		if (OnWebRpcResponseActions != null)
		{
			OnWebRpcResponseActions(response);
		}
	}
}
