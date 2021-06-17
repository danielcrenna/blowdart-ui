// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI
{
	public struct ElementRef
	{
		private readonly Value128? _id;
		private readonly Ui _ui;

		public ElementRef(Ui ui, Value128? id = default)
		{
			_ui = ui;
			_id = id;
		}

		internal bool OnEvent(string eventType, out object data)
		{
			if (_id.HasValue)
				return _ui.OnEvent(eventType, _id.Value, out data);
			data = default;
			return false;
		}
	}
}