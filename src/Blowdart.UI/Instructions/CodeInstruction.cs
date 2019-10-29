// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class CodeInstruction : RenderInstruction
	{
		public string Value { get; }
		public bool Block { get; }

		public CodeInstruction(string value, bool block)
		{
			Value = value;
			Block = block;
		}

		public override string DebuggerDisplay => $"{(Block ? "<pre>" : "")}<code>{Value}</code>{(Block ? "</pre>" : "")}";
	}
}