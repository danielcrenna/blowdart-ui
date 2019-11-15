// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Blowdart.UI.Web.Extensions;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class ButtonRenderer : IRenderer<ButtonInstruction, RenderTreeBuilder>
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
		
        private void RenderButton(RenderTreeBuilder b, ButtonInstruction instruction)
        {
            var css = instruction.Type switch
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

			switch (instruction.Size)
			{
				case ElementSize.Unspecified:
					break;
				case ElementSize.ExtraSmall:
					css += " btn-xs";
					break;
				case ElementSize.Small:
					css += " btn-sm";
					break;
				case ElementSize.Large:
					css += " btn-lg";
					break;
				case ElementSize.Block:
					css += " btn-block";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

            b.BeginButton();
            {
	            b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Button);
	            b.AddAttribute(HtmlAttributes.Role, HtmlInputTypes.Button);
				b.AddAttribute(HtmlAttributes.Id, instruction.Id);
	            b.AddAttribute(HtmlAttributes.Class, css);
	            b.AddAttribute(DomEvents.OnClick, _imGui.OnClickCallback(instruction.Id));

				switch (instruction.Alignment)
				{
					case ElementAlignment.Left:
						RenderDecorator(b, instruction);
						RenderButtonText(b, instruction);
						break;
					case ElementAlignment.Right:
						RenderButtonText(b, instruction);
						RenderDecorator(b, instruction);
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
