using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class TextRenderer : 
		IRenderer<TextInstruction, Panel>,
		IRenderer<TextBlockInstruction, Panel>,
		IRenderer<TextBoxInstruction, Panel>
	{
		public void Render(Panel p, TextInstruction instruction)
		{
			p.Controls.Add(new Label { Text = instruction.Text, AutoSize = true });
		}

		public void Render(Panel p, TextBlockInstruction instruction)
		{
			p.Controls.Add(new Label { Text = instruction.Text, AutoSize = true });
		}

		public void Render(Panel p, TextBoxInstruction instruction)
		{
			p.Controls.Add(new TextBox { Text = instruction.Value, AutoSize = true });
		}
	}
}
