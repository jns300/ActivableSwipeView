using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Threading.Tasks;

namespace ActivableSwipeView.Helper
{
    internal static partial class ViewExtensions
	{
		public static void UpdateIsEnabled(this object platformView, IView view) { }

		public static void Focus(this object platformView, FocusRequest request) { }

		public static void Unfocus(this object platformView, IView view) { }

		public static void UpdateVisibility(this object platformView, IView view) { }

		public static Task UpdateBackgroundImageSourceAsync(this object platformView, IImageSource? imageSource, IImageSourceServiceProvider? provider)
			=> Task.CompletedTask;

		public static void UpdateBackground(this object platformView, IView view) { }

		public static void UpdateClipsToBounds(this object platformView, IView view) { }

		public static void UpdateAutomationId(this object platformView, IView view) { }

		public static void UpdateClip(this object platformView, IView view) { }

		public static void UpdateShadow(this object platformView, IView view) { }

		public static void UpdateBorder(this object platformView, IView view) { }

		public static void UpdateOpacity(this object platformView, IView view) { }

		internal static void UpdateOpacity(this object platformView, double opacity) { }

		public static void UpdateSemantics(this object platformView, IView view) { }

		public static void UpdateFlowDirection(this object platformView, IView view) { }

		public static void UpdateTranslationX(this object platformView, IView view) { }

		public static void UpdateTranslationY(this object platformView, IView view) { }

		public static void UpdateScale(this object platformView, IView view) { }

		public static void UpdateRotation(this object platformView, IView view) { }

		public static void UpdateRotationX(this object platformView, IView view) { }

		public static void UpdateRotationY(this object platformView, IView view) { }

		public static void UpdateAnchorX(this object platformView, IView view) { }

		public static void UpdateAnchorY(this object platformView, IView view) { }

		public static void InvalidateMeasure(this object platformView, IView view) { }

		public static void UpdateWidth(this object platformView, IView view) { }

		public static void UpdateHeight(this object platformView, IView view) { }

		public static void UpdateMinimumHeight(this object platformView, IView view) { }

		public static void UpdateMaximumHeight(this object platformView, IView view) { }

		public static void UpdateMinimumWidth(this object platformView, IView view) { }

		public static void UpdateMaximumWidth(this object platformView, IView view) { }

		internal static Microsoft.Maui.Graphics.Rect GetPlatformViewBounds(this IView view) => view.Frame;

		internal static System.Numerics.Matrix4x4 GetViewTransform(this IView view) => new System.Numerics.Matrix4x4();

		internal static Microsoft.Maui.Graphics.Rect GetBoundingBox(this IView view) => view.Frame;

        internal static Element? GetParent(this View? view)
        {
            return view?.Parent;
        }

        internal static Element? GetParent(this Element? view)
        {
            return view?.Parent;
        }

        internal static IWindow? GetHostedWindow(this IView? view)
			=> null;

		public static void UpdateInputTransparent(this object nativeView, IViewHandler handler, IView view) { }
	}
}
