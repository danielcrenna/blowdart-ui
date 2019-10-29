// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class BeginElementInstruction : RenderInstruction
    {
        public string Style { get; }
        public ElementType Type { get; }

		public BeginElementInstruction(ElementType type, string style = "", int? ordinal = null)
        {
            Style = style;
            Ordinal = ordinal;
            Type = type;
        }

        public override string DebuggerDisplay => $"Begin{Type}";
    }
}
