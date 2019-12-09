// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace Blowdart.UI.Gaming
{
	public class GameServiceCollection : IServiceCollection
	{
		private readonly List<ServiceDescriptor> descriptors;
		private readonly Dictionary<Type, object> services;

		public GameServiceCollection(GameServiceContainer serviceProvider)
		{
			descriptors = new List<ServiceDescriptor>();
			services = (Dictionary<Type, object>) typeof(GameServiceContainer).GetField("services", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(serviceProvider);
			if (services == null)
				return;
			foreach (var service in services)
			{
				descriptors.Add(ServiceDescriptor.Singleton(service.Key, service.Value));
			}
		}
		
		public IEnumerator<ServiceDescriptor> GetEnumerator()
		{
			return descriptors.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) descriptors).GetEnumerator();
		}

		public void Add(ServiceDescriptor item)
		{
			descriptors.Add(item);
			if (item != null)
				services.Add(item.ServiceType, item.ImplementationInstance);
		}

		public void Clear()
		{
			descriptors.Clear();
		}

		public bool Contains(ServiceDescriptor item)
		{
			return descriptors.Contains(item);
		}

		public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
		{
			descriptors.CopyTo(array, arrayIndex);
		}

		public bool Remove(ServiceDescriptor item)
		{
			return descriptors.Remove(item);
		}

		public int Count => descriptors.Count;

		public bool IsReadOnly => false;

		public int IndexOf(ServiceDescriptor item)
		{
			return descriptors.IndexOf(item);
		}

		public void Insert(int index, ServiceDescriptor item)
		{
			descriptors.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			descriptors.RemoveAt(index);
		}

		public ServiceDescriptor this[int index]
		{
			get => descriptors[index];
			set => descriptors[index] = value;
		}
	}
}