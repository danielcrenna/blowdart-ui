// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Microsoft.Collections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TypeKitchen;

namespace Blowdart.UI
{
    public partial class Ui : IDisposable, IServiceProvider
	{
        internal List<RenderInstruction> Instructions { get; }

        internal IServiceProvider UiServices { get; set; }

        public Ui()
        {
            Instructions = new List<RenderInstruction>();
        }

		#region Elements

		#region Container

		public void BeginContainer(ContainerDirection? direction = null, ContainerLayout? layout = null)
		{
			var css = Pooling.StringBuilderPool.Scoped(sb =>
			{
				if (layout.HasValue)
				{
					switch (layout)
					{
						case ContainerLayout.FixedWidth:
							break;
						case ContainerLayout.Fluid:
							sb.Append("fluid");
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(layout), layout, null);
					}
				}

				if(direction.HasValue)
				{
					if (layout.HasValue)
						sb.Append(" ");

					switch (direction)
					{
						case ContainerDirection.LeftToRight:
							sb.Append("flex-row");
							break;
						case ContainerDirection.RightToLeft:
							sb.Append("flex-row-reverse");
							break;
						case ContainerDirection.TopToBottom:
							sb.Append("flex-column");
							break;
						case ContainerDirection.BottomToTop:
							sb.Append("flex-column-reverse");
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
					}
				}
			});

			Instructions.Add(new BeginElementInstruction(ElementType.Container, css));
		}

		public void Container(ContainerDirection direction, ContainerLayout layout, Action<Ui> handler)
		{
			BeginContainer(direction, layout);
			Component(handler);
			EndContainer();
		}

		public void Container(ContainerLayout layout, Action<Ui> handler)
		{
			BeginContainer(layout: layout);
			Component(handler);
			EndContainer();
		}

		public void Container(ContainerDirection direction, Action<Ui> handler)
		{
			BeginContainer(direction);
			Component(handler);
			EndContainer();
		}

		public void Container(Action<Ui> handler)
		{
			BeginContainer();
			Component(handler);
			EndContainer();
		}

		public void Container(ContainerDirection direction, ContainerLayout layout, Action handler)
		{
			BeginContainer(direction, layout);
			Component(handler);
			EndContainer();
		}

		public void Container(ContainerLayout layout, Action handler)
		{
			BeginContainer(layout: layout);
			Component(handler);
			EndContainer();
		}

		public void Container(ContainerDirection direction, Action handler)
		{
			BeginContainer(direction);
			Component(handler);
			EndContainer();
		}

		public void Container(Action handler)
		{
			BeginContainer();
			Component(handler);
			EndContainer();
		}

