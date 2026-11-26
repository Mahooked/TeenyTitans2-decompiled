using Photon.Realtime;

namespace Photon.Pun
{
	public struct PhotonMessageInfo
	{
		private readonly int timeInt;

		public readonly Photon.Realtime.Player Sender;

		public readonly PhotonView photonView;

		public double timestamp
		{
			get
			{
				uint num = (uint)timeInt;
				double num2 = num;
				return num2 / 1000.0;
			}
		}

		public PhotonMessageInfo(Photon.Realtime.Player player, int timestamp, PhotonView view)
		{
			Sender = player;
			timeInt = timestamp;
			photonView = view;
		}

		public override string ToString()
		{
			return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", timestamp, Sender);
		}
	}
}
