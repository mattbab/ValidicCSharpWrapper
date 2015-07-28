using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Validic.Core.AppLib.Models;

namespace Validic.Core.AppLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private void AddLine(LogItem a)
        {
            Dispatcher(() =>
            {
                LogItems.Add(a);
                SelectedLogItemIndex = LogItems.Count - 1;
            });
        }

        #region Members

        private int _selectedLogItemIndex;
        private MainRecordModelView _selectedMainRecord;

        #endregion

        #region Properties

        public MainModel Model { get; set; }

        public override Action<Action> Dispatcher
        {
            get { return base.Dispatcher; }
            set
            {
                base.Dispatcher = value;
                foreach (var record in MainRecords)
                {
                    record.Dispatcher = value;
                }
            }
        }

        public int SelectedLogItemIndex
        {
            get { return _selectedLogItemIndex; }
            set
            {
                if (_selectedLogItemIndex == value)
                    return;

                _selectedLogItemIndex = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<LogItem> LogItems { get; } = new ObservableCollection<LogItem>();

        public List<MainRecordModelView> MainRecords { get; } = new List<MainRecordModelView>();

        public MainRecordModelView SelectedMainRecord
        {
            get { return _selectedMainRecord; }
            set
            {
                if (_selectedMainRecord == value)
                    return;

                _selectedMainRecord = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Client.AddLine += AddLine;
            PropertyChanged += async (s, e) => await OnPropertyChanged(s, e);
        }

        private async Task OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedMainRecord")
            {
                if (_selectedMainRecord == null)
                    return;

                if (_selectedMainRecord.Organization != null)
                    return;

                await _selectedMainRecord.GetOrganization();
            }
        }

        public void LoadModel(Stream s, bool exist)
        {
            OpenOrCreateModel(s, exist);
            foreach (var organizationAuthenticationCredential in Model.OrganizationAuthenticationCredentials)
            {
                var record = new MainRecordModelView {OrganizationAuthenticationCredential = organizationAuthenticationCredential};
                MainRecords.Add(record);
            }
            SelectedMainRecord = MainRecords[0];
            // SaveToFile("validic.json", Model);
        }

        #endregion

        #region Support Functions

        public static void SaveToFile(Stream s, object value)
        {
            using (var sw = new StreamWriter(s))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                var serializer = new JsonSerializer();
                serializer.Serialize(jw, value);
            }
        }

        public static T ReadFromFile<T>(Stream s)
        {
            using (var sr = new StreamReader(s))
            using (var reader = new JsonTextReader(sr))
            {
                var o2 = (JObject) JToken.ReadFrom(reader);
                return o2.ToObject<T>();
            }
        }

        private void OpenOrCreateModel(Stream s, bool exist)
        {
            Model = new MainModel();
            // read JSON directly from a file
            if (!exist)
            {
                Model.Populate();
                SaveToFile(s, Model);
            }

            Model = ReadFromFile<MainModel>(s);
        }

        #endregion
    }
}