// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Blowdart.UI.Web
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    internal sealed class ImGui : ComponentBase, IDisposable
	{
        private readonly WebRenderTarget _target;

        public Ui Ui { get; }

        [Parameter] public Action<Ui> Handler { get; set; }
        
        public ImGui()
        {
            _target = new WebRenderTarget(this);
            Ui = new Ui();
        }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Begin();
            Handler(Ui);
            Ui.RenderToTarget(_target, builder);
        }
        
        private void Begin()
        {
            Sequence.Begin(0, this);
            Ui.Begin();
            _target.Begin();
        }

        public void Dispose()
        {
            _target.Dispose();
            Ui.Dispose();
        }
        
        public void OnClick(MouseEventArgs args, Value128 id)
        {
            Ui.AddEvent("onclick", id);
            Begin();
            Handler(Ui);
        }

        public EventCallback<MouseEventArgs> OnClickCallback(Value128 id)
        {
            return EventCallback.Factory.Create<MouseEventArgs>(this, args =>
            {
                OnClick(args, id);
            });
        }

        [Inject] private IServiceProvider ServiceProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Ui.UiServices = ServiceProvider;
            await base.OnInitializedAsync();
        }

        [Inject] private IJSRuntime Js { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
	        foreach (var _ in Ui.Instructions.OfType<CodeInstruction>())
	        {
		        await Js.InvokeVoidAsync("blowdart.highlight");
		        break;
	        }

            if(await Ui.DispatchDataLoaders())
            {
                Begin();
                Handler(Ui);
                if(firstRender)
                    StateHasChanged();
            }
        }
    }
}
