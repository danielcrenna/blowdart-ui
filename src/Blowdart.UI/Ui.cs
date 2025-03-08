// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI;

public partial class Ui(RenderTarget target)
{
	private readonly List<RenderInstruction> _instructions = [];
	private int _count;

	internal UInt128 nextIdHash;

	public IServiceProvider UiServices { get; set; } = null!;

	public int InstructionCount => _instructions.Count;

	public void Begin()
	{
		nextIdHash = default;

		_count = default;
		_instructions.Clear();

		target.Begin();

		CalledLayout = default;
	}

	public void RenderToTarget<TRenderer>(TRenderer? renderer)
	{
		target.AddInstructions(_instructions);
		target.Render(renderer);
	}

	internal void Add(RenderInstruction instruction)
	{
		_instructions.Add(instruction);
	}

	public UInt128 NextId(string? id = null, [CallerMemberName] string? callerMemberName = null) => Hashing.MurmurHash3(id ?? $"{callerMemberName}{_count++}", nextIdHash) ^ nextIdHash;

	public UInt128 NextId(StringBuilder id) => Ids.NextId(ref nextIdHash, id);

	public UInt128 NextId(int i) => Hashing.MurmurHash3((ulong) i, nextIdHash) ^ nextIdHash;

	public void Repeat<T>(IEnumerable<T> items, Action<Ui, T> action)
	{
		foreach (var item in items)
			action(this, item);
	}

	public void Dispose()
	{
		_instructions.Clear();
		_handlers.Clear();
		_instances.Clear();
		_styles.Clear();
		target.Dispose();
	}

	#region Layouts

	private string? _body;

	public bool CalledLayout { get; private set; }

	public void Body()
	{
		CalledLayout = true;
		Invoke(_body ?? throw new UiException("Missing layout body"));
	}

	public void SetLayoutBody(string? body)
	{
		_body = body;
	}

	private readonly Dictionary<string, MethodInfo> _handlers = new();
	private readonly Dictionary<string, object?> _instances = [];

	public void Invoke(string? handler)
	{
		if (handler == null)
			return;

		if (!_handlers.TryGetValue(handler, out var accessor))
		{
			var tokens = handler.Split('.');
			if (tokens.Length > 1)
			{
				var typeString = tokens[0];
				var methodString = tokens[1];

				var resolver = UiServices.GetRequiredService<ITypeResolver>();
				var type = resolver.FindFirstByName(typeString);

				if (type != null)
				{
					var instance = Activator.CreateInstance(type); // , UiServices);
					_instances[handler] = instance;
				}

				var method = type?.GetMethod(methodString); // use resolver cache
				if (method != null)
				{
					_handlers[handler] = method;
				}

				accessor = method;
			}
		}

		{
			var instance = _instances[handler];
			_ = accessor?.Invoke(instance, [this]); // , [UiServices]);
		}
	}

	#endregion

	#region Data Loading

	private readonly List<IPromise> _dataLoaders = [];

	private interface IPromise
	{
		Task LoadAsync(IServiceProvider serviceProvider);
	}

	private readonly struct Promise<T>(Func<IServiceProvider, Task<T?>> getData, Action<T?> setData) : IPromise
	{
		public async Task LoadAsync(IServiceProvider serviceProvider)
		{
			var data = await getData(serviceProvider);
			setData(data);
		}
	}

	public void DataLoader<TResult>(Func<IServiceProvider, Task<TResult?>> getData, Action<TResult?> setData)
	{
		_dataLoaders.Add(new Promise<TResult>(getData, setData));
	}

	public void DataLoader<TService, TResult>(Func<TService, Task<TResult?>> getData, Action<TResult?> setData) 
		where TService : notnull
	{
		_dataLoaders.Add(new Promise<TResult>(Fetch, setData));
		return;

		async Task<TResult?> Fetch(IServiceProvider r)
		{
			return await getData(r.GetRequiredService<TService>());
		}
	}

	public void DataLoader<TResult>(Func<HttpClient, Task<TResult?>> getData, Action<TResult?> setData)
	{
		_dataLoaders.Add(new Promise<TResult>(Fetch, setData));
		return;

		async Task<TResult?> Fetch(IServiceProvider r)
		{
			return await getData(r.GetRequiredService<HttpClient>());
		}
	}

	public void DataLoader<TResult>(string requestUri, Action<TResult?> setData)
	{
		DataLoader(http => http.GetFromJsonAsync<TResult>(requestUri), setData);
	}

	public async Task<bool> DispatchDataLoaders()
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

	private readonly Stack<Action<StyleContext>> _styles = [];

	public bool TryPopStyle(out Action<StyleContext>? style)
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

	private readonly Stack<(string name, object value)> _attributes = [];

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
}