// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class TabContentRenderer : 
		IWebRenderer<BeginTabContentInstruction>,
        IWebRenderer<EndTabContentInstruction>
	{
		public void Render(RenderTreeBuilder b, BeginTabContentInstruction beginTabContent)
		{
			RenderBeginTabContent(b, beginTabContent);
		}

		public void Render(RenderTreeBuilder b, EndTabContentInstruction endTabContent)
		{
			b.CloseElement();
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			switch (instruction)
			{
				case BeginTabContentInstruction begin:
					RenderBeginTabContent(b, begin);
					break;
				case EndTabContentInstruction end:
					Render(b, end);
					break;
			}
		}

		private static void RenderBeginTabContent(RenderTreeBuilder b, BeginTabContentInstruction beginTabContent)
		{
			/*
				<div class="tab-content" id="myTabContent">
				  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">...</div>
				  <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">...</div>				  
				</div>
			 */

			b.OpenElement(HtmlElements.Div);
			{
				var tabId = beginTabContent.Text.ToCssCase();

				b.AddAttribute(HtmlAttributes.Id, tabId);
				b.AddAttribute(HtmlAttributes.Class, beginTabContent.Active ? "tab-pane fade show active" : "tab-pane fade");
				b.AddAttribute(HtmlAttributes.Role, "tabpanel");
				b.AddAttribute(HtmlAttributes.Aria.LabelledBy, beginTabContent.Id);
			}
		}
	}
}