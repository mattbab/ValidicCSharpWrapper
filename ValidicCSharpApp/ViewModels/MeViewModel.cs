namespace ValidicCSharpApp.ViewModels
{
    using GalaSoft.MvvmLight;

    using ValidicCSharp.Model;

    public class MeViewModel : ViewModelBase
    {
        private RefreshToken _refreshToken;

        public Me Me { get; set; }

        public RefreshToken RefreshToken
        {
            get
            {
                return this._refreshToken;
            }
            set
            {
                if (this._refreshToken == value)
                {
                    return;
                }

                this._refreshToken = value;
                this.RaisePropertyChanged();
            }
        }
    }
}