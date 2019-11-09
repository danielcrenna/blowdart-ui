using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class TextRenderer : IFormRenderer
	{
		public void Render(RenderInstruction instruction, Panel panel)
		{
			var text = (TextInstruction) instruction;
			panel.Controls.Add(new Label { Text = text.Text, AutoSize = true });
		}
	}
}
