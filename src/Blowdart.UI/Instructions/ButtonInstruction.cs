// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI.Instructions
{
	public class ButtonInstruction : RenderInstruction
    {
        public Ui Ui { get; }
        public Value128 Id { get; }
        public ElementContext Context { get; }
        public ElementSize Size { get; }
        public ElementDecorator Decorator { get; }
        public ElementAlignment Alignment { get; }
        public ElementStyle Style { get; }
        
        public OpenIconicIcons? Iconic { get; }
		public MaterialIcons? Material { get; }

		public string Text { get; }
        public string Tooltip { get; }

        public ButtonInstruction(Ui ui, Value128 id, ElementContext context, ElementSize size, ElementDecorator decorator,
	        ElementAlignment alignment, ElementStyle style, OpenIconicIcons? iconic, string text, string tooltip)
        {
            Ui = ui;
            Id = id;
            Context = context;
            Size = size;
            Decorator = decorator;
            Alignment = alignment;
            Style = style;
            Iconic = iconic;
            Text = text;
            Tooltip = tooltip;
        }

        public ButtonInstruction(Ui ui, Value128 id, ElementContext context, ElementSize size, ElementDecorator decorator,
	        ElementAlignment alignment, ElementStyle style, MaterialIcons? material, string text, string tooltip)
        {
	        Ui = ui;
	        Id = id;
	        Context = context;
	        Size = size;
	        Decorator = decorator;
	        Alignment = alignment;
	        Style = style;
	        Material = material;
	        Text = text;
	        Tooltip = tooltip;
        }


		public override string DebuggerDisplay => $"Button: {Text}";
    }
}
