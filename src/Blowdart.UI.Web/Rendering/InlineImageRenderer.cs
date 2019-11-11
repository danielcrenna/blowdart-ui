// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class InlineImageRenderer : IRenderer<InlineImageInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, InlineImageInstruction image)
		{
			b.InlineImage(image.Source, image.Width, image.Height);
		}
	}
}