// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Blazor
{
	public static class Sequence
	{
		private static object? _lastHost;

		internal static int Current
		{
			get;
			private set;
		}

		public static void Begin(int startingAt, object host)
		{
			Current = startingAt;
			_lastHost = host;
		}

		public static int NextSequence(this object host, int? callerLineNumber = null)
		{
			if (_lastHost != host)
				Current += 10000;
			_lastHost = host;
			Current += callerLineNumber.GetValueOrDefault();
			return Current;
		}
	}
}