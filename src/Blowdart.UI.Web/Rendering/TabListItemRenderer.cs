// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class TabListItemRenderer : IWebRenderer<TabListItemInstruction>
	{
		private readonly ImGui _imGui;

		public TabListItemRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, TabListItemInstruction tabListItem)
		{
			RenderTab(b, tabListItem);
		}
        
		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			RenderTab(b, instruction as TabListItemInstruction);
		}

		private void RenderTab(RenderTreeBuilder b, TabListItemInstruction tabListItem)
		{
			var onclick = _imGui.OnClickCallback(tabListItem.Id);

			/*
				<ul class="nav nav-tabs" role="tablist">
				  <li class="nav-item">
				    <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Home</a>
				  </li>
				</ul>
			 */

			b.OpenElement(HtmlElements.ListItem);
			{
				b.AddAttribute(HtmlAttributes.Class, "nav-item");

				b.OpenElement(HtmlElements.Anchor);
				{
					var tabId = tabListItem.Text.ToCssCase();

					b.AddAttribute(HtmlAttributes.Class, tabListItem.Active ? "nav-link active" : "nav-link");
					b.AddAttribute(HtmlAttributes.Id, tabListItem.Id);
					b.AddAttribute(HtmlAttributes.Data.Toggle, "tab");
					b.AddAttribute(HtmlAttributes.Role, "tab");
					b.AddAttribute(HtmlAttributes.Aria.Selected, tabListItem.Active);
					b.AddAttribute(HtmlAttributes.Aria.Controls, tabId);
                    b.AddAttribute(Events.OnClick, onclick);
					b.AddContent(tabListItem.Text);

					b.CloseElement();
				}

				b.CloseElement();
			}
		}
	}
}