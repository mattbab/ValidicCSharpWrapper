using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Validic.Core.AppLib.Helpers
{
    public class StreamHelper
    {
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
                var o2 = (JObject)JToken.ReadFrom(reader);
                return o2.ToObject<T>();
            }
        }
    }
}
