// Copyright (c) Daniel Crenna. All rights reserved.
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, you can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI
{
	public class ReflectionTypeResolver : ITypeResolver
	{
		private readonly Lazy<IEnumerable<MethodInfo>> _loadedMethods;
		private readonly Lazy<IEnumerable<Type>> _loadedTypes;
		private readonly string[] _skipRuntimeAssemblies;

		public ReflectionTypeResolver(IEnumerable<Assembly> assemblies, ILogger logger,
			IEnumerable<string> skipRuntimeAssemblies = null)
		{
			_loadedTypes =
				new Lazy<IEnumerable<Type>>(() => LoadTypes(assemblies, logger, typeof(object).GetTypeInfo().Assembly));
			_loadedMethods = new Lazy<IEnumerable<MethodInfo>>(LoadMethods);

			_skipRuntimeAssemblies = new[]
			{
				"Microsoft.VisualStudio.ArchitectureTools.PEReader",
				"Microsoft.IntelliTrace.Core, Version=16.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
			};
			if (skipRuntimeAssemblies != null)
				_skipRuntimeAssemblies = _skipRuntimeAssemblies.Concat(skipRuntimeAssemblies).ToArray();
		}

		public ReflectionTypeResolver() : this(AppDomain.CurrentDomain.GetAssemblies(), null) { }

		public Type FindFirstByName(string name)
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
}