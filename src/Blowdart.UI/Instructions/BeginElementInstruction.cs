// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public sealed class BeginElementInstruction : RenderInstruction
	{
		public Value128? Id { get; }
		public string Name { get; }
		
		public BeginElementInstruction(string name, Value128? id = default)
		{
			Name = name;
			Id = id;
		}
	}
}