// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class BeginCollapsibleInstruction : RenderInstruction
	{
		public string Title { get; }
		public Value128 Id { get; }
		public ElementSize Size { get; }

		public BeginCollapsibleInstruction(string title, Value128 id, ElementSize size)
		{
			Title = title;
			Id = id;
			Size = size;
		}

		public override string DebuggerDisplay => "BeginCollapsible";
	}
}