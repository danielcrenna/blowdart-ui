// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blowdart.UI.Web.Components
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public sealed class ImGui : ComponentBase, IDisposable
	{
		private readonly WebRenderTarget _target;

        public Ui Ui { get; }

        [Parameter] public Action<Ui> Layout { get; set; }
		[Parameter] public Action<Ui> Handler { get; set; }
        
		[Inject] public IOptionsMonitor<BlowdartOptions> Options { get; set; }

		public ImGui()
		{
			_target = new WebRenderTarget();
			_target.RegisterRenderers<RenderTreeBuilder>(this);
			Ui = new Ui(_target);
		}

		protected override Task OnParametersSetAsync()
		{
			return base.OnParametersSetAsync();
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
	        OnEvent(id, DomEvents.OnClick, null);
        }

        public void OnChange(ChangeEventArgs args, Value128 id)
        {
	        OnEvent(id, DomEvents.OnChange, args.Value);
        }

        public void OnInput(ChangeEventArgs args, Value128 id)
        {
	        OnEvent(id, DomEvents.OnInput, args.Value);
        }

		private void OnEvent(Value128 id, string eventType, object data)
        {
	        var instructionCount = Ui.Instructions.Count;

	        Ui.AddEvent(eventType, id, data);
	        Begin();
	        Handler(Ui);

	        if (Ui.Instructions.Count != instructionCount)
	        {
		        OnInstructionsAdded();
	        }
		}

        public EventCallback<MouseEventArgs> OnClickCallback(Value128 id)
        {
            return EventCallback.Factory.Create<MouseEventArgs>(this, args =>
            {
                OnClick(args, id);
            });
        }

        public EventCallback<ChangeEventArgs> OnChangeCallback(Value128 id)
        {
	        return EventCallback.Factory.Create<ChangeEventArgs>(this, args =>
	        {
		        OnChange(args, id);
	        });
        }

        public EventCallback<ChangeEventArgs> OnInputCallback(Value128 id)
        {
	        return EventCallback.Factory.Create<ChangeEventArgs>(this, args =>
	        {
		        OnInput(args, id);
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
	        await Js.InvokeVoidAsync(Interop.OnReady);


	        foreach (var _ in Ui.Instructions.OfType<CodeInstruction>())
	        {
		        await Js.InvokeVoidAsync(Interop.SyntaxHighlight);
		        break;
	        }

	        foreach (var _ in Ui.Instructions.OfType<ShowModalInstruction>())
	        {
		        await Js.InvokeVoidAsync(Interop.ShowModal, _.Id);
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

        private void OnInstructionsAdded()
        {
	        LogToTargets();

	        foreach (var _ in Ui.Instructions.OfType<ShowModalInstruction>())
	        {
		        Js.InvokeVoidAsync(Interop.ShowModal, _.Id.ToString());
		        break;
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

        public void ShowModal(Value128 id)
        {
			Js.InvokeVoidAsync(Interop.ShowModal, id);
		}
	}
}
