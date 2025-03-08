// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI.Blazor;

public sealed partial class ImGui : ComponentBase, IDisposable
{
	[Inject] private IServiceProvider? ServiceProvider { get; set; }
	[Inject] private ILogger<ImGui> Logger { get; set; } = null!;

	public ImGui()
	{
		var target = new WebRenderTarget();
		target.RegisterRenderers(this);
		Ui = new Ui(target);
	}

	internal Ui Ui { get; }

	[Parameter] public string? Layout { get; set; }
	[Parameter] public string? Handler { get; set; }

	public void Dispose() => Ui.Dispose();

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		Begin();
		if (Layout != null)
		{
			Ui.SetLayoutBody(Handler);
			Ui.Invoke(Layout);
			if (!Ui.CalledLayout)
				throw new UiException("Layout did not call ui.Body();");
		}
		else
		{
			Ui.Invoke(Handler);
		}

		Ui.RenderToTarget(builder);
	}

	private void Begin()
	{
		Sequence.Begin(0, this);
		Ui.Begin();
	}

    protected override async Task OnInitializedAsync()
	{
		Ui.UiServices = new VirtualResolver(ServiceProvider, Ui, Logger);
		await base.OnInitializedAsync();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (await Ui.DispatchDataLoaders())
		{
			Begin();
			Ui.Invoke(Handler);
			if (firstRender)
				StateHasChanged();
		}
	}

	private void OnEvent<TEvent>(UInt128 id, string eventType, TEvent? data)
	{
		var instructionCount = Ui.InstructionCount;

		Trace.TraceInformation($"Event: {eventType} on element {id}");
		Ui.AddEvent(eventType, id, data);
		Begin();

		if (Handler != null)
			Ui.Invoke(Handler);

		if (Ui.InstructionCount != instructionCount)
			Trace.TraceInformation("Pending update");
	}

	private class VirtualResolver(IServiceProvider? inner, Ui ui, ILogger logger) : IServiceProvider
	{
		public object? GetService(Type serviceType)
		{
			var service = serviceType == typeof(Ui) ? ui : serviceType == typeof(ILogger<ImGui>) ? logger : inner?.GetService(serviceType);
			if(service == null)
				logger.LogDebug("Unable to find service with type {Type}", serviceType);

			return service;
		}
	}
}