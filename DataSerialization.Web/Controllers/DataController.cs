using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataSerialization.Data;
using Newtonsoft.Json;

namespace DataSerialization.Web.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private async Task CheckData()
        {
            if (DataManager.Status != DataManager.LoadStatus.Loaded)
                await DataManager.LoadData();
        }

        [HttpGet]
        public async Task<GenericDataModel> AllWorldCitiesSerializeJSON(int times = 1)
        {
            await CheckData();

            GenericDataModel model = new GenericDataModel();
            model.TimesRan = times;
            List<long> runTimes = new List<long>();

            var overallWatch = Stopwatch.StartNew();
            var timeWatch = new Stopwatch();
            for (int i = 0; i < times; i++)
            {
                timeWatch.Restart();
                JsonConvert.SerializeObject(DataManager.WorldCities);
                timeWatch.Stop();
                runTimes.Add(timeWatch.ElapsedTicks);
            }
            overallWatch.Stop();
            model.TotalTime = new TimeSpan(overallWatch.ElapsedTicks);
            model.AvgTime = new TimeSpan((long)runTimes.Average());

            return model;
        }
    }
}
