// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class RadioButtonRenderer : IWebRenderer<RadioButtonInstruction>
	{
		private readonly ImGui _imGui;

		public RadioButtonRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, RadioButtonInstruction radioButton)
		{
			RenderButton(b, radioButton);
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			RenderButton(b, instruction as RadioButtonInstruction);
		}

		private void RenderButton(RenderTreeBuilder b, RadioButtonInstruction radioButton)
		{
			var onclick = _imGui.OnClickCallback(radioButton.Id);

			void RenderLabel()
			{
				b.OpenElement(Strings.Label);
				{
					b.AddAttribute(Strings.Class, "form-radio-label");
					b.AddAttribute(Strings.For, radioButton.Id);
					b.AddContent(radioButton.Text);

					b.CloseElement();
				}
			}

			b.OpenElement(Strings.Div);
			{
				b.AddAttribute(Strings.Class, Strings.Radio);

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
				b.OpenElement(Strings.Input);
				{
					b.AddAttribute(Strings.Class, "form-radio-input");
					b.AddAttribute(Strings.Type, Strings.Radio);
					b.AddAttribute(Strings.Checked, radioButton.Value);
					b.AddAttribute(Strings.Id, radioButton.Id);
					b.AddAttribute(Events.OnClick, onclick);

					b.CloseElement();
				}
			}
		}
	}
}