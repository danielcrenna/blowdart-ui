// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class TextBoxRenderer : IRenderer<TextBoxInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public TextBoxRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, TextBoxInstruction instruction)
		{
			if (instruction.InForm)
				b.BeginDiv("form-group");

			if (!string.IsNullOrWhiteSpace(instruction.Placeholder))
			{
				b.BeginLabel();
				b.AddAttribute(HtmlAttributes.For, instruction.Id.ToString());
				b.Class(HtmlAttributes.ScreenReader.Only);
				b.AddContent(instruction.Placeholder);
				b.CloseElement();
			}

			b.BeginInput(instruction.InForm ? "form-control" : "");
			b.AddAttribute(DomEvents.OnChange, _imGui.OnChangeCallback(instruction.Id));
			b.AddAttribute(HtmlAttributes.Id, instruction.Id.ToString());
			{
				switch (instruction.Type)
				{
					case FieldType.Text:
						b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Text);
						break;
					case FieldType.Email:
						b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Email);
						break;
					case FieldType.Password:
						b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Password);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				if(!string.IsNullOrWhiteSpace(instruction.Placeholder))
					b.AddAttribute(HtmlAttributes.Placeholder, instruction.Placeholder);

				if(!string.IsNullOrWhiteSpace(instruction.Name))
					b.AddAttribute(HtmlAttributes.Name, instruction.Name);

				b.CloseElement();
			}

			if (instruction.InForm)
				b.CloseElement();
		}
	}
}