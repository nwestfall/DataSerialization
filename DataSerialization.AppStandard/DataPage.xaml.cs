using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DataSerialization.App.Data;
using DataSerialization.App.ViewModels;
using Acr.UserDialogs;

namespace DataSerialization.App
{
    public partial class DataPage : ContentPage
    {
        private IDataManager _manager;
        private DataViewModel _viewModel;

        public DataPage(IDataManager manager)
        {
            InitializeComponent();

            this._manager = manager;

            this.Title = _manager?.Title() ?? "N/A";

            _viewModel = new DataViewModel(_manager, UserDialogs.Instance);
            BindingContext = _viewModel;

            NumberOfTestsSlider.Minimum = 0;
            NumberOfTestsSlider.Maximum = 500.0;
            NumberOfTestsSlider.ValueChanged += (sender, e) => 
            {
                _viewModel.NumberOfTests = (int)e.NewValue;
            };
           
            TestResultsList.ItemsSource = _viewModel.TestResults;
        }
    }
}
