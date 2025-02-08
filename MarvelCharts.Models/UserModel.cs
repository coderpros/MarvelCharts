// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the UserModel class, which represents a user in the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.Models
{
    #region Usings

    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// Represents a user in the application.
    /// </summary>
    public class UserModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        #endregion
        
        #region Relationships
        /// <summary>
        /// Gets or sets the workspace associated with the user.
        /// </summary>
        public Models.WorkspaceModel Workspace { get; set; }
        #endregion
    }
}