// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Blowdart.UI
{
    public class PageMap
    {
        private readonly Dictionary<string, Action<Ui>> _handlers;
        private readonly Dictionary<string, Action<Ui>> _layouts;

		public PageMap()
        {
            _handlers = new Dictionary<string, Action<Ui>>();
            _layouts = new Dictionary<string, Action<Ui>>();
		}

        public Action<Ui> GetHandler(string template) => _handlers.TryGetValue(template, out var handler) ?  handler : null;
        public Action<Ui> GetLayout(string template) => _layouts.TryGetValue(template, out var handler) ? handler : null;

		public void AddPage(string template, Action<Ui> handler)
        {
            _handlers.Add(template, handler);
        }

        public void AddPage(string template, Action<Ui> layout, Action<Ui> handler)
        {
			_layouts.Add(template, layout);
	        _handlers.Add(template, handler);
        }
    }
}
