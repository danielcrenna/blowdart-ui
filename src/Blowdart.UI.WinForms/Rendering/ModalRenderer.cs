// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class ModalRenderer :
		IRenderer<BeginModalInstruction, Panel>,
		IRenderer<EndModalInstruction, Panel>,
		IRenderer<ShowModalInstruction, Panel>
	{
		public void Render(Panel renderer, BeginModalInstruction instruction)
		{
			
		}

		public void Render(Panel renderer, EndModalInstruction instruction)
		{

		}

		public void Render(Panel renderer, ShowModalInstruction instruction)
		{
			
		}
	}
}