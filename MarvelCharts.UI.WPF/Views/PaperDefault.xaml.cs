// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaperDefault.xaml.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the PaperDefault class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF.Views
{
    #region Usings
    using System.Windows.Controls;
    #endregion

    /// <summary>
    /// Interaction logic for PaperDefault
    /// </summary>
    public partial class PaperDefault : Page
    {
        #region Consrtructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PaperDefault"/> class.
        /// </summary>
        /// <param name="userName">The userName to be set.</param>
        public PaperDefault(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            this.InitializeComponent();
            this.UserName = userName;
            this.DataContext = this;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; }
        #endregion
    }
}