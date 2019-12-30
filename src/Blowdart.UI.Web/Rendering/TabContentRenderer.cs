// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class TabContentRenderer : 
		IRenderer<BeginTabContentInstruction, RenderTreeBuilder>,
        IRenderer<EndTabContentInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, BeginTabContentInstruction beginTabContent)
		{
			b.OpenElement(HtmlElements.Div);
			{
				var tabId = beginTabContent.Text.ToDashCase();

				b.AddAttribute(HtmlAttributes.Id, tabId);
				b.AddAttribute(HtmlAttributes.Class, beginTabContent.Active ? "tab-pane fade show active" : "tab-pane fade");
				b.AddAttribute(HtmlAttributes.Role, "tabpanel");
				b.AddAttribute(HtmlAttributes.Aria.LabelledBy, beginTabContent.Id);
			}
		}

		public void Render(RenderTreeBuilder b, EndTabContentInstruction endTabContent)
		{
			b.CloseElement();
		}
	}
}