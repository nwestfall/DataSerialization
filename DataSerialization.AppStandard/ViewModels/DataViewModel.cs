using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;

using DataSerialization.App.Data;
using DataSerialization.Data;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace DataSerialization.App.ViewModels
{
    public class DataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IUserDialogs Dialogs { get; }

        private IDataManager _manager;

        public DataViewModel(IDataManager manager, IUserDialogs dialog)
        {
            _manager = manager;
            Dialogs = dialog;

            _title = _manager?.Title() ?? "N/A";
            TestResults.CollectionChanged += (sender, e) => {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestResults)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasResults)));
            };
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        private int _numberOfTests = 1;
        public int NumberOfTests
        {
            get => _numberOfTests;
            set
            {
                _numberOfTests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumberOfTests)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumberOfTestsMessage)));
            }
        }

        public string NumberOfTestsMessage
        {
            get => $"Number of Tests: {NumberOfTests}";
        }

        public bool HasResults
        {
            get => TestResults?.Any() == true;
         }

        private ObservableCollection<GenericDataViewModel> _testResults = new ObservableCollection<GenericDataViewModel>();
        public ObservableCollection<GenericDataViewModel> TestResults
        {
            get => _testResults;
            set
            {
                _testResults = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestResults)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasResults)));
            }
        }

        private ICommand _runSerializeTestsCommand;
        public ICommand RunSerializeTestsCommand
        {
            get
            {
                return _runSerializeTestsCommand
                    ?? (_runSerializeTestsCommand = new Command(async () =>
                    {
                        this.Dialogs.ShowLoading("Loading...", MaskType.Gradient);
                        try
                        {
                            var result = await _manager.Serialize(NumberOfTests);
                            TestResults.Insert(0, new GenericDataViewModel(result));
                        }
                        catch (Exception ex)
                        {
                            this.Dialogs.Alert(ex.Message, "Error serializing data", "OK");
                        }
                        finally
                        {
                            this.Dialogs.HideLoading();
                        }
                    }));
            }
        }

        private ICommand _runDeserializeTestsCommand;
        public ICommand RunDeserializeTestsCommand
        {
            get
            {
                return _runDeserializeTestsCommand
                    ?? (_runDeserializeTestsCommand = new Command(async () =>
                    {
                        this.Dialogs.ShowLoading("Loading...", MaskType.Gradient);
                        try
                        {
                            var result = await _manager.Deserialize(NumberOfTests);
                            TestResults.Insert(0, new GenericDataViewModel(result));
                        }
                        catch (Exception ex)
                        {
                            this.Dialogs.Alert(ex.Message, "Error serializing data", "OK");
                        }
                        finally
                        {
                            this.Dialogs.HideLoading();
                        }
                    }));
            }
        }
    }
}
