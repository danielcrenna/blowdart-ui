// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Blowdart.UI;
using Blowdart.UI.Patterns;

namespace Demo.Web.Patterns
{
	public class AvatarListInstruction : RenderInstruction
	{
		public IEnumerable<Avatar> Avatars { get; }
		public int Size { get; }

		public AvatarListInstruction(IEnumerable<Avatar> avatars, int size)
		{
			Avatars = avatars;
			Size = size;
		}

		public override string DebuggerDisplay => "AvatarList";
	}
}