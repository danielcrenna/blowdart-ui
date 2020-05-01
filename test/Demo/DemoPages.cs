// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo
{
	public static class DemoPages
	{
		public static void Index(Ui ui)
		{
			ui.BeginElement("h1");
			ui.Text("Hello, world!");
			ui.EndElement("h1");

			ui.Text("Welcome to your new app.");

			ui.SurveyPrompt("How is Blazor working for you?");
		}
	}
}