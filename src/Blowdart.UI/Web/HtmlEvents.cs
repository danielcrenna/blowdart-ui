// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable CheckNamespace

/// <summary>
///     HTML Events
///     Source: https://www.w3schools.com/TAGS/ref_eventattributes.asp
/// </summary>
public static class HtmlEvents
{
	public static class Window
	{
		public const string OnAfterPrint = "onafterprint";
		public const string OnBeforePrint = "onbeforeprint";
		public const string OnBeforeUnload = "onbeforeunload";
		public const string OnError = "onerror";
		public const string OnHashChange = "onhashchange";
		public const string OnLoad = "onload";
		public const string OnMessage = "onmessage";
		public const string OnOffline = "onoffline";
		public const string OnOnline = "ononline";
		public const string OnPageHide = "onpagehide";
		public const string OnPageShow = "onpageshow";
		public const string OnPopState = "onpopstate";
		public const string OnResize = "onresize";
		public const string OnStorage = "onstorage";
		public const string OnClick = "onunload";
	}

	public static class Form
	{
		public const string OnBlur = "onblur";
		public const string OnChange = "onchange";
		public const string OnContextMenu = "oncontextmenu";
		public const string OnFocus = "onfocus";
		public const string OnInput = "oninput";
		public const string OnInvalid = "oninvalid";
		public const string OnReset = "onreset";
		public const string OnSearch = "onsearch";
		public const string OnSelect = "onselect";
		public const string OnSubmit = "onsubmit";
	}

	public static class Keyboard
	{
		public const string OnKeyDown = "onkeydown";
		public const string OnKeyPress = "onkeypress";
		public const string OnKeyUp = "onkeyup";
	}

	public static class Mouse
	{
		public const string OnClick = "onclick";
		public const string OnDoubleClick = "ondblclick";
		public const string OnMouseDown = "onmousedown";
		public const string OnMouseMove = "onmousemove";
		public const string OnMouseOut = "onmouseout";
		public const string OnMouseOver = "onmouseover";
		public const string OnMouseUp = "onmouseup";
		public const string OnWheel = "onwheel";
	}

	public static class Drag
	{
		public const string OnDrag = "ondrag";
		public const string OnDragEnd = "ondragend";
		public const string OnDragEnter = "ondragenter";
		public const string OnDragLeave = "ondragleave";
		public const string OnDragOver = "ondragover";
		public const string OnDragStart = "ondragstart";
		public const string OnDrop = "ondrop";
		public const string OnScroll = "onscroll";
	}

	public static class Clipboard
	{
		public const string OnCopy = "oncopy";
		public const string OnCut = "oncut";
		public const string OnPaste = "onpaste";
	}

	public static class Media
	{
		public const string OnAbort = "onabort";
		public const string OnCanPlay = "oncanplay";
		public const string OnCanPlayThrough = "oncanplaythrough";
		public const string OnCueChange = "oncuechange";
		public const string OnDurationChange = "ondurationchange";
		public const string OnEmptied = "onemptied";
		public const string OnEnded = "onended";
		public const string OnError = "onerror";
		public const string OnLoadedData = "onloadeddata";
		public const string OnLoadedMetadata = "onloadedmetadata";
		public const string OnLoadStart = "onloadstart";
		public const string OnPause = "onpause";
		public const string OnPlay = "onplay";
		public const string OnPlaying = "onplaying";
		public const string OnProgress = "onprogress";
		public const string OnRateChange = "onratechange";
		public const string OnSeeked = "onseeked";
		public const string OnSeeking = "onseeking";
		public const string OnStalled = "onstalled";
		public const string OnSuspend = "onsuspend";
		public const string OnTimeUpdate = "ontimeupdate";
		public const string OnVolumeChange = "onvolumechange";
		public const string OnWaiting = "onwaiting";
	}

	public static class Misc
	{
		public const string OnToggle = "ontoggle";
	}
}