// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable UnusedAutoPropertyAccessor.Local

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI.Blazor;

public sealed class ImGuiRouter : IComponent, IHandleAfterRender, IDisposable
{
	private static readonly char[] QueryFragmentTokens = ['?', '#'];

	private string _baseUri = null!;
	private string _locationAbsolute = null!;

	private ILogger<ImGui>? _logger;
	private bool _navigationInterceptionEnabled;
	private RenderFragment? _renderFragment;
	private RenderHandle _renderHandle;

	[Inject] private PageMap Pages { get; set; } = null!;
	[Inject] private NavigationManager NavigationManager { get; set; }  = null!;
	[Inject] private INavigationInterception NavigationInterception { get; set; }  = null!;
	[Inject] private ILoggerFactory LoggerFactory { get; set; }  = null!;

	[Parameter] public RenderFragment? NotFound { get; set; }

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
			: str[..firstIndex];
	}

	private void Refresh(bool isNavigationIntercepted)
	{
		var locationPath = NavigationManager.ToBaseRelativePath(_locationAbsolute);
		locationPath = StringUntilAny(locationPath, QueryFragmentTokens);

		_locationAbsolute = NavigationManager.Uri;
		NavigationManager.LocationChanged += OnLocationChanged;

		var route = $"/{locationPath}";

		var handler = Pages.GetHandler(route);

		if (handler != null)
		{
			var layout = Pages.GetLayout(route);
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

				if(NotFound != null)
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

	private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
	{
		_locationAbsolute = args.Location;
		if (_renderHandle.IsInitialized)
			Refresh(args.IsNavigationIntercepted);
	}
}