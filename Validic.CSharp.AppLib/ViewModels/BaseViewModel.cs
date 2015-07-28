using System;
using GalaSoft.MvvmLight;

namespace Validic.CSharp.AppLib.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public virtual Action<Action> Dispatcher { get; set; }
    }
}