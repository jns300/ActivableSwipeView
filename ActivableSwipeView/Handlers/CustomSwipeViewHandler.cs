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

using Microsoft.Maui.Handlers;
using ActivableSwipeView.Views;

namespace ActivableSwipeView.Handlers
{
#if ANDROID
    internal partial class CustomSwipeViewHandler : ViewHandler<ISwipeView, PlatformView>, ICustomSwipeViewHandler
    {
        public static IPropertyMapper<ISwipeView, ICustomSwipeViewHandler> Mapper = new PropertyMapper<ISwipeView, ICustomSwipeViewHandler>(ViewHandler.ViewMapper)
        {
            [nameof(IContentView.Content)] = MapContent,
            [nameof(ISwipeView.SwipeTransitionMode)] = MapSwipeTransitionMode,
            [nameof(ISwipeView.LeftItems)] = MapLeftItems,
            [nameof(ISwipeView.TopItems)] = MapTopItems,
            [nameof(ISwipeView.RightItems)] = MapRightItems,
            [nameof(ISwipeView.BottomItems)] = MapBottomItems,
#if ANDROID || IOS || TIZEN
			[nameof(IView.IsEnabled)] = MapIsEnabled,
#endif
#if ANDROID
			[nameof(IView.Background)] = MapBackground,
#endif
        };

        public static CommandMapper<ISwipeView, ICustomSwipeViewHandler> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(ISwipeView.RequestOpen)] = MapRequestOpen,
            [nameof(ISwipeView.RequestClose)] = MapRequestClose,
        };

        public CustomSwipeViewHandler() : base(Mapper, CommandMapper)
        {

        }

        public CustomSwipeViewHandler(IPropertyMapper? mapper)
            : base(mapper ?? Mapper, CommandMapper)
        {
        }

        public CustomSwipeViewHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
            : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
        {
        }

        ISwipeView ICustomSwipeViewHandler.VirtualView => VirtualView;

        PlatformView ICustomSwipeViewHandler.PlatformView => PlatformView;
    }
#endif
}
