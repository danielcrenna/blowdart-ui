// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable UnusedMember.Global

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor.Rendering
{
	internal sealed class AlertRenderer : IRenderer<AlertInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, AlertInstruction alert)
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

			b.OpenElement("div");
			b.AddAttribute("class", "alert alert-secondary mt-4");
			b.AddAttribute("role", "alert");
			{
				b.OpenElement("span");
				b.AddAttribute("class", "oi oi-pencil mr-2");
				b.AddAttribute("aria-hidden", "true");
				b.CloseElement();

				b.OpenElement("strong");
				b.AddContent(alert.Title);
				b.CloseElement();

				b.OpenElement("span");
				{
					b.AddAttribute("class", "text-nowrap");
					b.AddContent("Please take out");

					b.OpenElement("a");
					b.AddAttribute("target", "_blank");
					b.AddAttribute("class", "font-weight-bold");
					b.AddAttribute("href", "https://go.microsoft.com/fwlink/?linkid=2121313");
					b.AddContent("brief survey");
					b.CloseElement();
				}
				b.CloseElement();

				b.AddContent("and tell us what you think.");
			}
			b.CloseElement();
		}
	}
}