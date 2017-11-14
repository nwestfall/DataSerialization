using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

using DataSerialization.Data;
using Newtonsoft.Json;

namespace DataSerialization.App.Data
{
    public class JsonManager : IDataManager
    {
        public string Title() => "JSON";

        public JsonManager()
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
                JsonConvert.SerializeObject(DataManager.WorldCities);
                timeWatch.Stop();
                runTimes.Add(timeWatch.ElapsedTicks);
            }
            overallWatch.Stop();
            model.RawData = JsonConvert.SerializeObject(DataManager.WorldCities);
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

            var serializedObject = JsonConvert.SerializeObject(DataManager.WorldCities);

            Stopwatch overallWatch = Stopwatch.StartNew();
            var timeWatch = new Stopwatch();
            for (int i = 0; i < number; i++)
            {
                timeWatch.Restart();
                JsonConvert.DeserializeObject<IList<WorldCityModel>>(serializedObject);
                timeWatch.Stop();
                runTimes.Add(timeWatch.ElapsedTicks);
            }
            overallWatch.Stop();
            model.RawData = JsonConvert.DeserializeObject<IList<WorldCityModel>>(serializedObject);
            model.TotalTime = new TimeSpan(overallWatch.ElapsedTicks);
            model.AvgTime = new TimeSpan((long)runTimes.Average());

            return model;
        }
    }
}
