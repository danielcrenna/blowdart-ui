// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo
{
	public class DemoLayouts
	{
		public bool collapseNavMenu = true;

		public void MainLayout(Ui ui)
		{
			ui.BeginElement("div");
			ui.Attribute("class", "sidebar");
			{	
				ui.NavBar(collapseNavMenu);
				ui.EndElement("div");
			}

			ui.BeginElement("div");
			ui.Attribute("class", "main");
			{
				ui.BeginElement("div");
				{
					ui.Attribute("class", "top-row px-4");

					ui.BeginElement("a");
					ui.Attribute("href", "http://blazor.net");
					ui.Attribute("target", "_blank");
					ui.Attribute("class", "ml-md-auto");
					{
						ui._("About");
						ui.EndElement("a");
					}

					ui.EndElement("div");
				}

				ui.BeginElement("div");
				{
					ui.Attribute("class", "content px-4");
					ui.Body();
					ui.EndElement("div");
				}
				
				ui.EndElement("div");
			}
		}
	}
}