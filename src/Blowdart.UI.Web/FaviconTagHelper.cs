// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TypeKitchen;

namespace Blowdart.UI.Web
{
	/*
		
	 */

	[HtmlTargetElement("favicons")]
	public class FaviconTagHelper : TagHelper
	{
		private readonly IHtmlHelper _htmlHelper;
		private readonly BlowdartService _service;

		public FaviconTagHelper(IHtmlHelper htmlHelper, BlowdartService service)
		{
			_htmlHelper = htmlHelper;
			_service = service;
		}

		[HtmlAttributeName("location")]
		public string Location { get; set; }

		public object Parameters { get; set; }

		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			((IViewContextAware) _htmlHelper).Contextualize(ViewContext);

			var tags = Pooling.StringBuilderPool.Scoped(sb =>
			{
				sb.AppendLine($@"<!-- Favicons -->
	<link rel=""apple-touch-icon"" sizes=""57x57"" href=""{Location}/apple-icon-57x57.png"">
	<link rel=""apple-touch-icon"" sizes=""60x60"" href=""{Location}/apple-icon-60x60.png"">
	<link rel=""apple-touch-icon"" sizes=""72x72"" href=""{Location}/apple-icon-72x72.png"">
	<link rel=""apple-touch-icon"" sizes=""76x76"" href=""{Location}/apple-icon-76x76.png"">
	<link rel=""apple-touch-icon"" sizes=""114x114"" href=""{Location}/apple-icon-114x114.png"">
	<link rel=""apple-touch-icon"" sizes=""120x120"" href=""{Location}/apple-icon-120x120.png"">
	<link rel=""apple-touch-icon"" sizes=""144x144"" href=""{Location}/apple-icon-144x144.png"">
	<link rel=""apple-touch-icon"" sizes=""152x152"" href=""{Location}/apple-icon-152x152.png"">
	<link rel=""apple-touch-icon"" sizes=""180x180"" href=""{Location}/apple-icon-180x180.png"">
	<link rel=""icon"" type=""image/png"" sizes=""192x192"" href=""{Location}/android-icon-192x192.png"">
	<link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""{Location}/favicon-32x32.png"">
	<link rel=""icon"" type=""image/png"" sizes=""96x96"" href=""{Location}/favicon-96x96.png"">
	<link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""{Location}/favicon-16x16.png"">
	<link rel=""manifest"" href=""{Location}/manifest.json"">
	<meta name=""msapplication-TileColor"" content=""#ffffff"">
	<meta name=""msapplication-TileImage"" content=""{Location}/ms-icon-144x144.png"">
	<meta name=""theme-color"" content=""#ffffff"">");
			});

			output.Content.SetHtmlContent(tags);
			
			output.TagName = null;
		}
	}
}