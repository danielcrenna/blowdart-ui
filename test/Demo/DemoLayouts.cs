using Blowdart.UI;

namespace Demo;

public sealed class DemoLayouts
{
	public bool collapseNavMenu = true;

	public void MainLayout(Ui ui)
	{
		ui.BeginElement("div");
		ui.Attribute("class", "page");
		{
			ui.BeginElement("div");
			ui.Attribute("class", "sidebar");
			{
				ui.NavBar(collapseNavMenu);
			}
			ui.EndElement("div");

			ui.BeginElement("main");
			{
				ui.BeginElement("div");
				ui.Attribute("class", "top-row px-4");
				{
					ui.BeginElement("a");
					ui.Attribute("href", "https://learn.microsoft.com/aspnet/core/");
					ui.Attribute("target", "_blank");
					{
						ui._("About");
					}
					ui.EndElement("a");
				}
				ui.EndElement("div");

				ui.BeginElement("article");
				ui.Attribute("class", "content px-4");
				{
					 ui.Body();
				}
				ui.EndElement("article");
			}
			ui.EndElement("main");
		}
		ui.EndElement("div");
	}
}