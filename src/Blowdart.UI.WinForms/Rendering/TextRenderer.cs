using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class TextRenderer : IRenderer<TextInstruction, Panel>
	{
		public void Render(Panel renderer, TextInstruction instruction)
		{
			var text = (TextInstruction) instruction;
			renderer.Controls.Add(new Label { Text = text.Text, AutoSize = true });
		}
	}
}
