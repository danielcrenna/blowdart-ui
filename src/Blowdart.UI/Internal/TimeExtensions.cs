// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI.Internal
{
	internal static class TimeExtensions
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

		public static DateTimeOffset FromUnixTime(this long seconds)
		{
			return Epoch.AddSeconds(seconds).ToLocalTime();
		}

		public static long ToUnixTime(this DateTimeOffset dateTime)
		{
			var timeSpan = dateTime - Epoch;
			var timestamp = (long) timeSpan.TotalSeconds;

			return timestamp;
		}
	}
}