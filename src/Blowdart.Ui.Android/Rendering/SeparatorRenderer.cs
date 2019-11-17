// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class SeparatorRenderer : IRenderer<SeparatorInstruction, ViewGroup>
	{
		public void Render(ViewGroup v, SeparatorInstruction instruction)
		{
			var separator = new View(v.Context)
			{
				LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1),
				Background = new ColorDrawable(Color.DarkGray)
			};

			v.AddView(separator);
		}
	}
}