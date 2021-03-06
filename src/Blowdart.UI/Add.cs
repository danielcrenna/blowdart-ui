﻿// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blowdart.UI
{
	public static class Add
	{
		public static void AddBlowdart(this IServiceCollection services, Action<PageMap> pageBuilder)
		{
			var pageMap = new PageMap();
			pageBuilder?.Invoke(pageMap);

			services.TryAddSingleton<ITypeResolver, ReflectionTypeResolver>();
			services.AddSingleton(pageMap);
		}
	}
}