// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class SeparatorRenderer : IWebRenderer<SeparatorInstruction>
	{
		public void Render(RenderTreeBuilder b, SeparatorInstruction text)
		{
			b.OpenElement(HtmlElements.HorizontalRule);
			b.CloseElement();
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as SeparatorInstruction);
		}
	}
}