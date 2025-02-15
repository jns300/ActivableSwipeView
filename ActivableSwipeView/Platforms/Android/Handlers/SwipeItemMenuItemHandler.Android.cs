using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using AButton = AndroidX.AppCompat.Widget.AppCompatButton;
using ATextAlignment = Android.Views.TextAlignment;
using AView = Android.Views.View;
using Microsoft.Maui.Platform;
using ActivableSwipeView.Views;
using Microsoft.Maui.Handlers;
using SwipeViewExtensions = ActivableSwipeView.Helper.SwipeViewExtensions;
using ActivableSwipeView.Helper;

namespace ActivableSwipeView.Handlers
{
    internal partial class SwipeItemMenuItemHandler : ElementHandler<ISwipeItemMenuItem, AView>
	{
		protected override void ConnectHandler(AView platformView)
		{
			base.ConnectHandler(platformView);
			platformView.ViewAttachedToWindow += OnViewAttachedToWindow;
		}

		void OnViewAttachedToWindow(object? sender, AView.ViewAttachedToWindowEventArgs e)
		{
			UpdateSize();
		}

		protected override void DisconnectHandler(AView platformView)
		{
			base.DisconnectHandler(platformView);
			platformView.ViewAttachedToWindow -= OnViewAttachedToWindow;
		}

		public static void MapTextColor(global::Platform.Images.ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			(handler.PlatformView as TextView)?.UpdateTextColor(view);
		}

		public static void MapCharacterSpacing(global::Platform.Images.ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			(handler.PlatformView as TextView)?.UpdateCharacterSpacing(view);
		}

		public static void MapFont(global::Platform.Images.ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			var fontManager = ActivableSwipeView.Helper.ElementHandlerExtensions.GetRequiredService<IFontManager>(handler);

			(handler.PlatformView as TextView)?.UpdateFont(view, fontManager);
		}

		public static void MapText(global::Platform.Images.ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{

			(handler.PlatformView as TextView)?.UpdateTextPlainText(view);

			if (handler is SwipeItemMenuItemHandler platformHandler)
				platformHandler.UpdateSize();
		}

		public static void MapBackground(global::Platform.Images.ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			handler.PlatformView.UpdateBackground(handler.VirtualView.Background);

			var textColor = ActivableSwipeView.Helper.SwipeViewExtensions.GetTextColor(handler.VirtualView)?.ToPlatform();

			if (handler.PlatformView is TextView textView)
			{
				if (textColor != null)
					textView.SetTextColor(textColor.Value);

				textView.TextAlignment = ATextAlignment.Center;
			}
		}

		public static void MapVisibility(global::Platform.Images.ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			var swipeView = handler.PlatformView.Parent.GetParentOfType<CustomMauiSwipeView>();
			if (swipeView != null)
				swipeView.UpdateIsVisibleSwipeItem(view);

			handler.PlatformView.Visibility = view.Visibility.ToPlatformVisibility();
		}

		protected override AView CreatePlatformElement()
		{
			_ = MauiContext?.Context ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var swipeButton = new AButton(MauiContext.Context);
			swipeButton.SetOnTouchListener(null);

			if (!string.IsNullOrEmpty(VirtualView.AutomationId))
				swipeButton.ContentDescription = VirtualView.AutomationId;

			return swipeButton;
		}

		static int GetIconSize(global::Platform.Images.ISwipeItemMenuItemHandler handler)
		{
			if (handler.VirtualView is not IImageSourcePart imageSourcePart || imageSourcePart.Source is null)
				return 0;

			var mauiSwipeView = handler.PlatformView.Parent.GetParentOfType<CustomMauiSwipeView>();

			if (mauiSwipeView is null || handler.MauiContext?.Context is null)
				return 0;

			int contentHeight = mauiSwipeView.MeasuredHeight;
			int contentWidth = (int)ActivableSwipeView.Helper.ContextExtensions.ToPixels(handler.MauiContext.Context, SwipeViewExtensions.SwipeItemWidth);

			return Math.Min(contentHeight, contentWidth) / 2;
		}

		void UpdateSize()
		{
			var mauiSwipeView = PlatformView.Parent.GetParentOfType<CustomMauiSwipeView>();

			if (mauiSwipeView == null)
				return;

			var contentHeight = mauiSwipeView.MeasuredHeight;

			var swipeView = VirtualView?.FindParentOfType<ISwipeView>();
			float density = ActivableSwipeView.Helper.ContextExtensions.GetDisplayDensity(mauiSwipeView.Context);

			if (swipeView?.Content is IView content)
			{
				var verticalThickness = (int)(content.Margin.VerticalThickness * density);
				contentHeight -= verticalThickness;
			}

			var lineHeight = 0;

			if (PlatformView is TextView textView)
			{
				lineHeight = !string.IsNullOrEmpty(textView.Text) ? (int)textView.LineHeight : 0;
				var icons = textView.GetCompoundDrawables();
				if (icons.Length > 1 && icons[1] != null)
				{
					SourceLoader.Setter.SetImageSource(icons[1]);
				}
			}

			var iconSize = GetIconSize(this);
			var textPadding = 2 * density;
			var buttonPadding = (int)((contentHeight - (iconSize + lineHeight + textPadding)) / 2);
			PlatformView.SetPadding(0, buttonPadding, 0, buttonPadding);
		}

		partial class SwipeItemMenuItemImageSourcePartSetter
		{
			public override void SetImageSource(Drawable? platformImage)
			{
				if (Handler?.PlatformView is not TextView button || Handler?.VirtualView is not ISwipeItemMenuItem item)
					return;

				if (platformImage is not null)
				{
					var iconSize = GetIconSize(Handler);
					var textColor = ActivableSwipeView.Helper.SwipeViewExtensions.GetTextColor(item)?.ToPlatform();
					int drawableWidth = platformImage.IntrinsicWidth;
					int drawableHeight = platformImage.IntrinsicHeight;

					if (drawableWidth > drawableHeight)
					{
						var iconWidth = iconSize;
						var iconHeight = drawableHeight * iconWidth / drawableWidth;
						platformImage.SetBounds(0, 0, iconWidth, iconHeight);
					}
					else
					{
						var iconHeight = iconSize;
						var iconWidth = drawableWidth * iconHeight / drawableHeight;
						platformImage.SetBounds(0, 0, iconWidth, iconHeight);
					}

					if (textColor != null)
						platformImage.SetColorFilter(textColor.Value, FilterMode.SrcAtop);
				}

				button.SetCompoundDrawables(null, platformImage, null, null);
			}
		}
	}
}