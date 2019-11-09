// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class InlineImageInstruction : RenderInstruction
	{
		public string Source { get; }
		public int Width { get; }
		public int Height { get; }

		public InlineImageInstruction(string source, int width, int height)
		{
			Source = source;
			Width = width;
			Height = height;
		}

		public override string DebuggerDisplay => $"InlineImage ({Source})";
	}
}