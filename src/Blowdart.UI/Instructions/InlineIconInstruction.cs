// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class InlineIconInstruction : RenderInstruction
	{
		public OpenIconicIcons Icon { get; }

		public InlineIconInstruction(OpenIconicIcons icon)
		{
			Icon = icon;
		}

		public override string DebuggerDisplay => $"InlineIcon ({Icon.ToString()})";
	}
}