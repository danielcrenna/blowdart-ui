// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.WebAssembly
{
    public static class Add
    {
        public static IServiceCollection AddBlowdart(this IServiceCollection services, Action<BlowdartBuilder> configureAction)
        {
	        var pages = new PageMap();

			services.AddSingleton(pages);
			services.AddSingleton<ILocalizationProvider, LocalizationProvider>();
			services.AddSingleton<ILocalizationStore, MemoryLocalizationStore>();
			services.AddSingleton<IInputTransformer, InputTransformer>();
            
            var builder = new BlowdartBuilder(pages, services);
            services.AddSingleton(builder);
            configureAction.Invoke(builder);
			
            return services;
        }
    }
}
