// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ImGuiNET;
using Microsoft.Xna.Framework.Graphics;

namespace Blowdart.UI.FNA
{
	public static class VertexPositionTextureColor
	{
		public static readonly VertexDeclaration Declaration;

		public static readonly int Size;

		static VertexPositionTextureColor()
		{
			unsafe
			{ Size = sizeof(ImDrawVert); }

			Declaration = new VertexDeclaration(
				Size,

				// Position
				new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),

				// UV
				new VertexElement(8, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),

				// Color
				new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 0)
			);
		}
	}
}