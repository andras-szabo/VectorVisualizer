using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class VectorParserTest
{
	[Test]
	public void Tests()
	{
		var shouldSucceed = new Dictionary<string, Vector3>
		{
			{ "1, 2, 3", new Vector3(1f, 2f, 3f) },
			{ "0.1, 0.23; -12", new Vector3(0.1f, 0.23f, -12f) },
			{ "1.2; 99 98.34", new Vector3(1.2f, 99.0f, 98.34f) },
			{ "-0.1, 0, 0", new Vector3(-0.1f, 0f, 0f) },
			{ "1.1 2.2 3.3", new Vector3(1.1f, 2.2f, 3.3f) },
			{ "1.2, 3, 4.5  ", new Vector3(1.2f, 3.0f, 4.5f) }
		};

		var shouldFail = new List<string>
		{
			"junk",
			"123451",
			"12.232",
			"",
			"1",
			"1.0, 2.24",
			".12 .123 .123"
		};

		Vector3 vec;

		foreach (var kvPair in shouldSucceed)
		{
			var success = VectorParser.TryParse(kvPair.Key, out vec);
			Assert.IsTrue(success && vec == kvPair.Value, kvPair.Key);
		}

		foreach (var str in shouldFail)
		{
			var success = VectorParser.TryParse(str, out vec);

			if (success)
			{
				Debug.LogWarningFormat("{0}", vec);
			}

			Assert.IsFalse(success, str);
		}
	}


}
