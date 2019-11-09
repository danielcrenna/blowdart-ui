// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Game;
using ImGuiNET;
using Microsoft.Xna.Framework;

namespace Demo.Game
{
	public class SampleGame : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager _graphics;
		private readonly UiRenderer _uiRenderer;
		private bool _showDemoWindow = true;

		public SampleGame()
		{
			_uiRenderer = new UiRenderer(this);
			_graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1024, PreferredBackBufferHeight = 768, PreferMultiSampling = true
			};

			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_uiRenderer.Initialize();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			_uiRenderer.Render("/", gameTime);

            ImGui.ShowDemoWindow(ref _showDemoWindow);

			base.Draw(gameTime);
		}
	}
}