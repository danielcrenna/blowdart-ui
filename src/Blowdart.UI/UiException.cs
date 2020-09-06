// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI
{
	public sealed class UiException : Exception
	{
		public UiException(string message) : base(message) { }
	}
}