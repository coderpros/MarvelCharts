// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceModel.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the WorkspaceModel class, which represents a workspace in the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a workspace in the application.
    /// </summary>
    public class WorkspaceModel
    {
        /// <summary>
        /// Gets or sets the workspace ID.
        /// </summary>
        [Key]
        [Required]
        public required int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the workspace.
        /// </summary>
        [Required]
        public required string Name { get; set; }
    }
}