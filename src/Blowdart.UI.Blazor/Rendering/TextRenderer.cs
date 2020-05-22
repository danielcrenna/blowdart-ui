// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable UnusedMember.Global

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor.Rendering
{
	internal sealed class TextRenderer : IRenderer<TextInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, TextInstruction instruction)
		{
			var content = instruction.Text?.ToString();
			if(!string.IsNullOrEmpty(content))
				b.AddContent(content);
		}
	}
}