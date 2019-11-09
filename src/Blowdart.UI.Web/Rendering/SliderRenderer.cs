// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class SliderRenderer : IWebRenderer<SliderInstruction>
	{
		private readonly ImGui _imGui;

		public SliderRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, SliderInstruction slider)
		{
			RenderButton(b, slider);
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			RenderButton(b, instruction as SliderInstruction);
		}

		private void RenderButton(RenderTreeBuilder b, SliderInstruction slider)
		{
			void RenderLabel()
			{
				b.OpenElement(HtmlElements.Label);
				{
					b.AddAttribute(HtmlAttributes.Class, "form-check-label");
					b.AddAttribute(HtmlAttributes.For, slider.Id);
					b.AddContent(slider.Text);

					b.CloseElement();
				}
			}

			b.OpenElement(HtmlElements.Div);
			{
				b.AddAttribute(HtmlAttributes.Class, "form-inline");

				switch (slider.Alignment)
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
					b.AddAttribute(HtmlAttributes.Class, "form-range-input");
					b.AddAttribute(HtmlAttributes.Type, InputTypes.Range);
					b.AddAttribute(HtmlAttributes.Value, slider.Value);
					b.AddAttribute(HtmlAttributes.Id, slider.Id);
					b.AddAttribute(Events.OnChange, _imGui.OnChangeCallback(slider.Id));

					b.CloseElement();
				}
			}
		}
	}
}