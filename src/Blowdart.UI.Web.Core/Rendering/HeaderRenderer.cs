// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Core.Rendering
{
    internal sealed class HeaderRenderer : IRenderer<HeaderInstruction, RenderTreeBuilder>
    {
        public void Render(RenderTreeBuilder b, HeaderInstruction header)
        {
	        if (header.Level < 1 || header.Level > 6)
		        throw new BlowdartException("Headers can only have six levels of rank");

			b.OpenElement($"h{header.Level}");
			if(!string.IsNullOrWhiteSpace(header.Class))
				b.Class(header.Class);
            b.AddContent(header.Text);
            b.CloseElement();
        }
    }
}