﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace FWA.Wpf.Helpers
{
   public class WindowEx
   {
      private const int GwlExstyle = -20;
      private const int SwpFramechanged = 0x0020;
      private const int SwpNomove = 0x0002;
      private const int SwpNosize = 0x0001;
      private const int SwpNozorder = 0x0004;
      private const int WsExDlgmodalframe = 0x0001;

      public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.RegisterAttached(
          "ShowIcon",
          typeof(bool),
          typeof(WindowEx),
          new FrameworkPropertyMetadata(true, new PropertyChangedCallback((d, e) => RemoveIcon((Window)d))));


      public static bool GetShowIcon(UIElement element)
      {
         return (bool)element.GetValue(ShowIconProperty);
      }

      public static void RemoveIcon(Window window)
      {
         window.SourceInitialized += delegate {
            // Get this window's handle
            var hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, GwlExstyle);
            NativeMethods.SetWindowLong(hwnd, GwlExstyle, extendedStyle | WsExDlgmodalframe);

            // Update the window's non-client area to reflect the changes
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SwpNomove |
              SwpNosize | SwpNozorder | SwpFramechanged);
         };
      }

      public static void SetShowIcon(UIElement element, Boolean value)
      {
         element.SetValue(ShowIconProperty, value);
      }
   }
   public class NativeMethods
   {
        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
          IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
          int x, int y, int width, int height, uint flags);
    }
}