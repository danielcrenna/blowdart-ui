// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.WinForms
{
	public static class UiForm
	{
		public static void Start(string[] args, string title, Action<BlowdartBuilder> configureAction)
		{
			var pages = new PageMap();
			var services = new ServiceCollection();
			var builder = new BlowdartBuilder(pages, services);
			configureAction(builder);
			services.AddSingleton(pages);

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ImGui(title, services.BuildServiceProvider()));
		}
	}
}
