// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class EndCollapsibleRenderer : IWebRenderer<EndCollapsibleInstruction>
	{
		public void Render(RenderTreeBuilder b, EndCollapsibleInstruction collapsible)
		{
			b.CloseElement(); // BeginUnorderedList
			b.CloseElement(); // BeginDiv
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as EndCollapsibleInstruction);
		}
	}
}