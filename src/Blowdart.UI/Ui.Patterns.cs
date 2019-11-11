using System;
using System.Linq;
using System.Reflection;
using TypeKitchen;

namespace Blowdart.UI
{
	partial class Ui
	{
		#region Patterns

		private readonly ITypeResolver _resolver = new ReflectionTypeResolver();

		public void Pattern<T>(string name, T arg) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg);
		public void Pattern<T1, T2>(string name, T1 arg1, T2 arg2) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg1, arg2);
		public void Pattern<T1, T2, T3>(string name, T1 arg1, T2 arg2, T3 arg3) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg1, arg2, arg3);
		public void Pattern<T1, T2, T3, T4>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg1, arg2, arg3, arg4);
		public void Pattern<T1, T2, T3, T4, T5>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg1, arg2, arg3, arg4, arg5);
		public void Pattern<T1, T2, T3, T4, T5, T6>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => RenderPattern(_resolver.FindFirstByName($"{name}Renderer") ?? throw new BlowdartException($"No renderer found matching name {name}"), arg1, arg2, arg3, arg4, arg5, arg6);

		private void RenderPattern(Type rendererType, params object[] renderArgs)
		{
			foreach (var method in typeof(Ui).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				if (method.Name != nameof(CustomRenderer))
					continue;
				var parameterCount = method.GetParameters().Length;
				if (parameterCount != renderArgs.Length)
					continue;

				var types = new[] {rendererType}.Concat(renderArgs.Select(x => x.GetType())).ToArray();
				var genericMethod = method.MakeGenericMethod(types);
				genericMethod.Invoke(this, renderArgs);
				break;
			}
		}

		#endregion

		#region Custom Renderers

		private void CustomRenderer<TInstructionRenderer, T>(T arg) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg));
		private void CustomRenderer<TInstructionRenderer, T1, T2>(T1 arg1, T2 arg2) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg1, arg2));
		private void CustomRenderer<TInstructionRenderer, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg1, arg2, arg3));
		private void CustomRenderer<TInstructionRenderer, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg1, arg2, arg3, arg4));
		private void CustomRenderer<TInstructionRenderer, T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg1, arg2, arg3, arg4, arg5));
		private void CustomRenderer<TInstructionRenderer, T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) where TInstructionRenderer : IRenderer => Instructions.Add((RenderInstruction) Activator.CreateInstance(GetRenderInstructionType<TInstructionRenderer>(), arg1, arg2, arg3, arg4, arg5, arg6));

		private Type GetRenderInstructionType<TInstructionRenderer>() where TInstructionRenderer : IRenderer
		{
			var renderTargetType = _target.GetType().BaseType ?? throw new NullReferenceException();
			var rendererType = renderTargetType.GenericTypeArguments[0];
			var renderTarget = typeof(RenderTarget<>).MakeGenericType(rendererType);

			var typeInfo = typeof(TInstructionRenderer).GetTypeInfo();
			var interfaces = typeInfo.ImplementedInterfaces;

			var instructionType = interfaces.Single(x => typeof(IRenderer<,>).IsAssignableFromGeneric(x))
				.GenericTypeArguments[0];

			var method = renderTarget.GetMethod(nameof(RenderTarget<object>.TryAddRenderer)) ??
			             throw new NullReferenceException();
			var genericMethod = method.MakeGenericMethod(instructionType, typeof(TInstructionRenderer));
			var dependencies = new object[]
			{
				/* FIXME: get these from somewhere */
			};
			genericMethod.Invoke(_target, new object[] {dependencies});
			return instructionType;
		}

		#endregion
	}
}
