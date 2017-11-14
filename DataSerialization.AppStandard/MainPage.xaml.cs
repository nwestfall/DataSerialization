using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using DataSerialization.Data;
using DataSerialization.App.Data;
using Acr.UserDialogs;

namespace DataSerialization.App
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Title = "Data Serialization";

            this.Children.Add(new DataPage(new JsonManager())); //Json
            this.Children.Add(new DataPage(new XmlManager())); //XML
            this.Children.Add(new DataPage(new BondManager())); //Bond
            this.Children.Add(new DataPage(null)); //Google

            CheckData();
        }

        private async void CheckData()
        {
            if (DataManager.Status != DataManager.LoadStatus.Loaded)
            {
                await Task.Delay(2000); //User dialogs crashes
                UserDialogs.Instance.ShowLoading("Loading Data... This may take a while", MaskType.Gradient);
                await DataManager.LoadData();
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
