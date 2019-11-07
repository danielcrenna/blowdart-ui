// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class RadioButtonInstruction : RenderInstruction
	{
		public Ui Ui { get; }
		public Value128 Id { get; }
		public string Text { get; }
		public ElementAlignment Alignment { get; }
		public bool Value { get; }

		public RadioButtonInstruction(Ui ui, Value128 id, string text, ElementAlignment alignment, bool value)
		{
			Ui = ui;
			Id = id;
			Text = text;
			Alignment = alignment;
			Value = value;
		}

		public override string DebuggerDisplay => $"RadioButton: {Text}";
	}
}