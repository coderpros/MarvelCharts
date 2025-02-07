// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserSettings.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the UserSettings class, which represents the settings for a user in the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF.Models
{
    /// <summary>
    /// Represents the settings for a user in the application.
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the dark theme is enabled.
        /// </summary>
        public bool? IsDarkTheme { get; set; }

        /// <summary>
        /// Gets or sets the primary color.
        /// </summary>
        public string? PrimaryColor { get; set; }

        /// <summary>
        /// Gets or sets the secondary color.
        /// </summary>
        public string? SecondaryColor { get; set; }
    }
}