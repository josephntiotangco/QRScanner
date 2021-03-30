using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using QRScanner.Services;
using Xamarin.Essentials;
using QRScanner.ViewModel;

namespace QRScanner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            mainView = new MainViewModel();
            InitializeComponent();
        }

        public MainViewModel mainView
        {
            get { return BindingContext as MainViewModel; }
            set { BindingContext = value; }
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mainView.ScanCommand.Execute(null);
        }

    }
}
