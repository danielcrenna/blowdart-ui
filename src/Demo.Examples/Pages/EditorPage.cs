// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;
using Demo.Examples.Models;

namespace Demo.Examples.Pages
{
	public class EditorPage
	{
		private static readonly WeatherForecast EditObject = new WeatherForecast();

		public static void Index(Ui ui)
		{
			ui.Editor(EditObject);

			#region Code

			ui.SampleCode("ui.Editor(new WeatherForecast());");

			#endregion
		}
	}
}