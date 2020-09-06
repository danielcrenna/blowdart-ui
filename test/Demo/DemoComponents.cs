// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo
{
	public static class DemoComponents
	{
		public static void NavBar(this Ui ui, bool collapse)
		{
			ui.BeginElement("div");
			ui.Attribute("class", "top-row pl-4 navbar navbar-dark");
			{
				ui.BeginElement("a");
				ui.Attribute("class", "navbar-brand");
				ui.Attribute("href", "");
				{	
					ui._("BlazorApp1");
					ui.EndElement("a");
				}

				ui.BeginElement("button");
				ui.Attribute("class", "navbar-toggler");
				{
					ui.BeginElement("span");
					ui.Attribute("class", "navbar-toggler-icon");
					ui.EndElement("span");
					
					ui.EndElement("button");
				}

				ui.EndElement("div");
			}

			ui.BeginElement("div");
			ui.Attribute("class", collapse ? "collapse" : "");
			{
				ui.BeginElement("ul");
				ui.Attribute("class", "nav flex-column");
				{
					ui.BeginElement("li");
					ui.Attribute("class", "nav-item px-3");
					{
						ui.BeginElement("a");
						ui.Attribute("class", "nav-link");
						ui.Attribute("href", "");
						{
							ui.BeginElement("span");
							ui.Attribute("class", "oi oi-home");
							ui.Attribute("aria-hidden", true);
							ui.EndElement("span");

							ui._(" Home");
							ui.EndElement("a");
						}

						ui.BeginElement("a");
						ui.Attribute("class", "nav-link");
						ui.Attribute("href", "counter");
						{
							ui.BeginElement("span");
							ui.Attribute("class", "oi oi-plus");
							ui.Attribute("aria-hidden", true);
							ui.EndElement("span");

							ui._(" Counter");
							ui.EndElement("a");
						}
						
						ui.BeginElement("a");
						ui.Attribute("class", "nav-link");
						ui.Attribute("href", "fetchdata");
						{
							ui.BeginElement("span");
							ui.Attribute("class", "oi oi-list-rich");
							ui.Attribute("aria-hidden", true);
							ui.EndElement("span");

							ui._(" Fetch Data");
							ui.EndElement("a");
						}

						ui.EndElement("li");
					}

					ui.EndElement("ul");
				}
				
				ui.EndElement("div");
			}
		}

		public static void SurveyPrompt(this Ui ui, string title)
		{
			ui.BeginElement("div");
			ui.Attribute("class", "alert alert-secondary mt-4");
			ui.Attribute("role", "alert");
			{
				ui.BeginElement("span");
				ui.Attribute("class", "oi oi-pencil mr-2");
				ui.Attribute("aria-hidden", true);
				ui.EndElement("span");

				ui.BeginElement("strong");
				ui._(title);
				ui.EndElement("strong");

				ui.BeginElement("span");
				ui.Attribute("class", "text-nowrap");
				{
					ui._(" Please take our ");
					ui.BeginElement("a");
					{
						ui.Attribute("target", "_blank");
						ui.Attribute("class", "font-weight-bold");
						ui.Attribute("href", "https://go.microsoft.com/fwlink/?linkid=2121313");
						ui._(" brief survey");
						ui.EndElement("a");
					}
					ui.EndElement("span");
				}

				ui._(" and tell us what you think.");
				ui.EndElement("div");
			}
		}
	}
}