using Blowdart.UI.Instructions;

namespace Blowdart.UI.Blazor
{
	public static class WebUiExtensions
	{
		public static void Header(this Ui ui, int level, string text)
		{
			var name = $"h{level}";
			ui.BeginElement(name);
			ui.Text(text);
			ui.EndElement(name);
		}

		public static bool Button(this Ui ui, string text)
		{
			var id = ui.NextId();
			ui.Add(new ButtonInstruction(id, text));
			return ui.OnEvent("onclick", id, out _);
		}
	}
}
