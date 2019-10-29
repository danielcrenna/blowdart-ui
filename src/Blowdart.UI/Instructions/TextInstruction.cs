// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class TextInstruction : RenderInstruction
    {
        public string Value { get; }

        public TextInstruction(string value)
        {
            Value = value;
        }

        public override string DebuggerDisplay => $"p: {Value}";
    }
}
