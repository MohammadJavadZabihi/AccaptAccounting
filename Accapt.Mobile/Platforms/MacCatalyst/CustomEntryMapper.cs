﻿using Accapt.Mobile.Controls;
using CoreGraphics;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Accapt.Mobile.Platforms.Android
{
    public class CustomEntryMapper
    {
        public static void Map(IElementHandler handler, IElement view)
        {
            if (view is CustomeEntry)
            {
                var casted = (EntryHandler)handler;
                var viewData = (CustomeEntry)view;

                UpdateBackground(casted.PlatformView, viewData);

                var paddingViewLeft = new UIView(new CGRect(0, 0, 10, 0)); // Hardcoded for now
                casted.PlatformView.LeftView = paddingViewLeft;
                casted.PlatformView.LeftViewMode = UITextFieldViewMode.Always;

                var paddingViewRight = new UIView(new CGRect(0, 0, 10, 0)); // Hardcoded for now
                casted.PlatformView.RightView = paddingViewRight;
                casted.PlatformView.RightViewMode = UITextFieldViewMode.Always;
            }
        }

        private static void UpdateBackground(UITextField control, CustomeEntry entry)
        {
            if (control == null) return;

            control.Layer.CornerRadius = entry.CornerRadius;
            control.Layer.BorderWidth = entry.BorderThickness;
            control.Layer.BorderColor = entry.BorderColor.ToCGColor();
            control.BorderStyle = UITextBorderStyle.Line;
        }
    }
}
