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
		public ElementStyle Style { get; }
		public InputActivation Activation { get; }
		public OpenIconicIcons Iconic { get; }
		public MaterialIcons Material { get; }

		public string Name { get; }
		public string Value { get; }
		public string Placeholder { get; }
		public string Label { get; }
		public bool InForm { get; }
		public string Class { get; }
		public string LabelClass { get; }

		public TextBoxInstruction(Ui ui, Value128 id, FieldType type, ElementAlignment alignment, ElementStyle style,
			InputActivation activation,
			OpenIconicIcons iconic, MaterialIcons material, string name, string value, string placeholder, string label,
			bool inForm, string @class, string labelClass)
		{
			Ui = ui;
			Id = id;
			Type = type;
			Alignment = alignment;
			Style = style;
			Activation = activation;
			Iconic = iconic;
			Material = material;
			Name = name;
			Value = value;
			Placeholder = placeholder;
			Label = label;
			InForm = inForm;
			Class = @class;
			LabelClass = labelClass;
		}

		public override string DebuggerDisplay => $"TextBox: {Value}";
	}
}