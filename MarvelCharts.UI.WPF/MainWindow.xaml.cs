// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the MainWindow class, which represents the main window of the application and manages theme settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF
{
    #region Usings

    using System.Collections.Immutable;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;

    using MaterialDesignColors;

    using MaterialDesignThemes.Wpf;
    #endregion

    /// <summary>
    /// Interaction logic for the Main Window.
    /// </summary>
    [SuppressMessage("ReSharper", "StyleCop.SA1503")]
    public partial class MainWindow : Window
    {
        #region Fields
        /// <summary>
        /// The palette helper.
        /// </summary>
        private readonly PaletteHelper _paletteHelper = new();

        /// <summary>
        /// The user settings manager.
        /// </summary>
        private readonly SettingsManager<Models.UserSettings> _userSettingsManager;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this._userSettingsManager =
                new SettingsManager<Models.UserSettings>($"{AppDomain.CurrentDomain.BaseDirectory}UserSettings.json");

            var swatchProvider = new SwatchesProvider();
            var swatches = swatchProvider.Swatches.Select(s => s.Name).OrderBy(s => s).ToImmutableArray();

            this.InitializeComponent();

            this._userSettingsManager.LoadSettings();

            this.PrimaryColorSelector.ItemsSource = swatches;
            this.AccentColorSelector.ItemsSource = swatches;

            this.ToggleThemeCheckBox.IsChecked = App.UserSettings.IsDarkTheme;

            if (App.UserSettings.PrimaryColor != null && App.UserSettings.SecondaryColor != null)
            {
                this.PrimaryColorSelector.SelectedValue = App.UserSettings.PrimaryColor;
                this.AccentColorSelector.SelectedValue = App.UserSettings.SecondaryColor;

                this.ApplyColorChange();
            }

            // Bind the events here because Primary/Accent color selection will cause problems during initialization.
            this.PrimaryColorSelector.SelectionChanged += this.ColorSelectorSelectionChanged;
            this.AccentColorSelector.SelectionChanged += this.AccentColorSelectorSelectionChanged;
            this.MouseLeftButtonDown += delegate { this.DragMove(); };
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the click event for the Exit button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Handles the click event for the ToggleThemeCheckBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ToggleThemeCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            // Persist the theme changes.
            App.Me.IsDarkTheme = this.ToggleThemeCheckBox.IsChecked;
            App.UserSettings.IsDarkTheme = this.ToggleThemeCheckBox.IsChecked;

            this._userSettingsManager.SaveSettings(App.UserSettings);

            var theme = this._paletteHelper.GetTheme();

            // Determine the base theme based on the user settings or system theme.
            if (App.UserSettings.IsDarkTheme.HasValue)
            {
                this.RemoveThemeListenerHook();
                theme.SetBaseTheme(App.UserSettings.IsDarkTheme.Value ? BaseTheme.Dark : BaseTheme.Light);
            }
            else
            {
                var systemTheme = Theme.GetSystemTheme();

                App.Me.IsDarkTheme = systemTheme == BaseTheme.Dark;
                theme.SetBaseTheme(systemTheme.GetValueOrDefault());
                this.CreateThemeListenerHook();
            }

            // Apply the changes.
            this._paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// Handles the selection changed event for the PrimaryColorSelector.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ColorSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ApplyColorChange();
        }

        /// <summary>
        /// Handles the selection changed event for the AccentColorSelector.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void AccentColorSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ApplyColorChange();
        }

        /// <summary>
        /// Handles the loaded event for the MainWindow.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (App.UserSettings.IsDarkTheme == null) this.CreateThemeListenerHook();
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Applies the color changes based on the selected primary and secondary colors.
        /// </summary>
        private void ApplyColorChange()
        {
            var theme = this._paletteHelper.GetTheme();

            var swatchProvider = new SwatchesProvider();
            var primaryColorName = this.PrimaryColorSelector.SelectedValue?.ToString();
            var secondaryColorName = this.AccentColorSelector.SelectedValue?.ToString();
            BaseTheme baseTheme;

            if (string.IsNullOrEmpty(primaryColorName)) primaryColorName = App.UserSettings.PrimaryColor;
            if (string.IsNullOrEmpty(secondaryColorName)) secondaryColorName = App.UserSettings.SecondaryColor;

            if (App.UserSettings.IsDarkTheme != null)
            {
                baseTheme = App.UserSettings.IsDarkTheme.GetValueOrDefault() ? BaseTheme.Dark : BaseTheme.Light;
            }
            else
            {
                baseTheme = Theme.GetSystemTheme().GetValueOrDefault();
            }

            if (!string.IsNullOrEmpty(primaryColorName) && !string.IsNullOrEmpty(secondaryColorName))
            {
                var primaryColor = swatchProvider.Swatches.FirstOrDefault(s => s.Name == primaryColorName);
                var secondaryColor = swatchProvider.Swatches.FirstOrDefault(s => s.Name == secondaryColorName);

                App.UserSettings.PrimaryColor = primaryColorName;
                App.UserSettings.SecondaryColor = secondaryColorName;

                if (primaryColor != null && secondaryColor != null)
                {
                    theme = Theme.Create(baseTheme, primaryColor.ExemplarHue.Color, secondaryColor.ExemplarHue.Color);
                }
                else
                {
                    theme.SetBaseTheme(baseTheme);
                }
            }

            this._paletteHelper.SetTheme(theme);
            this._userSettingsManager.SaveSettings(App.UserSettings);
        }

        /// <summary>
        /// Creates a hook to detect when the Windows theme changes.
        /// </summary>
        private void CreateThemeListenerHook()
        {
            // Detect when the Windows theme changes
            var source = (HwndSource)PresentationSource.FromVisual(this);

            source.AddHook(
                (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
                {
                    const int WM_SETTINGCHANGE = 0x001A;
                    if (msg == WM_SETTINGCHANGE)
                    {
                        var theme = this._paletteHelper.GetTheme();

                        // Determine if user is using light or dark Windows theme.
                        App.UserSettings.IsDarkTheme = !Common.IsLightTheme();

                        theme.SetBaseTheme(App.UserSettings.IsDarkTheme == true ? BaseTheme.Dark : BaseTheme.Light);
                        this._paletteHelper.SetTheme(theme);
                    }

                    return IntPtr.Zero;
                });
        }

        /// <summary>
        /// Calculates the color distance between two colors in the Lab color space.
        /// </summary>
        /// <param name="color1">The first color.</param>
        /// <param name="color2">The second color.</param>
        /// <returns>The Euclidean distance between the two colors in the Lab color space.</returns>
        private double ColorDistance(System.Windows.Media.Color color1, System.Windows.Media.Color color2)
        {
            // Convert RGB to Lab color space
            var lab1 = RgbToLab(color1);
            var lab2 = RgbToLab(color2);

            // Calculate the Euclidean distance in Lab color space
            return Math.Sqrt(
                Math.Pow(lab1.L - lab2.L, 2) +
                Math.Pow(lab1.A - lab2.A, 2) +
                Math.Pow(lab1.B - lab2.B, 2));
        }

        /// <summary>
        /// Converts an RGB color to the Lab color space.
        /// </summary>
        /// <param name="color">The RGB color.</param>
        /// <returns>A tuple representing the Lab color space values.</returns>
        private (double L, double A, double B) RgbToLab(System.Windows.Media.Color color)
        {
            // Convert RGB to XYZ color space
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            r = r > 0.04045 ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = g > 0.04045 ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = b > 0.04045 ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            double x = r * 0.4124 + g * 0.3576 + b * 0.1805;
            double y = r * 0.2126 + g * 0.7152 + b * 0.0722;
            double z = r * 0.0193 + g * 0.1192 + b * 0.9505;

            // Convert XYZ to Lab color space
            x /= 0.95047;
            y /= 1.00000;
            z /= 1.08883;

            x = x > 0.008856 ? Math.Pow(x, 1.0 / 3.0) : (7.787 * x) + (16.0 / 116.0);
            y = y > 0.008856 ? Math.Pow(y, 1.0 / 3.0) : (7.787 * y) + (16.0 / 116.0);
            z = z > 0.008856 ? Math.Pow(z, 1.0 / 3.0) : (7.787 * z) + (16.0 / 116.0);

            double l = (116.0 * y) - 16.0;
            double a = 500.0 * (x - y);
            double b2 = 200.0 * (y - z); // Renamed variable to avoid conflict

            return (l, a, b2);
        }

        /// <summary>
        /// Removes the Windows theme listener hook.
        /// </summary>
        private void RemoveThemeListenerHook()
        {
            var source = (HwndSource)PresentationSource.FromVisual(this);

            source.RemoveHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) => IntPtr.Zero);
        }
        #endregion
    }
}