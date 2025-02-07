// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Provides common utility methods for the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF
{
    #region Usings

    using System.Windows.Media;
    using Microsoft.Win32;

    #endregion

    /// <summary>
    /// Provides common utility methods for the application.
    /// </summary>
    public class Common
    {
        #region Methods

        /// <summary>
        /// Determines whether the current Windows theme is light.
        /// </summary>
        /// <returns>True if the current Windows theme is light; otherwise, false.</returns>
        public static bool IsLightTheme()
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");

            return value is int i && i > 0;
        }

        /// <summary>
        /// Gets the current Windows accent color.
        /// </summary>
        /// <returns>The current Windows accent color, or null if it cannot be determined.</returns>
        public static Color? GetAccentColor()
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");
            var value = key?.GetValue("AccentColor");

            if (value is int colorValue)
            {
                return Color.FromArgb(
                    (byte)((colorValue >> 24) & 0xFF),
                    (byte)((colorValue >> 16) & 0xFF),
                    (byte)((colorValue >> 8) & 0xFF),
                    (byte)(colorValue & 0xFF));
            }

            return null;
        }

        #endregion
    }
}