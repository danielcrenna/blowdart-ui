// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Blowdart.UI.Web.Extensions;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ButtonRenderer : IWebRenderer<ButtonInstruction>
    {
        private readonly ImGui _imGui;

        public ButtonRenderer(ImGui imGui)
        {
            _imGui = imGui;
        }

        public void Render(RenderTreeBuilder b, ButtonInstruction button)
        {
            RenderButton(b, button);
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            RenderButton(b, instruction as ButtonInstruction);
        }

        private void RenderButton(RenderTreeBuilder b, ButtonInstruction button)
        {
            var css = button.Type switch
            {
                ElementContext.Primary => "btn btn-primary",
                ElementContext.Secondary => "btn btn-secondary",
                ElementContext.Success => "btn btn-success",
                ElementContext.Danger => "btn btn-danger",
                ElementContext.Warning => "btn btn-warning",
                ElementContext.Info => "btn btn-info",
                ElementContext.Light => "btn btn-light",
                ElementContext.Dark => "btn btn-dark",
                _ => throw new ArgumentOutOfRangeException()
            };

            var onclick = _imGui.OnClickCallback(button.Id);

            b.OpenElement(HtmlElements.Button);
            b.AddAttribute(HtmlAttributes.Id, button.Id);
            b.AddAttribute(HtmlAttributes.Class, css);
            b.AddAttribute(DomEvents.OnClick, onclick);
            b.AddContent(button.Text);
            b.CloseElement();
        }
    }
}
