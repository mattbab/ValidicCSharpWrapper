using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Helpers
{
    public static class Extensions
    {
        public static void Add(this List<BindInfo> list, string path, string stringFormat = null)
        {
            list.Add(new BindInfo {Name = path, Binding = stringFormat});
        }
    }
}
