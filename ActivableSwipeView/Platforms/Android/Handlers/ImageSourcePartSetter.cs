﻿using System;
#if __IOS__ || MACCATALYST
using PlatformImage = UIKit.UIImage;
#elif MONOANDROID || ANDROID
using PlatformImage = Android.Graphics.Drawables.Drawable;
#elif WINDOWS
using PlatformImage = Microsoft.UI.Xaml.Media.ImageSource;
#elif TIZEN
using PlatformImage = Microsoft.Maui.Platform.MauiImageSource;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformImage = System.Object;
#endif
using Microsoft.Maui.Platform;

namespace ActivableSwipeView.Handlers
{
    internal abstract class ImageSourcePartSetter<T> : IImageSourcePartSetter
		where T : class, IElementHandler
	{
		readonly WeakReference<T> _handler;

		public ImageSourcePartSetter(T handler) =>
			_handler = new(handler);

		public IImageSourcePart? ImageSourcePart =>
			Handler?.VirtualView as IImageSourcePart ?? Handler?.VirtualView as Microsoft.Maui.IImage;

		public T? Handler => _handler.GetTargetOrDefault();

		IElementHandler? IImageSourcePartSetter.Handler => Handler;

		public abstract void SetImageSource(PlatformImage? platformImage);
	}
}
