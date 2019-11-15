// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Blowdart.UI.Instructions;
using Blowdart.UI.Localization;
using Microsoft.Collections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TypeKitchen;

namespace Blowdart.UI
{
	public partial class Ui : IDisposable, IServiceProvider
	{
		private readonly RenderTarget _target;
		internal List<RenderInstruction> Instructions { get; }
        internal IServiceProvider UiServices { get; set; }
        public IPrincipal User => UiServices.GetRequiredService<IUserResolver>().GetCurrentUser();

        public Ui(RenderTarget target)
        {
	        _target = target;
	        Instructions = new List<RenderInstruction>();
        }

        internal void Begin()
        {
	        Instructions.Clear();
	        NextIdHash = default;
	        _count = default;
	        _body = default;
	        CalledLayout = default;
	        ClearBindings();
        }

		public void RenderToTarget<TRenderer>(RenderTarget target, TRenderer renderer)
        {
	        target.AddInstructions(Instructions);
	        target.Render(renderer);
		}

        public void Dispose()
        {
	        Instructions.Clear();
        }

		#region Elements

		#region Main Container

		#region Main

		public void BeginMainContainer()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.MainContainer));
		}

		public void MainContainer(Action<Ui> handler)
		{
			BeginMainContainer();
			Component(handler);
			EndMainContainer();
		}

		public void MainContainer(Action handler)
		{
			BeginMainContainer();
			Component(handler);
			EndMainContainer();
		}

		public void EndMainContainer()
		{
			Instructions.Add(new EndElementInstruction(ElementType.MainContainer));
		}

		#endregion

		#endregion

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

		public void BeginRow(string style = "", ColumnDirection? direction = null)
		{
			if (direction.HasValue)
			{
				switch (direction)
				{
					case ColumnDirection.LeftToRight:
						break;
					case ColumnDirection.RightToLeft:
						style += " flex-row-reverse";
						break;
					case ColumnDirection.TopToBottom:
						style += " flex-column";
						break;
					case ColumnDirection.BottomToTop:
						style += " flex-column-reverse";
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
				}
			}

			Instructions.Add(new BeginElementInstruction(ElementType.Row, style));
		}

		public void Row(ColumnDirection direction, Action<Ui> handler)
		{
			BeginRow("", direction);
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
			BeginRow("", direction);
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

		public void BeginSection(string @class = "")
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Section, @class));
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

		#region Region

		public void BeginRegion(string @class = "")
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Region, @class));
		}

		public void Region(Action<Ui> handler)
		{
			BeginRegion();
			Component(handler);
			EndRegion();
		}

		public void Region(Action handler)
		{
			BeginRegion();
			Component(handler);
			EndRegion();
		}

		public void EndRegion()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Region));
		}

		#endregion

		#region Menu

		public void BeginMenu()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.Menu));
			_inMenu = true;
		}

		public void BeginMenuHeader()
		{
			if (!_inMenu)
				throw new BlowdartException($"{nameof(BeginMenuHeader)} was called outside of a menu");

			if (!_hasMenuItems)
			{
				Instructions.Add(new BeginMenuInstruction());
				_hasMenuItems = true;
			}

			Instructions.Add(new BeginMenuHeaderInstruction());
		}

		public void EndMenuHeader()
		{
            if (!_inMenu)
				throw new BlowdartException($"{nameof(EndMenuHeader)} was called outside of a menu");
            if (!_hasMenuItems)
	            throw new BlowdartException($"{nameof(EndMenuHeader)} was called before {nameof(BeginMenuHeader)}");

			Instructions.Add(new EndMenuHeaderInstruction());
		}

		public void EndMenu()
		{
			if (_hasMenuItems)
			{
				Instructions.Add(new EndMenuInstruction());
			}
			
			Instructions.Add(new EndElementInstruction(ElementType.Menu));

			_inMenu = false;
			_hasMenuItems = false;
		}

		#endregion

		#region Tabs

		private bool _inTabList;

		public void BeginTabList()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.TabList));
			_inTabList = true;
		}

		public bool Tab(string text, ref bool active)
		{
			if (!_inTabList)
				throw new BlowdartException("Attempted to create a tab outside of a tab block.");

			var id = PushId(ResolveId());
			Instructions.Add(new TabListItemInstruction(this, id, _(text), active));
			var clicked = OnEvent(DomEvents.OnClick, id, out var _);
			if (clicked)
				active = !active;
			return clicked;
		}

		public void EndTabList()
		{
			Instructions.Add(new EndElementInstruction(ElementType.TabList));
			_inTabList = false;
		}

		private bool _inTabContent;

		public void BeginTabContent()
		{
			Instructions.Add(new BeginElementInstruction(ElementType.TabContent));
			_inTabContent = true;
		}

		public void TabContent(string text, bool active, Action<Ui> handler)
		{
			if (!_inTabContent)
				throw new BlowdartException("Attempted to create tab content outside of a tab content block.");
			Instructions.Add(new BeginTabContentInstruction(this, PopId(), _(text), active));
			Component(handler);
			Instructions.Add(new EndTabContentInstruction());
		}

		public void TabContent(string text, bool active, Action handler)
		{
			if (!_inTabContent)
				throw new BlowdartException("Attempted to create tab content outside of a tab content block.");
			Instructions.Add(new BeginTabContentInstruction(this, PopId(), _(text), active));
			Component(handler);
			Instructions.Add(new EndTabContentInstruction());
		}

		public void EndTabContent()
		{
			Instructions.Add(new EndElementInstruction(ElementType.TabContent));
			_inTabContent = false;
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
				Instructions.Add(new EndElementInstruction(ElementType.TableColumn));
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

		#region Form

		private bool _inForm;

		public void BeginForm(string @class = "")
		{
			_inForm = true;
			Instructions.Add(new BeginElementInstruction(ElementType.Form, @class));
		}

		public void Form(Action<Ui> handler)
		{
			BeginForm();
			Component(handler);
			EndForm();
		}

		public void Form(Action handler)
		{
			BeginForm();
			Component(handler);
			EndForm();
		}

		public void EndForm()
		{
			Instructions.Add(new EndElementInstruction(ElementType.Form));
			_inForm = false;
		}

		#endregion

		#endregion

		#region Commands 

		public void Link(string href, string title)
		{
			Instructions.Add(new LinkInstruction(href, _(title)));
		}

		public void TextBlock(string value, string @class = "")
		{
			TryPop(out ElementSize size);
			Instructions.Add(new TextBlockInstruction(_(value), @class, size));
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
            Instructions.Add(new TextInstruction(_(text)));
        }

        public void Header(int level, string innerText, string @class = "")
        {
            Instructions.Add(new HeaderInstruction(level, _(innerText), @class));
        }

        public bool Button(string text = "")
        {
	        var id = ResolveId();

			TryPop<ElementContext>(out var context);
			TryPop<ElementSize>(out var size);
			TryPop<ElementDecorator>(out var decorator);
			TryPop<ElementAlignment>(out var alignment);

			Instructions.Add(new ButtonInstruction(this, id, context, size, decorator, alignment, _(text)));
            return OnEvent(DomEvents.OnClick, id, out var _);
        }

        public void BeginModal(string title)
        {
	        var id = HashId($"modal:{title}");
	        Instructions.Add(new BeginModalInstruction(_(title), id));
        }

		public void EndModal()
		{
			Instructions.Add(new EndModalInstruction());
		}

		public void Component(Action<Ui> handler)
        {
            handler(this);
        }

        public void Component(Action handler)
        {
            handler();
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

		public void InlineImage(string source, int width, int height)
		{
			Instructions.Add(new InlineImageInstruction(source, width, height));
		}

		public void Separator()
		{
			Instructions.Add(new SeparatorInstruction());
		}

		public void NextLine()
		{
			Instructions.Add(new NextLineInstruction());
		}

		public void BeginAlert()
		{
			TryPop<ElementContext>(out var context);
			Instructions.Add(new BeginAlertInstruction(context));
		}

		public void EndAlert()
		{
			Instructions.Add(new EndAlertInstruction());
		}
        
        public void Alert(string text)
		{
			TryPop<ElementContext>(out var context);
			Instructions.Add(new BeginAlertInstruction(context));
			Instructions.Add(new TextInstruction(_(text)));
			Instructions.Add(new EndAlertInstruction());
		}

		#endregion

		#region Hashing 

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

        #endregion

        #region Events

        private readonly MultiValueDictionary<string, Value128> _events =
	        MultiValueDictionary<string, Value128>.Create<HashSet<Value128>>();

        private readonly Dictionary<Value128, object> _eventData = new Dictionary<Value128, object>();

        public void AddEvent(string eventType, Value128 id, object data)
        {
	        _events.Add(eventType, id);
	        if (data != null)
		        _eventData.Add(id, data);
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

		#endregion

		#region Data Loading

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

		#endregion

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

		public void LayoutBody()
		{
			CalledLayout = true;  
			Component(_body ?? throw new BlowdartException("Missing layout body"));
		}

		public void SetLayoutBody(Action<Ui> body)
		{
			_body = body;
		}

		#endregion

		#region Localization

		internal string _(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				return key;

			var provider = UiServices.GetService<ILocalizationProvider>();
			if (provider == null)
				return key;
			return provider.GetText(key);
		}

		#endregion

		public void ChangePage(string template)
		{
			Instructions.Add(new ChangePageInstruction(template));
		}
	}
}
