﻿using System;
using System.Runtime.CompilerServices;
using Android.Content;
using Android.Views;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;
using static Android.Views.View;
using LayoutDirection = Android.Views.LayoutDirection;
using PlatformView = Android.Views.View;

namespace ActivableSwipeView.Helper
{
    internal static partial class ViewHandlerExtensions
    {
        // _TODO: Possibly reconcile this code with LayoutViewGroup.OnLayout
        // If you make changes here please review if those changes should also
        // apply to LayoutViewGroup.OnLayout
        internal static Size LayoutVirtualView(
            this IPlatformViewHandler viewHandler,
            int l, int t, int r, int b,
            Func<Rect, Size>? arrangeFunc = null)
        {
            var context = viewHandler.MauiContext?.Context;
            var virtualView = viewHandler.VirtualView;
            var platformView = viewHandler.PlatformView;

            if (context == null || virtualView == null || platformView == null)
            {
                return Size.Zero;
            }

            var destination = context.ToCrossPlatformRectInReferenceFrame(l, t, r, b);
            arrangeFunc ??= virtualView.Arrange;
            return arrangeFunc(destination);
        }

        // _TODO: Possibly reconcile this code with LayoutViewGroup.OnMeasure
        // If you make changes here please review if those changes should also
        // apply to LayoutViewGroup.OnMeasure
        internal static Size MeasureVirtualView(
            this IPlatformViewHandler viewHandler,
            int widthMeasureSpec,
            int heightMeasureSpec,
            Func<double, double, Size>? measureFunc = null)
        {
            var context = viewHandler.MauiContext?.Context;
            var virtualView = viewHandler.VirtualView;
            var platformView = viewHandler.PlatformView;

            if (context == null || virtualView == null || platformView == null)
            {
                return Size.Zero;
            }

            var deviceIndependentWidth = widthMeasureSpec.ToDouble(context);
            var deviceIndependentHeight = heightMeasureSpec.ToDouble(context);

            var widthMode = MeasureSpec.GetMode(widthMeasureSpec);
            var heightMode = MeasureSpec.GetMode(heightMeasureSpec);

            measureFunc ??= virtualView.Measure;
            var measure = measureFunc(deviceIndependentWidth, deviceIndependentHeight);

            // If the measure spec was exact, we should return the explicit size value, even if the content
            // measure came out to a different size
            var width = widthMode == MeasureSpecMode.Exactly ? deviceIndependentWidth : measure.Width;
            var height = heightMode == MeasureSpecMode.Exactly ? deviceIndependentHeight : measure.Height;

            var platformWidth = context.ToPixels(width);
            var platformHeight = context.ToPixels(height);

            // Minimum values win over everything
            platformWidth = Math.Max(platformView.MinimumWidth, platformWidth);
            platformHeight = Math.Max(platformView.MinimumHeight, platformHeight);

            return new Size(platformWidth, platformHeight);
        }

        internal static void PlatformArrangeHandler(this IViewHandler viewHandler, Rect frame)
        {
            var platformView = viewHandler.ToPlatform();

            var Context = viewHandler.MauiContext?.Context;
            var MauiContext = viewHandler.MauiContext;

            if (platformView == null || MauiContext == null || Context == null)
            {
                return;
            }

            if (frame.Width < 0 || frame.Height < 0)
            {
                // This is a legacy layout value from Controls, nothing is actually laying out yet so we just ignore it
                return;
            }

            var left = Context.ToPixels(frame.Left);
            var top = Context.ToPixels(frame.Top);
            var bottom = Context.ToPixels(frame.Bottom);
            var right = Context.ToPixels(frame.Right);

            var viewParent = platformView.Parent;
            if (viewParent?.LayoutDirection == LayoutDirection.Rtl && viewParent is Android.Views.View parentView)
            {
                // Determine the flipped left/right edges for the RTL layout
                var width = right - left;
                left = parentView.Width - left - width;
                right = left + width;
            }

            platformView.Layout((int)left, (int)top, (int)right, (int)bottom);

            viewHandler.Invoke(nameof(IView.Frame), frame);
        }

        internal static void PrepareForTextViewArrange(this IViewHandler handler, Rect frame)
        {
            if (frame.Width < 0 || frame.Height < 0)
            {
                return;
            }

            var platformView = handler.ToPlatform();
            if (platformView == null)
            {
                return;
            }

            var virtualView = handler.VirtualView;
            if (virtualView == null)
            {
                return;
            }

            // Depending on our layout situation, the TextView may need an additional measurement pass at the final size
            // in order to properly handle any TextAlignment properties and some internal bookkeeping
            if (virtualView.NeedsExactMeasure())
            {
                platformView.Measure(platformView.MakeMeasureSpecExact(frame.Width), platformView.MakeMeasureSpecExact(frame.Height));
            }
        }

        internal static bool NeedsExactMeasure(this IView virtualView)
        {
            if (virtualView.VerticalLayoutAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill
                && virtualView.HorizontalLayoutAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill)
            {
                // Layout Alignments of Start, Center, and End will be laying out the TextView at its measured size,
                // so we won't need another pass with MeasureSpecMode.Exactly
                return false;
            }

            if (virtualView.Width >= 0 && virtualView.Height >= 0)
            {
                // If the Width and Height are both explicit, then we've already done MeasureSpecMode.Exactly in 
                // both dimensions; no need to do it again
                return false;
            }

            // We're going to need a second measurement pass so TextView can properly handle alignments
            return true;
        }

        internal static int MakeMeasureSpecExact(this PlatformView view, double size)
        {
            // Convert to a native size to create the spec for measuring
            var deviceSize = (int)view.ToPixels(size);
            return MeasureSpecMode.Exactly.MakeMeasureSpec(deviceSize);
        }
    }
}
