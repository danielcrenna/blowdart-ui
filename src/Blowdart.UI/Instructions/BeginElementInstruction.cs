// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions;

public sealed class BeginElementInstruction(string name, Value128? id = default) : RenderInstruction
{
	public Value128? Id { get; } = id;
	public string Name { get; } = name;
}