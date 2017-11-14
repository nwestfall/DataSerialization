using System;
using System.Threading.Tasks;

using DataSerialization.Data;

namespace DataSerialization.App.Data
{
    public class BondManager : IDataManager
    {
        public string Title() => "Bond";

        public BondManager()
        {
        }

        public async Task<GenericDataModel> Serialize(int number)
        {
            return null;
        }

        public async Task<GenericDataModel> Deserialize(int number)
        {
            return null;
        }
    }
}
