// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI;

internal static class TypeExtensions
{
	public static bool IsAssignableFromGeneric(this Type type, Type c)
	{
		if (!type.IsGenericType)
			return false;

		var interfaceTypes = c.GetInterfaces();

		foreach (var it in interfaceTypes)
			if (it.IsGenericType && it.GetGenericTypeDefinition() == type)
				return true;

		if (c.IsGenericType && c.GetGenericTypeDefinition() == type)
			return true;

		var baseType = c.BaseType;
		return baseType != null && IsAssignableFromGeneric(baseType, type);
	}
}