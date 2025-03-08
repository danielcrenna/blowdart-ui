// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Blowdart.UI.Extensions;

namespace Blowdart.UI;

public abstract class RenderTarget : IDisposable
{
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	internal abstract void AddInstructions(List<RenderInstruction> instructions);
	internal abstract void Render(object? renderer);
	internal abstract void Begin();

	protected virtual void Dispose(bool disposing)
	{
		if (disposing) { }
	}
}

public abstract class RenderTarget<TRenderer> : RenderTarget
{
	private readonly List<RenderFragment<TRenderer?>> _fragments = [];
	private readonly Dictionary<Type, IRenderer?> _rendererInstances = new();
	private readonly Dictionary<Type, IRenderer?> _renderers = new();

	internal override void Begin()
	{
		_fragments.Clear();
	}

	internal override void AddInstructions(List<RenderInstruction> instructions)
	{
		_fragments.Add(i =>
		{
			foreach (var instruction in instructions)
			{
				RenderInstruction(i, instruction);
			}
		});
	}

	internal override void Render(object? renderer)
	{
		Render((TRenderer?) renderer);
	}

	public void Render<T>(T? renderer) where T : TRenderer
	{
		foreach (var fragment in _fragments)
			fragment(renderer);
	}

	public void RegisterRenderers(params object[] dependencies)
	{
		var method = GetType().GetMethod(nameof(TryAddRenderer));
		if (method == null)
			throw new NullReferenceException();

		var parameters = new object[] {dependencies};
		var assembly = Assembly.GetCallingAssembly();
		var types = assembly.GetTypes();
		foreach (var type in types)
		{
			var typeInfo = type.GetTypeInfo();

			var interfaces = typeInfo.ImplementedInterfaces.AsList();
			if (interfaces != null && !interfaces.Contains(typeof(IRenderer)))
				continue;

			if (typeInfo.IsInterface || typeInfo.IsAbstract)
				continue;

			Trace.TraceInformation($"Found renderer type {type.Name}");

			foreach (var @interface in interfaces ?? [])
			{
				if (!typeof(IRenderer<,>).IsAssignableFromGeneric(@interface) || !@interface.IsGenericType)
					continue;

				var instructionType = @interface.GetGenericArguments()[0];
				var genericMethod = method.MakeGenericMethod(instructionType, type);
				genericMethod.Invoke(this, parameters);
			}
		}
	}

	public void RenderInstruction(TRenderer? renderer, RenderInstruction instruction)
	{
		var key = instruction.GetType();
		if (key.IsGenericType)
			key = key.BaseType ?? throw new NullReferenceException();

		if (!_renderers.TryGetValue(key, out var instance))
			throw new ArgumentException($"No renderer found for {key.Name}");

		var methodType = typeof(IRenderer<,>).MakeGenericType(key, typeof(TRenderer));
		var method = methodType.GetMethod(nameof(IRenderer<RenderInstruction, TRenderer>.Render)) ??
		             throw new NullReferenceException();

		method.Invoke(instance, [renderer, instruction]);
	}

	public bool TryAddRenderer<TInstruction, TInstructionRenderer>(params object[] dependencies)
		where TInstruction : RenderInstruction
		where TInstructionRenderer : IRenderer<TInstruction, TRenderer>
	{
		var instructionType = typeof(TInstruction);
		var dependentTypes = dependencies.Select(x => x.GetType()).ToArray();

		if (_renderers.ContainsKey(instructionType))
			return false;

		var rendererType = typeof(TInstructionRenderer);
		var ctor = rendererType.GetConstructor(Type.EmptyTypes);
		if (ctor != null)
		{
			return RegisterRendererInstance();
		}

		ctor = rendererType.GetConstructor(dependentTypes);
		return ctor != null && RegisterRendererInstance(dependencies);

		bool RegisterRendererInstance(params object[] args)
		{
			if (!_rendererInstances.TryGetValue(rendererType, out var renderer))
				_rendererInstances.Add(rendererType,
					renderer = (TInstructionRenderer?) Activator.CreateInstance(rendererType, args));
			_renderers.Add(instructionType, renderer);
			return true;
		}
	}
}