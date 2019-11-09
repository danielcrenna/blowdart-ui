// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI;
using Demo.Examples.Models;

namespace Demo.Examples
{
    public class HelloWorld
    {
	    #region Index Page

	    public static void Index(Ui ui)
	    {
			ui.Header(1, "Hello, world!");
			ui.Text("Welcome to your new app.");

			#region Code

			SampleCode(ui, @"
ui.Header(1, ""Hello, world!"");
ui.Literal(""Welcome to your new app."");");
			#endregion
		}

		#endregion

		#region Counter Page

		private static int _currentCount;

        public static void Counter(Ui ui)
        {
			ui.Header(1, "Counter");
			ui.TextBlock($"Current count: {_currentCount}");

			ui.Push(ElementContext.Light);
			if (ui.Button("Click me"))
			{
				_currentCount++;
			}

			#region Code

			SampleCode(ui, @"
ui.Header(1, ""Counter"");
ui.Text($""Current count: {_currentCount}"");

ui.Push(ElementContext.Light);
if (ui.Button(""Click me""))
	_currentCount++;");

			#endregion
		}

		#endregion

		#region Fetch Data Page

		private static IEnumerable<WeatherForecast> _forecasts;

		public static void FetchData(Ui ui)
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
		}

		#endregion

		#region Elements Page

		private static bool _alertsTabOpen = true;
		private static bool _inputControlsTabOpen;

		public static void Elements(Ui ui)
		{
			ui.BeginTabList();

			if (ui.Tab("Alerts", ref _alertsTabOpen))
			{
				_inputControlsTabOpen = false;
			}

			if (ui.Tab("Input Controls", ref _inputControlsTabOpen))
			{
				_alertsTabOpen = false;
			}

			ui.EndTabList();

			ui.BeginTabContent();
			ui.TabContent("Alerts", _alertsTabOpen, AlertsTab);
			ui.TabContent("Input Controls", _inputControlsTabOpen, InputControlsTab);
			ui.EndTabContent();
		}

		private static bool _checked;
		private static int _slider;
		private static bool _radioButton;

		public static void InputControlsTab(Ui ui)
		{
			ui.NextLine();
			ui.Header(3, "Input Controls");
			ui.Separator();

			ui.Push(ElementAlignment.Right);
			if (ui.CheckBox(ref _checked, "Check me"))
			{
				ui.Log($"checked the box: ({_checked})");
			}
			
			ui.NextLine();
			
			ui.Push(InputActivation.Continuous);
			if (ui.Slider(ref _slider, "Slide me"))
			{
				ui.Log($"changed slider: ({_slider})");
			}

			ui.NextLine();

			if (ui.RadioButton(ref _radioButton, "Press me"))
			{
				ui.Log($"changed radio button: ({_radioButton})");
			}

			#region Code

			SampleCode(ui, @"
ui.NextLine();

ui.Push(ElementAlignment.Right);
if (ui.CheckBox(ref _checked, ""Check me""))
{
	ui.Log($""checked the box: ({_checked})"");
}

ui.NextLine();

ui.Push(InputActivation.Continuous);
if (ui.Slider(ref _slider, ""Slide me""))
{
	ui.Log($""changed slider: ({_slider})"");
}

ui.NextLine();

if (ui.RadioButton(ref _radioButton, ""Press me""))
{
	ui.Log($""changed radio button: ({_radioButton})"");
}");
			#endregion
		}

		public static void AlertsTab(Ui ui)
		{
			ui.NextLine();
			ui.Header(3, "Alerts");
			ui.Separator();

			ui.Push(ElementContext.Primary);
			ui.Alert("This is a primary alert—check it out!");

			ui.Push(ElementContext.Secondary);
			ui.Alert("This is a secondary alert—check it out!");

			ui.Push(ElementContext.Success);
			ui.Alert("This is a success alert—check it out!");

			ui.Push(ElementContext.Danger);
			ui.Alert("This is a danger alert—check it out!");

			ui.Push(ElementContext.Warning);
			ui.Alert("This is a warning alert—check it out!");

			ui.Push(ElementContext.Info);
			ui.Alert("This is an info alert—check it out!");

			ui.Push(ElementContext.Dark);
			ui.Alert("This is a dark alert—check it out!");

			ui.Push(ElementContext.Light);
			ui.Alert("This is a light alert—check it out!");

			#region Code

			SampleCode(ui, @"
ui.Header(3, ""Alerts"");
ui.Separator();

ui.Push(ElementContext.Primary);
ui.Alert(""This is a primary alert—check it out!"");

ui.Push(ElementContext.Secondary);
ui.Alert(""This is a secondary alert—check it out!"");

ui.Push(ElementContext.Success);
ui.Alert(""This is a success alert—check it out!"");

ui.Push(ElementContext.Danger);
ui.Alert(""This is a danger alert—check it out!"");

ui.Push(ElementContext.Warning);
ui.Alert(""This is a warning alert—check it out!"");

ui.Push(ElementContext.Info);
ui.Alert(""This is an info alert—check it out!"");

ui.Push(ElementContext.Dark);
ui.Alert(""This is a dark alert—check it out!"");

ui.Push(ElementContext.Light);
ui.Alert(""This is a light alert—check it out!"");");
			#endregion
		}
		
		#endregion

		#region Editor Page

		private static readonly WeatherForecast EditObject = new WeatherForecast();

		public static void Editor(Ui ui)
		{
			ui.Editor(EditObject);

			#region Code

			SampleCode(ui, "ui.Editor(new WeatherForecast());");

			#endregion
		}

		#endregion

		#region Styles Page

		private static readonly OpenIconicIcons[] Icons = (OpenIconicIcons[]) Enum.GetValues(typeof(OpenIconicIcons));

		public static void Styles(Ui ui)
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
		}

		#endregion

		#region Split Testing Page

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

			SampleCode(ui, @"
");

			#endregion
		}

		#endregion

		private static void SampleCode(Ui ui, string code)
		{
			ui.Separator();
			ui.Header(3, "Code");
			ui.CodeBlock(code);
		}
    }
}
