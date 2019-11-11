using Blowdart.UI;

namespace Demo.Web
{
	public class WebLayout
	{
		public static void Index(Ui ui)
		{
			ui.BeginMenu();
			{
				ui.BeginMenuHeader();
				ui.InlineImage("_content/Blowdart.UI.Web/svg/logo.svg", 36, 36);
				ui.Text("Blowdart.UI Demo");
				ui.EndMenuHeader();

				ui.MenuItem(OpenIconicIcons.Home, "Home", "/");
				ui.MenuItem(OpenIconicIcons.Plus, "Counter", "/counter");
				ui.MenuItem(OpenIconicIcons.ListRich, "Fetch Data", "/fetchdata");
				ui.MenuItem(OpenIconicIcons.Code, "Elements", "/elements");
				ui.MenuItem(OpenIconicIcons.Folder, "Editor", "/editor");
				ui.MenuItem(OpenIconicIcons.Aperture, "Styles", "/styles");
				ui.MenuItem(OpenIconicIcons.Paperclip, "Patterns", "/patterns");

				ui.EndMenu();
			}

			ui.BeginMain();
			{
				ui.BeginTopRow();
				ui.InlineIcon(OpenIconicIcons.Fork);
				ui.Link("https://github.com/blowdart-ui/blowdart-ui", "About");
				ui.EndTopRow();

				ui.MainContent(ui.LayoutBody());

				ui.EndMain();
			}
		}
	}
}
