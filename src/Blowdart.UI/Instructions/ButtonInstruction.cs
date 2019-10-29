// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class ButtonInstruction : RenderInstruction
    {
        public Ui Ui { get; }
        public Value128 Id { get; }
        public ButtonType Type { get; }
        public string Text { get; }

        public ButtonInstruction(Ui ui, Value128 id, ButtonType type, string text)
        {
            Ui = ui;
            Id = id;
            Type = type;
            Text = text;
        }

        public override string DebuggerDisplay => $"button: {Text}";
    }
}
