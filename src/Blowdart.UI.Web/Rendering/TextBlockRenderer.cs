// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;
using Blowdart.UI.Web.Extensions;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class TextBlockRenderer : IRenderer<TextBlockInstruction, RenderTreeBuilder>
    {
        public void Render(RenderTreeBuilder b, TextBlockInstruction textBlock)
        {
            b.OpenElement(HtmlElements.Paragraph);
            b.AddContent(textBlock.Value);
            b.CloseElement();
        }
    }
}
