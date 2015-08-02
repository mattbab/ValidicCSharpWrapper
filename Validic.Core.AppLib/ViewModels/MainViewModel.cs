using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Validic.Core.AppLib.Helpers;
using Validic.Core.AppLib.Models;
using Validic.Logging;
using Validic.Mobile.DemoApp.Helpers;

namespace Validic.Core.AppLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILog _log = LogManager.GetLogger("ListViewDemoPage");

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

        #region Commands

        public RelayCommand CommandAddOrganization { get; private set; }
        public RelayCommand CommandDeleteOrganization { get; private set; }
        public RelayCommand CommandModifyOrganization { get; private set; }

        #endregion

        #region Events

        public Action AddOrganization;
        public Action DeleteOrganization;
        public Action ModifyOrganization;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Client.AddLine += AddLine;
            PropertyChanged += async (s, e) => await OnPropertyChanged(s, e);
            CommandAddOrganization = new RelayCommand(OnAddOrganization, () => CanAddAddOrganization);
            CommandDeleteOrganization = new RelayCommand(OnDeleteOrganization, () => CanDeleteOrganization);
            CommandModifyOrganization = new RelayCommand(OnModifyOrganization, () => CanModifyOrganization);
        }


        #endregion


        #region Command Implemenation


        private void OnAddOrganization()
        {
            _log.Debug("OnAddOrganization");
            var tmp = AddOrganization;
            tmp?.Invoke();
        }

        private void OnModifyOrganization()
        {
            _log.Debug("OnAddOrganization");
            var tmp = ModifyOrganization;
            tmp?.Invoke();
        }

        private void OnDeleteOrganization()
        {
            _log.Debug("OnAddOrganization");
            var tmp = DeleteOrganization;
            tmp?.Invoke();
        }

        private bool CanAddAddOrganization
        {
            get
            {
                return true;
            }
        }

        private bool CanModifyOrganization
        {
            get
            {
                var enable = !MainRecords.IsNullOrEmpty() && SelectedMainRecord != null;
                return enable;
            }
        }

        private bool CanDeleteOrganization
        {
            get
            {
                var enable = !MainRecords.IsNullOrEmpty() && SelectedMainRecord != null;
                return enable;
            }
        }


        #endregion

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
                var record = new MainRecordModelView { OrganizationAuthenticationCredential = organizationAuthenticationCredential };
                MainRecords.Add(record);
            }
            SelectedMainRecord = MainRecords[0];
            // SaveToFile("validic.json", Model);
        }

        #region Support Functions


        private void OpenOrCreateModel(Stream s, bool exist)
        {
            Model = new MainModel();
            // read JSON directly from a file
            if (!exist)
            {
                Model.Populate();
                Helpers.StreamHelper.SaveToFile(s, Model);
            }

            Model = Helpers.StreamHelper.ReadFromFile<MainModel>(s);
        }

        #endregion

        private void AddLine(LogItem a)
        {
            Dispatcher(() =>
            {
                LogItems.Add(a);
                SelectedLogItemIndex = LogItems.Count - 1;
            });
        }

    }
}