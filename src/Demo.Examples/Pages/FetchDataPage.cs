// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Blowdart.UI;
using Demo.Examples.Models;

namespace Demo.Examples.Pages
{
	public class FetchDataPage
	{
		private static IEnumerable<WeatherForecast> _forecasts;

		public static void Index(Ui ui)
		{
			ui.DataLoader<WeatherForecastService, WeatherForecast[]>(
				getData: service => service.GetWeatherForecasts(),
				setData: d => { _forecasts = d; });

			ui.Header(1, "Weather forecast");

			ui.TextBlock("This component demonstrates fetching data from the server.");

			if (_forecasts == null)
			{
				ui.TextBlock("Loading...");
			}
			else
			{
				ui.ObjectTable(_forecasts);
			}

			#region Code

			ui.SampleCode(@"
ui.DataLoader<WeatherForecastService, WeatherForecast[]>(
    getData: service => service.GetWeatherForecasts(),
    setData: d => { _forecasts = d; });

ui.Header(1, ""Weather forecast"");

ui.TextBlock(""This component demonstrates fetching data from the server."");

if (_forecasts == null)
{
	ui.Text(""Loading..."");
}
else
{
	ui.ObjectTable(_forecasts);
}");

			#endregion
		}
	}
}