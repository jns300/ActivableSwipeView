﻿#if __IOS__ || MACCATALYST
using PlatformView = UIKit.UIButton;
#elif MONOANDROID || ANDROID
using PlatformView = Android.Views.View;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.SwipeItem;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.Button;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;

namespace Platform.Images
{
	internal partial interface ISwipeItemMenuItemHandler : IElementHandler
	{
		new ISwipeItemMenuItem VirtualView { get; }
		new PlatformView PlatformView { get; }
#if NETSTANDARD2_0
		ImageSourcePartLoader? SourceLoader { get; }
#else
		ImageSourcePartLoader? SourceLoader => null;
#endif
	}
}
