// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsManager.cs" company="PlayOptions SRL">
//   Copyright 2025 PlayOptions SRL. All rights reserved.
// </copyright>
// <summary>
//   Defines the SettingsManager class, which manages the loading and saving of settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MarvelCharts.UI.WPF
{
    #region Usings
    using System;
    using System.IO;

    using Newtonsoft.Json;
    #endregion

    /// <summary>
    /// The settings manager.
    /// </summary>
    /// <typeparam name="T">
    /// The type.
    /// </typeparam>
    public class SettingsManager<T> where T : class
    {
        #region Properties & Fields
        /// <summary>
        /// The file path to the settings file.
        /// </summary>
        private readonly string _filePath;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsManager{T}"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public SettingsManager(string fileName)
        {
            this._filePath = this.GetLocalFilePath(fileName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the settings from the file.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T LoadSettings() =>
            File.Exists(this._filePath)
                ? JsonConvert.DeserializeObject<T>(File.ReadAllText(this._filePath))
                : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(new Models.UserSettings()));

        /// <summary>
        /// Saves the settings to the file.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public void SaveSettings(T settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(this._filePath, json);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Gets the local file path for the settings file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetLocalFilePath(string fileName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, fileName);
        }
        #endregion
    }
}