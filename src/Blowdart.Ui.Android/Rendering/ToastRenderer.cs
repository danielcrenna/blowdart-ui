// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.Ui.Extensions;
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
			toast.SetGravity(GravityFlags.Bottom | GravityFlags.Right, 0, 0);

			Color color;
			switch (instruction.Context)
			{
				case ElementContext.Primary:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Secondary:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Success:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Danger:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Warning:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Info:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Light:
					color = "#007bff".ToColor();
					break;
				case ElementContext.Dark:
					color = "#007bff".ToColor();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var matrix = new ColorMatrixColorFilter(new float[]
			{
				0, 0, 0, 0, color.R,
				0, 0, 0, 0, color.G, 
				0, 0, 0, 0, color.B, 
				0, 0, 0, 1, 0
			});
			toast.View.Background.SetColorFilter(matrix);
			toast.Show();
		}

		public void Render(ViewGroup v, EndToastInstruction instruction)
		{
			
		}
	}
}