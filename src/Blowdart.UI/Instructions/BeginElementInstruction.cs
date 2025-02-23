// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI.Instructions;

public sealed class BeginElementInstruction(string name, UInt128? id = default) : RenderInstruction
{
	public UInt128? Id { get; } = id;
	public string Name { get; } = name;
}