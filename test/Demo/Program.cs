using Blowdart.UI;
using Demo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Services.AddLogging(logging => 
{
	logging.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddBlowdart(bd =>
{
	bd.AddPage("/", "DemoLayouts.MainLayout", "DemoPages.Index");
	bd.AddPage("/counter", "DemoLayouts.MainLayout", "DemoPages.Counter");
	bd.AddPage("/weather", "DemoLayouts.MainLayout", "DemoPages.Weather");
});

await builder.Build().RunAsync();
