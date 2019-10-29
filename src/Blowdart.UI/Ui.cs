// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Microsoft.Collections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TypeKitchen;

namespace Blowdart.UI
{
    public partial class Ui : IDisposable
	{
        internal List<RenderInstruction> Instructions { get; }
        public IServiceProvider UiServices { get; internal set; }

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

		#endregion

		#region Commands 

		public void Text(string value)
		{
			Instructions.Add(new TextInstruction(value));
		}

		public void CodeBlock(string value)
		{
			Instructions.Add(new CodeInstruction(value, true));
		}

		public void CodeInline(string value)
		{
			Instructions.Add(new CodeInstruction(value, false));
		}

		public void Literal(string text)
        {
            Instructions.Add(new LiteralInstruction(text));
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
            return OnEvent("onclick", id);
        }
        
        internal bool OnEvent(string eventType, Value128 id)
        {
            var clicked = _events.Contains(eventType, id);
            if (clicked)
                _events.Remove(eventType, id);
            return clicked;
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

        public void Sidebar(string title, params SidebarPage[] pages)
        {
            Instructions.Add(new SidebarInstruction(title, pages));
        }

        public void Editor<T>(T instance)
        {
            Instructions.Add(new EditorInstruction<T>(instance));
        }

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

		#region List

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

        public void AddEvent(string eventType, Value128 id)
        {
            _events.Add(eventType, id);
        }

        internal void Begin()
        {
            Instructions.Clear();
            NextIdHash = default;
            _count = default;
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
	}
}