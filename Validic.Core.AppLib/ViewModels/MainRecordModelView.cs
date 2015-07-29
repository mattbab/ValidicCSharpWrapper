using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Validic.Core.Model;
using Validic.Core.Request;
using Validic.Core.Utility;
using Validic.Logging;

namespace Validic.Core.AppLib.ViewModels
{
    public class MainRecordModelView : BaseViewModel
    {
        private readonly ILog _log = LogManager.GetLogger("ListViewDemoPage");

        #region Constructor

        public MainRecordModelView()
        {
            MeData = new ObservableCollection<MeViewModel>();

            CommandGetOrganization = new RelayCommand(async () => await GetOrganization(), () => true);
            CommandClearOrganization = new RelayCommand(ClearOrganization, () => true);
            CommandGetOrganizationWeight = new RelayCommand(async () => await GetOrganizationWeight(), () => true);
            CommandGetOrganizationBiometrics = new RelayCommand(async () => await GetOrganizationBiometrics(), () => true);
            CommandGetOrganizationFitnessData = new RelayCommand(async () => await GetOrganizationFitnessData(), () => true);
            CommandGetOrganizationDiabetesData = new RelayCommand(async () => await GetOrganizationDiabetesData(), () => true);
            CommandGetOrganizationNutritionData = new RelayCommand(async () => await GetOrganizationNutritionData(), () => true);
            CommandGetOrganizationRoutineData = new RelayCommand(async () => await GetOrganizationRoutineData(), () => true);
            CommandGetOrganizationSleepData = new RelayCommand(async () => await GetOrganizationSleepData(), () => true);
            CommandGetOrganizationTobaccoCessationData = new RelayCommand(async () => await GetOrganizationTobaccoCessationData(), () => true);
            CommandGetOrganizationProfiles = new RelayCommand(async () => await GetOrganizationProfiles(), () => true);
            CommandGetOrganizationMeData = new RelayCommand(async () => await GetOrganizationMeDataAsync(), () => true);
            CommandGetOrganizationApps = new RelayCommand(async () => await GetOrganizationApps(), () => true);
            // 
            CommandDataSelected = new RelayCommand(DataSelected, () => true);
            CommandMeUpdate = new RelayCommand(async () => await MeUpdateAsync(), () => true);
            CommandMeUpdateAll = new RelayCommand(MeUpdateAll, () => true);
        }

        #endregion

        private void DataSelected()
        {
        }

        private async Task<string> GetOrganizationJsonData(CommandType commandType)
        {
            var oac = OrganizationAuthenticationCredential;
            if (oac == null)
                return null;

            var client = new Client {AccessToken = oac.AccessToken};
            var command = new Command().FromOrganization(oac.OrganizationId)
                .GetInformationType(commandType);
            // .GetLatest();

            var json = await client.PerformCommandAsync(command);
            return json;
        }

        private async Task<ValidicResult<List<T>>> GetOrganizationData<T>(CommandType commandType)
        {
            var json = await GetOrganizationJsonData(commandType);

            var result = json?.ToResult<List<T>>();
            return result;
        }

        #region Members

        private Organization _organization;
        private MeViewModel _selectedMeRecord;
        private object _selectedData;

        #endregion

        #region Properties

        public OrganizationAuthenticationCredentials OrganizationAuthenticationCredential { get; set; }

