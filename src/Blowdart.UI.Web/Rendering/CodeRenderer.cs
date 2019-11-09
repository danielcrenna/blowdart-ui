// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Internal;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class CodeRenderer : IWebRenderer<CodeInstruction>
	{
		public void Render(RenderTreeBuilder b, CodeInstruction code)
		{
			if (code.Block)
			{
				b.OpenElement(HtmlElements.Pre);
				b.AddAttribute(HtmlAttributes.Class, ".pre-scrollable");
			}
			b.OpenElement(HtmlElements.Code);
			b.AddContent(code.Value.FormatCode());
			b.CloseElement();
			if(code.Block)
				b.CloseElement();
		}
		
		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, instruction as CodeInstruction);
		}
	}
}