// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class EndMenuRenderer : IWebRenderer<EndMenuInstruction>
	{
		public void Render(RenderTreeBuilder b, EndMenuInstruction menu)
		{
			b.CloseElement(); // BeginUnorderedList
			b.CloseElement(); // BeginDiv
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as EndMenuInstruction);
		}
	}
}