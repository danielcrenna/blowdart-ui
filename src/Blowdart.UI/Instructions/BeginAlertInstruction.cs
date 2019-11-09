// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
	public class BeginAlertInstruction : RenderInstruction
	{
		public ElementContext Context { get; }
		public override string DebuggerDisplay => "Alert";

		public BeginAlertInstruction(ElementContext context)
		{
			Context = context;
		}
	}
}