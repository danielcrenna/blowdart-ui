// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor.Rendering
{
	internal sealed class BeginElementRenderer : IRenderer<BeginElementInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, BeginElementInstruction element)
		{
			b.OpenElement(element.Name);
		}
	}
}