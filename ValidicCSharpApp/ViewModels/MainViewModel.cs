namespace ValidicCSharpApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using ValidicCSharp;

    using ValidicCSharpApp.Models;

    public class MainViewModel : BaseViewModel
    {
        #region Ccnstructor

        public MainViewModel()
        {
            Client.AddLine += this.AddLine;
            this.OpenOrCreateModel();
            foreach (var organizationAuthenticationCredential in this.Model.OrganizationAuthenticationCredentials)
            {
                var record = new MainRecordModelView();
                record.OrganizationAuthenticationCredential = organizationAuthenticationCredential;
                this.MainRecords.Add(record);
            }
            this.SelectedMainRecord = this.MainRecords[0];
            // SaveToFile("validic.json", Model);
        }

        #endregion

        private void AddLine(LogItem a)
        {
            this._logItems.Add(a);
            this.SelectedLogItemIndex = this._logItems.Count;
        }

        #region Members

        private readonly List<MainRecordModelView> _mainRecords = new List<MainRecordModelView>();

        private readonly ObservableCollection<LogItem> _logItems = new ObservableCollection<LogItem>();

        private int _selectedLogItemIndex;

        #endregion

        #region Properties

        public MainModel Model { get; set; }

        public override Action<Action> Dispatcher
        {
            get
            {
                return base.Dispatcher;
            }
            set
            {
                base.Dispatcher = value;
                foreach (var record in this.MainRecords)
                {
                    record.Dispatcher = value;
                }
            }
        }

        public int SelectedLogItemIndex
        {
            get
            {
                return this._selectedLogItemIndex;
            }
            set
            {
                if (this._selectedLogItemIndex == value)
                {
                    return;
                }

                this._selectedLogItemIndex = value;
                this.RaisePropertyChanged("SelectedLogItemIndex");
            }
        }

        private MainRecordModelView _selectedMainRecord;

        public ObservableCollection<LogItem> LogItems
        {
            get
            {
                return this._logItems;
            }
        }

        public List<MainRecordModelView> MainRecords
        {
            get
            {
                return this._mainRecords;
            }
        }

        public MainRecordModelView SelectedMainRecord
        {
            get
            {
                return this._selectedMainRecord;
            }
            set
            {
                if (this._selectedMainRecord == value)
                {
                    return;
                }

                this._selectedMainRecord = value;
                if (this._selectedMainRecord.Organization == null)
                {
                    this._selectedMainRecord.GetOrganization();
                }

                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region Support Functions

        public static void SaveToFile(string path, object value)
        {
            using (var fs = File.Open(path, FileMode.CreateNew))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                var serializer = new JsonSerializer();
                serializer.Serialize(jw, value);
            }
        }

        public static T ReadFromFile<T>(string path)
        {
            using (var file = File.OpenText(path))
            using (var reader = new JsonTextReader(file))
            {
                var o2 = (JObject)JToken.ReadFrom(reader);
                return o2.ToObject<T>();
            }
        }

        private void OpenOrCreateModel()
        {
            var path = "validic.json";
            this.Model = new MainModel();
            // read JSON directly from a file
            if (!File.Exists(path))
            {
                this.Model.Populate();
                SaveToFile(path, this.Model);
            }

            this.Model = ReadFromFile<MainModel>("validic.json");
        }

        #endregion
    }
}