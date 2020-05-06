// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor.Rendering
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
			b.OpenElement("button");
			b.AddStyle(_imGui.Ui, new StyleContext());
			b.AddAttribute("role", "button");
			b.AddAttribute("id", instruction.Id);
			b.AddAttribute("onclick", _imGui.OnClick(instruction.Id));

			if (!string.IsNullOrWhiteSpace(instruction.Text))
				b.AddContent(instruction.Text);

			b.CloseElement();
		}
	}
}