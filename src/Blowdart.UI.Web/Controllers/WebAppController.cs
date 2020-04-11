using System.IO;
using System.Reflection;
using System.Text;
using Blowdart.UI.Internal;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SharpScss;
using TypeKitchen;

namespace Blowdart.UI.Web.Controllers
{
	public class WebAppController : Controller
	{
		private readonly IThemeProvider _themeProvider;
		private readonly IWebHostEnvironment _environment;
		private readonly IOptionsSnapshot<BlowdartOptions> _options;

		public WebAppController(IThemeProvider themeProvider, IWebHostEnvironment environment, IOptionsSnapshot<BlowdartOptions> options)
		{
			_themeProvider = themeProvider;
			_environment = environment;
			_options = options;
		}

		[Route("robots.txt")]
		public ContentResult Robots()
		{
			var content = Pooling.StringBuilderPool.Scoped(sb =>
			{
				sb.AppendLine($"# NoIndex = {_options.Value.App.RobotsTxt.DisallowAll.ToString().ToLower()}");
				sb.AppendLine("user-agent: *");
				sb.AppendLine(_options.Value.App.RobotsTxt.DisallowAll ? "Disallow: /" : "Disallow:");
			});

			return Content(content, "text/plain", Encoding.UTF8);
		}

		[Route("theme.{name}.css")]
		public IActionResult Theme(string name = "Default")
		{
			var theme = _themeProvider.GetTheme(name);
			if (theme == null)
				return NotFound();

			if (!(_environment.WebRootFileProvider is CompositeFileProvider composite))
				return NotFound();

			foreach (var provider in composite.FileProviders)
			{
				var type = provider.GetType();

				if (type.Name != "StaticWebAssetsFileProvider")
					continue;

				// _variables.scss
				var variables = GenerateVariables(theme);

				// _bootswatch.scss
				var bootswatch = GenerateSwatch();

				// FIXME: https://github.com/aspnet/AspNetCore/pull/14803
				var property = type.GetProperty("InnerProvider", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if (property == null)
					continue;
				var inner = (PhysicalFileProvider) property.GetValue(provider);

				var options = new ScssOptions();
				options.IncludePaths.Add(Path.Combine(inner.Root, "css"));
				options.IncludePaths.Add(Path.Combine(inner.Root, "css\\theme"));
				options.IncludePaths.Add(Path.Combine(inner.Root, "lib\\bootstrap\\scss"));
				
				var scss = variables + bootswatch;
				var result = Scss.ConvertToCss(scss, options);
				var content = result.Css;

				return Content(content, "text/css", Encoding.UTF8);
			}

			return NotFound();
		}

		private static string GenerateSwatch()
		{
			return @"
// Variables ===================================================================

$web-font-path: ""https://fonts.googleapis.com/css?family=Lato:400,700,400italic"" !default;
				@import url($web-font-path);

// Navbar ======================================================================

.bg-primary {
  .navbar-nav.active > .nav-link {
						color: $success!important;
					}
				}

.bg-light {
					&.navbar {
						background-color: $success!important;
					}

					&.navbar-light.navbar-nav {
    .nav-link:focus,
    .nav-link:hover,
    .active > .nav-link {
							color: $primary!important;
						}
					}
				}

// Buttons =====================================================================

// Typography ==================================================================

.blockquote {
	&-footer {
		color: $gray-600;
	}
}

// Tables ======================================================================

.table {

	&-primary {
		&, > th, > td {
			background-color: $primary;
		}
	}

	&-secondary {
		&, > th, > td {
			background-color: $secondary;
		}
	}

	&-light {
		&, > th, > td {
			background-color: $light;
		}
	}

	&-dark {
		&, > th, > td {
			background-color: $dark;
		}
	}

	&-success {
		&, > th, > td {
			background-color: $success;
		}
	}

	&-info {
		&, > th, > td {
			background-color: $info;
		}
	}

	&-danger {
		&, > th, > td {
			background-color: $danger;
		}
	}

	&-warning {
		&, > th, > td {
			background-color: $warning;
		}
	}

	&-active {
		&, > th, > td {
			background-color: $table-active-bg;
		}
	}

	&-hover {

    .table-primary:hover {
							&, > th, > td {
								background-color: darken($primary, 5%);
							}
						}

    .table-secondary:hover {
							&, > th, > td {
								background-color: darken($secondary, 5%);
							}
						}

    .table-light:hover {
							&, > th, > td {
								background-color: darken($light, 5%);
							}
						}

    .table-dark:hover {
							&, > th, > td {
								background-color: darken($dark, 5%);
							}
						}

    .table-success:hover {
							&, > th, > td {
								background-color: darken($success, 5%);
							}
						}

    .table-info:hover {
							&, > th, > td {
								background-color: darken($info, 5%);
							}
						}

    .table-danger:hover {
							&, > th, > td {
								background-color: darken($danger, 5%);
							}
						}

    .table-warning:hover {
							&, > th, > td {
								background-color: darken($warning, 5%);
							}
						}

    .table-active:hover {
							&, > th, > td {
								background-color: $table-active-bg;
							}
						}

					}
				}

// Forms =======================================================================

.input-group-addon {
					color: #fff;
}

// Navs ========================================================================

.nav-tabs,
.nav-pills {

  .nav-link,
  .nav-link.active,
  .nav-link.active:focus,
  .nav-link.active:hover,
  .nav-item.open.nav-link,
  .nav-item.open.nav-link:focus,
  .nav-item.open.nav-link:hover {
						color: #fff;
  }
				}

.breadcrumb a {
					color: #fff;
}

.pagination {
					a:
					hover {
						text-decoration: none;
					}
				}

// Indicators ==================================================================

.close {
					opacity:
					0.4;

					&:hover,
  &:focus {
						opacity:
						1;
					}
				}

.alert {
					border:
					none;
					color: $white;

					a,
  .alert-link {
						color: #fff;
    text-decoration: underline;
					}

					@each $color, $value in $theme-colors {
						&-#{$color} {
      @if $enable-gradients {
							background: $value linear-gradient(180deg, mix($white, $value, 15%), $value) repeat-x;
						}
						@else {
							background-color: $value;
						}
					}
				}
			}

// Progress bars ===============================================================

// Containers ==================================================================


.list-group-item-action {
				color: #fff;

  &:hover,
  &:focus {
					background-color: $gray-700;
					color: #fff;
  }

  .list-group-item-heading {
					color: #fff;
  }
			}
			";
		}

