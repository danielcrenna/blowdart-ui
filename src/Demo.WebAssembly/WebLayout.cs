using Blowdart.UI;

namespace Demo.WebAssembly
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
				ui.MenuItem(OpenIconicIcons.Globe, "i18n", "/i18n");
				ui.MenuItem(OpenIconicIcons.Paperclip, "Patterns", "/patterns");
				ui.MenuItem(OpenIconicIcons.CreditCard, "Payments", "/payments");

				ui.EndMenu();
			}

			ui.BeginMain();
			{
				ui.BeginTopRow();
				{
					if (ui.User.Identity.IsAuthenticated)
					{
						ui.Text($"Hello, {ui.User.Identity.Name}");
					}
					else
					{
						ui.Link("/signin", "Sign in");
					}

					ui.InlineIcon(OpenIconicIcons.Fork);
					ui.Link("https://github.com/blowdart-ui/blowdart-ui", "About");

					ui.EndTopRow();
				}

				ui.BeginMainContent();
				ui.LayoutBody();
				ui.EndMainContent();

				ui.EndMain();
			}
		}
	}
}
