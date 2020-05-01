// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Blowdart.UI.Blazor
{
	public sealed class ImGui : ComponentBase
	{
		public ImGui()
		{
			var target = new WebRenderTarget();
			target.RegisterRenderers(this);
			Ui = new Ui(target);
		}

		public Ui Ui { get; }

		[Parameter] public Action<Ui> Handler { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			Begin();
			Handler(Ui);
			Ui.RenderToTarget(builder);
		}

		private void Begin()
		{
			Sequence.Begin(0, this);
			Ui.Begin();
		}

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
			Ui.AddEvent(eventType, id, data);
			Begin();
			Handler(Ui);
		}

		#endregion
	}
}