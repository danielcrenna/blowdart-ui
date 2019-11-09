// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Components;
using Blowdart.UI.Web.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web
{
    internal class WebRenderTarget : RenderTarget
    {
	    private readonly List<RenderFragment> _fragments;
        private readonly Dictionary<Type, IWebRenderer> _renderers;

        public WebRenderTarget(ImGui imGui)
        {
	        _fragments = new List<RenderFragment>();

            var elementRenderer = new ElementRenderer();
            var tabContentRenderer = new TabContentRenderer();
			var collapsibleHeaderRenderer = new CollapsibleHeaderRenderer(imGui);

            _renderers = new Dictionary<Type, IWebRenderer>
            {
                {typeof(BeginElementInstruction), elementRenderer},
                {typeof(EndElementInstruction), elementRenderer},
                {typeof(TextInstruction), new TextRenderer()},
                {typeof(TextBlockInstruction), new TextBlockRenderer()},
                {typeof(CodeInstruction), new CodeRenderer()},
                {typeof(SeparatorInstruction), new SeparatorRenderer()},
				{typeof(ButtonInstruction), new ButtonRenderer(imGui)},
				{typeof(CheckBoxInstruction), new CheckBoxRenderer(imGui)},
				{typeof(RadioButtonInstruction), new RadioButtonRenderer(imGui)},
				{typeof(SliderInstruction), new SliderRenderer(imGui)},
				{typeof(LinkInstruction), new LinkRenderer()},
                {typeof(HeaderInstruction), new HeaderRenderer()},
                {typeof(EditorInstruction), new EditorRenderer()},
                {typeof(TabListItemInstruction), new TabListItemRenderer(imGui)},
                {typeof(BeginTabContentInstruction), tabContentRenderer},
                {typeof(EndTabContentInstruction), tabContentRenderer},
				{typeof(InlineIconInstruction), new InlineIconRenderer()},
				{typeof(InlineImageInstruction), new InlineImageRenderer()},
				{typeof(ObjectTableInstruction), new ObjectTableRenderer()},
                {typeof(MenuItemInstruction), new MenuItemRenderer()},
                {typeof(BeginCollapsibleInstruction), new BeginCollapsibleRenderer()},
                {typeof(BeginCollapsibleHeaderInstruction), collapsibleHeaderRenderer},
                {typeof(EndCollapsibleHeaderInstruction), collapsibleHeaderRenderer},
				{typeof(EndCollapsibleInstruction), new EndCollapsibleRenderer()},
			};
        }

        public override void Render<T>(T renderer)
        {
            var builder = renderer as RenderTreeBuilder;
            foreach (var fragment in _fragments)
                fragment(builder);
        }

        internal override void AddInstructions(List<RenderInstruction> instructions)
        {
            _fragments.Add(b =>
            {
                foreach (var instruction in instructions)
                {
                    RenderInstruction(b, instruction);
                }
            });
        }

        private void RenderInstruction(RenderTreeBuilder b, RenderInstruction instruction)
        {
            var key = instruction.GetType();
            if (key.IsGenericType)
                key = key.BaseType ?? throw new NullReferenceException();
            if (!_renderers.TryGetValue(key, out var renderer))
                throw new ArgumentException($"No renderer found for {key.Name}");
            renderer.Render(b, instruction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Begin();
            }
        }

        public override void Begin()
        {
            _fragments.Clear();
        }
    }
}
