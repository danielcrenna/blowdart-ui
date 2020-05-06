// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public sealed class ButtonInstruction : RenderInstruction
	{
		public Value128 Id { get; }
		public string Text { get; }

		public ButtonInstruction(Value128 id, string text)
		{
			Id = id;
			Text = text;
		}
	}
}