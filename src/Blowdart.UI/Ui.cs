// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI
{
	public class Ui
	{
		private readonly List<RenderInstruction> _instructions;
		private readonly RenderTarget _target;

		public Ui(RenderTarget target)
		{
			_target = target;
			_instructions = new List<RenderInstruction>();
		}

		public void Begin()
		{
			_instructions.Clear();
			_target.Begin();
		}

		public void RenderToTarget<TRenderer>(TRenderer renderer)
		{
			_target.AddInstructions(_instructions);
			_target.Render(renderer);
		}

		public void Add(RenderInstruction instruction)
		{
			_instructions.Add(instruction);
		}
	}
}