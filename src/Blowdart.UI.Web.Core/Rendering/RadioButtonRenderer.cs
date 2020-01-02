// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Core.Rendering
{
	internal sealed class RadioButtonRenderer : IRenderer<RadioButtonInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public RadioButtonRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder r, RadioButtonInstruction instruction)
		{
			r.OpenElement(HtmlElements.Div);
			{
				r.AddAttribute(HtmlAttributes.Class, HtmlInputTypes.Radio);

				switch (instruction.Alignment)
				{
					case ElementAlignment.Left:
						RenderInput();
						RenderLabel();
						break;
					case ElementAlignment.Right:
						RenderLabel();
						RenderInput();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				r.CloseElement();
			}

			void RenderInput()
			{
				r.OpenElement(HtmlElements.Input);
				{
					r.AddAttribute(HtmlAttributes.Class, "form-radio-input");
					r.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Radio);
					r.AddAttribute(HtmlAttributes.Checked, instruction.Value);
					r.AddAttribute(HtmlAttributes.Id, instruction.Id);
					r.AddAttribute(DomEvents.OnClick, _imGui.OnClickCallback(instruction.Id));

					r.CloseElement();
				}
			}

			void RenderLabel()
			{
				r.OpenElement(HtmlElements.Label);
				{
					r.AddAttribute(HtmlAttributes.Class, "form-radio-label");
					r.AddAttribute(HtmlAttributes.For, instruction.Id);
					r.AddContent(instruction.Text);

					r.CloseElement();
				}
			}
		}
	}
}