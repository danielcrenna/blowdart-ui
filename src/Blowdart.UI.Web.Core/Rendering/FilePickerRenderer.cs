using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Core.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Core.Rendering
{
	internal sealed class FilePickerRenderer : IRenderer<FilePickerInstruction, RenderTreeBuilder>
	{
		private readonly ImGui _imGui;

		public FilePickerRenderer(ImGui imGui)
		{
			_imGui = imGui;
		}

		public void Render(RenderTreeBuilder r, FilePickerInstruction instruction)
		{
			r.BeginDiv();
			{
				r.OpenElement(HtmlElements.Label);
				{
					r.AddAttribute(HtmlAttributes.Class, "btn");
					r.AddAttribute(HtmlAttributes.For, instruction.Id);
					r.AddContent(instruction.Label);

					r.CloseElement();
				}

				r.BeginInput();
				{
					r.AddAttribute(HtmlAttributes.Id, instruction.Id);
					r.AddAttribute(HtmlAttributes.Type, HtmlInputTypes.File);
					r.AddAttribute(DomEvents.OnChange, _imGui.OnChangeCallback(instruction.Id));
					r.AddAttribute(HtmlAttributes.Style, "visibility:hidden");

					r.CloseElement();
				}
				
				r.CloseElement();
			}
		}
	}
}