// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo
{
	public class DemoPages
	{
		public void Index(Ui ui)
		{
			ui.h1("Hello, world!");
			ui._("Welcome to your new app.");
			ui.SurveyPrompt("How is Blazor working for you?");
		}

		private int _currentCount;

		public void Counter(Ui ui)
		{
			ui.h1("Counter");
			ui.p($"Current count: {_currentCount}");

			ui.PushStyle(x => { x.Named("btn btn-primary"); });
			if (ui.Button("Click me"))
				_currentCount++;
		}

		public void FetchData(Ui ui)
		{
			ui.h1("Weather forecast");
			ui.p("This component demonstrates fetching data from the server.");
		}
	}
}