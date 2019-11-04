// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class EditorRenderer : IWebRenderer<EditorInstruction>
    {
        public void Render(RenderTreeBuilder b, EditorInstruction editor)
        {
            var type = typeof(DynamicEditor<>).MakeGenericType(editor.Type);
            b.OpenComponent(type);
            b.AddAttribute(nameof(DynamicEditor<object>.Model), editor.Object);
            b.CloseComponent();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as EditorInstruction);
        }
    }
}
