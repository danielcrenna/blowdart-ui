// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class ChangePageRenderer : IRenderer<ChangePageInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public ChangePageRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder renderer, ChangePageInstruction instruction)
		{
			_imGui.ChangePage(instruction.Template);
		}
	}
}