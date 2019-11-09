// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class SplitTestingPage
	{
		private static int _buttonClicks;

		public static void SplitTesting(Ui ui)
		{
			ui.Header(1, "Counter");
			ui.TextBlock($"Current count: {_buttonClicks}");

			//ui.SplitTest("test1", "split test demo", () =>
			//{
			//	if (ui.Button("Click me"))
			//	{
			//		_buttonClicks++;
			//	}
			//},
			//() =>
			//{
			//	if (ui.Button("Click me now!"))
			//	{
			//		_buttonClicks++;
			//	}
			//});

			#region Code

			ui.SampleCode(@"
");

			#endregion
		}
	}
}