namespace ValidicCSharpApp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    using GalaSoft.MvvmLight.Command;

    using ValidicCSharp;
    using ValidicCSharp.Model;
    using ValidicCSharp.Request;
    using ValidicCSharp.Utility;

    using ValidicCSharpApp.Views.DataViews;

    public class MainRecordModelView : BaseViewModel
    {
        #region Constructor

        public MainRecordModelView()
        {
            this.MeData = new ObservableCollection<MeViewModel>();

            this.CommandGetOrganization = new RelayCommand(this.GetOrganization, () => true);
            this.CommandClearOrganization = new RelayCommand(this.ClearOrganization, () => true);

            this.CommandGetOrganizationWeight = new RelayCommand(this.GetOrganizationWeight, () => true);
            this.CommandGetOrganizationBiometrics = new RelayCommand(this.GetOrganizationBiometrics, () => true);
            this.CommandGetOrganizationFitnessData = new RelayCommand(this.GetOrganizationFitnessData, () => true);
            this.CommandGetOrganizationDiabetesData = new RelayCommand(this.GetOrganizationDiabetesData, () => true);
            this.CommandGetOrganizationNutritionData = new RelayCommand(this.GetOrganizationNutritionData, () => true);
            this.CommandGetOrganizationRoutineData = new RelayCommand(this.GetOrganizationRoutineData, () => true);
            this.CommandGetOrganizationSleepData = new RelayCommand(this.GetOrganizationSleepData, () => true);
            this.CommandGetOrganizationTobaccoCessationData = new RelayCommand(
                this.GetOrganizationTobaccoCessationData,
                () => true);
            this.CommandGetOrganizationProfiles = new RelayCommand(this.GetOrganizationProfiles, () => true);
            this.CommandGetOrganizationMeData = new RelayCommand(this.GetOrganizationMeData, () => true);
            this.CommandGetOrganizationApps = new RelayCommand(this.GetOrganizationApps, () => true);
            // 
            this.CommandDataSelected = new RelayCommand(this.DataSelected, () => true);
            this.CommandMeUpdate = new RelayCommand(async () => await this.MeUpdateAsync(), () => true);
            this.CommandMeUpdateAll = new RelayCommand(this.MeUpdateAll, () => true);
        }

        #endregion

        private void TestSelecteData()
        {
            var tabItem = this.SelectedData as TabItem;
            if (tabItem == null)
            {
                return;
            }

            if (tabItem.Content is WeightView && this.Weights == null)
            {
                this.GetOrganizationWeight();
            }
            else if (tabItem.Content is AppsView && this.Apps == null)
            {
                this.GetOrganizationApps();
            }
            else if (tabItem.Content is BiometricsView && this.Biometrics == null)
            {
                this.GetOrganizationBiometrics();
            }
            else if (tabItem.Content is FitnessView && this.FitnessData == null)
            {
                this.GetOrganizationFitnessData();
            }
            else if (tabItem.Content is DiabetesView && this.DiabetesData == null)
            {
                this.GetOrganizationDiabetesData();
            }
            else if (tabItem.Content is NutritionView && this.NutritionData == null)
            {
                this.GetOrganizationNutritionData();
            }
            else if (tabItem.Content is RoutineView && this.RoutineData == null)
            {
                this.GetOrganizationRoutineData();
            }
            else if (tabItem.Content is SleepView && this.SleepData == null)
            {
                this.GetOrganizationSleepData();
            }
            else if (tabItem.Content is TobaccoCessationView && this.TobaccoCessationData == null)
            {
                this.GetOrganizationTobaccoCessationData();
            }
            else if (tabItem.Content is ProfileView && this.Profiles == null)
            {
                this.GetOrganizationProfiles();
            }
            else if (tabItem.Content is MeView && this.MeData.Count == 0)
            {
                this.GetOrganizationMeData();
            }
        }

        private void DataSelected()
        {
        }

        public string GetOrganizationJsonData(CommandType commandType)
        {
            var oac = this.OrganizationAuthenticationCredential;
            if (oac == null)
            {
                return null;
            }

            var client = new Client(oac.AccessToken);
            var command = new Command().FromOrganization(oac.OrganizationId).GetInformationType(commandType);
            // .GetLatest();

            var json = client.PerformCommand(command);
            return json;
        }

        public ValidicResult<List<T>> GetOrganizationData<T>(CommandType commandType)
        {
            var json = this.GetOrganizationJsonData(commandType);
            if (json == null)
            {
                return null;
            }

            var result = json.ToResult<List<T>>();
            return result;
        }

        #region Members

        private Organization _organization;

        private object _selectedData;

        private MeViewModel _selectedMeRecord;

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

        #region Properties

        public OrganizationAuthenticationCredentials OrganizationAuthenticationCredential { get; set; }

        public Organization Organization
        {
            get
            {
                return this._organization;
            }
            set
            {
                if (this._organization == value)
                {
                    return;
                }

                this._organization = value;
                this.RaisePropertyChanged();
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
            get
            {
                return this._selectedData;
            }
            set
            {
                if (this._selectedData == value)
                {
                    return;
                }

                this._selectedData = value;
                this.TestSelecteData();
                this.RaisePropertyChanged();
            }
        }

        public MeViewModel SelectedMeRecord
        {
            get
            {
                return this._selectedMeRecord;
            }
            set
            {
                if (this._selectedMeRecord == value)
                {
                    return;
                }

                this._selectedMeRecord = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region  Commands Implemenation

        public void GetOrganizationMeData()
        {
            var oac = this.OrganizationAuthenticationCredential;
            if (oac == null)
            {
                return;
            }

            var client = new Client(oac.AccessToken);
            var result = client.GetEnterpriseUsers(oac.OrganizationId);
            if (result == null)
            {
                return;
            }

            if (result.Object == null)
            {
                return;
            }

            foreach (var me in result.Object)
            {
                this.MeData.Add(new MeViewModel { Me = me, RefreshToken = new RefreshToken() });
            }

            this.RaisePropertyChanged("MeData");
        }

        public void GetOrganizationApps()
        {
            var json = this.GetOrganizationJsonData(CommandType.Apps);
            if (json == null)
            {
                return;
            }

            this.Apps = json.Objectify<Apps>().AppCollection;
            this.RaisePropertyChanged("Apps");
        }

        public void GetOrganizationProfiles()
        {
            var result = this.GetOrganizationData<Profile>(CommandType.Profile);
            this.Profiles = result != null ? result.Object : null;
            this.RaisePropertyChanged("Profiles");
        }

        public void GetOrganizationTobaccoCessationData()
        {
            var result = this.GetOrganizationData<Tobacco_Cessation>(CommandType.Tobacco_Cessation);
            this.TobaccoCessationData = result != null ? result.Object : null;
            this.RaisePropertyChanged("TobaccoCessationData");
        }

        public void GetOrganizationSleepData()
        {
            var result = this.GetOrganizationData<Sleep>(CommandType.Sleep);
            this.SleepData = result != null ? result.Object : null;
            this.RaisePropertyChanged("SleepData");
        }

        public void GetOrganizationRoutineData()
        {
            var result = this.GetOrganizationData<Routine>(CommandType.Routine);
            this.RoutineData = result != null ? result.Object : null;
            this.RaisePropertyChanged("RoutineData");
        }

        public void GetOrganizationNutritionData()
        {
            var result = this.GetOrganizationData<Nutrition>(CommandType.Nutrition);
            this.NutritionData = result != null ? result.Object : null;
            this.RaisePropertyChanged("NutritionData");
        }

        public void GetOrganizationDiabetesData()
        {
            var result = this.GetOrganizationData<Diabetes>(CommandType.Diabetes);
            this.DiabetesData = result != null ? result.Object : null;
            this.RaisePropertyChanged("DiabetesData");
        }

        public void GetOrganizationFitnessData()
        {
            var result = this.GetOrganizationData<Fitness>(CommandType.Fitness);
            this.FitnessData = result != null ? result.Object : null;
            this.RaisePropertyChanged("FitnessData");
        }

        public void GetOrganizationBiometrics()
        {
            var result = this.GetOrganizationData<Biometrics>(CommandType.Biometrics);
            this.Biometrics = result != null ? result.Object : null;
            this.RaisePropertyChanged("Biometrics");
        }

        public void GetOrganizationWeight()
        {
            var result = this.GetOrganizationData<Weight>(CommandType.Weight);
            this.Weights = result != null ? result.Object : null;
            this.RaisePropertyChanged("Weights");
        }

        public void GetOrganization()
        {
            var oac = this.OrganizationAuthenticationCredential;
            if (oac == null)
            {
                return;
            }

            var client = new Client(oac.AccessToken);
            var command = new Command().FromOrganization(oac.OrganizationId);

            var json = client.PerformCommand(command);
            var result = json.ToResult<Organization>();
            this.Organization = result.Object;
            this.RaisePropertyChanged("Organization");
        }

        public void ClearOrganization()
        {
            this.Organization = null;
            this.RaisePropertyChanged("Organization");
        }

        public void GetOrganizationWeight(Client client, string orgId)
        {
            // Assert.True(weight.Object.First()._id != null);

            //            var command = new Command().GetInformationType(CommandType.Weight);
            //            var json = client.PerformCommand(command);
            //            var result = json.ToResult<Organization>();
            //            Organization = (Organization)result.Object;
            //            RaisePropertyChanged("Organization");
        }

        private async Task UpdateAsync(MeViewModel record)
        {
            var oac = this.OrganizationAuthenticationCredential;
            if (oac == null)
            {
                return;
            }

            var client = new Client(oac.AccessToken);
            var result = await client.GetUserRefreshTokenAsync(record.Me.Id, oac.OrganizationId);
            record.RefreshToken = result.Object;
        }

        private async Task MeUpdateAsync()
        {
            await this.UpdateAsync(this.SelectedMeRecord);
        }

        private void MeUpdateAll()
        {
            foreach (var record in this.MeData)
            {
                Debug.WriteLine(record);
                this.Dispatcher(async () => await this.UpdateAsync(record));
            }
        }

        #endregion
    }
}