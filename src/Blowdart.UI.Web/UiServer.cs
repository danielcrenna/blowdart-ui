// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Blowdart.UI.Web
{
    public class UiServer
    {
        public static void Main(string[] args) { /* just here to support Web SDK */ }

        public static void Start(string[] args, Action<BlowdartBuilder> configureAction)
        {
            // https://github.com/aspnet/AspNetCore/issues/11921
            var appName = Assembly.GetCallingAssembly().GetName().Name;

            CreateHostBuilder(args, configureAction, appName).Build().Run();
        }

        public static void Start<TStartup>(string[] args, Action<BlowdartBuilder> configureAction) where TStartup : class
        {
            // https://github.com/aspnet/AspNetCore/issues/11921
            var appName = Assembly.GetCallingAssembly().GetName().Name;

            CreateHostBuilder<TStartup>(args, configureAction, appName).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Action<BlowdartBuilder> configureAction, string appName)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                BuildEmbeddedHost(configureAction, appName, webBuilder);
            });
        }

        public static IHostBuilder CreateHostBuilder<TStartup>(string[] args, Action<BlowdartBuilder> configureAction, string appName) where TStartup : class
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TStartup>();

                BuildEmbeddedHost(configureAction, appName, webBuilder);
            });
        }
        
        private static void BuildEmbeddedHost(Action<BlowdartBuilder> configureAction, string appName, IWebHostBuilder webBuilder)
        {
            webBuilder.ConfigureAppConfiguration((context, config) =>
            {
                if (appName != null)
                    context.HostingEnvironment.ApplicationName = appName;
            });

            webBuilder.ConfigureServices((context, services) =>
            {
                var config = context.Configuration.GetSection("Blowdart");
                services.Configure<BlowdartOptions>(config);
                
                services.AddSingleton<BlowdartService>();

				var options = new BlowdartOptions();
                config.Bind(options);

                switch (options.RunAt)
                {
                    case RunAt.Server:
                    {
                        services.AddScoped(r => new HttpClient
                        {
                            BaseAddress = new Uri(r.GetRequiredService<NavigationManager>().BaseUri)
                        });

                        break;
                    }
                    case RunAt.Client:{
                        services.AddResponseCompression(o =>
                        {
                            o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
                        });
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                services.AddBlowdart(configureAction);
            });

            webBuilder.UseStaticWebAssets();
            webBuilder.Configure(app =>
            {
                var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                var options = app.ApplicationServices.GetRequiredService<IOptions<BlowdartOptions>>();
                switch (options.Value.RunAt)
                {
                    case RunAt.Server:
                        break;
                    case RunAt.Client:
                        app.UseBlazorDebugging();
                        app.UseClientSideBlazorFiles<ClientStartup>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                app.UseBlowdart();
            });
        }
    }
}
