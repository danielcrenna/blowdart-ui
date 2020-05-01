// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
	public static class HttpClientServiceCollectionExtensions
	{
		public static IServiceCollection AddBaseAddressHttpClient(this IServiceCollection serviceCollection)
		{
			return serviceCollection.AddSingleton(s =>
			{
				// Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
				var navigationManager = s.GetRequiredService<NavigationManager>();
				return new HttpClient
				{
					BaseAddress = new Uri(navigationManager.BaseUri)
				};
			});
		}
	}
}