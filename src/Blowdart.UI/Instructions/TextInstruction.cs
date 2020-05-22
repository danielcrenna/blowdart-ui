// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public sealed class TextInstruction : RenderInstruction
	{
		public TextInstruction(object text) => Text = text;

		public object Text { get; }

		public override string DebuggerDisplay => Text?.ToString();
	}
}