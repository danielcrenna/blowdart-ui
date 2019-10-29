// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Demo.Models;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.Demo
{
    public class HelloWorld
    {
        private static int _currentCount;
        private static IEnumerable<WeatherForecast> _forecasts;

        public static void Index(Ui ui)
        {
            MainLayout(ui, () =>
            {
                ui.Header(1, "Hello, world!");
                ui.Literal("Welcome to your new app.");

                #region Code

                SampleCode(ui, @"
ui.Header(1, ""Hello, world!"");
ui.Literal(""Welcome to your new app."");");

                #endregion
            });
        }

        public static void Counter(Ui ui)
        {
	        MainLayout(ui, () =>
	        {
		        ui.Header(1, "Counter");
		        ui.Text($"Current count: {_currentCount}");
		        if (ui.Button("Click me"))
		        {
			        _currentCount++;
		        }

				#region Code

				SampleCode(ui, @"
ui.Header(1, ""Counter"");
ui.Text($""Current count: {_currentCount}"");
if (ui.Button(""Click me""))
	_currentCount++;");

				#endregion
			});
		}
        
        public static void FetchData(Ui ui)
        {
            MainLayout(ui, () =>
            {
	            ui.DataLoader<WeatherForecastService, WeatherForecast[]>(
		            getData: service => service.GetWeatherForecasts(),
		            setData: d => { _forecasts = d; });

				ui.Header(1, "Weather forecast");

                ui.Text("This component demonstrates fetching data from the server.");

                if (_forecasts == null)
                {
                    ui.Text("Loading...");
                }
                else
                {
                    ui.ObjectTable(_forecasts);
                }

                #region Code

                SampleCode(ui, @"
ui.DataLoader<WeatherForecastService, WeatherForecast[]>(
    getData: service => service.GetWeatherForecasts(),
    setData: d => { _forecasts = d; });

ui.Header(1, ""Weather forecast"");

ui.Text(""This component demonstrates fetching data from the server."");

if (_forecasts == null)
{
	ui.Text(""Loading..."");
}
else
{
	ui.ObjectTable(_forecasts);
}");

                #endregion
            });
        }

        private static readonly WeatherForecast EditObject = new WeatherForecast();

        public static void Editor(Ui ui)
        {
            MainLayout(ui, () =>
            {
                ui.Editor(EditObject); /* WeatherForecast */

				#region Code

				SampleCode(ui, "ui.Editor(new WeatherForecast()); /* WeatherForecast */");

				#endregion
			});
        }

		private static readonly OpenIconicIcons[] Icons = (OpenIconicIcons[]) Enum.GetValues(typeof(OpenIconicIcons));

		public static void Styles(Ui ui)
        {
	        MainLayout(ui, () =>
	        {
		        ui.Header(1, "Icons");

				ui.ListTable(Icons.InGroupsOf(5), (i, icons) =>
				{
					foreach (var icon in icons)
					{
						ui.InlineIcon(icon);
						ui.NextColumn(ref i);
						ui.Literal(icon.ToCssCase());
						ui.NextColumn(ref i);
					}
				});

				#region Code

				SampleCode(ui, @"
ui.Header(1, ""Icons"");

ui.ListTable(Icons.InGroupsOf(5), icons =>
{
	foreach (var icon in icons)
	{
		ui.InlineIcon(icon);
		ui.NextColumn();
		ui.Literal(icon.ToCssCase());
		ui.NextColumn();
	}
});");
				#endregion
			});
        }
		private static void SampleCode(Ui ui, string code)
		{
			ui.Separator();
			ui.Header(3, "Code");
			ui.CodeBlock(code);
		}

		private static void MainLayout(Ui ui, Action handler)
        {
            ui.Sidebar("Blowdart.UI Demo",
                new SidebarPage(OpenIconicIcons.Home, "/", "Home"),
                new SidebarPage(OpenIconicIcons.Plus, "/counter", "Counter"),
                new SidebarPage(OpenIconicIcons.ListRich, "/fetchdata", "Fetch Data"),
                new SidebarPage(OpenIconicIcons.Folder, "/editor", "Editor"),
                new SidebarPage(OpenIconicIcons.Aperture, "/styles", "Styles")
			);

            ui.Main(() =>
            {
                ui.TopRow(() => { ui.Link("https://docs.microsoft.com/en-us/aspnet/", "About"); });

                ui.MainContent(handler);
            });
        }
    }
}