using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivableSwipeView.Views
{
    /// <summary>
    /// Represents a swipe view which contains the swipe activation threshold property.
    /// </summary>
    public class CustomSwipeView : SwipeView
    {
        public static readonly BindableProperty ActivationThresholdProperty =
            BindableProperty.Create(nameof(ActivationThreshold), typeof(double), typeof(CustomSwipeView), default(double));

        /// <summary>
        /// Gets or sets value of device-independent units, which represents a minimum distance, 
        /// between a dragging initial point and a dragging current point, to activate the swipe.
        /// </summary>
        public double ActivationThreshold
        {
            get { return (double)GetValue(ActivationThresholdProperty); }
            set { SetValue(ActivationThresholdProperty, value); }
        }
    }
}
