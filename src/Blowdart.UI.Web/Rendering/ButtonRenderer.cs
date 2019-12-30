// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class ButtonRenderer : IRenderer<ButtonInstruction, RenderTreeBuilder>
	{
        private readonly ImGui _imGui;

        public ButtonRenderer(ImGui imGui)
        {
            _imGui = imGui;
        }

        public void Render(RenderTreeBuilder b, ButtonInstruction instruction)
        {
	        var modifier = instruction.Iconic != 0 ? "btn-labelled " : "";

	        var style = instruction.Style switch
	        {
		        ElementStyle.Outline => "btn-outline",
		        ElementStyle.Rounded => "btn-round",
		        _ => "btn"
	        };

	        var css = instruction.Context switch
	        {
		        ElementContext.Unspecified => $"btn {modifier}{style}",
		        ElementContext.Primary => $"btn {modifier}{style}-primary",
		        ElementContext.Secondary => $"btn {modifier}{style}-secondary",
		        ElementContext.Success => $"btn {modifier}{style}-success",
		        ElementContext.Danger => $"btn {modifier}{style}-danger",
		        ElementContext.Warning => $"btn {modifier}{style}-warning",
		        ElementContext.Info => $"btn {modifier}{style}-info",
		        ElementContext.Light => $"btn {modifier}{style}-light",
		        ElementContext.Dark => $"btn {modifier}{style}-dark",
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

				if(!string.IsNullOrWhiteSpace(instruction.Tooltip))
					b.AddAttribute(HtmlAttributes.Title, instruction.Tooltip);

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

        private static void RenderDecorator(RenderTreeBuilder b, ButtonInstruction instruction)
        {
	        void MaybeAddLoadingHint()
	        {
		        if (!string.IsNullOrWhiteSpace(instruction.Text))
			        return;
		        b.BeginSpan(HtmlAttributes.ScreenReader.Only);
		        b.AddContent("Loading...");
		        b.CloseElement();
	        }

	        if (instruction.Iconic.HasValue)
	        {
				b.BeginSpan("btn-label");
		        b.InlineIcon(instruction.Iconic.Value, "btn-label");
				b.CloseElement();
	        }

	        if (instruction.Material.HasValue)
	        {
		        b.BeginSpan();
		        b.InlineIcon(instruction.Material.Value);
		        b.CloseElement();
	        }

			switch (instruction.Decorator)
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
