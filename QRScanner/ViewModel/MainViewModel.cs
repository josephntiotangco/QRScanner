using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using QRScanner.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;

namespace QRScanner.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _result;
        public string Result
        {
            get { return _result; }
            set { SetValue(ref _result, value); OnPropertyChanged(nameof(Result)); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { SetValue(ref _error, value); OnPropertyChanged(nameof(Error)); }
        }
        private Color _errorColor;
        public Color ErrorColor
        {
            get { return _errorColor; }
            set { SetValue(ref _errorColor, value); OnPropertyChanged(nameof(ErrorColor)); }
        }
        private string _myAdId;
        public string MyAdId
        {
            get { return _myAdId; }
            set { SetValue(ref _myAdId, value); OnPropertyChanged(nameof(MyAdId)); }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); OnPropertyChanged(nameof(IsRefreshing)); }
        }

        public ICommand ScanCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public MainViewModel()
        {

            MyAdId = "ca-app-pub-3940256099942544/6300978111"; //Testing Only

            OpenCommand = new Command(async () => await Open());
            ScanCommand = new Command(async () => await Scan());
            CopyCommand = new Command(async () => await Copy());
            RefreshCommand = new Command(async () => await Refreshing());

            IsRefreshing = false;
        }
        private async Task Copy()
        {
            await Clipboard.SetTextAsync(Result);
            ErrorColor = Color.DarkOrange;
            Error = "Link is Copied!";
        }
        private async Task Open()
        {
            await Browser.OpenAsync(Result);
        }

        private async Task Refreshing()
        {
            IsRefreshing = true;
            await Task.Delay(1000);
            Result = "";
            Error = "";
            ErrorColor = Color.Green;
            IsRefreshing = false;
        }
        //JNT-2021/03/25 validate url return false if barcode
        public bool ValidateResult(string url)
        {
            Uri url_result;
            bool isCreateTrue = Uri.TryCreate(url, UriKind.Absolute, out url_result);
            if (isCreateTrue && url_result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task Scan()
        {
            try
            {
                var scanner = DependencyService.Get<IQRScanner>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    ErrorColor = Color.Blue;
                    Error = "Scan Successful!";
                    
                    if (ValidateResult(result))
                    {
                        await Browser.OpenAsync(result);
                    }

                    Result = result;

                }
            }
            catch (Exception ex)
            {
                ErrorColor = Color.DarkRed;
                Error = ex.Message;
                Result = "";
            }
        }
    }
}
