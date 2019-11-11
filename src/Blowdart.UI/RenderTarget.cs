// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TypeKitchen;

namespace Blowdart.UI
{
    public abstract class RenderTarget : IDisposable
    {
		protected RenderTarget()
		{
			Renderers = new Dictionary<Type, IRenderer>();
			_rendererInstances = new Dictionary<Type, IRenderer>();
		}

        internal abstract void AddInstructions(List<RenderInstruction> instructions);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Render<T>(T renderer);

        public void RegisterRenderers<TRenderer>(params object[] dependencies)
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
		        if (!interfaces.Contains(typeof(IRenderer)))
			        continue;

		        if (typeInfo.IsInterface || typeInfo.IsAbstract)
			        continue;

		        Trace.TraceInformation($"Found renderer type {type.Name}");

		        foreach (var @interface in interfaces)
		        {
			        if (!typeof(IRenderer<,>).IsAssignableFromGeneric(@interface) || !@interface.IsGenericType)
				        continue;

			        var instructionType = @interface.GetGenericArguments()[0];
			        var genericMethod = method.MakeGenericMethod(instructionType, type, typeof(TRenderer));
			        genericMethod.Invoke(this, parameters);
		        }
	        }
        }

		public void RenderInstruction<TRenderer>(TRenderer renderer, RenderInstruction instruction)
        {
	        var key = instruction.GetType();
	        if (key.IsGenericType)
		        key = key.BaseType ?? throw new NullReferenceException();
	        if (!Renderers.TryGetValue(key, out var instance))
		        throw new ArgumentException($"No renderer found for {key.Name}");

	        var methodType = typeof(IRenderer<,>).MakeGenericType(key, typeof(TRenderer));
	        var method = methodType.GetMethod(nameof(IRenderer<RenderInstruction, TRenderer>.Render)) ?? throw new NullReferenceException();
	        method.Invoke(instance, new object[] { renderer, instruction });
        }

		public abstract void Begin();

        protected readonly Dictionary<Type, IRenderer> Renderers;
        private readonly Dictionary<Type, IRenderer> _rendererInstances;

		public bool TryAddRenderer<TInstruction, TInstructionRenderer, TRenderer>(params object[] dependencies) 
	        where TInstruction : RenderInstruction
	        where TInstructionRenderer : IRenderer<TInstruction, TRenderer>
		{
			var instructionType = typeof(TInstruction);
			var dependentTypes = dependencies.Select(x => x.GetType()).ToArray();

			if (Renderers.ContainsKey(instructionType))
		        return false;

	        var rendererType = typeof(TInstructionRenderer);
	        var ctor = rendererType.GetConstructor(Type.EmptyTypes);
	        if (ctor != null)
	        {
				return RegisterRendererInstance();
			}
			
	        ctor = rendererType.GetConstructor(dependentTypes);
	        if (ctor != null)
	        {
				return RegisterRendererInstance(dependencies);
			}

	        return false;

	        bool RegisterRendererInstance(params object[] args)
	        {
		        if (!_rendererInstances.TryGetValue(rendererType, out var renderer))
			        _rendererInstances.Add(rendererType, renderer = (TInstructionRenderer) Activator.CreateInstance(rendererType, args));
		        Renderers.Add(instructionType, renderer);
		        return true;
	        }
		}
	}
}
