using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using ActivableSwipeView.Views;
using ActivableSwipeView.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivableSwipeView.Helper
{
    /// <summary>
    /// Represents a helper class which allows to configure this library.
    /// </summary>
    public static class ActivableSwipeViewConfigurator
    {
        /// <summary>
        /// Adds common handlers defined in this library.
        /// </summary>
        /// <param name="builder">the app builder</param>
        /// <returns>the app builder from the argument</returns>
        public static MauiAppBuilder UseActivableSwipeView(this MauiAppBuilder builder)
        {
            return builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(CustomSwipeView), typeof(CustomSwipeViewHandler));
                handlers.AddHandler<SwipeItem, ActivableSwipeView.Handlers.SwipeItemMenuItemHandler>();
#else
                handlers.AddHandler(typeof(CustomSwipeView), typeof(SwipeViewHandler));
#endif
            });
        }
    }
}
