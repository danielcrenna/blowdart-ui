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
		    _target = new FormRenderTarget(this);

		    _panel = new FlowLayoutPanel
		    {
				FlowDirection = FlowDirection.TopDown,
				AutoSize = true,
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				Size = Size,
				Dock = DockStyle.Fill,
				Location = Point.Empty
		    };

			_pages = serviceProvider.GetRequiredService<PageMap>();
		    _ui = new Ui {UiServices = serviceProvider};
		    InitializeComponent(title);
	    }

	    private void Begin()
	    {
		    _ui.Begin();
		    _target.Begin();
	    }

		private void InitializeComponent(string title)
		{
			Text = title;
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
            _components = new Container();

			Controls.Add(_panel);

			Begin();

			var handler = _pages.GetHandler("/");
			handler(_ui);
			
			SuspendLayout();

			_ui.RenderToTarget(_target, _panel);

			ResumeLayout(true);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_components?.Dispose();
			base.Dispose(disposing);
		}
	}
}
