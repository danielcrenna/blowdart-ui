// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class CollapsibleHeaderRenderer : 
		IWebRenderer<BeginCollapsibleHeaderInstruction>,
		IWebRenderer<EndCollapsibleHeaderInstruction>
	{
		private readonly ImGui _imGui;
		private static bool _collapseNavMenu;

		private static string NavMenuCssClass(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;

		public CollapsibleHeaderRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, BeginCollapsibleHeaderInstruction begin)
		{
			b.BeginAnchor("navbar-brand");
		}

		public void Render(RenderTreeBuilder b, EndCollapsibleHeaderInstruction end)
		{
			b.CloseElement(); // BeginAnchor

			if (b.Button(_imGui, "navbar-toggler", () => b.Span("navbar-toggler-icon")))
				_collapseNavMenu = !_collapseNavMenu;

			b.CloseElement(); // BeginCollapsibleHeader
			b.BeginDiv(NavMenuCssClass(_collapseNavMenu));
			b.BeginUnorderedList("nav flex-column");
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			switch (instruction)
			{
				case BeginCollapsibleHeaderInstruction begin:
					Render(b, begin);
					break;
				case EndCollapsibleHeaderInstruction end:
					Render(b, end);
					break;
			}
		}
	}
}