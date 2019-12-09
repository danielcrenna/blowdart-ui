// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;

namespace Blowdart.UI
{
	public static class IconExtensionMethods
	{
		public static string ToIconCase(this OpenIconicIcons icon)
		{
			return ToDashCase(icon.ToString());
		}

		public static string ToIconCase(this MaterialIcons icon)
		{
			return icon == MaterialIcons.ThreeDRotation ? "3d_rotation" : ToSnakeCase(icon.ToString());
		}

		internal static string ToDashCase(this string value)
		{
			return string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? $"-{x}" : $"{x}"))
				.ToLowerInvariant();
		}

		internal static string ToSnakeCase(this string value)
		{
			return string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? $"_{x}" : $"{x}")).ToLowerInvariant();
		}
	}
}