		public void EndContainer()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Container));
		}

		#endregion

		#region Row

		public void BeginRow(ColumnDirection? direction = null)
		{
			string css = null;
			if (direction.HasValue)
			{
				switch (direction)
				{
					case ColumnDirection.LeftToRight:
						break;
					case ColumnDirection.RightToLeft:
						css = "flex-row-reverse";
						break;
					case ColumnDirection.TopToBottom:
						css = "flex-column";
						break;
					case ColumnDirection.BottomToTop:
						css = "flex-column-reverse";
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
				}
			}

			Instructions.Add(new BeginElementInstruction(ElementType.Row, css));
		}

		public void Row(ColumnDirection direction, Action<Ui> handler)
		{
			BeginRow(direction);
			Component(handler);
			EndRow();
		}

		public void Row(Action<Ui> handler)
		{
			BeginRow();
			Component(handler);
			EndRow();
		}

		public void Row(ColumnDirection direction, Action handler)
		{
			BeginRow(direction);
			Component(handler);
			EndRow();
		}

		public void Row(Action handler)
		{
			BeginRow();
			Component(handler);
			EndRow();
		}

		public void EndRow()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Row));
		}

		#endregion

		#region Column

		public void BeginColumn()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Column));
		}

		public void Column(Action<Ui> handler)
		{
			BeginColumn();
			Component(handler);
			EndColumn();
		}

		public void Column(Action handler)
		{
			BeginColumn();
			Component(handler);
			EndColumn();
		}

		public void EndColumn()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Column));
		}

		#endregion

		#region Main

		public void BeginMain()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Main));
		}

		public void Main(Action<Ui> handler)
		{
			BeginMain();
			Component(handler);
			EndMain();
		}

		public void Main(Action handler)
		{
			BeginMain();
			Component(handler);
			EndMain();
		}

		public void EndMain()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Main));
		}

		#endregion

		#region TopRow

		public void BeginTopRow()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.TopRow));
		}

		public void TopRow(Action<Ui> handler)
		{
			BeginTopRow();
			Component(handler);
			EndTopRow();
		}

		public void TopRow(Action handler)
		{
			BeginTopRow();
			Component(handler);
			EndTopRow();
		}

		public void EndTopRow()
		{
			Instructions.Add(new EndElementInstruction(ElementType.TopRow));
		}

		#endregion

		#region MainContent

		public void BeginMainContent()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.MainContent));
		}

		public void MainContent(Action<Ui> handler)
		{
			BeginMainContent();
			Component(handler);
			EndMainContent();
		}

		public void MainContent(Action handler)
		{
			BeginMainContent();
			Component(handler);
			EndMainContent();
		}

		public void EndMainContent()
		{
			Instructions.Add(new EndElementInstruction(ElementType.MainContent));
		}

		#endregion

		#region Section

		public void BeginSection()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Section));
		}

		public void Section(Action<Ui> handler)
		{
			BeginSection();
			Component(handler);
			EndSection();
		}

		public void Section(Action handler)
		{
			BeginSection();
			Component(handler);
			EndSection();
		}

		public void EndSection()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Section));
		}

		#endregion

		#region Menu

		public void BeginMenu(string title)
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Menu));
			_inMenu = true;
			_menuTitle = title;
		}

		public void EndMenu()
		{
			if (_hasMenuItems)
			{
				Instructions.Add(new EndCollapsibleInstruction());
			}
			
			Instructions.Add(new EndElementInstruction(ElementType.Menu));

			_inMenu = false;
			_hasMenuItems = false;
			_menuTitle = null;
		}

		#endregion

		#region Tables

		public void ObjectTable<T>(IEnumerable<T> data)
		{
			Instructions.Add(new ObjectTableInstruction<T>(data));
		}

		public void ListTable<T>(IEnumerable<T> data, Action<Ui, T> template)
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Table));
			var ordinal = 0;
			foreach (var item in data)
			{
				Instructions.Add(new BeginElementInstruction(ElementType.TableRow));
				Instructions.Add(new BeginElementInstruction(ElementType.TableColumn, ordinal: ++ordinal));
				_inTable = true;
				template(this, item);
				_inTable = false;
				Instructions.Add(new BeginElementInstruction(ElementType.TableColumn));
				Instructions.Add(new EndElementInstruction(ElementType.TableRow));
			}
			Instructions.Add(new EndElementInstruction(ElementType.Table));
		}

		public void ListTable<T>(IList<T> data, Action<int, T> template)
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Table));
			var ordinal = 0;
			foreach (var item in data)
			{
				Instructions.Add(new BeginElementInstruction(ElementType.TableRow));
				Instructions.Add(new BeginElementInstruction(ElementType.TableColumn, ordinal: ++ordinal));
				_inTable = true;
				template(ordinal, item);
				_inTable = false;
				Instructions.Add(new EndElementInstruction(ElementType.TableColumn));
				Instructions.Add(new EndElementInstruction(ElementType.TableRow));
			}
			Instructions.Add(new EndElementInstruction(ElementType.Table));
		}

		#endregion

		#region Lists

		public void List<T>(ListDirection direction, IEnumerable<T> data, Action<Ui, T> template)
		{
			var style = GetListStyle(direction);
			Instructions.Add(new BeginElementInstruction(ElementType.List, style));
			foreach (var item in data)
			{
				Instructions.Add(new BeginElementInstruction(ElementType.ListItem, "border-0"));
				template(this, item);
				Instructions.Add(new EndElementInstruction(ElementType.ListItem));
			}
			Instructions.Add(new EndElementInstruction(ElementType.List));
		}

		public void List<T>(IList<T> list, Action<Ui, T> template)
		{
			List(ListDirection.TopToBottom, list, template);
		}

		public void List<T>(ListDirection direction, IEnumerable<T> data, Action<T> template)
		{
			var style = GetListStyle(direction);
			Instructions.Add(new BeginElementInstruction(ElementType.List, style));
			foreach (var item in data)
			{
				Instructions.Add(new BeginElementInstruction(ElementType.ListItem, "border-0"));
				template(item);
				Instructions.Add(new EndElementInstruction(ElementType.ListItem));
			}
			Instructions.Add(new EndElementInstruction(ElementType.List));
		}

		public void List<T>(IList<T> list, Action<T> template)
		{
			List(ListDirection.TopToBottom, list, template);
		}

		private static string GetListStyle(ListDirection direction)
		{
			string style = null;
			switch (direction)
			{
				case ListDirection.LeftToRight:
					style = "list-group-horizontal";
					break;
				case ListDirection.TopToBottom:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}

			return style;
		}

		#endregion


		#endregion

		#region Commands 

		public void TextBlock(string value)
		{
			Instructions.Add(new TextBlockInstruction(value));
		}

		public void CodeBlock(string value)
		{
			Instructions.Add(new CodeInstruction(value, true));
		}

		public void CodeInline(string value)
		{
			Instructions.Add(new CodeInstruction(value, false));
		}

		public void Text(string text)
        {
            Instructions.Add(new TextInstruction(text));
        }

        public void Header(int level, string innerText)
        {
            Instructions.Add(new HeaderInstruction(level, innerText));
        }

        public bool Button(string text, ButtonType type = ButtonType.Primary)
        {
            NextId();
            var id = NextIdHash;
            Instructions.Add(new ButtonInstruction(this, id, type, text));
            return OnEvent(Events.OnClick, id, out _);
        }

        public bool CheckBox(ref bool value, string text, ElementAlignment alignment = ElementAlignment.Left)
        {
	        NextId();
	        var id = NextIdHash;
	        Instructions.Add(new CheckBoxInstruction(this, id, text, alignment, value));
	        var clicked = OnEvent(Events.OnClick, id, out _);
	        if (clicked)
		        value = !value;
	        return clicked;
        }

        public bool Slider(ref int value, string text, ElementAlignment alignment = ElementAlignment.Left)
		{
	        NextId();
	        var id = NextIdHash;
	        Instructions.Add(new SliderInstruction(this, id, text, alignment, value));
	        var changed = OnEvent(Events.OnChange, id, out var data);
	        if (changed && data != default && data is string dataString)
				int.TryParse(dataString, out value);
			return changed;
        }

        public bool RadioButton(ref bool value, string text, ElementAlignment alignment = ElementAlignment.Left)
        {
	        NextId();
	        var id = NextIdHash;
	        Instructions.Add(new RadioButtonInstruction(this, id, text, alignment, value));
	        var clicked = OnEvent(Events.OnClick, id, out _);
	        if (clicked)
		        value = !value;
	        return clicked;
        }

		internal bool OnEvent(string eventType, Value128 id, out object data)
        {
            var contains = _events.Contains(eventType, id);
            if (contains)
            {
	            _events.Remove(eventType, id);
	            if (_eventData.TryGetValue(id, out data))
					_eventData.Remove(id);
	            return true;
            }

            data = default;
            return false;
        }
		
		public void Component(Action<Ui> handler)
        {
            handler(this);
        }

        public void Component(Action handler)
        {
            handler();
        }

		public void Link(string href, string title)
        {
            Instructions.Add(new LinkInstruction(href, title));
        }
        
        public void Editor<T>(T instance)
        {
            Instructions.Add(new EditorInstruction<T>(instance));
        }

		public void Log(string message)
        {
	        Instructions.Add(new LogInstruction(message));
        }

		public void InlineIcon(OpenIconicIcons icon)
        {
	        Instructions.Add(new InlineIconInstruction(icon));
        }

		#endregion

		public void RenderToTarget<T>(RenderTarget target, T renderer)
		{
            target.AddInstructions(Instructions);
            target.Render(renderer);
        }

        public void Dispose()
        {
            Instructions.Clear();
        }

        #region Hashing 

        internal Value128 NextIdHash;
        private int _count;

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

        #endregion

        private readonly MultiValueDictionary<string, Value128> _events =
            MultiValueDictionary<string, Value128>.Create<HashSet<Value128>>();

        private readonly Dictionary<Value128, object> _eventData = new Dictionary<Value128, object>();

		public void AddEvent(string eventType, Value128 id, object data)
        {
            _events.Add(eventType, id);
            if (data != null)
				_eventData.Add(id, data);
		}

        internal void Begin()
        {
            Instructions.Clear();
            NextIdHash = default;
            _count = default;
            _body = default;
            CalledLayout = default;
        }

        private readonly List<IPromise> _dataLoaders = new List<IPromise>();

        private interface IPromise
        {
            Task LoadAsync(IServiceProvider serviceProvider);
        }

        private struct Promise<T> : IPromise
        {
            private readonly Func<IServiceProvider, T> _getData;
            private readonly Action<T> _setData;

            public Promise(Func<IServiceProvider, T> getData, Action<T> setData)
            {
                _getData = getData;
                _setData = setData;
            }

            public Task LoadAsync(IServiceProvider serviceProvider)
            {
                var data = _getData(serviceProvider);
                _setData(data);
                return Task.CompletedTask;
            }
        }

        public void DataLoader<T>(Func<IServiceProvider, T> getData, Action<T> setData)
        {
            _dataLoaders.Add(new Promise<T>(getData, setData));
        }

        public void DataLoader<TService, TResult>(Func<TService, TResult> getData, Action<TResult> setData)
        {
	        TResult Fetch(IServiceProvider r) => getData(r.GetRequiredService<TService>());

	        _dataLoaders.Add(new Promise<TResult>(Fetch, setData));
        }

		internal async Task<bool> DispatchDataLoaders()
        {
            if (_dataLoaders.Count == 0)
                return false;

            foreach (var dataLoader in _dataLoaders)
                await dataLoader.LoadAsync(UiServices);
            return true;
        }

        public void Separator()
        {
	        Instructions.Add(new SeparatorInstruction());
        }

		#region IServiceProvider

		public object GetService(Type serviceType)
        {
	        return UiServices.GetService(serviceType);
        }

		#endregion

		#region Split Testing

		//public void SplitTest(string name, string description, Action a, Action b, params Action[] moreActions)
		//{
		//	var identifier = UiServices.GetService<ICohortIdentifier>();
		//	if (identifier == null)
		//	{
		//      Trace.TraceWarning("No split testing identifier was registered, skipping test");
		//		a();
		//		return;
		//	}

		//	var experiment = AddOrGetExperiment(name, description, a, b, moreActions, identifier);
		//	experiment.Choose(experiment);
		//}

		//private static Experiment AddOrGetExperiment(string name, string description, Action a, Action b, Action[] moreActions,
		//	ICohortIdentifier identifier)
		//{
		//	var options = moreActions.Concat(new[] {a, b});
		//	var experiment = new Experiment(identifier, name, description, options, "foo");
		//	return Experiments.Inner.GetOrAdd(new ExperimentKey(name), experiment);
		//}

		#endregion

		#region Layouts

		private Action<Ui> _body;

		internal bool CalledLayout { get; private set; }

		public Action<Ui> LayoutBody()
		{
			CalledLayout = true;
			return _body ?? throw new BlowdartException("Missing layout body");
		}

		public void SetLayoutBody(Action<Ui> body)
		{
			_body = body;
		}

		#endregion
	}
}
