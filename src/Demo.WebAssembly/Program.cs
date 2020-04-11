using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.WebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBaseAddressHttpClient();
            await builder.Build().RunAsync();
        }

        //public static async Task Main(string[] args) => await UiServer.StartAsync(args, builder =>
        //{
	       // builder.AddSingleton<WeatherForecastService>();
	       // builder.AddScoped<ISignInService, WebSignInService>();

	       // builder.AddPage("/", WebLayout.Index, IndexPage.Index);
	       // builder.AddPage("/counter", WebLayout.Index, CounterPage.Index);
	       // builder.AddPage("/fetchdata", WebLayout.Index, FetchDataPage.Index);
	       // builder.AddPage("/elements", WebLayout.Index, ElementsPage.Index);
	       // builder.AddPage("/editor", WebLayout.Index, EditorPage.Index);
	       // builder.AddPage("/styles", WebLayout.Index, StylesPage.Index);
	       // builder.AddPage("/i18n", WebLayout.Index, LocalizationPage.Index);
	       // builder.AddPage("/patterns", WebLayout.Index, PatternsPage.Index);
	       // builder.AddPage("/payments", WebLayout.Index, PaymentsPage.Index);

	       // builder.AddPage("/signin", AuthPages.SignIn);
        //});
    }
}
