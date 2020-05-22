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
			ui.PushAttribute("class", "top-row pl-4 navbar navbar-dark");
			{
				ui.BeginElement("a");
				ui.PushAttribute("class", "navbar-brand");
				ui.PushAttribute("href", "");
				{	
					ui._("BlazorApp1");
					ui.EndElement("a");
				}

				ui.BeginElement("button");
				ui.PushAttribute("class", "navbar-toggler");
				{
					ui.BeginElement("span");
					ui.PushAttribute("class", "navbar-toggler-icon");
					ui.EndElement("span");
					
					ui.EndElement("button");
				}

				ui.EndElement("div");
			}

			ui.BeginElement("div");
			ui.PushAttribute("class", collapse ? "collapse" : "");
			{
				ui.BeginElement("ul");
				ui.PushAttribute("class", "nav flex-column");
				{
					ui.BeginElement("li");
					ui.PushAttribute("class", "nav-item px-3");
					{
						ui.BeginElement("a");
						ui.PushAttribute("class", "nav-link");
						ui.PushAttribute("href", "");
						{
							ui.BeginElement("span");
							ui.PushAttribute("class", "oi oi-home");
							ui.PushAttribute("aria-hidden", true);
							ui.EndElement("span");

							ui._(" Home");
							ui.EndElement("a");
						}

						ui.BeginElement("a");
						ui.PushAttribute("class", "nav-link");
						ui.PushAttribute("href", "counter");
						{
							ui.BeginElement("span");
							ui.PushAttribute("class", "oi oi-plus");
							ui.PushAttribute("aria-hidden", true);
							ui.EndElement("span");

							ui._(" Counter");
							ui.EndElement("a");
						}
						
						ui.BeginElement("a");
						ui.PushAttribute("class", "nav-link");
						ui.PushAttribute("href", "fetchdata");
						{
							ui.BeginElement("span");
							ui.PushAttribute("class", "oi oi-list-rich");
							ui.PushAttribute("aria-hidden", true);
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
			ui.PushAttribute("class", "alert alert-secondary mt-4");
			ui.PushAttribute("role", "alert");
			{
				ui.BeginElement("span");
				ui.PushAttribute("class", "oi oi-pencil mr-2");
				ui.PushAttribute("aria-hidden", true);
				ui.EndElement("span");

				ui.BeginElement("strong");
				ui._(title);
				ui.EndElement("strong");

				ui.BeginElement("span");
				ui.PushAttribute("class", "text-nowrap");
				{
					ui._(" Please take our ");
					ui.BeginElement("a");
					{
						ui.PushAttribute("target", "_blank");
						ui.PushAttribute("class", "font-weight-bold");
						ui.PushAttribute("href", "https://go.microsoft.com/fwlink/?linkid=2121313");
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