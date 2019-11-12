// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using Blowdart.UI.Localization;
//using Blowdart.SplitTesting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.Web
{
    public static class Add
    {
        public static IServiceCollection AddBlowdart(this IServiceCollection services, Action<BlowdartBuilder> configureAction)
        {
	        //services.AddSplitTesting();

			var pages = new PageMap();

			services.AddSingleton(pages);
			services.AddSingleton<IUserResolver, WebUserResolver>();
			services.AddSingleton<ILocaleResolver, WebLocaleResolver>();
			services.AddSingleton<ILocalizationProvider, LocalizationProvider>();
			services.AddSingleton<ILocalizationStore, MemoryLocalizationStore>();
			services.AddSingleton<IThemeProvider, WebThemeProvider>();
            services.AddSingleton<IInputTransformer, InputTransformer>();
            
			services.AddRazorPages(o =>
            {
                o.RootDirectory = "/Pages";
            });

            var builder = new BlowdartBuilder(pages, services);
            services.AddSingleton(builder);
            configureAction.Invoke(builder);

            services.AddServerSideBlazor(o =>
            {
                o.DetailedErrors = Debugger.IsAttached;
            });

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            return services;
        }
    }
}
