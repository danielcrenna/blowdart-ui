// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;
using Blowdart.UI.Blazor;
using static Blowdart.UI.Blazor.WebElements;

namespace Demo
{
	public static class DemoPages
	{
		public static void Index(Ui ui)
		{
			ui.h1("Hello, world!");
			ui.Text("Welcome to your new app.");
			ui.SurveyPrompt("How is Blazor working for you?");
		}

		private static int _currentCount;

		public static void Counter(Ui ui)
		{
			ui.h1("Counter");
			ui.p($"Current count: {_currentCount}");
			if (ui.Button("Click me"))
				_currentCount++;
		}

		public static void FetchData(Ui ui)
		{
			ui.h1("Weather forecast");
			ui.p("This component demonstrates fetching data from the server.");
		}
	}
}