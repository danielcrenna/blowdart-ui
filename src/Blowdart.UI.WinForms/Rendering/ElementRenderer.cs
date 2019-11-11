// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class ElementRenderer : IRenderer<BeginElementInstruction, Panel>
	{
		public void Render(Panel renderer, BeginElementInstruction instruction)
		{
			var element = (BeginElementInstruction) instruction;
		}
	}
}