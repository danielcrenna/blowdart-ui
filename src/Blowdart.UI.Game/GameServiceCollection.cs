// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace Blowdart.UI.Game
{
	public class GameServiceCollection : IServiceCollection
	{
		private readonly List<ServiceDescriptor> _descriptors;
		private readonly Dictionary<Type, object> _services;

		public GameServiceCollection(GameServiceContainer serviceProvider)
		{
			_descriptors = new List<ServiceDescriptor>();
			_services = (Dictionary<Type, object>) typeof(GameServiceContainer).GetField("services", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(serviceProvider);
			if (_services == null)
				return;
			foreach (var service in _services)
			{
				_descriptors.Add(ServiceDescriptor.Singleton(service.Key, service.Value));
			}
		}
		
		public IEnumerator<ServiceDescriptor> GetEnumerator()
		{
			return _descriptors.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _descriptors).GetEnumerator();
		}

		public void Add(ServiceDescriptor item)
		{
			_descriptors.Add(item);
			if (item != null)
				_services.Add(item.ServiceType, item.ImplementationInstance);
		}

		public void Clear()
		{
			_descriptors.Clear();
		}

		public bool Contains(ServiceDescriptor item)
		{
			return _descriptors.Contains(item);
		}

		public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
		{
			_descriptors.CopyTo(array, arrayIndex);
		}

		public bool Remove(ServiceDescriptor item)
		{
			return _descriptors.Remove(item);
		}

		public int Count => _descriptors.Count;

		public bool IsReadOnly => false;

		public int IndexOf(ServiceDescriptor item)
		{
			return _descriptors.IndexOf(item);
		}

		public void Insert(int index, ServiceDescriptor item)
		{
			_descriptors.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			_descriptors.RemoveAt(index);
		}

		public ServiceDescriptor this[int index]
		{
			get => _descriptors[index];
			set => _descriptors[index] = value;
		}
	}
}