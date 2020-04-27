// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

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
			Sequence.Begin(0, this);
			Ui.Begin();
			Handler(Ui);
			Ui.RenderToTarget(builder);
		}
	}
}