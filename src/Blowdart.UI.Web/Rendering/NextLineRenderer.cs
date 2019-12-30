// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class NextLineRenderer : IRenderer<NextLineInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, NextLineInstruction nextLine)
		{
			b.OpenElement("br");
			b.CloseElement();
		}
	}
}