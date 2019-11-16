// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;
using Blowdart.UI.Instructions;

namespace Blowdart.UI.WinForms.Rendering
{
	internal sealed class NoRenderer :
		IRenderer<EndMenuInstruction, Panel>,
		IRenderer<EndElementInstruction, Panel>
	{
		public void Render(Panel p, EndMenuInstruction instruction) { }
		public void Render(Panel p, EndElementInstruction instruction) { }
	}
}