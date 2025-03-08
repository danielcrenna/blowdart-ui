// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI;

public readonly struct ElementRef(Ui ui, UInt128? id = default)
{
	public bool OnEvent<TEvent>(string eventType, out TEvent? data, Func<TEvent, bool>? predicate)
	{
		if (id.HasValue)
			return ui.OnEvent(eventType, id.Value, out data, predicate);
		data = default;
		return false;
	}
}