using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.WinForms
{
    public partial class ImGui : Form
    {
	    private IContainer _components;
	    
	    private readonly Panel _panel;
		private readonly PageMap _pages;
	    private readonly Ui _ui;
	    private readonly FormRenderTarget _target;
	    private Action<Ui> _layout;
	    private Action<Ui> _handler;

	    public ImGui(string title, IServiceProvider serviceProvider)
	    {
		    Icon = Resources.icon;

			_target = new FormRenderTarget();
			_target.RegisterRenderers(this);

		    _panel = new FlowLayoutPanel
		    {
				FlowDirection = FlowDirection.TopDown,
				AutoSize = true,
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				Size = Size,
				Dock = DockStyle.Fill,
				Location = Point.Empty
		    };
			_panel.ControlAdded += OnControlAdded;
			Controls.Add(_panel);

			_pages = serviceProvider.GetRequiredService<PageMap>();
		    _ui = new Ui(_target) {UiServices = serviceProvider};
		    InitializeComponent(title);
	    }

		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.Control.Name))
				e.Control.Name = _ui.NextId().ToString();
		}

		private void InitializeComponent(string title)
		{
			_components = new Container();

			Text = title;
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);

            ChangePage("/");
		}

		internal void ChangePage(string template)
		{
			_handler = _pages.GetHandler(template);
			_layout  = _pages.GetLayout(template);
			RenderPage();
		}

		private void Begin()
		{
			_ui.Begin();
			_target.Begin();
			_panel.Controls.Clear();
		}

		private void RenderPage()
		{
			SuspendLayout();
			Begin();
			if (_handler != null)
			{
				if (_layout != null)
				{
					_ui.SetLayoutBody(_handler);
					_layout(_ui);
					if (!_ui.CalledLayout)
						throw new BlowdartException("Layout did not call ui.LayoutBody();");
				}
				else
				{
					_handler(_ui);
				}
				
				_ui.RenderToTarget(_target, _panel);
			}
			ResumeLayout(true);
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_components?.Dispose();
				_ui?.Dispose();
				_panel?.Dispose();
				_target?.Dispose();
			}
			base.Dispose(disposing);
		}
    }
}
