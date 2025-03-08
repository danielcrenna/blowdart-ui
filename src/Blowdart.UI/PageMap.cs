// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI;

public class PageMap
{
	private readonly Dictionary<string, string> _handlers = new();
	private readonly Dictionary<string, string> _layouts = new();

	public string? GetHandler(string route)
	{
		return _handlers.GetValueOrDefault(route);
	}

	public string? GetLayout(string route)
	{
		return _layouts.GetValueOrDefault(route);
	}

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