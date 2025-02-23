// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blowdart.UI.Blazor;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI;

public sealed class ReflectionTypeResolver : ITypeResolver
{
	private readonly Lazy<IEnumerable<MethodInfo>> _loadedMethods;
	private readonly Lazy<IEnumerable<Type>> _loadedTypes;
	private readonly string[] _skipRuntimeAssemblies;

	// PERF: Determine why IServiceProvider would resolve this on every page load, when this type is declared as a singleton
	public ReflectionTypeResolver(ILogger<ImGui> logger, IEnumerable<string>? skipRuntimeAssemblies = null)
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		_loadedTypes = new Lazy<IEnumerable<Type>>(() => LoadTypes(assemblies, logger, typeof(object).GetTypeInfo().Assembly));
		_loadedMethods = new Lazy<IEnumerable<MethodInfo>>(LoadMethods);

		_skipRuntimeAssemblies =
		[
			"Microsoft.VisualStudio.ArchitectureTools.PEReader",
			"Microsoft.IntelliTrace.Core, Version=16.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
		];
		if (skipRuntimeAssemblies != null)
			_skipRuntimeAssemblies = _skipRuntimeAssemblies.Concat(skipRuntimeAssemblies).ToArray();
	}

	public Type? FindFirstByName(string name)
	{
		foreach (var type in _loadedTypes.Value)
			if (type.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				return type;

		return null;
	}

	private IEnumerable<MethodInfo> LoadMethods()
	{
		foreach (var type in _loadedTypes.Value)
		foreach (var method in type.GetMethods())
			yield return method;
	}

	private IEnumerable<Type> LoadTypes(IEnumerable<Assembly> assemblies, ILogger logger,
		params Assembly[] skipAssemblies)
	{
		var types = new HashSet<Type>();

		foreach (var assembly in assemblies)
		{
			if (assembly.IsDynamic || ((IList) skipAssemblies).Contains(assembly) ||
			    ((IList) _skipRuntimeAssemblies).Contains(assembly.FullName))
				continue;

			try
			{
				foreach (var type in assembly.GetTypes())
					types.Add(type);
			}
			catch (Exception e)
			{
				logger?.LogError(new EventId(500), e, "Failed to load types in assembly {AssemblyName}",
					assembly.GetName().Name);
			}
		}

		return types;
	}
}