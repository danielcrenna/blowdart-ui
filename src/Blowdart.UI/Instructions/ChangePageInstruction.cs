// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class ChangePageInstruction : RenderInstruction
	{
		public string Template { get; }

		public ChangePageInstruction(string template)
		{
			Template = template;
		}

		public override string DebuggerDisplay => $"ChangePage({Template})";
	}
}