// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable UnusedAutoPropertyAccessor.Local

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI.Blazor
{
	public sealed class ImGuiRouter : IComponent, IHandleAfterRender, IDisposable
	{
		private static readonly char[] QueryFragmentTokens = {'?', '#'};
		private string _baseUri;
		private string _locationAbsolute;
		private ILogger<ImGui> _logger;
		private bool _navigationInterceptionEnabled;
		private RenderFragment _renderFragment;

		private RenderHandle _renderHandle;

		[Inject] private PageMap Pages { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }
		[Inject] private INavigationInterception NavigationInterception { get; set; }
		[Inject] private ILoggerFactory LoggerFactory { get; set; }

		[Parameter] public RenderFragment NotFound { get; set; }

		public void Attach(RenderHandle renderHandle)
		{
			_renderHandle = renderHandle;
			_baseUri = NavigationManager.BaseUri;
			_locationAbsolute = NavigationManager.Uri;
			_logger = LoggerFactory.CreateLogger<ImGui>();
			NavigationManager.LocationChanged += OnLocationChanged;
		}

		public Task SetParametersAsync(ParameterView parameters)
		{
			parameters.SetParameterProperties(this);

			if (NotFound == null)
				throw new InvalidOperationException(
					$"The {nameof(ImGui)} component requires a value for the parameter {nameof(NotFound)}.");

			Refresh(false);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			NavigationManager.LocationChanged -= OnLocationChanged;
		}

		public Task OnAfterRenderAsync()
		{
			if (_navigationInterceptionEnabled)
				return Task.CompletedTask;
			_navigationInterceptionEnabled = true;
			return NavigationInterception.EnableNavigationInterceptionAsync();
		}

		private static string StringUntilAny(string str, char[] chars)
		{
			var firstIndex = str.IndexOfAny(chars);
			return firstIndex < 0
				? str
				: str.Substring(0, firstIndex);
		}

		private void Refresh(bool isNavigationIntercepted)
		{
			var locationPath = NavigationManager.ToBaseRelativePath(_locationAbsolute);
			locationPath = StringUntilAny(locationPath, QueryFragmentTokens);

			_locationAbsolute = NavigationManager.Uri;
			NavigationManager.LocationChanged += OnLocationChanged;

			var path = $"/{locationPath}";

			var handler = Pages.GetHandler(path);

			if (handler != null)
			{
				var layout = Pages.GetLayout(path);
				if (layout != null)
				{
					_renderFragment = builder =>
					{
						builder.OpenComponent<ImGui>();
						builder.AddAttribute(nameof(ImGui.Layout), layout);
						builder.AddAttribute(nameof(ImGui.Handler), handler);
						builder.CloseComponent();
					};
				}
				else
				{
					_renderFragment = builder =>
					{
						builder.OpenComponent<ImGui>();
						builder.AddAttribute(nameof(ImGui.Handler), handler);
						builder.CloseComponent();
					};
				}
			}

			if (_renderFragment != null)
			{
				LogMessages.ImGuiRouter.NavigatingToComponent(_logger, locationPath, _baseUri);
				_renderHandle.Render(_renderFragment);
			}
			else
			{
				if (!isNavigationIntercepted)
				{
					LogMessages.ImGuiRouter.DisplayingNotFound(_logger, locationPath, _baseUri);
					_renderHandle.Render(NotFound);
				}
				else
				{
					LogMessages.ImGuiRouter.NavigatingToExternalUri(_logger, _locationAbsolute, locationPath, _baseUri);
					NavigationManager.NavigateTo(_locationAbsolute, true);
				}
			}

			NavigationManager.LocationChanged -= OnLocationChanged;
		}

		private void OnLocationChanged(object sender, LocationChangedEventArgs args)
		{
			_locationAbsolute = args.Location;
			if (_renderHandle.IsInitialized)
			{
				Refresh(args.IsNavigationIntercepted);
			}
		}
	}
}