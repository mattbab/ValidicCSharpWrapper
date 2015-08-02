using System.Collections.Generic;
using Validic.Mobile.DemoApp.Helpers;

namespace Validic.Core.AppLib.Helpers
{
    public static class Extensions
    {
        public static void Add(this List<BindInfo> list, string path, string stringFormat = null)
        {
            list.Add(new BindInfo {Name = path, Binding = stringFormat});
        }

        // Alan
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list != null && list.Count != 0;
        }
    }
}
