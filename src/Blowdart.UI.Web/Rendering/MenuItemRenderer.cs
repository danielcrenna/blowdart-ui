// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class MenuItemRenderer : IWebRenderer<MenuItemInstruction>
	{
		public void Render(RenderTreeBuilder b, MenuItemInstruction menu)
		{
			b.ListItem("nav-item px-3", () =>
			{
				b.Anchor("nav-link", menu.Template, () =>
				{
					b.InlineIcon(menu.Icon);
					b.AddContent(menu.Title);
				});
			});
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as MenuItemInstruction);
		}
	}
}