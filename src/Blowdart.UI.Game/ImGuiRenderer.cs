// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ImGuiNET;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ImVec2 = System.Numerics.Vector2;

namespace Blowdart.UI.Gaming
{
	public class ImGuiRenderer
	{
		private readonly UiGame game;

		// Graphics
		private GraphicsDevice graphicsDevice;

		private BasicEffect effect;
		private readonly RasterizerState rasterizerState;

		private byte[] vertexData;
		private VertexBuffer vertexBuffer;
		private int vertexBufferSize;

		private byte[] indexData;
		private IndexBuffer indexBuffer;
		private int indexBufferSize;

		// Textures
		private readonly Dictionary<IntPtr, Texture2D> loadedTextures;
		private int textureId;
		private IntPtr? fontTextureId;

		// Input
		private int scrollWheelValue;

		private readonly List<int> keys = new List<int>();
		private readonly Ui ui;

		public void Initialize()
		{
			graphicsDevice = game.GraphicsDevice;
			RebuildFontAtlas();
		}

		public ImGuiRenderer(UiGame game)
		{
			var context = ImGui.CreateContext();
			ImGui.SetCurrentContext(context);

			this.game = game;
			loadedTextures = new Dictionary<IntPtr, Texture2D>();
			rasterizerState = new RasterizerState
			{
				CullMode = CullMode.None,
				DepthBias = 0,
				FillMode = FillMode.Solid,
				MultiSampleAntiAlias = false,
				ScissorTestEnable = true,
				SlopeScaleDepthBias = 0
			};

			var target = new GameRenderTarget();
			target.RegisterRenderers(game);
			ui = new Ui(target) {UiServices = game.Services};
			
			SetupInput();
		}

		#region ImGuiRenderer

		/// <summary>
		/// Creates a texture and loads the font data from ImGui. Should be called when the <see cref="GraphicsDevice" /> is initialized but before any rendering is done
		/// </summary>
		public virtual unsafe void RebuildFontAtlas()
		{
			// Get font texture from ImGui
			var io = ImGuiNET.ImGui.GetIO();
			io.Fonts.GetTexDataAsRGBA32(out byte* pixelData, out int width, out int height, out int bytesPerPixel);

			// Copy the data to a managed array
			var pixels = new byte[width * height * bytesPerPixel];
			Marshal.Copy(new IntPtr(pixelData), pixels, 0, pixels.Length);

			// Create and register the texture as an XNA texture
			var tex2d = new Texture2D(graphicsDevice, width, height, false, SurfaceFormat.Color);
			tex2d.SetData(pixels);

			// Should a texture already have been build previously, unbind it first so it can be deallocated
			if (fontTextureId.HasValue)
				UnbindTexture(fontTextureId.Value);

			// Bind the new texture to an ImGui-friendly id
			fontTextureId = BindTexture(tex2d);

			// Let ImGui know where to find the texture
			io.Fonts.SetTexID(fontTextureId.Value);
			io.Fonts.ClearTexData(); // Clears CPU side texture data
		}

		public virtual IntPtr BindTexture(Texture2D texture)
		{
			var id = new IntPtr(textureId++);
			loadedTextures.Add(id, texture);
			return id;
		}

		public virtual void UnbindTexture(IntPtr textureId)
		{
			loadedTextures.Remove(textureId);
		}

		public virtual void BeforeLayout(GameTime gameTime)
		{
			ImGui.GetIO().DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			UpdateInput();

			ImGui.NewFrame();
		}

		public void Render(string template, GameTime gameTime)
		{
			BeforeLayout(gameTime);

			var pageMap = game.Services.GetRequiredService<PageMap>();
			var handler = pageMap.GetHandler(template);
			
			ui.Begin();
			handler(ui);
			ui.RenderToTarget(game);

			AfterLayout();
		}

		public virtual void AfterLayout()
		{
			ImGui.Render();
			RenderDrawData(ImGui.GetDrawData());
		}

		#endregion ImGuiRenderer

