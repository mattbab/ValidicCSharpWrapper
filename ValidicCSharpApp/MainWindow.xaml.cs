using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Validic.CSharp.AppLib.ViewModels;
using ValidicCSharpApp.Properties;
using ValidicCSharpApp.Views.DataViews;

namespace ValidicCSharpApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Start(MainViewModel model)
        {
            model.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "SelectedMainRecord")
                {
                    model.SelectedMainRecord.PropertyChanged += async (s1, a1) =>
                    {
                        if (a1.PropertyName == "SelectedData")
                        {
                            await TestSelecteData(model.SelectedMainRecord);
                        }
                    };
                }
            };

            LoadSettings(model);
            LoadModel(model);
        }

        public void Stop(MainViewModel model)
        {
            SaveSettings(model);
        }

        private void LoadModel(MainViewModel model)
        {
            // string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames(); 
            var assembly = typeof (MainWindow).GetTypeInfo().Assembly;
            var name = "ValidicCSharpApp.Resources.validic.json";
            var stream = assembly.GetManifestResourceStream(name);
            model.LoadModel(stream, true);
        }

        private void LoadSettings(MainViewModel model)
        {
            var s = Settings.Default;
            // Load Window State
            WindowState = s.WindowState;
            Top = s.WindowTop;
            Left = s.WindowLeft;
            Width = s.WindowWidth;
            Height = s.WindowHeight;
        }

        private void SaveSettings(MainViewModel model)
        {
            var s = Settings.Default;
            // Save Settings

            // Save Window State
            s.WindowState = WindowState;
            s.WindowTop = Top;
            s.WindowLeft = Left;
            s.WindowWidth = Width;
            s.WindowHeight = Height;
            s.Save();
        }

        private async Task TestSelecteData(MainRecordModelView m)
        {
            var tabItem = m.SelectedData as TabItem;
            if (tabItem == null)
                return;

            if (tabItem.Content is WeightView && m.Weights == null) await m.GetOrganizationWeight();
            else if (tabItem.Content is AppsView && m.Apps == null) await m.GetOrganizationApps();
            else if (tabItem.Content is BiometricsView && m.Biometrics == null) await m.GetOrganizationBiometrics();
            else if (tabItem.Content is FitnessView && m.FitnessData == null) await m.GetOrganizationFitnessData();
            else if (tabItem.Content is DiabetesView && m.DiabetesData == null) await m.GetOrganizationDiabetesData();
            else if (tabItem.Content is NutritionView && m.NutritionData == null) await m.GetOrganizationNutritionData();
            else if (tabItem.Content is RoutineView && m.RoutineData == null) await m.GetOrganizationRoutineData();
            else if (tabItem.Content is SleepView && m.SleepData == null) await m.GetOrganizationSleepData();
            else if (tabItem.Content is TobaccoCessationView && m.TobaccoCessationData == null) await m.GetOrganizationTobaccoCessationData();
            else if (tabItem.Content is ProfileView && m.Profiles == null) await m.GetOrganizationProfiles();
            else if (tabItem.Content is MeView && m.MeData.Count == 0) await m.GetOrganizationMeDataAsync();
        }
    }
}