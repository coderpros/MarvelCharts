// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginPage.xaml.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the LoginPage class, which represents the interaction logic for LoginPage.xaml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF.Views
{
    #region Usings
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using MarvelCharts.UI.WPF.ViewModels;
    #endregion

    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the click event for the LiveTradingButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void LiveTradingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Login(this.UsernameTextBox.Text, this.PasswordTextBox.Password))
            {
                this.NavigationService.Navigate(new LiveDefault(this.UsernameTextBox.Text));

                // Make the window slowly grow to maximized.
                this.AnimateWindowSize(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height, 1);
            }
        }

        /// <summary>
        /// Handles the click event for the PaperTradingButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void PaperTradingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Login(this.UsernameTextBox.Text, this.PasswordTextBox.Password))
            {
                this.NavigationService.Navigate(new PaperDefault(this.UsernameTextBox.Text));

                // Make the window slowly grow to maximized.
                this.AnimateWindowSize(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height, 1);
            }
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Attempts to log in with the provided username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the login is successful; otherwise, false.</returns>
        private bool Login(string username, string password)
        {
            var loginViewModel = new LoginViewModel();

            var user = loginViewModel.Users.FirstOrDefault(u => u.Email == username && u.Password == password);

            if (user == null)
            {
                MessageBox.Show(
                    Window.GetWindow(this),
                    "Invalid username and/or password.",
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

            return user != null;
        }

        /// <summary>
        /// Animates the window size to the specified width and height over the given duration.
        /// </summary>
        /// <param name="toWidth">The target width.</param>
        /// <param name="toHeight">The target height.</param>
        /// <param name="durationInSeconds">The duration of the animation in seconds.</param>
        /// <remarks>This would theoretically work better with a WPF storyboard. However, I couldn't get it to grow horizontally and vertically at the same time.</remarks>
        private void AnimateWindowSize(double toWidth, double toHeight, double durationInSeconds)
        {
            var window = Window.GetWindow(this);

            if (window != null)
            {
                // Get the current monitor.
                var windowInteropHelper = new System.Windows.Interop.WindowInteropHelper(window);
                var monitor = NativeMethods.MonitorFromWindow(windowInteropHelper.Handle, NativeMethods.MONITOR_DEFAULTTONEAREST);

                // Get the monitor info.
                var monitorInfo = new NativeMethods.MONITORINFO();
                monitorInfo.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(monitorInfo);
                if (NativeMethods.GetMonitorInfo(monitor, ref monitorInfo))
                {
                    window.Left = monitorInfo.rcMonitor.Left;
                    window.Top = monitorInfo.rcMonitor.Top;
                }

                double fromWidth = window.Width;
                double fromHeight = window.Height;

                double deltaWidth = toWidth - fromWidth;
                double deltaHeight = toHeight - fromHeight;

                // Increase the frame rate for smoother animation
                int frameRate = 120; // Increased frame rate
                double interval = 1.0 / frameRate; // Interval in seconds
                int totalFrames = (int)(durationInSeconds * frameRate);

                int currentFrame = 0;

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(interval)
                };

                timer.Tick += (s, e) =>
                {
                    currentFrame++;

                    // Calculate progress as a value between 0 and 1
                    double progress = (double)currentFrame / totalFrames;

                    if (progress >= 1.0)
                    {
                        // Ensure final size is set precisely
                        window.Width = toWidth;
                        window.Height = toHeight;

                        // Stop the timer once the animation is complete
                        timer.Stop();
                    }
                    else
                    {
                        // Apply easing for smoother animation
                        double easedProgress = this.EaseOutCubic(progress);

                        // Incrementally update Width and Height
                        window.Width = fromWidth + (deltaWidth * easedProgress);
                        window.Height = fromHeight + (deltaHeight * easedProgress);
                    }
                };

                // Start the animation timer
                timer.Start();
            }
        }

        /// <summary>
        /// Cubic easing out function for smoother animation.
        /// </summary>
        /// <param name="t">The progress value between 0 and 1.</param>
        /// <returns>The eased progress value.</returns>
        private double EaseOutCubic(double t)
        {
            return 1 - Math.Pow(1 - t, 3);
        }

        #endregion
    }
}