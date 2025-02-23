// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Blowdart.UI;

public partial class Ui
{
	private static readonly ImmutableList<string> NoEvents = new List<string>().ToImmutableList();

	private readonly Hashtable _eventData = new();

	private readonly Dictionary<UInt128, HashSet<string>> _eventsHandled = new();
	private readonly Dictionary<string, HashSet<UInt128>> _eventsRaised = new();
 
	public void AddEvent(string eventType, UInt128 id, object? data)
	{
		if (!_eventsRaised.ContainsKey(eventType))
			_eventsRaised[eventType] = [];

		_eventsRaised[eventType].Add(id);

		if (data != null)
			_eventData[id] = data;
	}

	internal bool OnEvent(string eventType, UInt128 id, out object? data)
	{
		if (_eventsRaised.ContainsKey(eventType) && _eventsRaised[eventType].Contains(id))
		{
			_eventsRaised[eventType].Remove(id);
			if (_eventsRaised[eventType].Count == 0)
				_eventsRaised.Remove(eventType);

			if (_eventsHandled.ContainsKey(id))
			{
				_eventsHandled[id].Remove(eventType);
				if (_eventsHandled[id].Count == 0)
					_eventsHandled.Remove(id);
			}

			data = _eventData[id];
			if (data != null)
				_eventData.Remove(id);
			return true;
		}

		if (!_eventsHandled.ContainsKey(id))
			_eventsHandled[id] = [];

		_eventsHandled[id].Add(eventType);

		data = default;
		return false;
	}

	public IEnumerable<string> GetEventsFor(UInt128 id)
	{
		return _eventsHandled.TryGetValue(id, out var events) ? events : NoEvents;
	}
}