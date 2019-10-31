// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blowdart.UI.Web
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    internal sealed class ImGui : ComponentBase, IDisposable
	{
		private WebRenderTarget _target;

        public Ui Ui { get; }

        [Parameter] public Action<Ui> Handler { get; set; }
		
		[Inject] public IOptionsMonitor<BlowdartOptions> Options { get; set; }

		public ImGui()
        {
	        Ui = new Ui();
        }

		protected override Task OnParametersSetAsync()
		{
			_target = new WebRenderTarget(this);
			return base.OnParametersSetAsync();
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
	        OnEvent(id, Events.OnClick);
        }

        private void OnEvent(Value128 id, string eventType)
        {
	        var instructionCount = Ui.Instructions.Count;

	        Ui.AddEvent(eventType, id);
	        Begin();
	        Handler(Ui);

			if (Ui.Instructions.Count != instructionCount)
				LogToTargets();
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

        [Inject] internal IJSRuntime Js { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
	        foreach (var _ in Ui.Instructions.OfType<CodeInstruction>())
	        {
		        await Js.InvokeVoidAsync(Interop.SyntaxHighlight);
		        break;
	        }

			LogToTargets();

			if (await Ui.DispatchDataLoaders())
            {
                Begin();
                Handler(Ui);
                if(firstRender)
                    StateHasChanged();
            }
        }

        private void LogToTargets()
        {
	        foreach (var log in Ui.Instructions.OfType<LogInstruction>())
	        {
		        LogInstruction(log);
		        break;
	        }
        }

        public void LogInstruction(LogInstruction log)
        {
	        foreach (var target in Options.CurrentValue.LogTargets)
	        {
				switch (target)
				{
					case LogTarget.Trace:
						Trace.WriteLine(log.Message);
						break;
					case LogTarget.Console:
						Console.WriteLine(log.Message);
						break;
					case LogTarget.Browser:
						Js.InvokeVoidAsync(Interop.Log, log.Message);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
        }
	}
}
