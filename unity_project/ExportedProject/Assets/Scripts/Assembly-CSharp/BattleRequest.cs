using System;
using System.Runtime.Serialization;

[Serializable]
public class BattleRequest
{
	public string playerId;

	public string region;

	public string roomName;

	[OptionalField(VersionAdded = 2)]
	public bool isLocal;

	public BattleRequest Copy()
	{
		BattleRequest battleRequest = new BattleRequest();
		battleRequest.playerId = playerId;
		battleRequest.region = region;
		battleRequest.roomName = roomName;
		battleRequest.isLocal = isLocal;
		return battleRequest;
	}

	[OnDeserializing]
	private void HandleSerializationVersioning(StreamingContext context)
	{
		isLocal = false;
	}
}
