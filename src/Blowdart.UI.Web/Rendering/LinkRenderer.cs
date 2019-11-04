// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class LinkRenderer : IWebRenderer<LinkInstruction>
    {
        public void Render(RenderTreeBuilder b, LinkInstruction link)
        {
            b.OpenElement(Strings.Anchor);
            b.AddAttribute(Strings.Href, link.Href);
            b.AddAttribute(Strings.Target, "_blank");
            b.AddContent(link.Title);
            b.CloseElement();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as LinkInstruction);
        }
    }
}
