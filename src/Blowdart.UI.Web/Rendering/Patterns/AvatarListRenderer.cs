using Blowdart.UI.Instructions.Patterns;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering.Patterns
{
	public class AvatarListRenderer : IWebRenderer<AvatarListInstruction>
	{
		public void Render(RenderTreeBuilder b, AvatarListInstruction instruction)
		{
			b.BeginDiv("d-flex align-items-center");
			{
				b.BeginUnorderedList("avatars");

				var first = true;
				foreach (var avatar in instruction.Avatars)
				{
					b.BeginListItem();
					if (!first)
						b.AddAttribute(HtmlAttributes.Style, $"margin-left:-{instruction.Size / 2}px");
					first = false;
                    {
						b.BeginAnchor();
						{
							b.AddAttribute(HtmlAttributes.Href, "#");
							b.AddAttribute(HtmlAttributes.Data.Toggle, "tooltip");
							b.AddAttribute(HtmlAttributes.Data.Placement, "top");
							b.AddAttribute(HtmlAttributes.Title, avatar.FullName);

							b.BeginElement(HtmlElements.Image, "avatar");
							b.AddAttribute(HtmlAttributes.Alt, avatar.FullName);
							b.AddAttribute(HtmlAttributes.Src, avatar.ImageUrl);
                            b.AddAttribute(HtmlAttributes.Height, instruction.Size);
                            b.AddAttribute(HtmlAttributes.Width, instruction.Size);
							b.CloseElement();
						}
						b.CloseElement();
					}
					b.CloseElement();
				}
				b.CloseElement();
			}
			b.CloseElement();
		}

		public void Render(RenderTreeBuilder b, RenderInstruction instruction)
		{
			Render(b, (AvatarListInstruction) instruction);
		}
	}
}
