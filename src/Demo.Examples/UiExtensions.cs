// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo.Examples
{
	internal static class UiExtensions
	{
		public static void SampleCode(this Ui ui, string code)
		{
			ui.Separator();
			ui.Header(3, "Code");
			ui.CodeBlock(code);
		}
	}
}