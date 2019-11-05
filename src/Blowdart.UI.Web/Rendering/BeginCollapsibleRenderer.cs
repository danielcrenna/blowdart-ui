// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class BeginCollapsibleRenderer : IWebRenderer<BeginCollapsibleInstruction>
	{
		private readonly ImGui _imGui;
		private static bool _collapseNavMenu;

		private static string NavMenuCssClass(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;

		public BeginCollapsibleRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, BeginCollapsibleInstruction collapsible)
		{
			b.Div("top-row pl-4 navbar navbar-dark", () =>
			{
				b.Anchor("navbar-brand", "", () => b.AddContent(collapsible.Title));
				if (b.Button(_imGui, "navbar-toggler", () => b.Span("navbar-toggler-icon")))
				{
					_collapseNavMenu = !_collapseNavMenu;
				}
			});

			b.BeginDiv(NavMenuCssClass(_collapseNavMenu));
			b.BeginUnorderedList("nav flex-column");
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as BeginCollapsibleInstruction);
		}
	}
}