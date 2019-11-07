// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web;
using Demo.Examples;
using Demo.Examples.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Web
{
    public class Program
    {
        public static void Main(string[] args) => UiServer.Start(args, builder =>
        {
            builder.AddSingleton<WeatherForecastService>();

            builder.AddPage("/", WebLayout.Index, HelloWorld.Index);
            builder.AddPage("/counter", WebLayout.Index, HelloWorld.Counter);
            builder.AddPage("/fetchdata", WebLayout.Index, HelloWorld.FetchData);
            builder.AddPage("/elements", WebLayout.Index, HelloWorld.Elements);
			builder.AddPage("/editor", WebLayout.Index, HelloWorld.Editor);
            builder.AddPage("/styles", WebLayout.Index, HelloWorld.Styles);
		});
    }
}
