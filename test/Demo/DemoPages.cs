// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Net.Http;
using Blowdart.UI;
using Microsoft.AspNetCore.Components;

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

			ui.PushStyle(x => x.Named("btn btn-primary"));
			if (ui.Button("Click me"))
				_currentCount++;
		}

		private IEnumerable<WeatherForecast> _forecasts;

		public void FetchData(Ui ui)
		{
			ui.DataLoader<HttpClient, WeatherForecast[]>(
				http => http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json"),
				d => { _forecasts = d; });

			ui.h1("Weather forecast");
			ui.p("This component demonstrates fetching data from the server.");

			if (_forecasts == null)
			{
				ui._("Loading...");
			}
			else
			{
				ui.BeginElement("table");
				{
					ui.BeginElement("thead");
					{
						ui.BeginElement("tr");
						{
							ui.BeginElement("th");
							ui._("Date");
							ui.EndElement("th");

							ui.BeginElement("th");
							ui._("Temp. (C)");
							ui.EndElement("th");

							ui.BeginElement("th");
							ui._("Temp. (F)");
							ui.EndElement("th");

							ui.BeginElement("th");
							ui._("Summary");
							ui.EndElement("th");

							ui.EndElement("tr");
						}

						ui.EndElement("thead");
					}

					ui.BeginElement("tbody");
					{
						foreach (var forecast in _forecasts)
						{
							ui.BeginElement("tr");
							{
								ui.BeginElement("td");
								ui._(forecast.Date);
								ui.EndElement("td");

								ui.BeginElement("td");
								ui._(forecast.TemperatureC);
								ui.EndElement("td");

								ui.BeginElement("td");
								ui._(forecast.TemperatureF);
								ui.EndElement("td");

								ui.BeginElement("td");
								ui._(forecast.Summary);
								ui.EndElement("td");

								ui.EndElement("tr");
							}
						}

						ui.EndElement("tbody");
					}

					ui.EndElement("table");
				}
			}
		}

		public void NotFound(Ui ui)
		{
			ui.p("Sorry, there's nothing at this address.");
		}
	}
}