		#region Setup & Update

		/// <summary>
		/// Maps ImGui keys to XNA keys. We use this later on to tell ImGui what keys were pressed
		/// </summary>
		protected void SetupInput()
		{
			var io = ImGui.GetIO();

			keys.Add(io.KeyMap[(int) ImGuiKey.Tab] = (int) Keys.Tab);
			keys.Add(io.KeyMap[(int) ImGuiKey.LeftArrow] = (int) Keys.Left);
			keys.Add(io.KeyMap[(int) ImGuiKey.RightArrow] = (int) Keys.Right);
			keys.Add(io.KeyMap[(int) ImGuiKey.UpArrow] = (int) Keys.Up);
			keys.Add(io.KeyMap[(int) ImGuiKey.DownArrow] = (int) Keys.Down);
			keys.Add(io.KeyMap[(int) ImGuiKey.PageUp] = (int) Keys.PageUp);
			keys.Add(io.KeyMap[(int) ImGuiKey.PageDown] = (int) Keys.PageDown);
			keys.Add(io.KeyMap[(int) ImGuiKey.Home] = (int) Keys.Home);
			keys.Add(io.KeyMap[(int) ImGuiKey.End] = (int) Keys.End);
			keys.Add(io.KeyMap[(int) ImGuiKey.Delete] = (int) Keys.Delete);
			keys.Add(io.KeyMap[(int) ImGuiKey.Backspace] = (int) Keys.Back);
			keys.Add(io.KeyMap[(int) ImGuiKey.Enter] = (int) Keys.Enter);
			keys.Add(io.KeyMap[(int) ImGuiKey.Escape] = (int) Keys.Escape);
			keys.Add(io.KeyMap[(int) ImGuiKey.A] = (int) Keys.A);
			keys.Add(io.KeyMap[(int) ImGuiKey.C] = (int) Keys.C);
			keys.Add(io.KeyMap[(int) ImGuiKey.V] = (int) Keys.V);
			keys.Add(io.KeyMap[(int) ImGuiKey.X] = (int) Keys.X);
			keys.Add(io.KeyMap[(int) ImGuiKey.Y] = (int) Keys.Y);
			keys.Add(io.KeyMap[(int) ImGuiKey.Z] = (int) Keys.Z);

#if FNA
			// FNA-specific ///////////////////////////
			TextInputEXT.TextInput += c =>
			{
				if (c == '\t')
					return;

				ImGui.GetIO().AddInputCharacter(c);
			};
			///////////////////////////////////////////
#else
			// MonoGame-specific //////////////////////
			game.Window.TextInput += (s, a) =>
			{
				if (a.Character == '\t')
					return;

				io.AddInputCharacter(a.Character);
			};
			///////////////////////////////////////////
#endif
            
			ImGui.GetIO().Fonts.AddFontDefault();
		}

		/// <summary>
		/// Updates the <see cref="Effect" /> to the current matrices and texture
		/// </summary>
		protected virtual Effect UpdateEffect(Texture2D texture)
		{
			effect = effect ?? new BasicEffect(graphicsDevice);

			var io = ImGui.GetIO();

#if FNA
			// FNA-specific ///////////////////////////
			const float offset = 0f;
			///////////////////////////////////////////
#else
            // MonoGame-specific //////////////////////
			const float offset = .5f;
			///////////////////////////////////////////
#endif

			effect.World = Matrix.Identity;
			effect.View = Matrix.Identity;
			effect.Projection = Matrix.CreateOrthographicOffCenter(offset, io.DisplaySize.X + offset, io.DisplaySize.Y + offset, offset, -1f, 1f);
			effect.TextureEnabled = true;
			effect.Texture = texture;
			effect.VertexColorEnabled = true;

			return effect;
		}

