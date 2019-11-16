// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class CollapsibleRenderer : 
		IRenderer<BeginCollapsibleInstruction, Panel>,
		IRenderer<ShowCollapsibleInstruction, Panel>,
		IRenderer<EndCollapsibleInstruction, Panel>
	{
		public void Render(Panel p, BeginCollapsibleInstruction instruction)
		{
			
		}

		public void Render(Panel p, ShowCollapsibleInstruction instruction)
		{
			
		}

		public void Render(Panel p, EndCollapsibleInstruction instruction)
		{
			
		}
	}
}