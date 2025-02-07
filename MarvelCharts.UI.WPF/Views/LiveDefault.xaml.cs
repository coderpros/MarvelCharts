// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LiveDefault.xaml.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the LiveDefault class, which represents the interaction logic for LiveDefault.xaml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF.Views
{
    #region Usings
    using System.Windows.Controls;
    #endregion

    /// <summary>
    /// Interaction logic for LiveDefault
    /// </summary>
    public partial class LiveDefault
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LiveDefault"/> class.
        /// </summary>
        /// <param name="userName">The username to be set.</param>
        public LiveDefault(string userName)
        {
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