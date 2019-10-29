// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI
{
	public static class LayoutExtensions
	{
		public static List<List<T>> InGroupsOf<T>(this IList<T> collection, int size)
		{
			var batches = new List<List<T>>();
			var count = 0;
			var temp = new List<T>();

			foreach (var element in collection)
			{
				if (count++ == size)
				{
					batches.Add(temp);
					temp = new List<T>();
					count = 1;
				}
				temp.Add(element);
			}

			batches.Add(temp);
			return batches;
		}
	}
}
