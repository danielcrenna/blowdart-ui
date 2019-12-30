// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Gaming;
using ImGuiNET;
using Microsoft.Xna.Framework;

namespace Demo.Game
{
	public class SampleGame : UiGame
	{
		public SampleGame() : base(1024, 768)
		{
			IsMouseVisible = true;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			base.Draw(gameTime);
		}
	}
}