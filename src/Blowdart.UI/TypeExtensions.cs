// Copyright (c) Daniel Crenna. All rights reserved.
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, you can obtain one at http://mozilla.org/MPL/2.0/.

using System;

namespace Blowdart.UI
{
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
}