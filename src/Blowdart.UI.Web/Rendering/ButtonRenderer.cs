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
            {
	            b.AddAttribute(HtmlAttributes.Id, button.Id);
	            b.AddAttribute(HtmlAttributes.Class, css);
	            b.AddAttribute(DomEvents.OnClick, onclick);

				switch (button.Alignment)
				{
					case ElementAlignment.Left:
						RenderDecorator(b, button);
						RenderButtonText(b, button);
						break;
					case ElementAlignment.Right:
						RenderButtonText(b, button);
						RenderDecorator(b, button);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

	            b.CloseElement();
			}
        }

        private static void RenderButtonText(RenderTreeBuilder b, ButtonInstruction button)
        {
	        if (!string.IsNullOrWhiteSpace(button.Text))
		        b.AddContent(button.Text);
        }

        private static void RenderDecorator(RenderTreeBuilder b, ButtonInstruction button)
        {
	        void MaybeAddLoadingHint()
	        {
		        if (!string.IsNullOrWhiteSpace(button.Text))
			        return;
		        b.BeginSpan(HtmlAttributes.ScreenReader.Only);
		        b.AddContent("Loading...");
		        b.CloseElement();
	        }

	        switch (button.Decorator)
	        {
		        case ElementDecorator.None:
			        break;
		        case ElementDecorator.SpinnerBorder:
			        b.BeginSpan("spinner-border spinner-border-sm");
			        b.AddAttribute(HtmlAttributes.Role, "status");
			        b.AriaHidden();
			        b.CloseElement();
			        MaybeAddLoadingHint();
			        break;
		        case ElementDecorator.SpinnerGrow:
			        b.BeginSpan("spinner-grow spinner-grow-sm");
			        b.AddAttribute(HtmlAttributes.Role, "status");
			        b.AriaHidden();
			        b.CloseElement();
			        MaybeAddLoadingHint();
			        break;
		        default:
			        throw new ArgumentOutOfRangeException();
	        }
        }
    }
}
