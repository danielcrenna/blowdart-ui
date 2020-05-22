using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blowdart.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

			builder.Services.AddSingleton(r => new HttpClient { BaseAddress = new Uri(r.GetRequiredService<NavigationManager>().BaseUri)});
			builder.Services.AddBlowdart(bd =>
			{
				bd.AddPage("/", "DemoLayouts.MainLayout", "DemoPages.Index");
				bd.AddPage("/counter", "DemoLayouts.MainLayout", "DemoPages.Counter");
				bd.AddPage("/fetchdata", "DemoLayouts.MainLayout", "DemoPages.FetchData");
			});

			await builder.Build().RunAsync();
        }
    }
}
