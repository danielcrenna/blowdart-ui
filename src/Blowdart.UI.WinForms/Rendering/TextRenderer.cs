using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class TextRenderer : 
		IRenderer<TextInstruction, Panel>,
		IRenderer<TextBlockInstruction, Panel>
	{
		public void Render(Panel p, TextInstruction instruction)
		{
			var text = instruction;
			p.Controls.Add(new Label { Text = text.Text, AutoSize = true });
		}

		public void Render(Panel p, TextBlockInstruction instruction)
		{
			var text = instruction;
			p.Controls.Add(new Label { Text = text.Text, AutoSize = true });
		}
	}
}
