// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class InlineImageRenderer : IRenderer<InlineImageInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, InlineImageInstruction image)
		{
			b.OpenElement(HtmlElements.Span);
			b.AriaHidden();
			{
				b.OpenElement(HtmlElements.Image);
				b.AddAttribute(HtmlAttributes.Src, image.Source);
				b.AddAttribute(HtmlAttributes.Width, image.Width);
				b.AddAttribute(HtmlAttributes.Height, image.Height);
				b.CloseElement();
			}
			b.CloseElement();
		}
	}
}