		/// <summary>
		/// Sends XNA input state to ImGui
		/// </summary>
		protected virtual void UpdateInput()
		{
			var io = ImGui.GetIO();

			var mouse = Mouse.GetState();
			var keyboard = Keyboard.GetState();

			for (int i = 0; i < keys.Count; i++)
			{
				io.KeysDown[keys[i]] = keyboard.IsKeyDown((Keys) keys[i]);
			}

			io.KeyShift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);
			io.KeyCtrl = keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl);
			io.KeyAlt = keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt);
			io.KeySuper = keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows);

			io.DisplaySize = new System.Numerics.Vector2(graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
			io.DisplayFramebufferScale = new System.Numerics.Vector2(1f, 1f);

			io.MousePos = new System.Numerics.Vector2(mouse.X, mouse.Y);

			io.MouseDown[0] = mouse.LeftButton == ButtonState.Pressed;
			io.MouseDown[1] = mouse.RightButton == ButtonState.Pressed;
			io.MouseDown[2] = mouse.MiddleButton == ButtonState.Pressed;

			var scrollDelta = mouse.ScrollWheelValue - scrollWheelValue;
			io.MouseWheel = scrollDelta > 0 ? 1 : scrollDelta < 0 ? -1 : 0;
			scrollWheelValue = mouse.ScrollWheelValue;
		}

#endregion Setup & Update

