![logo](https://github.com/Blowdart-UI/blowdart-ui/blob/master/src/Blowdart.UI.Web/wwwroot/favicon-96x96.png "Logo")

## Blowdart UI

Blowdart UI lets you create user interfaces in pure C#.

It has the ambitious aim of running on multiple platforms, including native mobile applications.

![screenshot](https://github.com/Blowdart-UI/blowdart-ui/blob/master/docs/screenshot.png "Screenshot")

### Hello, World!

First, you need to grab the package from [NuGet](https://www.nuget.org/packages/Blowdart.UI)

```powershell
PM> Install-Package Blowdart.UI.Web -IncludePrerelease
```

Then, construct a UI in a `netcoreapp3.0` console application:

[Full Example (gist)](https://gist.github.com/danielcrenna/b6be9b32d18dbc4eb503f7bb8d241017)

```csharp
using Blowdart.UI.Web;

namespace Blowdart.UI.Demo
{
    public class Program
    {
        public static void Main(string[] args) => UiServer.Start(args, builder =>
        {
            builder.AddPage("/", ui =>
            {
                ui.Header(1, "Hello, world!");
                ui.Literal("Welcome to your new app.");
            });
        });
    }
}
```

### Roadmap :world_map:

- [X] Layouts
- [X] Elements
- [X] Lists
- [X] Tables
- [ ] Local / Remote Data API
- [ ] Forms
- [ ] Localization
- [ ] Accessibility
- [ ] Styles
- [ ] Test Harness
- [ ] Split Tests
- [ ] Pattern Languages
- [ ] Schema-Driven

### Planned Platforms :white_square_button:

- [x] HTML/CSS
- [ ] WebAssembly
- [ ] FNA
- [ ] iOS
- [ ] tvOS
- [ ] macOS
- [ ] Android
- [ ] WinForms / Modern.Forms
- [ ] CLI
