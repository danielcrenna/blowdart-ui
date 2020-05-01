// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo
{
	public static class DemoComponents
	{
		/*
			<div class="alert alert-secondary mt-4" role="alert">
			    <span class="oi oi-pencil mr-2" aria-hidden="true"></span>
			    <strong>@Title</strong>
			    <span class="text-nowrap">
			        Please take our
			        <a target="_blank" class="font-weight-bold" href="https://go.microsoft.com/fwlink/?linkid=2121313">brief survey</a>
			    </span>
			    and tell us what you think.
			</div>
		 */
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
				ui.Text(title);
				ui.EndElement("strong");

				ui.BeginElement("span");
				ui.PushAttribute("class", "text-nowrap");
				{
					ui.Text(" Please take our ");
					ui.BeginElement("a");
					{
						ui.PushAttribute("target", "_blank");
						ui.PushAttribute("class", "font-weight-bold");
						ui.PushAttribute("href", "https://go.microsoft.com/fwlink/?linkid=2121313");
						ui.Text(" brief survey");
						ui.EndElement("a");
					}
					ui.EndElement("span");
				}

				ui.Text(" and tell us what you think.");
				ui.EndElement("div");
			}
		}
	}
}