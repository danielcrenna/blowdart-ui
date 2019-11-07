// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class CheckBoxRenderer : IWebRenderer<CheckBoxInstruction>
	{
		private readonly ImGui _imGui;

		public CheckBoxRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, CheckBoxInstruction checkBox)
		{
			RenderButton(b, checkBox);
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			RenderButton(b, instruction as CheckBoxInstruction);
		}

		private void RenderButton(RenderTreeBuilder b, CheckBoxInstruction checkBox)
		{
			var onclick = _imGui.OnClickCallback(checkBox.Id);

			void RenderLabel()
			{
				b.OpenElement(Strings.Label);
				{
					b.AddAttribute(Strings.Class, "form-check-label");
					b.AddAttribute(Strings.For, checkBox.Id);
					b.AddContent(checkBox.Text);

					b.CloseElement();
				}
			}

			b.OpenElement(Strings.Div);
			{
				b.AddAttribute(Strings.Class, "form-inline");

				switch (checkBox.Alignment)
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
					b.AddAttribute(Strings.Class, "form-check-input");
					b.AddAttribute(Strings.Type, Strings.Checkbox);
					b.AddAttribute(Strings.Value, checkBox.Value);
					b.AddAttribute(Strings.Id, checkBox.Id);
					b.AddAttribute(Events.OnClick, onclick);

					b.CloseElement();
				}
			}
		}
	}
}