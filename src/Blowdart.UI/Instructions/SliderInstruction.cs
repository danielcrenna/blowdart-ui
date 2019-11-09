// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class SliderInstruction : RenderInstruction
	{
		public Ui Ui { get; }
		public Value128 Id { get; }
		public string Text { get; }
		public ElementAlignment Alignment { get; }
		public InputActivation Activation { get; }
		public int Value { get; }

		public SliderInstruction(Ui ui, Value128 id, string text, ElementAlignment alignment, InputActivation activation, int value)
		{
			Ui = ui;
			Id = id;
			Text = text;
			Alignment = alignment;
			Activation = activation;
			Value = value;
		}

		public override string DebuggerDisplay => $"Slider: {Text}";
	}
}