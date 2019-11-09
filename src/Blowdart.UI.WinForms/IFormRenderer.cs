// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows.Forms;

namespace Blowdart.UI.WinForms
{
	public interface IFormRenderer
	{
		void Render(RenderInstruction instruction, Panel panel);
	}
}