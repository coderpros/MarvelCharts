// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the LoginViewModel class, which represents the view model for the login functionality.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.ViewModels
{

    #region Usings

    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    #endregion

    /// <summary>
    /// Represents the view model for the login functionality.
    /// </summary>
    public class LoginViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            this.Workspaces = new System.Collections.ObjectModel.ObservableCollection<Models.WorkspaceModel>
                                  {
                                      new() { Id = 1, Name = "Empty" },
                                      new() { Id = 2, Name = "coderPro.net" },
                                      new() { Id = 3, Name = "Someone Else" }
                                  };

            this.Users = new System.Collections.ObjectModel.ObservableCollection<Models.UserModel>
                             {
                                 new()
                                     {
                                         Id = 1,
                                         Email = "brandon.osborne@coderpro.net",
                                         Password = "password",
                                         Workspace = this.Workspaces[0]
                                     }
                             };

            this.SelectedWorkspace = this.Workspaces[0];
        }

        #endregion

        #region Fields

        /// <summary>
        /// The selected workspace.
        /// </summary>
        private MarvelCharts.Models.WorkspaceModel? _selectedWorkspace;

        /// <summary>
        /// The collection of workspaces.
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<Models.WorkspaceModel> _workspaces;

        /// <summary>
        /// The collection of users.
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<Models.UserModel> _users;


        /// <summary>
        /// Gets or sets the selected workspace.
        /// </summary>
        public Models.WorkspaceModel SelectedWorkspace
        {
            get => this._selectedWorkspace;
            set => this.SetField(ref this._selectedWorkspace, value);
        }

        /// <summary>
        /// Gets or sets the collection of workspaces.
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<Models.WorkspaceModel> Workspaces
        {
            get => this._workspaces;
            set => this.SetField(ref this._workspaces, value);
        }

        /// <summary>
        /// Gets or sets the collection of users.
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<Models.UserModel> Users
        {
            get => this._users;
            set => this.SetField(ref this._users, value);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the field and raises the <see cref="PropertyChanged"/> event if the value changes.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <returns>True if the value was changed; otherwise, false.</returns>
        protected bool SetField<T>(
            ref T field,
            T value,
            [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}