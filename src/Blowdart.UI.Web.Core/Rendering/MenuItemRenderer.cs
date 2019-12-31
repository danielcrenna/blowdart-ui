// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Core.Rendering
{
	internal sealed class MenuItemRenderer : IRenderer<MenuItemInstruction, RenderTreeBuilder>
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
	}
}