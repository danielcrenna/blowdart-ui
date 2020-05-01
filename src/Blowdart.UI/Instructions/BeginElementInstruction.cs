﻿// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public sealed class BeginElementInstruction : RenderInstruction
	{
		public string Name { get; }

		public BeginElementInstruction(string name)
		{
			Name = name;
		}
	}
}