// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class CollapsibleRenderer :
		IRenderer<BeginCollapsibleInstruction, RenderTreeBuilder>,
		IRenderer<EndCollapsibleInstruction, RenderTreeBuilder>,
		IRenderer<ShowCollapsibleInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public CollapsibleRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, BeginCollapsibleInstruction instruction)
		{
			b.BeginDiv("collapse");
			b.AddAttribute(HtmlAttributes.Id, instruction.Id);
		}

		public void Render(RenderTreeBuilder b, EndCollapsibleInstruction instruction)
		{
			b.CloseElement();
		}

		public void Render(RenderTreeBuilder b, ShowCollapsibleInstruction instruction)
		{
			_imGui.ShowCollapsible(instruction.Id);
		}
	}
}