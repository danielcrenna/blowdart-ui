// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class AlertRenderer : 
		IRenderer<BeginAlertInstruction, RenderTreeBuilder>,
		IRenderer<EndAlertInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, BeginAlertInstruction alert)
		{
			var context = alert.Context.ToString().ToLowerInvariant();
			
			b.BeginDiv($"alert alert-{context} alert-dismissible fade show");
			b.AddAttribute(HtmlAttributes.Role, "alert");
            
            b.BeginElement("button", "close");
            {
                b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Button);
	            b.AddAttribute(HtmlAttributes.Data.Dismiss, "alert");
	            b.AddAttribute(HtmlAttributes.Aria.Label, "close");
				b.BeginSpan("");
				{
                    b.AriaHidden();
                    b.AddContent("x");
                    b.CloseElement();
				}
	            b.CloseElement();
			}
		}

		public void Render(RenderTreeBuilder b, EndAlertInstruction alert)
		{
			b.CloseElement();
		}
	}
}