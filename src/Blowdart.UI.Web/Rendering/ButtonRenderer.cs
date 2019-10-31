// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

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
                ButtonType.Primary => "btn btn-primary",
                ButtonType.Secondary => "btn btn-secondary",
                ButtonType.Success => "btn btn-success",
                ButtonType.Danger => "btn btn-danger",
                ButtonType.Warning => "btn btn-warning",
                ButtonType.Info => "btn btn-info",
                ButtonType.Light => "btn btn-light",
                ButtonType.Dark => "btn btn-dark",
                _ => throw new ArgumentOutOfRangeException()
            };

            var onclick = _imGui.OnClickCallback(button.Id);

            b.OpenElement(b.NextSequence(), Strings.Button);
            b.AddAttribute(b.NextSequence(), Strings.Id, button.Id);
            b.AddAttribute(b.NextSequence(), Strings.Class, css);
            b.AddAttribute(b.NextSequence(), Events.OnClick, onclick);
            b.AddContent(b.NextSequence(), button.Text);
            b.CloseElement();
        }
    }
}
