using Blowdart.UI;

namespace Demo;

public static class DemoComponents
{
	public static void NavBar(this Ui ui, bool collapse)
	{
		ui.BeginElement("div");
		ui.Attribute("class", "top-row ps-3 navbar navbar-dark");
		{
			ui.BeginElement("div");
			ui.Attribute("class", "container-fluid");
			{
				ui.BeginElement("a");
				ui.Attribute("class", "navbar-brand");
				ui.Attribute("href", "");
				{
					ui._("BlazorApp1");
					ui.EndElement("a");
				}

				ui.BeginElement("button");
				ui.Attribute("title", "Navigation menu");
				ui.Attribute("class", "navbar-toggler");
				{
					ui.BeginElement("span");
					ui.Attribute("class", "navbar-toggler-icon");
					ui.EndElement("span");
				}
				ui.EndElement("button");
			}
			ui.EndElement("div");
		}
		ui.EndElement("div");

		ui.BeginElement("div");
		ui.Attribute("class", collapse ? "collapse" : "nav-scrollable");
		{
			ui.BeginElement("nav");
			ui.Attribute("class", "nav flex-column");
			{
				AddNavLink(ui, "", "bi bi-house-door-fill-nav-menu", " Home");
				AddNavLink(ui, "counter", "bi bi-plus-square-fill-nav-menu", " Counter");
				AddNavLink(ui, "weather", "bi bi-list-nested-nav-menu", " Weather");
			}
			ui.EndElement("nav");
		}
		ui.EndElement("div");
	}

	private static void AddNavLink(Ui ui, string href, string iconClass, string text)
	{
		ui.BeginElement("div");
		ui.Attribute("class", "nav-item px-3");
		{
			ui.BeginElement("a");
			ui.Attribute("class", "nav-link");
			ui.Attribute("href", href);
			{
				ui.BeginElement("span");
				ui.Attribute("class", iconClass);
				ui.Attribute("aria-hidden", true);
				ui.EndElement("span");

				ui._(text);
				ui.EndElement("a");
			}
		}
		ui.EndElement("div");
	}
}