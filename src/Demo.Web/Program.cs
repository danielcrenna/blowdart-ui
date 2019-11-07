// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web;
using Demo.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args) => UiServer.Start(args, builder =>
        {
            builder.AddSingleton<WeatherForecastService>();

            builder.AddPage("/", HelloWorld.Index);
            builder.AddPage("/counter", HelloWorld.Counter);
            builder.AddPage("/fetchdata", HelloWorld.FetchData);
            builder.AddPage("/elements", HelloWorld.Elements);
			builder.AddPage("/editor", HelloWorld.Editor);
            builder.AddPage("/styles", HelloWorld.Styles);
		});
    }
}
