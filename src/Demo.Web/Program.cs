// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web;
using Demo.Examples.Models;
using Demo.Examples.Pages;
using Demo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Web
{
    public class Program
    {
        public static void Main(string[] args) => UiServer.Start(args, builder =>
        {
            builder.AddSingleton<WeatherForecastService>();
            builder.AddScoped<ISignInService, WebSignInService>();
            builder.AddAuthentication(IdentityConstants.ApplicationScheme)
	            .AddCookie(IdentityConstants.ApplicationScheme);

            builder.AddPage("/", WebLayout.Index, IndexPage.Index);
            builder.AddPage("/counter", WebLayout.Index, CounterPage.Index);
            builder.AddPage("/fetchdata", WebLayout.Index, FetchDataPage.Index);
            builder.AddPage("/elements", WebLayout.Index, ElementsPage.Index);
			builder.AddPage("/editor", WebLayout.Index, EditorPage.Index);
            builder.AddPage("/styles", WebLayout.Index, StylesPage.Index);
            builder.AddPage("/i18n", WebLayout.Index, LocalizationPage.Index);
			builder.AddPage("/patterns", WebLayout.Index, PatternsPage.Index);

			builder.AddPage("/signin", AuthPages.SignIn);
		});
    }
}
