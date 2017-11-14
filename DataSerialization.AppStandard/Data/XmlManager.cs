using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using DataSerialization.Data;

namespace DataSerialization.App.Data
{
    public class XmlManager : IDataManager
    {
        public string Title() => "XML";

        public XmlManager()
        {
        }

        public async Task<GenericDataModel> Serialize(int number)
        {
            await DataManager.LoadData();

            if (DataManager.Status == DataManager.LoadStatus.Error)
                throw new Exception("Could not load Data");

            var model = new GenericDataModel();
            model.RunTime = DateTime.Now;
            model.TimesRan = number;
            model.Transform = GenericDataModel.TransformType.Serialize;
            var runTimes = new List<long>();

            Stopwatch overallWatch = Stopwatch.StartNew();
            var timeWatch = new Stopwatch();
            for (int i = 0; i < number; i++)
            {
                timeWatch.Restart();
                var serializer = new XmlSerializer(typeof(WorldCityModel));
                var sb = new StringBuilder();
                using(var tw = XmlWriter.Create(sb))
                {
                    serializer.Serialize(tw, DataManager.WorldCities);   
                }
                timeWatch.Stop();
                runTimes.Add(timeWatch.ElapsedTicks);
            }
            overallWatch.Stop();
            var tempSerializer = new XmlSerializer(typeof(WorldCityModel));
            var tempsb = new StringBuilder();
            using (var tw = XmlWriter.Create(tempsb))
            {
                tempSerializer.Serialize(tw, DataManager.WorldCities);
            }
            model.RawData = tempsb.ToString();
            model.TotalTime = new TimeSpan(overallWatch.ElapsedTicks);
            model.AvgTime = new TimeSpan((long)runTimes.Average());

            return model;
        }

        public async Task<GenericDataModel> Deserialize(int number)
        {
            await DataManager.LoadData();

            if (DataManager.Status == DataManager.LoadStatus.Error)
                throw new Exception("Could not load Data");

            var model = new GenericDataModel();
            model.RunTime = DateTime.Now;
            model.TimesRan = number;
            model.Transform = GenericDataModel.TransformType.Deserialize;
            var runTimes = new List<long>();

            var serializer = new XmlSerializer(typeof(WorldCityModel));
            var sb = new StringBuilder();
            using (var tw = XmlWriter.Create(sb))
            {
                serializer.Serialize(tw, DataManager.WorldCities);
            }
            var serializedBytes = Encoding.ASCII.GetBytes(sb.ToString());

            Stopwatch overallWatch = Stopwatch.StartNew();
            var timeWatch = new Stopwatch();
            for (int i = 0; i < number; i++)
            {
                timeWatch.Restart();
                var deserializer = new XmlSerializer(typeof(WorldCityModel));
                using (var ms = new MemoryStream(serializedBytes))
                {
                    deserializer.Deserialize(ms);
                }
                timeWatch.Stop();
                runTimes.Add(timeWatch.ElapsedTicks);
            }
            overallWatch.Stop();
            var tempDeserializer = new XmlSerializer(typeof(WorldCityModel));
            using (var ms = new MemoryStream(serializedBytes))
            {
                model.RawData = tempDeserializer.Deserialize(ms);
            }
            model.TotalTime = new TimeSpan(overallWatch.ElapsedTicks);
            model.AvgTime = new TimeSpan((long)runTimes.Average());

            return model;
        }
    }
}
