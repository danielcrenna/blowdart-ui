// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Blowdart.UI.Instructions;
using Blowdart.UI.WinForms.Rendering;
using TextRenderer = Blowdart.UI.WinForms.Rendering.TextRenderer;

namespace Blowdart.UI.WinForms
{
	public delegate void RenderFragment(Panel panel);

	internal sealed class FormRenderTarget : RenderTarget
	{
		private readonly List<RenderFragment> _fragments;
		private readonly Dictionary<Type, IFormRenderer> _renderers;
		private readonly ImGui _imGui;

		public FormRenderTarget(ImGui imGui)
		{
			_imGui = imGui;
			_fragments = new List<RenderFragment>();
			_renderers = new Dictionary<Type, IFormRenderer>
			{
				{typeof(TextInstruction), new TextRenderer()},
				{typeof(HeaderInstruction), new HeaderRenderer()},
				{typeof(SeparatorInstruction), new SeparatorRenderer()},
				{typeof(CodeInstruction), new CodeRenderer()},
			};
		}
		
		internal override void AddInstructions(List<RenderInstruction> instructions)
		{
			_fragments.Add(panel =>
			{
				foreach (var instruction in instructions)
				{
					RenderInstruction(panel, instruction);
				}
			});
		}

		private void RenderInstruction(Panel panel, RenderInstruction instruction)
		{
			var key = instruction.GetType();
			if (key.IsGenericType)
				key = key.BaseType ?? throw new NullReferenceException();
			if (!_renderers.TryGetValue(key, out var renderer))
				throw new ArgumentException($"No renderer found for {key.Name}");

			renderer.Render(instruction, panel);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Begin();
			}
		}

		public override void Render<T>(T renderer)
		{
			var panel = renderer as Panel;
			foreach (var fragment in _fragments)
				fragment(panel);

			_imGui.Invalidate();
		}

		public override void Begin()
		{
			_fragments.Clear();
		}
	}
}