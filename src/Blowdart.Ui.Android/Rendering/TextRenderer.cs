using System;
using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class TextRenderer : IRenderer<TextInstruction, ViewGroup>
	{
		public void Render(ViewGroup v, TextInstruction instruction)
		{
			var text = new TextView(v.Context);
			text.Text = instruction.Text;

			v.AddView(text);
		}
	}
}