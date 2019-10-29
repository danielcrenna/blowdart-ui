// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class InlineIconRenderer : IWebRenderer<InlineIconInstruction>
	{
		public void Render(RenderTreeBuilder b, InlineIconInstruction icon)
		{
			b.InlineIcon(icon.Icon);
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as InlineIconInstruction);
		}
	}
}