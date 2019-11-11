using System;

namespace Blowdart.UI
{
	partial class Ui
	{
		public void Pattern<TInstruction, TInstructionRenderer, TRenderer>(params object[] args) 
			where TInstruction : RenderInstruction
			where TInstructionRenderer : IRenderer<TInstruction, TRenderer>
		{
			_target.TryAddRenderer<TInstruction, TInstructionRenderer, TRenderer>();

            var instruction = (RenderInstruction) Activator.CreateInstance(typeof(TInstruction), args);
            Instructions.Add(instruction);
		}
	}
}
