// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions;

public sealed class TextInstruction(object text) : RenderInstruction
{
	public object? Text { get; } = text;

	public override string DebuggerDisplay => Text?.ToString() ?? string.Empty;
}