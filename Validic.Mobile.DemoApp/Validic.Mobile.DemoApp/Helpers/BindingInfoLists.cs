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
            {"Uid",""},
            {"Uid",""},
            {"Uid",""},
            {"Uid",""},
            {"Uid",""},
            {"Uid",""},
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
    }
}
