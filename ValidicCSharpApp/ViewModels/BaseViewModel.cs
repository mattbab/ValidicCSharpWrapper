namespace ValidicCSharpApp.ViewModels
{
    using System;

    using GalaSoft.MvvmLight;

    public class BaseViewModel : ViewModelBase
    {
        public virtual Action<Action> Dispatcher { get; set; }
    }
}