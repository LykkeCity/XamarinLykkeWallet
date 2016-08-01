using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet
{
    public interface IBarcodeService
    {
        Stream ConvertImageStream(string text, int width = 300, int height = 130);
    }
}
