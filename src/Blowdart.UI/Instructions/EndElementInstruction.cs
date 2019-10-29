// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class EndElementInstruction : RenderInstruction
    {
        public ElementType Type;

        public EndElementInstruction(ElementType type)
        {
            Type = type;
        }

        public override string DebuggerDisplay => $"End{Type}";
    }
}
