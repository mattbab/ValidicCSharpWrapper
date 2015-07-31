using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validic.Mobile.DemoApp.Helpers
{
    public class BindingInfoLists
    {
        public static Dictionary<string, string> MesasurmentBindingList = new Dictionary<string, string>
        {
            {"Id", ""},
            {"Time", "{0:MM/dd/yyy hh:mm:ss tt}"},
            {"Timestamp", "{0:MM/dd/yyy hh:mm:ss tt}"},
            {"UtcOffset", ""},
            {"LastUpdated", "{0:MM/dd/yyy hh:mm:ss tt}"},
            {"Source", ""},
            {"SourceName", ""},
            {"Extras", ""},
            {"UserId", ""},
        };

        public static Dictionary<string, string> ProfileBindingList = new Dictionary<string, string>
        {
            {"Uid",""},
            {"Id",""},
            {"Gender",""},
            {"Location",""},
            {"Country",""},
            {"BirthYear",""},
            {"Height",""},
            {"Weight",""},
        };

        public static Dictionary<string, string> WeightBindingList = new Dictionary<string, string>
        {
            {"Value", "{0:0.##}"},
            {"Height",""},
            {"FreeMass", ""},
            {"FatPercent",""},
            {"MassWeight",""},
            {"Bmi", "{0:0.##}"}
        };

        public static Dictionary<string, string> BiometricsBindingList = new Dictionary<string, string>
        {
            {"BloodCalcium", ""},
            {"BloodChromium", ""},
            {"BloodFolicAcid", ""},
            {"BloodMagnesium", ""},
            {"BloodPotassium", ""},
            {"BloodSodium", ""},
            {"BloodVitaminB12", ""},
            {"BloodZinc", ""},
            {"CreatineKinase", ""},
            {"Crp", ""},
            {"Diastolic", ""},
            {"Ferritin", ""},
            {"Hdl", ""},
            {"Hscrp", ""},
            {"Il6", ""},
            {"Ldl", ""},
            {"RestingHeartrate", ""},
            {"Systolic", ""},
            {"Testosterone", ""},
            {"TotalCholesterol", ""},
            {"Tsh", ""},
            {"UricAcid", ""},
            {"VitaminD", ""},
            {"WhiteCellCount", ""},

        };

        public static Dictionary<string, string> FitnessBindingList = new Dictionary<string, string>
        {
            {"Type", ""},
            {"Intensity", ""},
            {"StartTime", "{0:MM/dd/yyy hh:mm:ss tt}"},
            {"Distance", "{0:0.##}"},
            {"Duration", ""},
            {"Calories", "{0:0.##}"},
        };

        public static Dictionary<string, string> DiabetesBindingList = new Dictionary<string, string>
        {
            {"CPeptide", ""},
            {"FastingPlasmaGlucoseTest", ""},
            {"Hba1C", ""},
            {"Insulin", ""},
            {"OralGlucoseToleranceTest", ""},
            {"RandomPlasmaGlucoseTest", ""},
            {"Triglyceride", ""},
            {"BloodGlucose", "{0:0.##}"},
        };

        public static Dictionary<string, string> NutritionBindingList = new Dictionary<string, string>
        {
            {"Calories", "{0:0.##}"},
            {"Carbohydrates", "{0:0.##}"},
            {"Fat", "{0:0.##}"},
            {"Fiber", "{0:0.##}"},
            {"Protein", "{0:0.##}"},
            {"Sodium", "{0:0.##}"},
            {"Water", ""},
            {"Meal", ""},
        };

        public static Dictionary<string, string> RoutineBindingList = new Dictionary<string, string>
        {
            {"Steps","{0:0.##}"},
            {"CaloriesBurned","{0:0.##}"},
            {"Distance","{0:0.##}"},
            {"Floors",""},
            {"Elevation",""},
        };

        public static Dictionary<string, string> SleepBindingList = new Dictionary<string, string>
        {
            {"Awake", ""},
            {"Deep", ""},
            {"Light", ""},
            {"Rem", ""},
            {"TimesWoken", ""},
            {"TotalSleep", ""},
        };

        public static Dictionary<string, string> TobaccoCessationBindingList = new Dictionary<string, string>
        {
            {"CigarettesAllowed", ""},
            {"CigarettesSmoked}", ""},
            {"Cravings}", ""},
            {"LastSmoked}", ""},
        };

        public static Dictionary<string, string> MeBindingList = new Dictionary<string, string>
        {
            {"Me.Id", ""},
        };
    }
}
