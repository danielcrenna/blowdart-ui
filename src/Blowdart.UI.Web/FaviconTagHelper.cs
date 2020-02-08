// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Blowdart.UI.Web.Core.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using TypeKitchen;

namespace Blowdart.UI.Web
{
	[HtmlTargetElement("favicons")]
	public class FaviconTagHelper : TagHelper
	{
		private const string DefaultLocation = @"_content/Blowdart.UI.Web/favicons";

		private readonly IHtmlHelper _htmlHelper;
		private readonly IOptionsSnapshot<BlowdartOptions> _options;

		public FaviconTagHelper(IHtmlHelper htmlHelper, IOptionsSnapshot<BlowdartOptions> options)
		{
			_htmlHelper = htmlHelper;
			_options = options;
		}
		
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			((IViewContextAware) _htmlHelper).Contextualize(ViewContext);

			var tags = Pooling.StringBuilderPool.Scoped(sb =>
			{
				var location = _options.Value.App.FaviconsLocation?? DefaultLocation;

				sb.AppendLine($@"<!-- Favicons -->
	<link rel=""apple-touch-icon"" sizes=""57x57"" href=""{location}/apple-icon-57x57.png"">
	<link rel=""apple-touch-icon"" sizes=""60x60"" href=""{location}/apple-icon-60x60.png"">
	<link rel=""apple-touch-icon"" sizes=""72x72"" href=""{location}/apple-icon-72x72.png"">
	<link rel=""apple-touch-icon"" sizes=""76x76"" href=""{location}/apple-icon-76x76.png"">
	<link rel=""apple-touch-icon"" sizes=""114x114"" href=""{location}/apple-icon-114x114.png"">
	<link rel=""apple-touch-icon"" sizes=""120x120"" href=""{location}/apple-icon-120x120.png"">
	<link rel=""apple-touch-icon"" sizes=""144x144"" href=""{location}/apple-icon-144x144.png"">
	<link rel=""apple-touch-icon"" sizes=""152x152"" href=""{location}/apple-icon-152x152.png"">
	<link rel=""apple-touch-icon"" sizes=""180x180"" href=""{location}/apple-icon-180x180.png"">
	<link rel=""icon"" type=""image/png"" sizes=""192x192"" href=""{location}/android-icon-192x192.png"">
	<link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""{location}/favicon-32x32.png"">
	<link rel=""icon"" type=""image/png"" sizes=""96x96"" href=""{location}/favicon-96x96.png"">
	<link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""{location}/favicon-16x16.png"">
	<link rel=""manifest"" href=""{location}/manifest.json"">
	<meta name=""msapplication-TileColor"" content=""#ffffff"">
	<meta name=""msapplication-TileImage"" content=""{location}/ms-icon-144x144.png"">
	<meta name=""theme-color"" content=""#ffffff"">");
			});

			output.Content.SetHtmlContent(tags);
			output.TagName = null;

			return Task.CompletedTask;
		}
	}
}