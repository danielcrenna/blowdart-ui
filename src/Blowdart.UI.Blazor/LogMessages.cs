// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Logging;

namespace Blowdart.UI.Blazor
{
	internal static class LogMessages
	{
		public static class ImGuiRouter
		{
			private static readonly Action<ILogger, string, string, Exception> _displayingNotFound =
				LoggerMessage.Define<string, string>(LogLevel.Debug, new EventId(1, "DisplayingNotFound"), $"Displaying {nameof(Blazor.ImGuiRouter.NotFound)} because path '{{Path}}' with base URI '{{BaseUri}}' does not match any component route");

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