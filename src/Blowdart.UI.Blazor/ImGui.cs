// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using TypeKitchen;
using TypeKitchen.Creation;

namespace Blowdart.UI.Blazor
{
	public sealed class ImGui : ComponentBase, IDisposable
	{
		public ImGui()
		{
			var target = new WebRenderTarget();
			target.RegisterRenderers(this);
			Ui = new Ui(target);
		}

		internal Ui Ui { get; }

		[Parameter] public string Handler { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			Begin();
			InvokeHandler();
			Ui.RenderToTarget(builder);
		}
		
		private void Begin()
		{
			Sequence.Begin(0, this);
			Ui.Begin();
		}

		#region Service Location

		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		[Inject] private IServiceProvider ServiceProvider { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Ui.UiServices = new VirtualResolver(ServiceProvider, Ui);
			await base.OnInitializedAsync();
		}

		#endregion

		#region Events

		public EventCallback<MouseEventArgs> OnClick(Value128 id)
		{
			return EventCallback.Factory.Create<MouseEventArgs>(this, args =>
			{
				OnEvent(id, "onclick", null);
			});
		}

		private void OnEvent(Value128 id, string eventType, object data)
		{
			var instructionCount = Ui.InstructionCount;

			Trace.TraceInformation($"Event: {eventType} on element {id}");
			Ui.AddEvent(eventType, id, data);
			Begin();
			InvokeHandler();

			if (Ui.InstructionCount != instructionCount)
			{
				Trace.TraceInformation("Pending update");
			}
		}

		#endregion

		private static readonly ITypeResolver TypeResolver = new ReflectionTypeResolver();
		
		private IMethodCallAccessor _accessor;
		private object _instance;

		private void InvokeHandler()
		{
			if (_accessor == default)
			{
				var tokens = Handler?.Split('.');
				var typeString = tokens?[0];
				var methodString = tokens?[1];

				var type = TypeResolver.FindFirstByName(typeString);
				var method = type.GetMethod(methodString);

				_instance = Instancing.CreateInstance(type, Ui.UiServices);
				_accessor = CallAccessor.Create(method);
			}

			_accessor.Call(_instance, Ui.UiServices);
		}

		#region Virtual Resolver

		private class VirtualResolver : IServiceProvider
		{
			private readonly IServiceProvider _inner;
			private readonly Ui _ui;

			public VirtualResolver(IServiceProvider inner, Ui ui)
			{
				_inner = inner;
				_ui = ui;
			}

			public object GetService(Type serviceType) => serviceType == typeof(Ui) ? _ui : _inner.GetService(serviceType);
		}

		#endregion

		public void Dispose()
		{
			Ui?.Dispose();

			if(_instance is IDisposable disposable)
				disposable.Dispose();
		}
	}
}