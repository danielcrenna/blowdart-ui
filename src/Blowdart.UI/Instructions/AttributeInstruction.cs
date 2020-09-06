// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public sealed class AttributeInstruction : RenderInstruction
	{
		public AttributeInstruction(object key, object value)
		{
			Key = key;
			Value = value;
		}

		public object Key { get; }
		public object Value { get; }

		public string KeyString => Key is string key ? key : Key?.ToString() ?? string.Empty;
	}
}