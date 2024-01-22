using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.MinorHelper
{
    internal static class JavaObjectExtensions
    {
        public static bool IsDisposed(this Java.Lang.Object obj)
        {
            return obj.Handle == nint.Zero;
        }

        public static bool IsDisposed(this global::Android.Runtime.IJavaObject obj)
        {
            return obj.Handle == nint.Zero;
        }

        public static bool IsAlive([NotNullWhen(true)] this Java.Lang.Object obj)
        {
            if (obj == null)
                return false;

            return !obj.IsDisposed();
        }

        public static bool IsAlive([NotNullWhen(true)] this global::Android.Runtime.IJavaObject obj)
        {
            if (obj == null)
                return false;

            return !obj.IsDisposed();
        }
    }
}
