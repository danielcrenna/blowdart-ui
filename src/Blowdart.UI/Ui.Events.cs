// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Collections.Extensions;

namespace Blowdart.UI
{
	public partial class Ui
	{
		private readonly MultiValueDictionary<Value128, string> _eventsHandled = 
			MultiValueDictionary<Value128, string>.Create<HashSet<string>>();

		private readonly MultiValueDictionary<string, Value128> _eventsRaised =
			MultiValueDictionary<string, Value128>.Create<HashSet<Value128>>();
		
		private readonly Hashtable _eventData = new Hashtable();

		internal void AddEvent(string eventType, Value128 id, object data)
		{
			_eventsRaised.Add(eventType, id);
			if (data != null)
				_eventData[id] = data;
		}

		internal bool OnEvent(string eventType, Value128 id, out object data)
		{
			var contains = _eventsRaised.Contains(eventType, id);
			if (contains)
			{
				_eventsRaised.Remove(eventType, id);
				_eventsHandled.Remove(id, eventType);

				data = _eventData[id];
				if (data != null)
					_eventData.Remove(id);
				return true;
			}
			
			_eventsHandled.Add(id, eventType);
			data = default;
			return false;
		}

		private static readonly ImmutableList<string> NoEvents = new List<string>().ToImmutableList();
		internal IEnumerable<string> GetEventsFor(Value128 id)
		{
			return _eventsHandled.TryGetValue(id, out var @events) ? @events : NoEvents;
		}
	}
}