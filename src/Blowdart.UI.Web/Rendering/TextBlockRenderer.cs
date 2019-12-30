// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class TextBlockRenderer : IRenderer<TextBlockInstruction, RenderTreeBuilder>
    {
        public void Render(RenderTreeBuilder b, TextBlockInstruction instruction)
        {
	        var elementType = instruction.Size switch
	        {
		        ElementSize.Small => HtmlElements.Small,
		        ElementSize.Unspecified => HtmlElements.Paragraph,
		        ElementSize.ExtraSmall => HtmlElements.Paragraph,
		        ElementSize.Large => HtmlElements.Paragraph,
		        ElementSize.Block => HtmlElements.Paragraph,
		        _ => HtmlElements.Paragraph
	        };

	        b.OpenElement(elementType);
            {
	            if (!string.IsNullOrWhiteSpace(instruction.Class))
		            b.Class(instruction.Class);
	            b.AddContent(instruction.Text);
			}

            b.CloseElement();
        }
    }
}
