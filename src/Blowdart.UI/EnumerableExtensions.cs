// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Blowdart.UI;

public static class EnumerableExtensions
{
	public static List<T>? AsList<T>(this IEnumerable<T>? source) =>
		source is null or List<T> ? (List<T>?) source : source.ToList();
}