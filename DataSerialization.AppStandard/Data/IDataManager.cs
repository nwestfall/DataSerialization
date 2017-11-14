using System;
using System.Threading.Tasks;

using DataSerialization.Data;

namespace DataSerialization.App.Data
{
    public interface IDataManager
    {
        string Title();

        Task<GenericDataModel> Serialize(int number);

        Task<GenericDataModel> Deserialize(int number);
    }
}
