// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using TypeKitchen;

namespace Blowdart.UI.Internal
{
	internal static class StringExtensions
	{
		// Source: https://stackoverflow.com/a/41963144/18440
		public static string TabsToSpaces(this string value, int length)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			return Pooling.StringBuilderPool.Scoped(sb =>
			{
				int outputPosition = 1;
				foreach (var c in value)
				{
					switch (c)
					{
						case '\t':
							var spacesToAdd = NextTabStop(outputPosition, length) - outputPosition;
							sb.Append(new string(' ', spacesToAdd));
							outputPosition += spacesToAdd;
							break;

						case '\n':
							sb.Append(c);
							outputPosition = 1;
							break;

						default:
							sb.Append(c);
							outputPosition++;
							break;
					}
				}
			});
		}

		private static int NextTabStop(int position, int length)
		{
			if (position % length == 1)
				position += length;
			else
			{
				for (var i = 0; i < length; i++, position++)
					if ((position % length) == 1)
						break;
			}

			return position;
		}
	}
}
