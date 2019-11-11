using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.WinForms
{
    public class ImGui : Form
    {
	    private IContainer _components;
	    
	    private readonly Panel _panel;
		private readonly PageMap _pages;
	    private readonly Ui _ui;
	    private readonly FormRenderTarget _target;

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
		    Controls.Add(_panel);

			_pages = serviceProvider.GetRequiredService<PageMap>();
		    _ui = new Ui(_target) {UiServices = serviceProvider};
		    InitializeComponent(title);
	    }

	    private void Begin()
	    {
		    _ui.Begin();
		    _target.Begin();
	    }

		private void InitializeComponent(string title)
		{
			_components = new Container();

			Text = title;
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
            
			Begin();

			var handler = _pages.GetHandler("/");
			if (handler != null)
			{
				var layout = _pages.GetLayout("/");
				if (layout != null)
				{
					_ui.SetLayoutBody(handler);
					layout(_ui);
					if (!_ui.CalledLayout)
						throw new BlowdartException("Layout did not call ui.LayoutBody();");
				}
				else
				{
					handler(_ui);
				}

				SuspendLayout();
				_ui.RenderToTarget(_target, _panel);
				ResumeLayout(true);
			}
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
