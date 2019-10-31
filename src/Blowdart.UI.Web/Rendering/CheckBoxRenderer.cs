// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
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
				b.OpenElement(b.NextSequence(), Strings.Label);
				{
					b.AddAttribute(_(), Strings.Class, "form-check-label");
					b.AddAttribute(_(), Strings.For, checkBox.Id);
					b.AddContent(_(), checkBox.Text);

					b.CloseElement();
				}
			}

			b.OpenElement(b.NextSequence(), Strings.Div);
			{
				b.AddAttribute(_(), Strings.Class, "form-inline");

				switch (checkBox.Alignment)
				{
					case CheckBoxAlignment.Left:
						RenderInput();
						RenderLabel();
						break;
					case CheckBoxAlignment.Right:
						RenderLabel();
						RenderInput();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

                b.CloseElement();
			}

			int _()
			{
				return b.NextSequence();
			}

			void RenderInput()
			{
				b.OpenElement(b.NextSequence(), Strings.Input);
				{
					b.AddAttribute(_(), Strings.Class, "form-check-input");
					b.AddAttribute(_(), Strings.Type, Strings.Checkbox);
					b.AddAttribute(_(), Strings.Value, checkBox.Value);
					b.AddAttribute(_(), Strings.Id, checkBox.Id);
					b.AddAttribute(_(), Events.OnClick, onclick);

					b.CloseElement();
				}
			}
		}
	}
}