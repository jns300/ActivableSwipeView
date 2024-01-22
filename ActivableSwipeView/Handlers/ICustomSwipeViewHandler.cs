#if IOS || MACCATALYST
//using PlatformView = ActivableSwipeView.Custom.CustomMauiSwipeView;
#elif ANDROID
using PlatformView = ActivableSwipeView.Views.CustomMauiSwipeView;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.SwipeControl;
#elif TIZEN
using PlatformView = Microsoft.Maui.Platform.MauiSwipeView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif


namespace ActivableSwipeView.Handlers
{
#if ANDROID
    internal partial interface ICustomSwipeViewHandler : IViewHandler, IElementHandler
    {
        new ISwipeView VirtualView { get; }
        new PlatformView PlatformView { get; }
    }
#endif
}
