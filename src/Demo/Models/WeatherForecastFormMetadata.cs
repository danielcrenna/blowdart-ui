// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    [DisplayName("Forecast")]
    public class WeatherForecastFormMetadata
    {
        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date", Prompt = "Enter the date of the forecast")]
        public DateTime? Date { get; set; }
		
        [Required]
        [Display(Name = "Temp. (C)", Prompt = "Enter temperature in celsius")]
        [DisplayName("Temp. (C)")]
        public int TemperatureC { get; set; }

        [Required]
        [DisplayName("Summary")]
        [Display(Name = "Summary", Prompt = "Enter a summary description")]
        public string Summary { get; set; }

        [DisplayName("Temp. (F)")]
        [Display(Name = "Temp. (F)")]
        public int TemperatureF { get; set; }

        public bool Verified { get; set; }
    }
}
