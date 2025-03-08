// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Blowdart.UI;

public partial class Ui
{
    private readonly Hashtable _eventData = new();
    private readonly Dictionary<UInt128, HashSet<string>> _eventsHandled = new();
    private readonly Dictionary<string, HashSet<UInt128>> _eventsRaised = new();
    private readonly Dictionary<(UInt128 id, string eventType), Delegate> _eventPredicates = new();

    internal void AddEvent<TEvent>(string eventType, UInt128 id, TEvent? data)
    {
        if (!_eventsRaised.ContainsKey(eventType))
            _eventsRaised[eventType] = [];

        _eventsRaised[eventType].Add(id);

        if (data != null)
            _eventData[id] = data;
    }

    internal Func<TEvent, bool>? GetEventPredicate<TEvent>(UInt128 id, string eventType)
    {
	    if (!_eventPredicates.TryGetValue((id, eventType), out var predicate)) 
		    return default;

	    try
	    {
		    return predicate as Func<TEvent, bool>;
	    }
	    finally
	    {
		    _eventPredicates.Remove((id, eventType));
	    }
    }

    internal bool OnEvent<TEvent>(string eventType, UInt128 id, out TEvent? data, Func<TEvent, bool>? predicate)
    {
	    if (_eventsRaised.TryGetValue(eventType, out var ids) && ids.Contains(id))
        {
	        data = (TEvent?) _eventData[id];

			if (data != null && predicate != null && !predicate(data))
				return false;

			if (data != null)
		        _eventData.Remove(id);

            ids.Remove(id);

            if (_eventsRaised[eventType].Count == 0)
                _eventsRaised.Remove(eventType);

            if (!_eventsHandled.TryGetValue(id, out var handled))
	            return true;

            handled.Remove(eventType);
            if (_eventsHandled[id].Count == 0)
	            _eventsHandled.Remove(id);

            return true;
        }
	   
	    if (!_eventsHandled.ContainsKey(id))
            _eventsHandled[id] = [];

        _eventsHandled[id].Add(eventType);

        if(predicate != null)
	        _eventPredicates[(id, eventType)] = predicate;

        data = default;
        return false;
    }

    public IEnumerable<(string eventType, object? eventData)> GetEventsFor(UInt128 id)
    {
	    if (!_eventsHandled.TryGetValue(id, out var events))
		    yield break;

	    foreach (var evt in events)
		    yield return (evt,  _eventData.ContainsKey(id) ? _eventData[id] : null);
    }
}
