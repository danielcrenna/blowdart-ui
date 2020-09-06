// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TypeKitchen;
using TypeKitchen.Creation;

namespace Blowdart.UI
{
	public partial class Ui
	{
		private readonly List<RenderInstruction> _instructions;
		private readonly RenderTarget _target;
		
		public Ui(RenderTarget target)
		{
			_target = target;
			_instructions = new List<RenderInstruction>();
		}

		public IServiceProvider UiServices { get; internal set; }
		
		public void Begin()
		{
			nextIdHash = default;

			_count = default;
			_instructions?.Clear();
			_target?.Begin();

			CalledLayout = default;
		}

		public int InstructionCount => _instructions.Count;

		public void RenderToTarget<TRenderer>(TRenderer renderer)
		{
			_target.AddInstructions(_instructions);
			_target.Render(renderer);
		}

		internal void Add(RenderInstruction instruction)
		{
			_instructions.Add(instruction);
		}

		internal Value128 nextIdHash;
		private int _count;
		
		public Value128 NextId(string id = null, [CallerMemberName] string callerMemberName = null)
		{
			nextIdHash = Hashing.MurmurHash3(id ?? $"{callerMemberName}{_count++}", nextIdHash) ^ nextIdHash;
			return nextIdHash;
		}

		public Value128 NextId(StringBuilder id)
		{
			nextIdHash = Hashing.MurmurHash3(id, nextIdHash) ^ nextIdHash;
			return nextIdHash;
		}

		public Value128 NextId(int i)
		{
			nextIdHash = Hashing.MurmurHash3((ulong)i, nextIdHash) ^ nextIdHash;
			return nextIdHash;
		}
		
		#region Layouts

		private string _body;

		internal bool CalledLayout { get; private set; }

		public void Body()
		{
			CalledLayout = true;
			Invoke(_body ?? throw new UiException("Missing layout body"));
		}

		public void SetLayoutBody(string body)
		{
			_body = body;
		}

		private readonly Dictionary<string, IMethodCallAccessor> _handlers = new Dictionary<string, IMethodCallAccessor>();
		private readonly Dictionary<string, object> _instances = new Dictionary<string, object>();

		public void Invoke(string handler)
		{
			if (!_handlers.TryGetValue(handler, out var accessor))
			{
				var tokens = handler.Split('.');
				if (tokens.Length > 1)
				{
					var typeString = tokens[0];
					var methodString = tokens[1];

					var resolver = UiServices.GetRequiredService<ITypeResolver>();
					var type = resolver.FindFirstByName(typeString);
					var method = type.GetMethod(methodString);

					_instances[handler] = Instancing.CreateInstance(type, UiServices);
					_handlers[handler] = accessor = CallAccessor.Create(method);
				}
			}

			accessor?.Call(_instances[handler], UiServices);
		}

		#endregion

		#region Data Loading

		private readonly List<IPromise> _dataLoaders = new List<IPromise>();

		private interface IPromise
		{
			Task LoadAsync(IServiceProvider serviceProvider);
		}

		private struct Promise<T> : IPromise
		{
			private readonly Func<IServiceProvider, Task<T>> _getData;
			private readonly Action<T> _setData;

			public Promise(Func<IServiceProvider, Task<T>> getData, Action<T> setData)
			{
				_getData = getData;
				_setData = setData;
			}

			public async Task LoadAsync(IServiceProvider serviceProvider)
			{
				var data = await _getData(serviceProvider);
				_setData(data);
			}
		}

		public void DataLoader<TResult>(Func<IServiceProvider, Task<TResult>> getData, Action<TResult> setData)
		{
			_dataLoaders.Add(new Promise<TResult>(getData, setData));
		}

		public void DataLoader<TService, TResult>(Func<TService, Task<TResult>> getData, Action<TResult> setData)
		{
			async Task<TResult> Fetch(IServiceProvider r) => await getData(r.GetRequiredService<TService>());

			_dataLoaders.Add(new Promise<TResult>(Fetch, setData));
		}

		public void DataLoader<TResult>(Func<HttpClient, Task<TResult>> getData, Action<TResult> setData)
		{
			async Task<TResult> Fetch(IServiceProvider r) => await getData(r.GetRequiredService<HttpClient>());

			_dataLoaders.Add(new Promise<TResult>(Fetch, setData));
		}

		public void DataLoader<TResult>(string requestUri, Action<TResult> setData) => DataLoader(http => http.GetFromJsonAsync<TResult>(requestUri), setData);
		
		internal async Task<bool> DispatchDataLoaders()
		{
			if (_dataLoaders.Count == 0)
				return false;
			foreach (var dataLoader in _dataLoaders)
				await dataLoader.LoadAsync(UiServices);
			return true;
		}

		#endregion
		
		#region Stack Objects

		public void PushStyle(Action<StyleContext> styleBuilder)
		{
			_styles.Push(styleBuilder);
		}

		private readonly Stack<Action<StyleContext>> _styles = new Stack<Action<StyleContext>>();
		internal bool TryPopStyle(out Action<StyleContext> style)
		{
			if (_styles.Count == 0)
			{
				style = default;
				return false;
			}

			style = _styles.Pop();
			return true;
		}

		public void PushAttribute(string key, object value)
		{
			_attributes.Push((key, value));
		}

		private readonly Stack<(string name, object value)> _attributes = new Stack<(string name, object value)>();
		internal bool TryPopAttribute(out (string name, object value) attribute)
		{
			if (_attributes.Count == 0)
			{
				attribute = default;
				return false;
			}

			attribute = _attributes.Pop();
			return true;
		}

		#endregion

		#region Helpers

		public void Repeat<T>(IEnumerable<T> items, Action<Ui, T> action)
		{
			foreach (var item in items)
				action(this, item);
		}

		#endregion

		public void Dispose()
		{
			_instructions?.Clear();
			_handlers?.Clear();
			_instances?.Clear();
			_styles?.Clear();
			_target?.Dispose();
		}
	}
}