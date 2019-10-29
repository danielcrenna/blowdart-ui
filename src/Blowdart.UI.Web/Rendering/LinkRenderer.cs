// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class LinkRenderer : IWebRenderer<LinkInstruction>
    {
        public void Render(RenderTreeBuilder b, LinkInstruction link)
        {
            b.OpenElement(b.NextSequence(), "a");
            b.AddAttribute(b.NextSequence(), "href", link.Href);
            b.AddAttribute(b.NextSequence(), "target", "_blank");
            b.AddContent(b.NextSequence(), link.Title);
            b.CloseElement();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as LinkInstruction);
        }
    }
}