        public Organization Organization
        {
            get { return _organization; }
            set
            {
                if (_organization == value)
                    return;

                _organization = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<MeViewModel> MeData { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Weight> Weights { get; set; }
        public List<Biometrics> Biometrics { get; set; }
        public List<Fitness> FitnessData { get; set; }
        public List<Diabetes> DiabetesData { get; set; }
        public List<Nutrition> NutritionData { get; set; }
        public List<Routine> RoutineData { get; set; }
        public List<Sleep> SleepData { get; set; }
        public List<Tobacco_Cessation> TobaccoCessationData { get; set; }
        public List<App> Apps { get; set; }

        public object SelectedData
        {
            get { return _selectedData; }
            set
            {
                if (_selectedData == value)
                    return;


                _selectedData = value;
                RaisePropertyChanged();
            }
        }

        public MeViewModel SelectedMeRecord
        {
            get { return _selectedMeRecord; }
            set
            {
                if (_selectedMeRecord == value)
                    return;


                _selectedMeRecord = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand CommandGetOrganization { get; private set; }
        public RelayCommand CommandClearOrganization { get; private set; }
        public RelayCommand CommandGetOrganizationWeight { get; private set; }
        public RelayCommand CommandGetOrganizationBiometrics { get; private set; }
        public RelayCommand CommandGetOrganizationFitnessData { get; private set; }
        public RelayCommand CommandGetOrganizationDiabetesData { get; private set; }
        public RelayCommand CommandGetOrganizationNutritionData { get; private set; }
        public RelayCommand CommandGetOrganizationRoutineData { get; private set; }
        public RelayCommand CommandGetOrganizationSleepData { get; private set; }
        public RelayCommand CommandGetOrganizationTobaccoCessationData { get; private set; }
        public RelayCommand CommandGetOrganizationProfiles { get; private set; }
        public RelayCommand CommandGetOrganizationMeData { get; private set; }
        public RelayCommand CommandGetOrganizationApps { get; private set; }
        public RelayCommand CommandDataSelected { get; private set; }
        public RelayCommand CommandMeUpdate { get; private set; }
        public RelayCommand CommandMeUpdateAll { get; private set; }

        #endregion

        #region  Commands Implemenation

        public async Task GetOrganizationMeDataAsync()
        {
            _log.Debug("[GetOrganizationMeDataAsync]");
            var oac = OrganizationAuthenticationCredential;
            if (oac == null)
                return;

            var client = new Client {AccessToken = oac.AccessToken};
            var result = await client.GetEnterpriseUsersAsync(oac.OrganizationId);
            if (result?.Object == null)
                return;

            foreach (var me in result.Object)
                MeData.Add(new MeViewModel {Me = me, RefreshToken = new RefreshToken()});

            RaisePropertyChanged("MeData");
        }

        public async Task GetOrganizationApps()
        {
            var json = await GetOrganizationJsonData(CommandType.Apps);
            if (json == null)
                return;

            Apps = json.Objectify<Apps>().AppCollection;
            RaisePropertyChanged("Apps");
        }


        public async Task GetOrganizationProfiles()
        {
            var result = await GetOrganizationData<Profile>(CommandType.Profile);
            Profiles = result?.Object;
            RaisePropertyChanged("Profiles");
        }

        public async Task GetOrganizationTobaccoCessationData()
        {
            var result = await GetOrganizationData<Tobacco_Cessation>(CommandType.Tobacco_Cessation);
            TobaccoCessationData = result?.Object;
            RaisePropertyChanged("TobaccoCessationData");
        }

        public async Task GetOrganizationSleepData()
        {
            var result = await GetOrganizationData<Sleep>(CommandType.Sleep);
            SleepData = result?.Object;
            RaisePropertyChanged("SleepData");
        }

        public async Task GetOrganizationRoutineData()
        {
            var result = await GetOrganizationData<Routine>(CommandType.Routine);
            RoutineData = result != null ? result.Object : null;
            RaisePropertyChanged("RoutineData");
        }

        public async Task GetOrganizationNutritionData()
        {
            var result = await GetOrganizationData<Nutrition>(CommandType.Nutrition);
            NutritionData = result != null ? result.Object : null;
            RaisePropertyChanged("NutritionData");
        }

        public async Task GetOrganizationDiabetesData()
        {
            var result = await GetOrganizationData<Diabetes>(CommandType.Diabetes);
            DiabetesData = result != null ? result.Object : null;
            RaisePropertyChanged("DiabetesData");
        }


        public async Task GetOrganizationFitnessData()
        {
            var result = await GetOrganizationData<Fitness>(CommandType.Fitness);
            FitnessData = result?.Object;
            RaisePropertyChanged("FitnessData");
        }

        public async Task GetOrganizationBiometrics()
        {
            var result = await GetOrganizationData<Biometrics>(CommandType.Biometrics);
            Biometrics = result?.Object;
            RaisePropertyChanged("Biometrics");
        }

        public async Task GetOrganizationWeight()
        {
            var result = await GetOrganizationData<Weight>(CommandType.Weight);
            Weights = result?.Object;
            RaisePropertyChanged("Weights");
        }

        public async Task GetOrganization()
        {
            var oac = OrganizationAuthenticationCredential;
            if (oac == null)
                return;

            var client = new Client {AccessToken = oac.AccessToken};
            var command = new Command().FromOrganization(oac.OrganizationId);

            var json = await client.PerformCommandAsync(command);
            var result = json.ToResult<Organization>();
            Organization = result.Object;
        }


        private void ClearOrganization()
        {
            Organization = null;
            RaisePropertyChanged("Organization");
        }

        private void GetOrganizationWeight(Client client, string orgId)
        {
            // Assert.True(weight.Object.First()._id != null);


            //            var command = new Command().GetInformationType(CommandType.Weight);
            //            var json = client.PerformCommand(command);
            //            var result = json.ToResult<Organization>();
            //            Organization = (Organization)result.Object;
            //            RaisePropertyChanged("Organization");
        }


        private async Task MeUpdateAsync(MeViewModel record)
        {
            try
            {
                if (record == null)
                    return;

                var oac = OrganizationAuthenticationCredential;
                if (oac == null)
                    return;

                var client = new Client {AccessToken = oac.AccessToken};
                var result = await client.GetUserRefreshTokenAsync(record.Me.Id, oac.OrganizationId);
                record.RefreshToken = result.Object;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task MeUpdateAsync()
        {
            await MeUpdateAsync(SelectedMeRecord);
        }

        private void MeUpdateAll()
        {
            foreach (var record in MeData)
            {
                Debug.WriteLine(record);
                Dispatcher(async () => await MeUpdateAsync(record));
            }
        }

        #endregion
    }
}