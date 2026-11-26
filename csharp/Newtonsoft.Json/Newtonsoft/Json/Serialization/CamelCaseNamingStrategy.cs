using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization;

public class CamelCaseNamingStrategy : NamingStrategy
{
	public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
	{
		ProcessDictionaryKeys = processDictionaryKeys;
		OverrideSpecifiedNames = overrideSpecifiedNames;
	}

	public CamelCaseNamingStrategy()
	{
	}

	protected override string ResolvePropertyName(string name)
	{
		return StringUtils.ToCamelCase(name);
	}
}
