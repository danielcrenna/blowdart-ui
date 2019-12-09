// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI.Instructions
{
	public class BeginToastInstruction : RenderInstruction
	{
		public ElementContext Context { get; }
		public string Body { get; }
		public string HeaderText { get; }
		public DateTimeOffset? Timestamp { get; }

		public BeginToastInstruction(ElementContext context, string body, string headerText = "", DateTimeOffset? timestamp = null)
		{
			Context = context;
			Body = body;
			HeaderText = headerText;
			Timestamp = timestamp;
		}
	}
}