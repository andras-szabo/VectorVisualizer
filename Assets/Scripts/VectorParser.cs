using UnityEngine;

public static class VectorParser 
{
	// vector components need to have at least 1 whitespace between them
	public static string VEC3_PATTERN = @"^([^0-9\-]*)(?<xval>[\-]?[0-9]+(\.[0-9]+)*)([^0-9\-]*)\s+(?<yval>[\-]?[0-9]+(\.[0-9]+)*)([^0-9\-]*)\s+(?<zval>[\-]?[0-9]+(\.[0-9]+)*)";

	public static bool TryParse(string input, out Vector3 vector)
	{
		vector = Vector3.zero;

		var regex = new System.Text.RegularExpressions.Regex(VEC3_PATTERN);
		var match = regex.Match(input);

		if (match.Success)
		{
			var xAsString = match.Groups["xval"].Value;
			var yAsString = match.Groups["yval"].Value;
			var zAsString = match.Groups["zval"].Value;

			var x = (float) System.Convert.ToDouble(xAsString);
			var y = (float) System.Convert.ToDouble(yAsString);
			var z = (float) System.Convert.ToDouble(zAsString);

			vector = new Vector3(x, y, z);

			return true;
		}

		return false;
	}
}
