// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using TypeKitchen;

namespace Demo.Examples.Models
{
    [MetadataType("Default", typeof(WeatherForecastFormMetadata))]
    public class WeatherForecast
    {
        public DateTime? Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public bool Verified { get; set; }
    }
}
