using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime;

[Serializable]
public class AppSettings
{
	public string AppIdRealtime;

	public string AppIdChat;

	public string AppIdVoice;

	public string AppVersion;

	public bool UseNameServer = true;

	public string FixedRegion;

	public string Server;

	public int Port;

	public ConnectionProtocol Protocol;

	public bool EnableLobbyStatistics;

	public DebugLevel NetworkLogging = DebugLevel.ERROR;

	public bool IsMasterServerAddress => !UseNameServer;

	public bool IsBestRegion => UseNameServer && string.IsNullOrEmpty(FixedRegion);

	public bool IsDefaultNameServer => UseNameServer && string.IsNullOrEmpty(Server);

	public bool IsDefaultPort => Port <= 0;

	public string ToStringFull()
	{
		return $"IsBestRegion: {IsBestRegion} IsDefaultNameServer: {IsDefaultNameServer} IsDefaultPort: {IsDefaultPort}";
	}
}
