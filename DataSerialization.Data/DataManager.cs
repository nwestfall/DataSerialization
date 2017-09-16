using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace DataSerialization.Data
{
    /// <summary>
    /// Data Manager for the text file of World Cities
    /// </summary>
    public static class DataManager
    {
        /// <summary>
        /// Current load status of the data
        /// </summary>
        public enum LoadStatus
        {
            NotLoaded,
            Error,
            Loaded
        }

        private const string _fileLocation = "data/worldcitiespop.txt";

        /// <summary>
        /// Get the <see cref="LoadStatus"/> of the data text file
        /// </summary>
        public static LoadStatus Status { get; private set; } = LoadStatus.NotLoaded;

        /// <summary>
        /// Get all the world cities loaded from the text file
        /// </summary>
        public static List<WorldCityModel> WorldCities { get; private set; } = new List<WorldCityModel>();

        /// <summary>
        /// Load the world cities from the text file
        /// </summary>
        /// <returns></returns>
        public static Task LoadData()
        {
            return Task.Run(() =>
            {
                try
                {
                    Status = LoadStatus.NotLoaded;
                    WorldCities.Clear();
                    if (File.Exists(_fileLocation))
                    {
                        var lines = File.ReadAllLines(_fileLocation);
                        for (var i = 1; i < lines.Count(); i++)
                            WorldCities.Add(new WorldCityModel(lines[i]));
                    }
                    Status = LoadStatus.Loaded;
                }
                catch(Exception ex)
                {
                    Status = LoadStatus.Error;
                }
            });
        }
    }
}
