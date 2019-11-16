// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class ShowCollapsibleInstruction : RenderInstruction
	{
		public Value128 Id { get; }

		public ShowCollapsibleInstruction(Value128 id)
		{
			Id = id;
		}

		public override string DebuggerDisplay => "ShowCollapsible";
	}
}