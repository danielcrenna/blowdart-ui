// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Blowdart.UI.Web.Configuration;
using Blowdart.UI.Web.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blowdart.UI.Web
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public sealed partial class ImGui : ComponentBase, IDisposable
	{
		public Ui Ui { get; }

        [Parameter] public Action<Ui> Layout { get; set; }
		[Parameter] public Action<Ui> Handler { get; set; }
		[Parameter] public object Model { get; set; }
		[Parameter] public EventCallback OnModelChanged { get; set; }

		[Inject] public IOptionsMonitor<BlowdartOptions> Options { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }

		public ImGui()
		{
			var target = new WebRenderTarget();
			target.RegisterRenderers(this);
			Ui = new Ui(target);
		}
		
		protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Begin();
            if (Layout != null)
            {
	            Ui.SetLayoutBody(Handler);
	            Layout(Ui);
	            if (!Ui.CalledLayout)
		            throw new BlowdartException("Layout did not call ui.LayoutBody();");
            }
            else
            {
	            Handler(Ui);
            }
            Ui.RenderToTarget(builder);
        }
        
        private void Begin()
        {
            Sequence.Begin(0, this);
            Ui.Begin(Model);
        }

        public void Dispose()
        {
            Ui.Dispose();
        }

        [Inject] private IServiceProvider ServiceProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Ui.UiServices = ServiceProvider;
            await base.OnInitializedAsync();
        }

        [Inject] internal IJSRuntime Js { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
	        await Js.InvokeVoidAsync(Interop.OnReady);

			RunInterop();

			if (await Ui.DispatchDataLoaders())
            {
                Begin();
                Handler(Ui);
                if(firstRender)
                    StateHasChanged();
            }

			if(Ui.PendingRefresh)
			{
				await OnModelChanged.InvokeAsync(Model);
				Ui.PendingRefresh = false;
			}
        }
	}
}
