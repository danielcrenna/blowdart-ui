// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Blowdart.UI.Instructions;

namespace Blowdart.Ui.Rendering
{
	internal sealed class HeaderRenderer : IRenderer<HeaderInstruction, ViewGroup>
	{
		public void Render(ViewGroup v, HeaderInstruction instruction)
		{
			var header = new TextView(v.Context);
			header.Text = instruction.Text;
			v.AddView(header);
		}
	}
}