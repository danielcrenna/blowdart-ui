// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Blowdart.UI.Internal
{
	internal static class ColorExtensions
	{
		public static Color ToColor(this string hex)
		{
			var match = Regex.Match(hex, "^#?([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", RegexOptions.Compiled);
			if (match.Success)
				return Color.FromArgb(int.Parse(match.Groups[1].Value, NumberStyles.HexNumber));
			Trace.TraceError($"Could not parse color {hex}.");
			return Color.Red;
		}

		public static string ToCss(this Color color)
		{
			if (color == Color.Transparent)
				return "transparent";
			var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
			return hex;
		}

		public static Color Lighten(this Color color, float percentage)
		{
			return color.Lerp(Color.White, percentage);
		}

		public static Color Darken(this Color color, float percentage)
		{
			return color.Lerp(Color.Black, percentage);
		}

		private static Color Lerp(this Color color, Color to, float amount)
		{
			var r = (byte) color.R.Lerp(to.R, amount);
			var g = (byte) color.G.Lerp(to.G, amount);
			var b = (byte) color.B.Lerp(to.B, amount);
			return Color.FromArgb(r, g, b);
		}

		private static float Lerp(this byte start, byte end, float value)
		{
			return start + (end - start) * value;
		}
	}
}