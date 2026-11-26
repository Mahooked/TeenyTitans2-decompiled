using Photon.Realtime;

namespace Photon.Pun
{
	public interface IPunOwnershipCallbacks
	{
		void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer);

		void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner);
	}
}
