using GalaSoft.MvvmLight;
using Validic.Core.Model;

namespace Validic.Core.AppLib.ViewModels
{
    public class MeViewModel : ViewModelBase
    {
        private RefreshToken _refreshToken;
        public Me Me { get; set; }

        public RefreshToken RefreshToken
        {
            get { return _refreshToken; }
            set
            {
                if (_refreshToken == value)
                    return;

                _refreshToken = value;
                RaisePropertyChanged();
            }
        }
    }
}