// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;

namespace Blowdart.UI
{
	public sealed class StyleContext
	{
		private readonly StringBuilder _writer;

		public StyleContext()
		{
			_writer = new StringBuilder();
		}

		public void Named(string className)
		{
			_writer.Append(className);
		}

		public override string ToString()
		{
			return _writer.ToString();
		}
	}
}