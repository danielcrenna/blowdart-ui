// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web
{
    internal class WebRenderTarget : RenderTarget
    {
	    private readonly List<RenderFragment> _fragments;

        public WebRenderTarget()
        {
	        _fragments = new List<RenderFragment>();
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
