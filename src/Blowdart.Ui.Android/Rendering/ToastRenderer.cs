// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class ToastRenderer : 
		IRenderer<BeginToastInstruction, ViewGroup>,
		IRenderer<EndToastInstruction, ViewGroup>
	{
		public void Render(ViewGroup v, BeginToastInstruction instruction)
		{
			const ToastLength duration = ToastLength.Short;
			var text = instruction.Body;
			var toast = Toast.MakeText(v.Context, text, duration);
			toast.Show();
		}

		public void Render(ViewGroup v, EndToastInstruction instruction)
		{
			
		}
	}
}