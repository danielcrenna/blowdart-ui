// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI;
using Blowdart.UI.Instructions;
using Demo.Models;

namespace Demo
{
    public class HelloWorld
    {
        public static void Index(Ui ui)
        {
            MainLayout(ui, () =>
            {
                ui.Header(1, "Hello, world!");
                ui.Text("Welcome to your new app.");

                #region Code

                SampleCode(ui, @"
ui.Header(1, ""Hello, world!"");
ui.Literal(""Welcome to your new app."");");

                #endregion
            });
        }

		private static int _currentCount;

		public static void Counter(Ui ui)
        {
	        MainLayout(ui, () =>
	        {
		        ui.Header(1, "Counter");
		        ui.TextBlock($"Current count: {_currentCount}");
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

		private static IEnumerable<WeatherForecast> _forecasts;

		public static void FetchData(Ui ui)
        {
            MainLayout(ui, () =>
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

        private static bool _checked;
        private static int _slider;

		public static void Elements(Ui ui)
        {
	        MainLayout(ui, () =>
	        { 
		        if (ui.CheckBox(ref _checked, "Check me", ElementAlignment.Right))
		        {
			        ui.Log($"checked the box: ({_checked})");
		        }

		        if (ui.Slider(ref _slider, "Slide me"))
		        {
			        ui.Log($"changed slider: ({_slider})");
		        }

				#region Code

				SampleCode(ui, @"
if (ui.CheckBox(ref _checked, ""Remember me"", ElementAlignment.Right))
{
    ui.Log($""checked the box: ({_checked})""); // logs to configured log target
}

if (ui.Slider(ref _slider, ""Slide me""))
{
    ui.Log($""changed slider: ({_slider})"");
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
						ui.Text(icon.ToCssCase());
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
			ui.BeginMenu("Blowdart.UI Demo");
			{
				ui.MenuItem(OpenIconicIcons.Home, "Home", "/");
				ui.MenuItem(OpenIconicIcons.Plus, "Counter", "/counter");
				ui.MenuItem(OpenIconicIcons.ListRich, "Fetch Data", "/fetchdata");
				ui.MenuItem(OpenIconicIcons.Code, "Elements", "/elements");
				ui.MenuItem(OpenIconicIcons.Folder, "Editor", "/editor");
				ui.MenuItem(OpenIconicIcons.Aperture, "Styles", "/styles");

				ui.EndMenu();
			}

            ui.Main(() =>
            {
                ui.TopRow(() => { ui.Link("https://docs.microsoft.com/en-us/aspnet/", "About"); });

                ui.MainContent(handler);
            });
        }
    }
}
