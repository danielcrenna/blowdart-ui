// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class TextBoxInstruction : RenderInstruction
	{
		public Ui Ui { get; }
		public Value128 Id { get; }
		public FieldType Type { get; }
		public ElementAlignment Alignment { get; }
		public string Name { get; }
		public string Value { get; }
		public string Placeholder { get; }
		public string Label { get; }
		public bool InForm { get; }

		public TextBoxInstruction(Ui ui, Value128 id, FieldType type, ElementAlignment alignment, string name, string value, string placeholder, string label, bool inForm)
		{
			Ui = ui;
			Id = id;
			Type = type;
			Alignment = alignment;
			Name = name;
			Value = value;
			Placeholder = placeholder;
			Label = label;
			InForm = inForm;
		}

		public override string DebuggerDisplay => $"TextBox: {Value}";
	}
}