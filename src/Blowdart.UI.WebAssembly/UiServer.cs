// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Blowdart.UI.WebAssembly.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.WebAssembly
{
	public class UiServer
    {
        // public static void Main(string[] args) { /* just here to support Web SDK */ }

        public static async Task StartAsync(string[] args, Action<BlowdartBuilder> configureAction)
        {
            // https://github.com/aspnet/AspNetCore/issues/11921
            var appName = Assembly.GetCallingAssembly().GetName().Name;

            await CreateHostBuilder(args, configureAction, appName).Build().RunAsync();
        }

        public static WebAssemblyHostBuilder CreateHostBuilder(string[] args, Action<BlowdartBuilder> configureAction, string appName)
        {
	        var webBuilder = WebAssemblyHostBuilder.CreateDefault(args);
	        webBuilder.RootComponents.Add<ImGui>("app");
	        webBuilder.Services.AddBaseAddressHttpClient();
	        
	        var configRoot = webBuilder.Configuration.Build();
	        var config = configRoot.GetSection("Blowdart");

	        webBuilder.Services.Configure<BlowdartOptions>(config.Bind);
	        webBuilder.Services.AddScoped(r => new HttpClient
	        {
		        BaseAddress = new Uri(r.GetRequiredService<NavigationManager>().BaseUri)
	        });
				
	        webBuilder.Services.AddBlowdart(configureAction);
	        return webBuilder;
        }
    }
}
