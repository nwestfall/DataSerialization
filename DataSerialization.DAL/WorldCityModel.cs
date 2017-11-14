namespace DataSerialization.Data
{
    /// <summary>
    /// World City Model
    /// </summary>
    public class WorldCityModel
    {
        /// <summary>
        /// Get or set the country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Get or set the city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Get or set the accent city
        /// </summary>
        public string AccentCity { get; set; }
        /// <summary>
        /// Get or set the region
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Get or set the population
        /// </summary>
        public int? Population { get; set; }
        /// <summary>
        /// Get or set the latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Get or set the longitude
        /// </summary>
        public double Longitude { get; set; }

        public WorldCityModel(string line)
        {
            if (string.IsNullOrEmpty(line) || !line.Contains(","))
                return;

            var columns = line.Split(',');
            if (columns.Length == 0)
                return;

            Country = columns[0];
            City = columns[1];
            AccentCity = columns[2];
            Region = columns[3];
            if (int.TryParse(columns[4], out int pop))
                Population = pop;
            if (double.TryParse(columns[5], out double lat))
                Latitude = lat;
            if (double.TryParse(columns[6], out double lng))
                Longitude = lng;
        }
    }
}
