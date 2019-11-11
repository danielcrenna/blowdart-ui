// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
	internal sealed class ModalRenderer : 
		IWebRenderer<BeginModalInstruction>,
		IWebRenderer<EndModalInstruction>,
		IWebRenderer<ShowModalInstruction>
	{
		private readonly ImGui _imGui;

		public ModalRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder b, BeginModalInstruction beginModal)
		{
			b.BeginDiv("modal fade");
			b.AddAttribute(HtmlAttributes.TabIndex, -1);
			b.AddAttribute(HtmlAttributes.Role, "dialog");
			b.AddAttribute(HtmlAttributes.Id, beginModal.Id);
			b.AriaLabelledBy($"modal-title-{beginModal.Id}");
			b.AriaHidden();
			{
				b.BeginDiv("modal-dialog");
				b.AddAttribute(HtmlAttributes.Role, "document");
				{
					b.BeginDiv("modal-content");
					{
						RenderHeader(b, beginModal);
                        b.BeginDiv("modal-body");
					}
				}
			}
		}

		public void Render(RenderTreeBuilder b, EndModalInstruction instruction)
		{
			b.CloseElement(); // modal-body
			RenderFooter(b);
			b.CloseElement(); // modal-content
			b.CloseElement(); // modal-dialog
			b.CloseElement(); // modal
		}

		private static void RenderHeader(RenderTreeBuilder b, BeginModalInstruction beginModal)
		{
			b.BeginDiv("modal-header");
			{
				b.BeginElement("h5", "modal-title");
                b.AddAttribute(HtmlAttributes.Id, $"modal-title-{beginModal.Id}");
				b.AddContent(beginModal.Title);
				b.CloseElement();

				b.BeginElement("button", "close");
				b.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.Button);
				b.AddAttribute(HtmlAttributes.Data.Dismiss, "modal");
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
		}
        
		private static void RenderFooter(RenderTreeBuilder b)
		{
			b.BeginDiv("modal-footer");
			{
				b.BeginElement("button", "btn btn-secondary");
				b.AddAttribute(HtmlAttributes.Data.Dismiss, "modal");
				b.AddContent("Close");
				b.CloseElement();

				b.BeginElement("button", "btn btn-primary");
				b.AddContent("Save changes");
				b.CloseElement();

				b.CloseElement();
			}
		}

		public void Render(RenderTreeBuilder b, ShowModalInstruction instruction)
		{
			_imGui.ShowModal(instruction.Id);
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			switch (instruction)
			{
				case BeginModalInstruction begin:
					Render(b, begin);
					break;
				case EndModalInstruction end:
					Render(b, end);
					break;
				case ShowModalInstruction show:
					Render(b, show);
					break;
			}
		}
	}
}