// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web
{
    public interface IWebRenderer {
        void Render(RenderTreeBuilder b, RenderInstruction instruction);
    }
    public interface IWebRenderer<in T> : IWebRenderer where T : RenderInstruction
    {
        void Render(RenderTreeBuilder b, T instruction);
    }
}
