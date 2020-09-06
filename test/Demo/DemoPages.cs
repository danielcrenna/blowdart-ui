// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Blowdart.UI;

namespace Demo
{
	public class DemoPages
	{
		private int _currentCount;

		private IEnumerable<WeatherForecast> _forecasts;

		public void Index(Ui ui)
		{
			ui.h1("Hello, world!");
			ui._("Welcome to your new app.");
			ui.SurveyPrompt("How is Blazor working for you?");
		}

		public void Counter(Ui ui)
		{
			ui.h1("Counter");
			ui.p($"Current count: {_currentCount}");

			var id = ui.NextId();
			ui.PushStyle(x => { x.Named("btn btn-primary"); });
			if (ui.button("Click me", id).Click())
				_currentCount++;
		}

		public void FetchData(Ui ui)
		{
			ui.DataLoader<WeatherForecast[]>("sample-data/weather.json", d => { _forecasts = d; });

			ui.h1("Weather forecast");
			ui.p("This component demonstrates fetching data from the server.");

			if (_forecasts == null)
			{
				ui._("Loading...");
			}
			else
			{
				ui.table(t =>
				{
					t.thead(h =>
					{
						h.tr(r =>
						{
							r.th("Date");
							r.th("Temp. (C)");
							r.th("Temp. (F)");
							r.th("Summary");
						});
					});

					t.tbody(b =>
					{
						b.Repeat(_forecasts, (x, row) =>
						{
							x.tr(r =>
							{
								r.td(row.Date);
								r.td(row.TemperatureC);
								r.td(row.TemperatureF);
								r.td(row.Summary);
							});
						});
					});
				});
			}
		}

		public void NotFound(Ui ui)
		{
			ui.p("Sorry, there's nothing at this address.");
		}
	}
}