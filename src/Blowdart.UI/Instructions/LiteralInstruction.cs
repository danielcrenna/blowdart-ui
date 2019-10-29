// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class LiteralInstruction : RenderInstruction
    {
        public string Text { get; }

        public LiteralInstruction(string text)
        {
            Text = text;
        }

        public override string DebuggerDisplay => Text;
    }
}
