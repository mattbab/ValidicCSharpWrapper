using System;
using GalaSoft.MvvmLight;

namespace Validic.Core.AppLib.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public virtual Action<Action> Dispatcher { get; set; }
    }
}