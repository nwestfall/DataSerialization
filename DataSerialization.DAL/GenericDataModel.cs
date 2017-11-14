using System;

namespace DataSerialization.Data
{
    /// <summary>
    /// Generic Data Model to return for demo purposes
    /// </summary>
    public class GenericDataModel
    {
        public enum TransformType
        {
            Serialize,
            Deserialize
        }

        /// <summary>
        /// Raw Data
        /// </summary>
        public object RawData { get; set; }
        /// <summary>
        /// Total time to deserialize/serialize data
        /// </summary>
        public TimeSpan TotalTime { get; set; }
        /// <summary>
        /// Total times ran
        /// </summary>
        public int TimesRan { get; set; }
        /// <summary>
        /// Avg time
        /// </summary>
        public TimeSpan AvgTime { get; set; }
        /// <summary>
        /// Gets or sets the run time.
        /// </summary>
        public DateTime RunTime { get; set; }
        /// <summary>
        /// Gets or sets the transform.
        /// </summary>
        public TransformType Transform { get; set; }
    }
}
