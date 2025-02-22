// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions;

public sealed class AttributeInstruction(object key, object value) : RenderInstruction
{
	public object Key { get; } = key;
	public object Value { get; } = value;

	public string KeyString => Key is string key ? key : Key?.ToString() ?? string.Empty;
}