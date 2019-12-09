using System;
using System.Collections.Generic;
using System.IO;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blowdart.UI.Gaming
{
	public class UiGame : Game
	{
		private readonly ImGuiRenderer imGui;
		private readonly GraphicsDeviceManager graphics;

		public UiGame(int width, int height)
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = width,
				PreferredBackBufferHeight = height,
				PreferMultiSampling = true
			};

			IsMouseVisible = true;

			imGui = new ImGuiRenderer(this);
		}

		public BasicEffect basicEffect;
		public SpriteBatch spriteBatch;

		protected override void Initialize()
		{
			imGui.Initialize();

			spriteBatch = new SpriteBatch(GraphicsDevice);
			basicEffect = new BasicEffect(GraphicsDevice);

			basicEffect.World = Matrix.Identity;
			basicEffect.View = Matrix.Identity;

			var projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);
			var offset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
			basicEffect.Projection = offset * projection;
			basicEffect.TextureEnabled = true;
			basicEffect.VertexColorEnabled = true;

			base.Initialize();
		}

		private bool showDemoWindow = true;

		public int ticks;

		protected override void Update(GameTime gameTime)
		{
			ticks++;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			imGui.Render("/", gameTime);
			ImGui.ShowDemoWindow(ref showDemoWindow);
			base.Draw(gameTime);
		}

		private static readonly Dictionary<string, IntPtr> BoundTextures = 
			new Dictionary<string, IntPtr>();

		public IntPtr LoadTexture(string path)
		{
			if (BoundTextures.TryGetValue(path, out var ptr))
				return ptr;
			var texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(path));
			BoundTextures.Add(path, ptr = imGui.BindTexture(texture));
			return ptr;
		}
	}
}