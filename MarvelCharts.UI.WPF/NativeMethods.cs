// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the NativeMethods class, which represents a collection of native methods for working with the Windows API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF
{
    /// <summary>
    /// Contains native methods for working with the Windows API.
    /// </summary>
    internal static class NativeMethods
    {
        #region COM Interop Win32 API Constants
        /// <summary>
        /// The default primary monitor.
        /// </summary>
        public const int MONITOR_DEFAULTTONEAREST = 2;
        #endregion

        /// <summary>
        /// Contains the coordinates of a rectangle that defines the bounding box of a window or screen.
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// Contains information about a display monitor.
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }


        #region COM Interpop/Win32 API Methods
        /// <summary>
        /// Used to get the monitor that a window is on.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        /// <summary>
        /// Used to get information about a monitor.
        /// </summary>
        /// <param name="hMonitor"></param>
        /// <param name="lpmi"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
        #endregion
    }
}
