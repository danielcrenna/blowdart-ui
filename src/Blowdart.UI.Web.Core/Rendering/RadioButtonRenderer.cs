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

		public void Render(RenderTreeBuilder b, RadioButtonInstruction radioButton)
		{
			var onclick = _imGui.OnClickCallback(radioButton.Id);

			void RenderLabel()
			{
				b.OpenElement(HtmlElements.Label);
				{
					b.AddAttribute(HtmlAttributes.Class, "form-radio-label");
					b.AddAttribute(HtmlAttributes.For, radioButton.Id);
					b.AddContent(radioButton.Text);

					b.CloseElement();
				}
			}

			b.OpenElement(HtmlElements.Div);
			{
				b.AddAttribute(HtmlAttributes.Class, HtmlInputTypes.Radio);

				switch (radioButton.Alignment)
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

				b.CloseElement();
			}

			void RenderInput()
			{
				b.OpenElement(HtmlElements.Input);
				{
					b.AddAttribute(HtmlAttributes.Class, "form-radio-input");
					b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Radio);
					b.AddAttribute(HtmlAttributes.Checked, radioButton.Value);
					b.AddAttribute(HtmlAttributes.Id, radioButton.Id);
					b.AddAttribute(DomEvents.OnClick, onclick);

					b.CloseElement();
				}
			}
		}
	}
}