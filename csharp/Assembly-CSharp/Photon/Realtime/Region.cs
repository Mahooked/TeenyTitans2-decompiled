namespace Photon.Realtime;

public class Region
{
	public string Code { get; private set; }

	public string Cluster { get; private set; }

	public string HostAndPort { get; protected internal set; }

	public int Ping { get; protected internal set; }

	public bool WasPinged => Ping != int.MaxValue;

	public Region(string code, string address)
	{
		SetCodeAndCluster(code);
		HostAndPort = address;
		Ping = int.MaxValue;
	}

	private void SetCodeAndCluster(string codeAsString)
	{
		if (codeAsString == null)
		{
			Code = string.Empty;
			Cluster = string.Empty;
			return;
		}
		codeAsString = codeAsString.ToLower();
		int num = codeAsString.IndexOf('/');
		Code = ((num > 0) ? codeAsString.Substring(0, num) : codeAsString);
		Cluster = ((num > 0) ? codeAsString.Substring(1, num) : string.Empty);
	}

	public override string ToString()
	{
		string text = Code;
		if (!string.IsNullOrEmpty(Cluster))
		{
			text = text + "/" + Cluster;
		}
		if (!WasPinged)
		{
			return $"'{text}' \tavailable but was not pinged.";
		}
		return $"'{text}' \t{Ping}ms \t{HostAndPort}";
	}
}
