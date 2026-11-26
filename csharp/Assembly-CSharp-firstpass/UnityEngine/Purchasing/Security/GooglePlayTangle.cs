using System;

namespace UnityEngine.Purchasing.Security;

public class GooglePlayTangle
{
	private static byte[] data = Convert.FromBase64String("TeZdHSXT/tEmpz4Ff91ZVZdks4/vbGJtXe9sZ2/vbGxtoysXFsWS5Q6UUNNbl8QUK06uCYdy2fhYKV84Xe9sT11ga2RH6yXrmmBsbGxobW4Ce0R36zbfsb4yj6OUv2Jew6W0UUIaTLVwsdadu4Y5uvHH+cWuNcWRdIUILIRLCP0JlFBQFQgKzwo/Y7KX/QZGSHiZrSokw1Rn38OYV/qR8ykyAP3gi6XK+Bxp+S6MskDKu6DCFMrjPoiZx3t8Iro8+H2dpsqpaeL9sJ0KAk5AmzZYcp6MDSCKyHwuO/0yByxhbTIFPJi5fZPDxFo+GqUELFLs8CNfjL2R+lQSzHj8qOIz4iqVC/x1pJsVJJrFsswivU714an5wQPrkGEzclgZ9G9ubG1s");

	private static int[] order = new int[15]
	{
		9, 1, 7, 9, 10, 13, 6, 8, 10, 10,
		11, 13, 13, 13, 14
	};

	private static int key = 109;

	public static readonly bool IsPopulated = true;

	public static byte[] Data()
	{
		if (!IsPopulated)
		{
			return null;
		}
		return Obfuscator.DeObfuscate(data, order, key);
	}
}
