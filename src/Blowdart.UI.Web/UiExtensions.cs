// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blowdart.UI.Web
{
	public static class UiExtensions
	{
		public static BlowdartOptions Options(this Ui ui)
		{
			return ui.GetRequiredService<IOptionsMonitor<BlowdartOptions>>().CurrentValue;
		}
	}
}