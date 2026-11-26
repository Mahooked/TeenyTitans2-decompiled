namespace Newtonsoft.Json.Serialization;

public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
{
	public CamelCasePropertyNamesContractResolver()
		: base(shareCache: true)
	{
		NamingStrategy = new CamelCaseNamingStrategy
		{
			ProcessDictionaryKeys = true,
			OverrideSpecifiedNames = true
		};
	}
}
