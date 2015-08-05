using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validic.Core.AppLib.ViewModels
{
    public class ModelDispatcherViewModelBase<T> : DispatcherViewModelBase
    {
        protected T Model { get; set; }
    }
}
