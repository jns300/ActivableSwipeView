
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ActivableSwipeView.Views;

namespace ActivableSwipeView.Handlers
{
    internal partial class CustomSwipeViewHandler
    {
        protected override CustomMauiSwipeView CreatePlatformView()
        {
            var returnValue = new CustomMauiSwipeView(Context)
            {
                CrossPlatformLayout = VirtualView
            };

            returnValue.SetElement(VirtualView);
            return returnValue;
        }

        public override void SetVirtualView(IView view)
        {
            base.SetVirtualView(view);
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");

            PlatformView.CrossPlatformLayout = VirtualView;
        }

        public static void MapLeftItems(ICustomSwipeViewHandler handler, ISwipeView view)
        {
        }
        public static void MapTopItems(ICustomSwipeViewHandler handler, ISwipeView view)
        {
        }
        public static void MapRightItems(ICustomSwipeViewHandler handler, ISwipeView view)
        {
        }
        public static void MapBottomItems(ICustomSwipeViewHandler handler, ISwipeView view)
        {
        }

        public static void MapContent(ICustomSwipeViewHandler handler, ISwipeView swipeView)
        {
            _ = handler.PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");
            _ = handler.VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

            handler.PlatformView.UpdateContent();
        }

        public static void MapIsEnabled(ICustomSwipeViewHandler handler, ISwipeView swipeView)
        {
            handler.PlatformView.UpdateIsSwipeEnabled(swipeView.IsEnabled);
            ViewHandler.MapIsEnabled(handler, swipeView);
        }

        public static void MapBackground(ICustomSwipeViewHandler handler, ISwipeView swipeView)
        {
            if (swipeView.Background == null)
                handler.PlatformView.Control?.SetWindowBackground();
            else
                ViewHandler.MapBackground(handler, swipeView);
        }

        public static void MapSwipeTransitionMode(ICustomSwipeViewHandler handler, ISwipeView swipeView)
        {
            handler.PlatformView.UpdateSwipeTransitionMode(swipeView.SwipeTransitionMode);
        }

        public static void MapRequestOpen(ICustomSwipeViewHandler handler, ISwipeView swipeView, object? args)
        {
            if (args is not SwipeViewOpenRequest request)
            {
                return;
            }

            handler.PlatformView.OnOpenRequested(request);
        }

        public static void MapRequestClose(ICustomSwipeViewHandler handler, ISwipeView swipeView, object? args)
        {
            if (args is not SwipeViewCloseRequest request)
            {
                return;
            }

            handler.PlatformView.OnCloseRequested(request);
        }
    }
}
