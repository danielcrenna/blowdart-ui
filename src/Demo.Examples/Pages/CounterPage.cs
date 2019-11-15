// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class CounterPage
	{
		private static int _currentCount;

		public static void Index(Ui ui)
		{
			ui.Header(1, "Counter");
			ui.TextBlock($"Current count: {_currentCount}");
            
			if (ui.Button("Click me"))
			{
				_currentCount++;
			}

			#region Code

			ui.SampleCode(@"
ui.Header(1, ""Counter"");
ui.TextBlock($""Current count: {_currentCount}"");

if (ui.Button(""Click me""))
	_currentCount++;");
			#endregion
		}
	}
}