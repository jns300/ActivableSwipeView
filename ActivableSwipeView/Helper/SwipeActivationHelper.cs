using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivableSwipeView.Helper
{
    /// <summary>
    /// Provides helper methods related to the activable swipe view.
    /// </summary>
    internal class SwipeActivationHelper
    {
        /// <summary>
        /// Checks whether the gap between the initial point and the point from the argument 
        /// is sufficient to start the swipe.
        /// </summary>
        /// <param name="initialPoint">the initial point of the swipe</param>
        /// <param name="point">the point to examine</param>
        /// <param name="direction">the detected swipe direction</param>
        /// <param name="swipeActivationThreshold">the swipe activation threshold</param>
        /// <returns>whether the swipe can be started</returns>
        public static bool CheckActivationThreshold(Point initialPoint, Point point, SwipeDirection? direction, double swipeActivationThreshold)
        {
            return (direction == SwipeDirection.Left || direction == SwipeDirection.Right) && Math.Abs(initialPoint.X - point.X) >= swipeActivationThreshold
                || (direction == SwipeDirection.Up || direction == SwipeDirection.Down) && Math.Abs(initialPoint.Y - point.Y) >= swipeActivationThreshold;
        }
    }
}
