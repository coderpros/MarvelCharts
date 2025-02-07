// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the App class, which represents the application entry point and manages application-wide settings and themes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Ignore Spelling: App

namespace MarvelCharts.UI.WPF
{
    #region Usings
    using System.Windows;

    using MaterialDesignThemes.Wpf;

    using Microsoft.Extensions.Configuration;
    #endregion

    /// <summary>
    /// Interaction logic for the application entry point and manages application-wide settings and themes.
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        /// <summary>
        /// The palette helper is responsible for setting the theme of the application.
        /// </summary>
        private static readonly PaletteHelper PaletteHelper = new PaletteHelper();

        /// <summary>
        /// The application settings manager.
        /// </summary>
        private readonly SettingsManager<Models.ApplicationSettings> _applicationSettingsManager;

        /// <summary>
        /// The user settings manager.
        /// </summary>
        private readonly SettingsManager<Models.UserSettings> _userSettingsManager;

        /// <summary>
        /// The application settings.
        /// </summary>
        private readonly Models.ApplicationSettings _applicationSettings;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this._applicationSettingsManager = new SettingsManager<Models.ApplicationSettings>(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.json"));
            this._userSettingsManager = new SettingsManager<Models.UserSettings>(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserSettings.json"));

            this._applicationSettings = this._applicationSettingsManager.LoadSettings();

            ApplicationSettings = this._applicationSettings;
            UserSettings = this._userSettingsManager.LoadSettings();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The "me" property provides static access to the App class throughout the application.
        /// </summary>
        public static App Me => (App)System.Windows.Application.Current;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        public static Models.ApplicationSettings ApplicationSettings { get; set; }

        /// <summary>
        /// Gets or sets the user settings.
        /// </summary>
        public static Models.UserSettings UserSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is dark theme.
        /// </summary>
        public bool? IsDarkTheme { get; set; }
        #endregion

        #region Public Helpers
        /// <summary>
        /// Applies the theme to the application based on user settings or system theme.
        /// </summary>
        public static void ApplyTheme()
        {
            // Get the current theme used in the application
            var theme = PaletteHelper.GetTheme();

            // Determine the base theme based on the user settings or system theme
            if (App.UserSettings.IsDarkTheme.HasValue)
            {
                theme.SetBaseTheme(App.UserSettings.IsDarkTheme.Value ? BaseTheme.Dark : BaseTheme.Light);
            }
            else
            {
                var systemTheme = Theme.GetSystemTheme();

                theme.SetBaseTheme(systemTheme.GetValueOrDefault());
            }

            // Apply the changes.
            PaletteHelper.SetTheme(theme);
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the startup event of the application.
        /// </summary>
        /// <param name="e">The startup event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("AppSettings.json", false, true)
                .AddJsonFile("UserSettings.json", false, true)
                .Build();

            App.Configuration = config;

            base.OnStartup(e);

            this.MainWindow = new MainWindow();
            ApplyTheme();
        }
        #endregion
    }
}