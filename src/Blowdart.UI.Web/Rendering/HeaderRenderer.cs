// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class HeaderRenderer : IWebRenderer<HeaderInstruction>
    {
        public void Render(RenderTreeBuilder b, HeaderInstruction header)
        {
            b.OpenElement(b.NextSequence(), $"h{header.Level}");
            b.AddContent(b.NextSequence(), header.Text);
            b.CloseElement();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as HeaderInstruction);
        }
    }
}
