// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class SliderRenderer : IRenderer<SliderInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public SliderRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder r, SliderInstruction slider)
		{
			void RenderLabel()
			{
				r.OpenElement(HtmlElements.Label);
				{
					r.AddAttribute(HtmlAttributes.Class, "form-check-label");
					r.AddAttribute(HtmlAttributes.For, slider.Id);
					r.AddContent(slider.Text);

					r.CloseElement();
				}
			}

			r.OpenElement(HtmlElements.Div);
			{
				r.AddAttribute(HtmlAttributes.Class, "form-inline");

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

				r.CloseElement();
			}

			void RenderInput()
			{
				r.OpenElement(HtmlElements.Input);
				{
					r.AddAttribute(HtmlAttributes.Class, "form-range-input");
					r.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Range);
					r.AddAttribute(HtmlAttributes.Value, slider.Value);
					r.AddAttribute(HtmlAttributes.Id, slider.Id);

					switch (slider.Activation)
					{
						case InputActivation.OnChange:
							r.AddAttribute(DomEvents.OnChange, _imGui.OnChangeCallback(slider.Id));
							break;
						case InputActivation.OnInput:
							r.AddAttribute(DomEvents.OnInput, _imGui.OnInputCallback(slider.Id));
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}

					r.CloseElement();
				}
			}
		}
	}
}