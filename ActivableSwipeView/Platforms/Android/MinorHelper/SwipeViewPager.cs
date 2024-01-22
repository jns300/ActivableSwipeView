using Android.Content;
using Android.Views;
using AndroidX.ViewPager.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.MinorHelper
{
    internal class SwipeViewPager : ViewPager
    {
        public SwipeViewPager(Context context) : base(context)
        {
        }

        public bool EnableGesture { get; set; } = true;

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            // Same as:
            // if (!EnableGesture) return false;
            // However this is, at least in theory a tidge faster which in this particular area is good
            return EnableGesture && base.OnInterceptTouchEvent(ev);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return EnableGesture && base.OnTouchEvent(e);
        }
    }
}
