// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI
{
	public class PageMap
	{
		private readonly Dictionary<string, string> _handlers;
		private readonly Dictionary<string, string> _layouts;

		public PageMap()
		{
			_handlers = new Dictionary<string, string>();
			_layouts = new Dictionary<string, string>();
		}

		public string GetHandler(string template) => _handlers.TryGetValue(template, out var handler) ?  handler : null;
		public string GetLayout(string template) => _layouts.TryGetValue(template, out var handler) ? handler : null;
		
		public void AddPage(string template, string handler)
		{
			_handlers.Add(template, handler);
		}

		public void AddPage(string template, string layout, string handler)
		{
			_layouts.Add(template, layout);
			_handlers.Add(template, handler);
		}
	}
}