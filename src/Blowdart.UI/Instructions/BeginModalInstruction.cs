// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class BeginModalInstruction : RenderInstruction
	{
		public string Title { get; }
		public Value128 Id { get; }

		public BeginModalInstruction(string title, Value128 id)
		{
			Title = title;
			Id = id;
		}

		public override string DebuggerDisplay => "BeginModal";
	}
}