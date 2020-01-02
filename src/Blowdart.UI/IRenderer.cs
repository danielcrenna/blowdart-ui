// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI
{
    public interface IRenderer { }
    
	public interface IRenderer<in T, in TRenderer> : IRenderer where T : RenderInstruction
	{
		void Render(TRenderer r, T instruction);
	}
}