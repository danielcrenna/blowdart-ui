// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.Views;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class CollapsibleRenderer : 
		IRenderer<BeginCollapsibleInstruction, ViewGroup>,
		IRenderer<EndCollapsibleInstruction, ViewGroup>
	{
		public void Render(ViewGroup renderer, BeginCollapsibleInstruction instruction) { }
		public void Render(ViewGroup renderer, EndCollapsibleInstruction instruction) { }
	}
}