﻿using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Blowdart.UI;
using Demo.Examples.Pages;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials;

namespace Blowdart.Ui
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class ImGui : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
	    private readonly UI.Ui _ui;

	    public ImGui()
	    {
		    var target = new AndroidRenderTarget();
		    target.RegisterRenderers(this);
		    _ui = new UI.Ui(target);

			var pageMap = new PageMap();

			var services = new ServiceCollection();
			services.AddSingleton(pageMap);

			var builder = new BlowdartBuilder(pageMap, services);
            builder.AddPage("/", ElementsPage.ToastsTab);
            
			_ui.UiServices = services.BuildServiceProvider();
		}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            var layout = RenderPage();
            SetContentView(layout);
		}

        private LinearLayout RenderPage()
        {
	        var layout = new LinearLayout(this) {Orientation = Orientation.Vertical};
	        var pageMap = _ui.GetRequiredService<PageMap>();
	        var handler = pageMap.GetHandler("/");
	        Begin();
	        handler(_ui);
	        _ui.RenderToTarget(layout);
	        return layout;
        }

        private void Begin()
        {
	        _ui.Begin();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, 
	        [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OnEvent(Value128 id, string eventType, object data)
        {
	        _ui.AddEvent(eventType, id, data);
	        var layout = RenderPage();
			SetContentView(layout);
		}

        public void OnClick(Value128 id)
        {
	        OnEvent(id, DomEvents.OnClick, null);
        }

        public EventHandler OnClickCallback(Value128 id)
        {
	        return (sender, args) => OnClick(id);
        }
	}
}
