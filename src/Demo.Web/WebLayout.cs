using Blowdart.UI;

namespace Demo.Web
{
	public class WebLayout
	{
		public static void Index(Ui ui)
		{
			ui.BeginMenu("Blowdart.UI Demo");
			{
				ui.MenuItem(OpenIconicIcons.Home, "Home", "/");
				ui.MenuItem(OpenIconicIcons.Plus, "Counter", "/counter");
				ui.MenuItem(OpenIconicIcons.ListRich, "Fetch Data", "/fetchdata");
				ui.MenuItem(OpenIconicIcons.Code, "Elements", "/elements");
				ui.MenuItem(OpenIconicIcons.Folder, "Editor", "/editor");
				ui.MenuItem(OpenIconicIcons.Aperture, "Styles", "/styles");

				ui.EndMenu();
			}

			ui.Main(() =>
			{
				ui.TopRow(() => { ui.Link("https://github.com/blowdart-ui/blowdart-ui", "About"); });

				ui.MainContent(ui.LayoutBody());
			});
		}
	}
}
