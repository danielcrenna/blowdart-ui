// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class ToastRenderer :
		IRenderer<BeginToastInstruction, RenderTreeBuilder>,
		IRenderer<EndToastInstruction, RenderTreeBuilder>
	{
		public void Render(RenderTreeBuilder b, BeginToastInstruction instruction)
		{
			BeginToast(b, instruction);
		}
		
		public void Render(RenderTreeBuilder b, EndToastInstruction instruction)
		{
			EndToast(b);
		}
		
		private static void BeginToast(RenderTreeBuilder b, BeginToastInstruction instruction)
		{
			b.BeginDiv("toast");
			b.AddAttribute(HtmlAttributes.Role, "alert");
			b.AddAttribute(HtmlAttributes.Aria.Live, "assertive");
			b.AddAttribute(HtmlAttributes.Aria.Atomic, "true");
			{
				b.BeginDiv("toast-header");
				{
					if (!string.IsNullOrWhiteSpace(instruction.HeaderText))
					{
						b.BeginElement("strong", "mr-auto");
						b.AddContent(instruction.HeaderText);
						b.CloseElement();
					}

					if (instruction.Timestamp.HasValue)
					{
						b.BeginElement("small", "text-muted");
						b.AddContent(instruction.Timestamp.ToString());
						b.CloseElement();
					}

					b.BeginButton("ml=2 mb-1 close");
					b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Button);
					b.AddAttribute(HtmlAttributes.Data.Dismiss, "toast");
					b.AddAttribute(HtmlAttributes.Aria.Label, "Close");
					{
						b.BeginSpan();
						{
							b.AriaHidden();
							b.AddContent("x");

							b.CloseElement();
						}

						b.CloseElement();
					}

					b.CloseElement();
				}

				b.BeginDiv("toast-body");
			}
		}

		private static void EndToast(RenderTreeBuilder b)
		{
			b.CloseElement(); // toast-body
			b.CloseElement(); // toast
		}
	}
}