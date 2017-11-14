using System;

using DataSerialization.Data;

namespace DataSerialization.App.ViewModels
{
    public class GenericDataViewModel
    {
        private GenericDataModel _model;

        public string AvgTime
        {
            get => _model.AvgTime.ToString("G");
        }

        public string TotalTime
        {
            get => _model.TotalTime.ToString("G");
        }

        public string RunTime
        {
            get => _model.RunTime.ToString("s");
        }

        public string TimesRan
        {
            get => _model.TimesRan.ToString("N0");
        }

        public string TransformType
        {
            get
            {
                switch(_model.Transform)
                {
                    case GenericDataModel.TransformType.Deserialize:
                        return "Deserialize";
                    case GenericDataModel.TransformType.Serialize:
                        return "Serialize";
                    default:
                        return "N/A";
                }
            }
        }

        public GenericDataViewModel(GenericDataModel model)
        {
            _model = model;
        }
    }
}
