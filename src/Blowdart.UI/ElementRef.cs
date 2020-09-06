// Copyright (c) The Egregore Project & Contributors. All rights reserved.
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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