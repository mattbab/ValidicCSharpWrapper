using System;
using System.Diagnostics;
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
                SaveToFile(sw, value);
        }

        public static T ReadFromFile<T>(Stream s)
        {
            using (var sr = new StreamReader(s))
                return ReadFromFile<T>(sr);
        }

        public static void SaveToFile(StreamWriter sw, object value)
        {
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                var serializer = new JsonSerializer();
                serializer.Serialize(jw, value);
            }
        }

        public static void SaveToFile2(StreamWriter sw, object value)
        {
            var jw = new JsonTextWriter(sw);
            jw.Formatting = Formatting.Indented;
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
        }

        public static T ReadFromFile<T>(StreamReader sr)
        {
            using (var reader = new JsonTextReader(sr))
            {
                try
                {
                    var jtoken = JToken.ReadFrom(reader);
                    var o2 = (JObject)jtoken;
                    return o2.ToObject<T>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return default(T);
                }
            }
        }
    }
}