#region Internals

		/// <summary>
		/// Gets the geometry as set up by ImGui and sends it to the graphics device
		/// </summary>
		private void RenderDrawData(ImDrawDataPtr drawData)
		{
			// Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers
			var lastViewport = graphicsDevice.Viewport;
			var lastScissorBox = graphicsDevice.ScissorRectangle;
			var lastBlendFactor = graphicsDevice.BlendFactor;
			var lastBlendState = graphicsDevice.BlendState;
			var lastRasterizerState = graphicsDevice.RasterizerState;
			var lastDepthStencilState = graphicsDevice.DepthStencilState;
			
			graphicsDevice.BlendFactor = Color.White;
			graphicsDevice.BlendState = BlendState.NonPremultiplied;
			graphicsDevice.RasterizerState = rasterizerState;
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

			// Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
			drawData.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

			// Setup projection
			graphicsDevice.Viewport = new Viewport(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
			UpdateBuffers(drawData);
			RenderCommandLists(drawData);

			// Restore modified state
			graphicsDevice.Viewport = lastViewport;
			graphicsDevice.ScissorRectangle = lastScissorBox;
			graphicsDevice.BlendFactor = lastBlendFactor;
			graphicsDevice.BlendState = lastBlendState;
			graphicsDevice.RasterizerState = lastRasterizerState;
			graphicsDevice.DepthStencilState = lastDepthStencilState;
		}

		private unsafe void UpdateBuffers(ImDrawDataPtr drawData)
		{
			if (drawData.TotalVtxCount == 0)
			{
				return;
			}

			// Expand buffers if we need more room
			if (drawData.TotalVtxCount > vertexBufferSize)
			{
				vertexBuffer?.Dispose();

				vertexBufferSize = (int) (drawData.TotalVtxCount * 1.5f);
				vertexBuffer = new VertexBuffer(graphicsDevice, VertexPositionTextureColor.Declaration, vertexBufferSize, BufferUsage.None);
				vertexData = new byte[vertexBufferSize * VertexPositionTextureColor.Size];
			}

			if (drawData.TotalIdxCount > indexBufferSize)
			{
				indexBuffer?.Dispose();

				indexBufferSize = (int) (drawData.TotalIdxCount * 1.5f);
				indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, indexBufferSize, BufferUsage.None);
				indexData = new byte[indexBufferSize * sizeof(ushort)];
			}

			// Copy ImGui's vertices and indices to a set of managed byte arrays
			int vtxOffset = 0;
			int idxOffset = 0;

			for (int n = 0; n < drawData.CmdListsCount; n++)
			{
				ImDrawListPtr cmdList = drawData.CmdListsRange[n];

				fixed (void* vtxDstPtr = &vertexData[vtxOffset * VertexPositionTextureColor.Size])
				fixed (void* idxDstPtr = &indexData[idxOffset * sizeof(ushort)])
				{
					Buffer.MemoryCopy((void*) cmdList.VtxBuffer.Data, vtxDstPtr, vertexData.Length, cmdList.VtxBuffer.Size * VertexPositionTextureColor.Size);
					Buffer.MemoryCopy((void*) cmdList.IdxBuffer.Data, idxDstPtr, indexData.Length, cmdList.IdxBuffer.Size * sizeof(ushort));
				}

				vtxOffset += cmdList.VtxBuffer.Size;
				idxOffset += cmdList.IdxBuffer.Size;
			}

			// Copy the managed byte arrays to the gpu vertex- and index buffers
			vertexBuffer.SetData(vertexData, 0, drawData.TotalVtxCount * VertexPositionTextureColor.Size);
			indexBuffer.SetData(indexData, 0, drawData.TotalIdxCount * sizeof(ushort));
		}

		private void RenderCommandLists(ImDrawDataPtr drawData)
		{
			graphicsDevice.SetVertexBuffer(vertexBuffer);
			graphicsDevice.Indices = indexBuffer;

			int vtxOffset = 0;
			int idxOffset = 0;

			for (int n = 0; n < drawData.CmdListsCount; n++)
			{
				ImDrawListPtr cmdList = drawData.CmdListsRange[n];

				for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
				{
					ImDrawCmdPtr drawCmd = cmdList.CmdBuffer[cmdi];

					if(!loadedTextures.TryGetValue(drawCmd.TextureId, out var texture))
					{
						throw new InvalidOperationException($"Could not find a texture with id '{drawCmd.TextureId}', please check your bindings");
					}

					graphicsDevice.ScissorRectangle = new Rectangle(
						(int) drawCmd.ClipRect.X,
						(int) drawCmd.ClipRect.Y,
						(int) (drawCmd.ClipRect.Z - drawCmd.ClipRect.X),
						(int) (drawCmd.ClipRect.W - drawCmd.ClipRect.Y)
					);

					var e = UpdateEffect(texture);

					foreach (var pass in e.CurrentTechnique.Passes)
					{
						pass.Apply();

#pragma warning disable CS0618 // // FNA does not expose an alternative method.
						graphicsDevice.DrawIndexedPrimitives(
							primitiveType: PrimitiveType.TriangleList,
							baseVertex: vtxOffset,
							minVertexIndex: 0,
							numVertices: cmdList.VtxBuffer.Size,
							startIndex: idxOffset,
							primitiveCount: (int) drawCmd.ElemCount / 3
						);
#pragma warning restore CS0618
					}

					idxOffset += (int) drawCmd.ElemCount;
				}

				vtxOffset += cmdList.VtxBuffer.Size;
			}
		}

		#endregion Internals

		#region Axis Flip

		private static int animationStart;

		public static void BeginAnimation()
		{
			animationStart = ImGui.GetWindowDrawList().VtxBuffer.Size;
		}

		public static void EndFlip(float radians, float focalPlane = 2000, bool aroundViewport = false, bool horizontal = true, bool vertical = false, ImVec2? center = null)
		{
			// aroundViewport == does the perspective effect happen around the center of the viewport (otherwise: around the center of the rotated vertices)
			// focalPlane == how far away the focal plane is (smaller values for more pronounced effect)

			//
			// IMPORTANT: This doesn't work properly!
			//
			// While this method is able to place the *vertices* correctly for perspective,
			//  it is unable to do a perspective-correct *texture* map! See:
			//  https://en.wikipedia.org/wiki/File:Perspective_correct_texture_mapping.jpg
			//
			// Normally the GPU would do this by doing a perspective-correct interpolation
			//  when it calculates texture coordinates (ie: the 0.5 point of the texture
			//  appears closer to the "far" side, effectively making the "near" half of the texture
			//  bigger, and the "far" half smaller, as you would expect for perspective).
			//  However this requires that the GPU has valid homogeneous coordinates (4D),
			//  which are generated when a 3D position is multiplied with a projection matrix.
			//
			// Sadly, the vertex buffer that is being drawn to only provides for a 2D position.
			//  So, without modifying ImGui to have a 3D position instead (and also set an appropriate
			//  perspective projection matrix -- which would have to be global to the viewport), there
			//  is no option besides drawing the perspective graphic separately.
			//
			// That being said, the error in texture mapping is most pronounced with a small value
			//  for `focalPlane`. Larger values make the error much less noticeable, possibly acceptable.
			//
			// (Also: it might be worth considering lighting, which could be used to really
			//  "sell" the rotation effect. But would probably require a pixel shader.)
			//
			
			var buffer = ImGui.GetWindowDrawList().VtxBuffer;
			int count = buffer.Size - animationStart;
			if (count <= 0)
				return;

			// Calculate rotation:
			float cos = (float)Math.Cos(radians);
			float sin = (float)Math.Sin(radians);

			// Estimate the centre:
			ImVec2 c;
			if (center.HasValue)
				c = center.Value;
			else
			{
				c = new ImVec2(0, 0);
				for (var i = animationStart; i < buffer.Size; i++)
					c += buffer[i].pos;
				c /= (float) count;
			}

			// Gather the viewport size:
			ImVec2 viewportCenter = ImGui.GetIO().DisplaySize / 2f;

			// Calculate new vertex positions:
			for (var i = animationStart; i < buffer.Size; i++)
			{
				// Translate to origin:
				float x = buffer[i].pos.X -= c.X;
				float y = buffer[i].pos.Y -= c.Y;
				float z = 0;

				// Rotate:
				if (horizontal)
				{
					float x1 = x * cos; // - z sin
					z = x * sin; // + z cos
					x = x1;
				}
				if (vertical)
				{
					float y1 = y * cos - z * sin;
					z = y * sin + z * cos;
					y = y1;
				}

				if (aroundViewport)
				{
					x += c.X;
					y += c.Y;
					x -= viewportCenter.X;
					y -= viewportCenter.Y;
				}

				// Apply perspective transform:
				z += focalPlane;
				x *= (focalPlane / z);
				y *= (focalPlane / z);

				// Translate back to world position:
				if (aroundViewport)
				{
					x += viewportCenter.X;
					y += viewportCenter.Y;
				}
				else
				{
					x += c.X;
					y += c.Y;
				}

				buffer[i].pos = new ImVec2(x, y);
			}
		}

		#endregion

		#region Rotation

		// to use in imgui, need to transform vertices directly:
		// https://github.com/ocornut/imgui/issues/2031
		// https://github.com/ocornut/imgui/issues/1286
		// https://github.com/CedricGuillemet/ImGuizmo
		
		public static void EndRotate(float radians, ImVec2? center = null)
		{
			center = center ?? GetCenter();

			var s = (float) Math.Sin(radians);
			var c = (float) Math.Cos(radians);
			center = Rotate(center.Value, s, c) - center;

			var buffer = ImGui.GetWindowDrawList().VtxBuffer;
			for (var i = animationStart; i < buffer.Size; i++)
				buffer[i].pos = Rotate(buffer[i].pos, s, c) - center.Value;
		}
		
		private static ImVec2 Rotate(ImVec2 v, float cos, float sin)
		{
			return new ImVec2(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
		}

		private static ImVec2 GetCenter()
		{
			var l = new ImVec2( float.MaxValue,  float.MaxValue);
			var u = new ImVec2(-float.MaxValue, -float.MaxValue);
			var buffer = ImGui.GetWindowDrawList().VtxBuffer;
			for (var i = animationStart; i < buffer.Size; i++)
			{
				l = ImVec2.Min(l, buffer[i].pos);
				u = ImVec2.Max(u, buffer[i].pos);
			}
			return new ImVec2((l.X + u.X) / 2, (l.Y + u.Y) / 2);
		}

		#endregion
	}
}