using Blowdart.UI;

namespace Demo.WinForms
{
	public static class FormLayout
	{
		public static void Index(Ui ui)
		{
			ui.BeginMenu();
			{
				ui.MenuItem(OpenIconicIcons.Home, "Home", "/");
				ui.MenuItem(OpenIconicIcons.Plus, "Counter", "/counter");
				ui.MenuItem(OpenIconicIcons.ListRich, "Fetch Data", "/fetchdata");
				ui.MenuItem(OpenIconicIcons.Code, "Elements", "/elements");
				ui.MenuItem(OpenIconicIcons.Folder, "Editor", "/editor");
				ui.MenuItem(OpenIconicIcons.Aperture, "Styles", "/styles");

				ui.EndMenu();
			}

			ui.BeginMain();
			{
				ui.BeginTopRow();
				{
					ui.InlineIcon(OpenIconicIcons.Fork);
					ui.Link("https://github.com/blowdart-ui/blowdart-ui", "About");
					ui.EndTopRow();
				}
				
				ui.BeginMainContent();
				{
					ui.LayoutBody();
					ui.EndMainContent();
				}

				ui.EndMain();
			}
		}
	}
}
