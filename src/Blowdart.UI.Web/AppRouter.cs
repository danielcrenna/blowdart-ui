// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using Blowdart.UI.Web.Extensions;

namespace Blowdart.UI.Web
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class AppRouter : IComponent, IHandleAfterRender, IDisposable
    {
        private static readonly char[] QueryFragmentTokens = { '?', '#' };

        private RenderHandle _renderHandle;
        private string _baseUri;
        private string _locationAbsolute;
        private bool _navigationInterceptionEnabled;
        private ILogger<AppRouter> _logger;
        private RenderFragment _renderFragment;

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
            _logger = LoggerFactory.CreateLogger<AppRouter>();
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (NotFound == null)
                throw new InvalidOperationException($"The {nameof(AppRouter)} component requires a value for the parameter {nameof(NotFound)}.");

            Refresh(false);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
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
                _renderFragment = builder =>
                {
                    builder.OpenComponent<ImGui>();
                    builder.AddAttribute(nameof(ImGui.Handler), handler);
                    builder.CloseComponent();
                };
            }
            
            if (_renderFragment != null)
            {
                Log.NavigatingToComponent(_logger, locationPath, _baseUri);
				_renderHandle.Render(_renderFragment);
            }
            else
            {
                if (!isNavigationIntercepted)
                {
                    Log.DisplayingNotFound(_logger, locationPath, _baseUri);
                    _renderHandle.Render(NotFound);
                }
                else
                {
                    Log.NavigatingToExternalUri(_logger, _locationAbsolute, locationPath, _baseUri);
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

        public Task OnAfterRenderAsync()
        {
            if (_navigationInterceptionEnabled)
                return Task.CompletedTask;
            _navigationInterceptionEnabled = true;
            return NavigationInterception.EnableNavigationInterceptionAsync();
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, string, Exception> _displayingNotFound =
                LoggerMessage.Define<string, string>(LogLevel.Debug, new EventId(1, "DisplayingNotFound"), $"Displaying {nameof(NotFound)} because path '{{Path}}' with base URI '{{BaseUri}}' does not match any component route");

            private static readonly Action<ILogger, Type, string, string, Exception> _navigatingToComponent =
                LoggerMessage.Define<Type, string, string>(LogLevel.Debug, new EventId(2, "NavigatingToComponent"), "Navigating to component {ComponentType} in response to path '{Path}' with base URI '{BaseUri}'");

            private static readonly Action<ILogger, string, string, string, Exception> _navigatingToExternalUri =
                LoggerMessage.Define<string, string, string>(LogLevel.Debug, new EventId(3, "NavigatingToExternalUri"), "Navigating to non-component URI '{ExternalUri}' in response to path '{Path}' with base URI '{BaseUri}'");

            internal static void DisplayingNotFound(ILogger logger, string path, string baseUri)
            {
                _displayingNotFound(logger, path, baseUri, null);
            }

            internal static void NavigatingToComponent(ILogger logger, string path, string baseUri)
            {
                _navigatingToComponent(logger, typeof(ImGui), path, baseUri, null);
            }

            internal static void NavigatingToExternalUri(ILogger logger, string externalUri, string path, string baseUri)
            {
                _navigatingToExternalUri(logger, externalUri, path, baseUri, null);
            }
        }
    }
}
