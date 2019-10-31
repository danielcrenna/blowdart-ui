// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Blowdart.UI.Web
{
	public class BlowdartService
	{
		private readonly IOptionsMonitor<BlowdartOptions> _options;

		public BlowdartService(IOptionsMonitor<BlowdartOptions> options)
		{
			_options = options;
		}

		public Task<string> RunAtAsync(RenderMode renderMode)
		{
			switch(_options.CurrentValue.RunAt)
			{
				case RunAt.Server:
					return Task.FromResult("<script src=\"_framework/blazor.server.js\"></script>");
				case RunAt.Client:
					return Task.FromResult("<script src=\"_framework/blazor.webassembly.js\"></script>");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
