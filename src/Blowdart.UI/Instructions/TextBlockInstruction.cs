// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class TextBlockInstruction : RenderInstruction
    {
        public string Value { get; }
        public string Class { get; }
        public ElementSize Size { get; }

        public TextBlockInstruction(string value, string @class, ElementSize size)
        {
	        Value = value;
	        Class = @class;
	        Size = size;
        }

        public override string DebuggerDisplay => $"p: {Value}";
    }
}
