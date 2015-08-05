using System;
using GalaSoft.MvvmLight;

namespace Validic.Core.AppLib.ViewModels
{
    public class DispatcherViewModelBase : ViewModelBase
    {
        public virtual Action<Action> Dispatcher { get; set; }
    }
}