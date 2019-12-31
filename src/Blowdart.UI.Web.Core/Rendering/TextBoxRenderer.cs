// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Core.Rendering
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
			bool grouped;

			switch (instruction.Style)
			{
				case ElementStyle.None:
					grouped = instruction.InForm;
					if (grouped)
						b.BeginDiv("form-group");
					break;
				case ElementStyle.Rounded:
					grouped = true;
					b.BeginDiv("input-group input-group-round");
					break;
				case ElementStyle.Outline:
					throw new NotImplementedException();
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (grouped && (instruction.Iconic != 0 || instruction.Material != 0))
			{
				b.BeginDiv("input-group-prepend");
				b.BeginSpan("input-group-text");
				if (instruction.Iconic != 0)
					b.InlineIcon(instruction.Iconic);
				else
					b.InlineIcon(instruction.Material);
				b.CloseElement();
				b.CloseElement();
			}

			if (!string.IsNullOrWhiteSpace(instruction.Placeholder))
			{
				b.BeginLabel();
				b.AddAttribute(HtmlAttributes.For, instruction.Id.ToString());
				b.Class(HtmlAttributes.ScreenReader.Only);
				b.AddContent(instruction.Placeholder);
				b.CloseElement();
			}

			b.BeginInput(grouped ? $"form-control {instruction.Class}" : instruction.Class);
			switch (instruction.Activation)
			{
				case InputActivation.OnInput:
					b.AddAttribute(DomEvents.OnInput, _imGui.OnInputCallback(instruction.Id));
					break;
				case InputActivation.OnChange:
					b.AddAttribute(DomEvents.OnChange, _imGui.OnChangeCallback(instruction.Id));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

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
					case FieldType.Search:
						b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Search);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				if (!string.IsNullOrWhiteSpace(instruction.Placeholder))
				{
					b.AddAttribute(HtmlAttributes.Placeholder, instruction.Placeholder);
					b.AriaLabel(instruction.Placeholder);
				}

				if(!string.IsNullOrWhiteSpace(instruction.Name))
					b.AddAttribute(HtmlAttributes.Name, instruction.Name);

				b.CloseElement();
			}

			if (grouped)
				b.CloseElement();
		}
	}
}