using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QRScanner.Services;
using QRScanner.Droid.Services;
using ZXing.Mobile;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(QRScanner.Droid.Services.QRService))]
namespace QRScanner.Droid.Services
{
    public class QRService : IQRScanner
    {
        public Task<string> ScanAsync()
        {
            return Task.Run(() =>
            {
                return Scan();
            });
        }

        public string Scan()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var options = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var xzing_result = scanner.Scan(options);

            string result = xzing_result.Result.Text;

            return result;
        }
    }
}