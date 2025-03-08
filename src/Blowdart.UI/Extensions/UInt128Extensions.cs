using System;

namespace Blowdart.UI.Extensions;

internal static class UInt128Extensions
{
	public static string ToShortId(this UInt128 value)
	{
		var bytes = new byte[16];
		for (var i = 0; i < 16; i++)
			bytes[i] = (byte)(value >> ((15 - i) * 8));
		var base64 = Convert.ToBase64String(bytes);
		return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
	}
}