using System;

namespace Blowdart.UI
{
	partial class Ui
	{
		public void Pattern<TInstruction, TInstructionRenderer, TRenderer>(params object[] args) 
			where TInstruction : RenderInstruction
			where TInstructionRenderer : IRenderer<TInstruction, TRenderer>
		{
			var renderTarget = typeof(RenderTarget<>).MakeGenericType(typeof(TRenderer));
			var method = renderTarget.GetMethod(nameof(RenderTarget<object>.TryAddRenderer)) ?? throw new NullReferenceException();
			
			var genericMethod = method.MakeGenericMethod(typeof(TInstruction), typeof(TInstructionRenderer), typeof(TRenderer));
			genericMethod.Invoke(_target, args);

            var instruction = (RenderInstruction) Activator.CreateInstance(typeof(TInstruction), args);
            Instructions.Add(instruction);
		}
	}
}
