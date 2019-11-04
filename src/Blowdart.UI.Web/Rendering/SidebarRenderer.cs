using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class SidebarRenderer : IWebRenderer<SidebarInstruction>
    {
        private readonly ImGui _imGui;
        private static bool _collapseNavMenu;

        private static string NavMenuCssClass(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;

        public SidebarRenderer(ImGui imGui)
        {
            _imGui = imGui;
        }

        public void Render(RenderTreeBuilder b, SidebarInstruction sidebar)
        {
            b.Div("sidebar", () =>
            {
                b.Div("top-row pl-4 navbar navbar-dark", () =>
                {
                    b.Anchor("navbar-brand", "", () => b.AddContent(sidebar.Title));
                    if (b.Button(_imGui, "navbar-toggler", () => b.Span("navbar-toggler-icon")))
                    {
                        _collapseNavMenu = !_collapseNavMenu;
                    }
                });

                b.Div(NavMenuCssClass(_collapseNavMenu), () =>
                {
                    b.UnorderedList("nav flex-column", () =>
                    {
                        foreach (var page in sidebar.Pages)
                        {
                            b.ListItem("nav-item px-3", () =>
                            {
                                b.Anchor("nav-link", page.Template, () =>
                                {
                                    b.InlineIcon(page.Icon);
                                    b.AddContent(page.Title);
                                });
                            });
                        }
                    });
                });
            });
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as SidebarInstruction);
        }
    }
}
