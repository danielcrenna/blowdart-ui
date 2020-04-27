// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
	public static class UiExtensions
	{
		public static void Text(this Ui ui, string text)
		{
			ui.Add(new TextInstruction(text));
		}
	}
}