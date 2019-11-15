// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class IndexPage
    {
		public static void Index(Ui ui)
		{
			ui.Header(1, "Hello, world!");
			ui.Text("Welcome to your new app.");

			#region Code

			ui.SampleCode(@"
ui.Header(1, ""Hello, world!"");
ui.Text(""Welcome to your new app."");");
			#endregion
		}
	}
}
