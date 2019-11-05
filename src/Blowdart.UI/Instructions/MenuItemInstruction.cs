// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class MenuItemInstruction : RenderInstruction
	{
		public OpenIconicIcons Icon { get; }
		public string Template { get; }
		public string Title { get; }
		
		public MenuItemInstruction(OpenIconicIcons icon, string template, string title)
		{
			Icon = icon;
			Template = template;
			Title = title;
		}

		public override string DebuggerDisplay => $"MenuItem: {Title}";
	}
}