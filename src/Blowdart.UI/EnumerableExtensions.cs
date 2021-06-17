// Copyright (c) Daniel Crenna. All rights reserved.
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, you can obtain one at http://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;

namespace Blowdart.UI
{
	public static class EnumerableExtensions
	{
		public static List<T>? AsList<T>(this IEnumerable<T>? source) =>
			(source == null || source is List<T>) ? (List<T>?) source : source.ToList();
	}
}