		private static string GenerateVariables(Theme theme)
		{
			return $@"
// Darkly 4.3.1
// Bootswatch

//
// Color system
//

$white:    {theme.White.ToCss()} !default;
$gray-100: {theme.Gray100.ToCss()} !default;
$gray-200: {theme.Gray200.ToCss()} !default;
$gray-300: {theme.Gray300.ToCss()} !default;
$gray-400: {theme.Gray400.ToCss()} !default;
$gray-500: {theme.Gray500.ToCss()} !default;
$gray-600: {theme.Gray600.ToCss()} !default;
$gray-700: {theme.Gray700.ToCss()}!default;
$gray-800: {theme.Gray800.ToCss()} !default;
$gray-900: {theme.Gray900.ToCss()} !default;
$black:    {theme.Black.ToCss()} !default;

$blue:    {theme.Blue.ToCss()} !default;
$indigo:  {theme.Indigo.ToCss()} !default;
$purple:  {theme.Purple.ToCss()} !default;
$pink:    {theme.Pink.ToCss()} !default;
$red:     {theme.Red.ToCss()} !default;
$orange:  {theme.Orange.ToCss()} !default;
$yellow:  {theme.Yellow.ToCss()} !default;
$green:   {theme.Green.ToCss()} !default;
$teal:    {theme.Teal.ToCss()} !default;
$cyan:    {theme.Cyan.ToCss()} !default;

$primary:       {theme.Primary.ToCss()} !default;
$secondary:     {theme.Secondary.ToCss()} !default;
$success:       {theme.Success.ToCss()} !default;
$info:          {theme.Info.ToCss()} !default;
$warning:       {theme.Warning.ToCss()} !default;
$danger:        {theme.Danger.ToCss()} !default;
$light:         {theme.Light.ToCss()} !default;
$dark:          {theme.Dark.ToCss()} !default;

$yiq-contrasted-threshold:  175 !default;

// Body

$body-bg:                   {theme.BodyBackground.ToCss()} !default;
$body-color:                {theme.BodyForeground.ToCss()} !default;

// Links

$link-color:                {theme.LinkColor.ToCss()} !default;

// Fonts

$font-family-sans-serif:      ""Lato"", -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif, ""Apple Color Emoji"", ""Segoe UI Emoji"", ""Segoe UI Symbol"" !default;
$font-size-base:              0.9375rem !default;

$h1-font-size:                3rem !default;
$h2-font-size:                2.5rem !default;
$h3-font-size:                2rem !default;

// Tables

$table-accent-bg:             {theme.TableAccent.ToCss()} !default;
$table-border-color:          {theme.TableBorder.ToCss()} !default;

// Forms

$input-border-color:                {theme.InputBorder.ToCss()} !default;
$input-group-addon-color:           {theme.InputGroupAddOnForeground.ToCss()} !default;
$input-group-addon-bg:              {theme.InputGroupAddOnBackground.ToCss()} !default;
$custom-file-color:                 {theme.CustomFileForeground.ToCss()} !default;
$custom-file-border-color:          {theme.CustomFileBorder.ToCss()} !default;

// Dropdowns

$dropdown-bg:                       {theme.DropdownBackground.ToCss()} !default;
$dropdown-border-color:             {theme.DropdownBorder.ToCss()} !default;
$dropdown-divider-bg:               {theme.DropdownDividerBackground.ToCss()} !default;

$dropdown-link-color:               {theme.DropdownLink.ToCss()} !default;
$dropdown-link-hover-color:         {theme.DropdownLinkHover.ToCss()} !default;
$dropdown-link-hover-bg:            {theme.DropdownLinkHoverBackground.ToCss()} !default;

// Navs

$nav-link-padding-x:                2rem !default;
$nav-link-disabled-color:           {theme.NavLinkDisabled.ToCss()} !default;

$nav-tabs-border-color:             {theme.NavTabsBorder.ToCss()} !default;
$nav-tabs-link-hover-border-color:  {theme.NavTabsLinkHoverBorder.ToCss()} {theme.NavTabsLinkHoverBorder.ToCss()} transparent !default;
$nav-tabs-link-active-color:        {theme.NavTabsLinkActive.ToCss()} !default;
$nav-tabs-link-active-border-color: {theme.NavTabsLinkActiveBorder.ToCss()} {theme.NavTabsLinkActiveBorder.ToCss()} transparent !default;

// Navbar

$navbar-padding-y:                  1rem !default;

$navbar-dark-color:                 {theme.NavBarDark.ToCss()} !default;
$navbar-dark-hover-color:           {theme.NavBarDarkHover.ToCss()}  !default;

$navbar-light-color:                {theme.NavBarLight.ToCss()}  !default;
$navbar-light-hover-color:          {theme.NavBarLightHover.ToCss()} !default;
$navbar-light-active-color:         {theme.NavBarLightActive.ToCss()} !default;
$navbar-light-disabled-color:       {theme.NavBarLightDisabled.ToCss()} !default;
$navbar-light-toggler-border-color: {theme.NavBarLightTogglerBorder.ToCss()} !default;

// Pagination

$pagination-color:                  {theme.PaginationForeground.ToCss()} !default;
$pagination-bg:                     {theme.PaginationBackground.ToCss()} !default;
$pagination-border-width:           0 !default;
$pagination-border-color:           {theme.PaginationBorder.ToCss()} !default;

$pagination-hover-color:            {theme.PaginationHoverForeground.ToCss()} !default;
$pagination-hover-bg:               {theme.PaginationHoverBackground.ToCss()} !default;
$pagination-hover-border-color:     {theme.PaginationBorder.ToCss()} !default;

$pagination-active-bg:              {theme.PaginationActiveBackground.ToCss()} !default;
$pagination-active-border-color:    {theme.PaginationActiveBorder.ToCss()} !default;

$pagination-disabled-color:         {theme.PaginationDisabledForeground.ToCss()} !default;
$pagination-disabled-bg:            {theme.PaginationDisabledBackground.ToCss()} !default;
$pagination-disabled-border-color:  {theme.PaginationDisabledBorder.ToCss()} !default;

// Jumbotron

$jumbotron-bg:                      {theme.JumbotronBackground.ToCss()} !default;

// Cards

$card-cap-bg:                       {theme.CardCapBackground.ToCss()} !default;
$card-bg:                           {theme.CardBackground.ToCss()} !default;

// Popovers

$popover-bg:                        {theme.PopoverBackground.ToCss()} !default;

$popover-header-bg:                 {theme.PopoverHeaderBackground.ToCss()} !default;

// Modals

$modal-content-bg:                  {theme.ModalContentBackground.ToCss()} !default;
$modal-content-border-color:        {theme.ModalContentBorder.ToCss()} !default;

$modal-header-border-color:         {theme.ModalHeaderBorder.ToCss()} !default;

// Progress bars

$progress-height:                   0.625rem !default;
$progress-font-size:                0.625rem !default;
$progress-bg:                       {theme.ProgressBackground.ToCss()} !default;

// List group

$list-group-bg:                     {theme.ListGroupBackground.ToCss()} !default;
$list-group-border-color:           {theme.ListGroupBorder.ToCss()} !default;

$list-group-hover-bg:               {theme.ListGroupHoverBackground.ToCss()}!default;

// Breadcrumbs

$breadcrumb-bg:                     {theme.BreadcrumbBackground.ToCss()} !default;

// Close

$close-color:                       {theme.CloseForeground.ToCss()} !default;
$close-text-shadow:                 none !default;

// Code

$pre-color:                         inherit !default;

@import ""../blowdart.theme.scss"";
				";
		}
	}
}
