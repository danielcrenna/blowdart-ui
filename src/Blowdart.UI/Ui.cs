// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Blowdart.UI.Instructions;
using Microsoft.Collections.Extensions;

namespace Blowdart.UI
{
	public class Ui
	{
		private readonly List<RenderInstruction> _instructions;
		private readonly RenderTarget _target;
		
		public Ui(RenderTarget target)
		{
			_target = target;
			_instructions = new List<RenderInstruction>();
		}

		public IServiceProvider UiServices { get; internal set; }
		
		public void Begin()
		{
			NextIdHash = default;

			_count = default;
			_instructions?.Clear();
			_target?.Begin();
		}

		public int InstructionCount => _instructions.Count;

		public void RenderToTarget<TRenderer>(TRenderer renderer)
		{
			_target.AddInstructions(_instructions);
			_target.Render(renderer);
		}

		internal void Add(RenderInstruction instruction)
		{
			_instructions.Add(instruction);
		}

		internal Value128 NextIdHash;
		private int _count;

		internal Value128 HashId(string id = null, [CallerMemberName] string callerMemberName = null)
		{
			NextIdHash = Hashing.MurmurHash3(id ?? $"{callerMemberName}{_count++}");
			return NextIdHash;
		}

		public Value128 NextId(string id = null, [CallerMemberName] string callerMemberName = null)
		{
			NextIdHash = Hashing.MurmurHash3(id ?? $"{callerMemberName}{_count++}", NextIdHash) ^ NextIdHash;
			return NextIdHash;
		}

		public Value128 NextId(StringBuilder id)
		{
			NextIdHash = Hashing.MurmurHash3(id, NextIdHash) ^ NextIdHash;
			return NextIdHash;
		}

		public Value128 NextId(int i)
		{
			NextIdHash = Hashing.MurmurHash3((ulong)i, NextIdHash) ^ NextIdHash;
			return NextIdHash;
		}

		#region Events

		private readonly MultiValueDictionary<string, Value128> _events =
			MultiValueDictionary<string, Value128>.Create<HashSet<Value128>>();

		private readonly Hashtable _eventData = new Hashtable();

		internal void AddEvent(string eventType, Value128 id, object data)
		{
			_events.Add(eventType, id);
			if (data != null)
				_eventData[id] = data;
		}

		internal bool OnEvent(string eventType, Value128 id, out object data)
		{
			var contains = _events.Contains(eventType, id);
			if (contains)
			{
				_events.Remove(eventType, id);
				data = _eventData[id];
				if (data != null)
					_eventData.Remove(id);
				return true;
			}

			data = default;
			return false;
		}

		#endregion

		public void PushAttribute(object key, object value)
		{
			Add(new AttributeInstruction(key, value));
		}

		public void PushStyle(Action<StyleContext> styleBuilder)
		{
			_styles.Push(styleBuilder);
		}

		private readonly Stack<Action<StyleContext>> _styles = new Stack<Action<StyleContext>>();

		internal bool TryPopStyle(out Action<StyleContext> style)
		{
			if (_styles.Count == 0)
			{
				style = default;
				return false;
			}

			style = _styles.Pop();
			return true;
		}

		public void Dispose()
		{
			_instructions?.Clear();
			_target?.Dispose();
		}
	}
}