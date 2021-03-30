using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QRScanner.Services
{
    public interface IQRScanner
    {
        Task<string> ScanAsync();
    }
}
