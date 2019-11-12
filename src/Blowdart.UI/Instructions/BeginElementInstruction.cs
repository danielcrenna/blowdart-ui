// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class BeginElementInstruction : RenderInstruction
    {
        public string Class { get; }
        public ElementType Type { get; }

		public BeginElementInstruction(ElementType type, string @class = "", int? ordinal = null)
        {
            Class = @class;
            Ordinal = ordinal;
            Type = type;
        }

        public override string DebuggerDisplay => $"Begin{Type}";
    }
}
