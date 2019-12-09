// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class ElementsPage
	{
		#region Elements Page

		private static bool _alertsTabOpen = true;
		private static bool _inputControlsTabOpen;
		private static bool _toastsTabOpen;

		public static void Index(Ui ui)
		{
			ui.BeginTabList();
			{
				if (ui.Tab("Alerts", ref _alertsTabOpen))
				{
					_inputControlsTabOpen = false;
					_toastsTabOpen = false;
				}

				if (ui.Tab("Input Controls", ref _inputControlsTabOpen))
				{
					_alertsTabOpen = false;
					_toastsTabOpen = false;
				}

				if (ui.Tab("Toasts", ref _toastsTabOpen))
				{
					_inputControlsTabOpen = false;
					_alertsTabOpen = false;
				}

				ui.EndTabList();
			}
			
			ui.BeginTabContent();
			{
				ui.TabContent("Alerts", _alertsTabOpen, AlertsTab);
				ui.TabContent("Input Controls", _inputControlsTabOpen, InputControlsTab);
				ui.TabContent("Toasts", _toastsTabOpen, ToastsTab);

				ui.EndTabContent();
			}
		}

		public static void ToastsTab(Ui ui)
		{
			ui.NextLine();
			ui.Header(3, "Toasts");
			ui.Separator();

			if (ui.Button("Add toast"))
				ui.Toast("That happened.", "Blowdart says...", DateTimeOffset.UtcNow);
		}

		private static string _text;
		private static bool _checked;
		private static int _slider;
		private static bool _radioButton;
		private static readonly List<string> _list = new List<string>
		{
			"apple", "pear", "plum", 
			"banana", "peach"
		};
		
		public static void InputControlsTab(Ui ui)
		{
			ui.NextLine();
			ui.Header(3, "Input Controls");
			ui.Separator();
			
			ui.Push(ElementStyle.Rounded);
			ui.Push(MaterialIcons.FilterList);
			ui.Push(FieldType.Search);
			ui.Push(InputActivation.OnInput);
			if(ui.TextBox(ref _text, placeholder: "Filter fruit"))
				ui.Log("Filter changed to " + _text);
			ui.List(ListDirection.LeftToRight, _list.Where(x => string.IsNullOrWhiteSpace(_text) || x.Contains(_text)), ui.Text);

			ui.NextLine();

			ui.Push(ElementAlignment.Right);
			if (ui.CheckBox(ref _checked, "Check me"))
				ui.Log($"checked the box: ({_checked})");

			ui.NextLine();

			ui.Push(InputActivation.OnInput);
			if (ui.Slider(ref _slider, "Slide me"))
			{
				ui.Log($"changed slider: ({_slider})");
			}

			ui.NextLine();

			if (ui.RadioButton(ref _radioButton, "Press me"))
			{
				ui.Log($"changed radio button: ({_radioButton})");
			}

			ui.NextLine();

			ui.Push(ElementContext.Dark);
			ui.Push(ElementDecorator.SpinnerBorder);
			if (ui.Button())
			{
				ui.ShowModal("Example");
				ui.Log($"clicked the first button");
			}

			ui.Push(ElementContext.Dark);
			ui.Push(ElementDecorator.SpinnerBorder);
			ui.Push(ElementAlignment.Right);
			if (ui.Button("Click me "))
			{
				ui.ShowModal("Example");
				ui.Log($"clicked the second button");
			}

			ui.BeginModal("Example");
			ui.Text("Hello from a modal!");
			ui.EndModal();

			#region Code

			ui.SampleCode(@"
ui.NextLine();
ui.Header(3, ""Input Controls"");
ui.Separator();

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
}

ui.NextLine();

ui.Push(ElementContext.Dark);
ui.Push(ElementDecorator.SpinnerBorder);
if (ui.Button())
{
	ui.ShowModal(""Example"");
	ui.Log($""clicked the first button"");
}

ui.Push(ElementContext.Dark);
ui.Push(ElementDecorator.SpinnerBorder);
ui.Push(ElementAlignment.Right);
if (ui.Button(""Click me ""))
{
	ui.ShowModal(""Example"");
	ui.Log($""clicked the second button"");
}

ui.BeginModal(""Example"");
ui.Text(""Hello from a modal!"");
ui.EndModal();
");
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

			ui.SampleCode(@"
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
	}
}
 